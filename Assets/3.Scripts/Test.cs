using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    /** string 매개변수 */
    public string TestMethod(string a)
    {
        return a + "ㅎㅎ";
    }

    public void Start()
    {
        TestMethod("a");
    }
}
