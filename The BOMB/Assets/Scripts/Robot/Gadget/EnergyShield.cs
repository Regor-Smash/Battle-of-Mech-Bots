using UnityEngine;

public class EnergyShield : MonoBehaviour, GadgetInterface
{
    public GameObject shieldPrefab;
    private GameObject shield;

    GadgetData data;
    public float energyCost { get; private set; }
    int duration;

    void Awake()
    {
        data = Resources.Load<GadgetData>("Part Database/Gadgets/Energy Shield");
        energyCost = data.cost;
        duration = data.duration;
    }


	void Update ()
    {
		if (Input.GetButtonDown("Gadget"))
        {
            Activate();
        }
	}

    public void Activate()
    {
        if (shield != null)
        {
            DestroyImmediate(shield);
        }

        shield = Instantiate(shieldPrefab, transform.root.position, Quaternion.identity);
        Destroy(shield, duration);
    }
}
