using UnityEngine;

public class TEST : MonoBehaviour
{
#if UNITY_EDITOR
    int a;

    delegate void Testing();
    event Testing Testing1;
    event Testing Testing2;
    event Testing Testing3;

    void Start()
    {
        Testing1 += test1;
        Testing2 += test2;
        Testing3 += test3;

        Testing1 += Testing3;
        Testing1 += Testing2;

        Testing1();
    }

    void test1()
    {
        a = 1;
        Debug.Log("Test 1 = " + a);
    }

    void test2()
    {
        a += 3;
        Debug.Log("Test 2 = " + a);
    }

    void test3()
    {
        a *= 2;
        Debug.Log("Test 3 = " + a);
    }
#endif
}
