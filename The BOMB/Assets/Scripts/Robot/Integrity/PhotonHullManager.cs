using UnityEngine;

[AddComponentMenu("Integrity/Hull Integrity", 1)]
public class PhotonHullManager : MonoBehaviour, IntegrityInterface
{
    public static PhotonHullManager staticHull { get; private set; }
    HullData data;

    public float integrity { get; protected set; }
    public float energy { get; protected set; }
    public void UseEnergy(float use)
    {
        //maybe add an if (might not stop function called from though)
        energy -= use;
        HealthManager.hullEnergy = energy;
    }

    GameObject deathCam;

    public GameObject HUD;  //Set by spawner

    void Awake()
    {
        if (GetComponentInParent<PhotonView>().isMine)
        {
            enabled = true;

            if (staticHull == null)
            {
                staticHull = this;
            } 
            else if (staticHull != this)
            {
                Debug.LogError("You have 2 active hulls!");
            }

            data = SaveBot.CurrentPresetData.hull;
            integrity = data.maxIntegrity;
            energy = data.maxEnergy;
        }
    }

    void Start()
    {
        deathCam = GameObject.Find("DeathCam");
        HealthManager.hullHealthMax = integrity;
        HealthManager.hullHealth = integrity;

        HealthManager.hullEnergyMax = energy;
        HealthManager.hullEnergy = energy;
    }

    void Update()
    {
        if (Input.GetButtonDown("Self Destruct"))
        {
            ScoreKeeper.Score = ScoreKeeper.Score - ScoreKeeper.killPoints;
            PhotonNetwork.Instantiate("Mushroom Cloud", gameObject.transform.position, new Quaternion(-0.7f, 0, 0, 0.7f), 0);
            Die(null);
        }
    }

    public void Die(PhotonPlayer killer)
    {
        if (killer != null && killer.isLocal)
        {
            ScoreKeeper.Score += ScoreKeeper.killPoints;
        }

        if (GetComponent<PhotonView>().isMine)
        {
            deathCam.GetComponent<Camera>().enabled = true;
            deathCam.GetComponent<AudioListener>().enabled = true;
            deathCam.GetComponent<Respawn>().time = 3;
            Cursor.lockState = CursorLockMode.None;
            PhotonNetwork.Instantiate("Bot Death", gameObject.transform.position, Quaternion.identity, 0);

            staticHull = null;
            Destroy(HUD);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void AddAmmoHull()
    {
        BroadcastMessage("AddAmmo");
    }

    [PunRPC]
    public void TakeDamage(float dam, PhotonPlayer player)
    {
        integrity = integrity - dam;
        if (integrity <= 0)
        {
            Die(player);
        }
        else
        {
            HealthManager.hullHealth = integrity;
        }
    }
}
