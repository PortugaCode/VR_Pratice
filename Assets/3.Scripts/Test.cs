using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    /** string �Ű����� */
    public string TestMethod(string a)
    {
        return a + "����";
    }

    public void Start()
    {
        TestMethod("a");
    }
}
