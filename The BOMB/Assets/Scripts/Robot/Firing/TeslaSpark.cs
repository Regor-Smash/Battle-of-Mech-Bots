using UnityEngine;

public class TeslaSpark : WeaponTemplate
{
    LineRenderer defaultLine;
    LineRenderer[] arcs = new LineRenderer[0];
    private float arcRand = 0.3f;
    Animator anim;

    Collider[] hits;
    public LayerMask includedLayers;

    protected override void Awake()
    {
        data = Resources.Load<WeaponData>("Part Database/Weapons/Tesla Coil");
        base.Awake();

        // = data.special;

        defaultLine = GetComponentInChildren<LineRenderer>();
        defaultLine.gameObject.SetActive(false);

        anim = GetComponentInParent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire" + Slot) && !MultiplayerPause.isPaused && hullMang.energy >= cost)
        {
            InvokeRepeating("CheckAmmo", 0, fireRate);

            anim.SetBool("Firing", true);
        }

        if (Input.GetButtonUp("Fire" + Slot) || MultiplayerPause.isPaused || hullMang.energy < cost)
        {
            CancelInvoke("CheckAmmo");

            anim.SetBool("Firing", false);
            hits = new Collider[0];

            if (arcs.Length != 0) //Clear old lines
            {
                //Debug.Log("There are " + arcs.Length + " arcs.");
                if (arcs[0] == null)
                {
                    //Debug.Log("Arc 0 is not here.");
                }
                else
                {
                    foreach (LineRenderer line in arcs)
                    {
                        Destroy(line.gameObject);
                    }
                }
                arcs = new LineRenderer[0];
            }
        }
    }

    protected override void Fire()
    {
        hits = Physics.OverlapSphere(transform.position, range, includedLayers);
        #region HitListDebug
        string names = "";
        foreach (Collider col in hits)
        {
            names += col.name + ", ";
        }
        Debug.Log("Tesla Coil hit: " + names);
        //*/
        #endregion HitListDebug

        if (arcs.Length != 0) //Clear old lines
        {
            //Debug.Log("There are " + arcs.Length + " arcs.");
            if (arcs[0] == null)
            {
                //Debug.Log("Arc 0 is not here.");
            }
            else
            {
                foreach (LineRenderer line in arcs)
                {
                    Destroy(line.gameObject);
                }
            }
            arcs = new LineRenderer[0];
        }
        arcs = new LineRenderer[hits.Length]; //Make new lines

        for (int a = 0; a < hits.Length; a++)
        {
            NewArc(a, transform.InverseTransformPoint(hits[a].ClosestPointOnBounds(transform.position))); //Draw new line

            if (hits[a].GetComponent<IntegrityInterface>() != null)
            {
                if (hits[a].GetComponent<IntegrityInterface>().integrity > 0)
                {
                    hits[a].GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBufferedViaServer, damage, myView.owner);
                }
                else
                {
                    Debug.Log(hits[a].name + " should be dead");
                }
            }

        }
    }

    void NewArc(int index, Vector3 pos) //Make an arc
    {
        //Debug.Log("Making a tesla arc.");
        arcs[index] = Instantiate(defaultLine, transform);
        arcs[index].gameObject.SetActive(true);
        int pointsNum = arcs[index].positionCount;

        for (int i = 0; i < pointsNum; i++) //For a line, adjust all points
        {
            Vector3 pointPos = Vector3.Lerp(Vector3.zero, pos, i / (pointsNum - 1.0f));
            if (i != 0 && i != (pointsNum - 1.0f))
            {
                pointPos += new Vector3(Random.Range(-arcRand, arcRand), Random.Range(-arcRand, arcRand), Random.Range(-arcRand, arcRand));
            }
            arcs[index].SetPosition(i, pointPos);
        }
        
    }
}
