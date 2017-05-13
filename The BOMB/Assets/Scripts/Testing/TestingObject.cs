using UnityEngine;

public class TestingObject : MonoBehaviour
{
    //Put on objects that exist solely for testing purposes
#if UNITY_EDITOR

    private void Start()
    {

    }
    
    private void Update()
    {

    }

#else

    private void Awake()
    {
        Destroy(gameObject);
    }

#endif
}
