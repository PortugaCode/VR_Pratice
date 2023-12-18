using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour
{
    //앞으로만 가게 만들어주는 것
    [Range(1f, 100f)]
    public float speed = 20.0f;

    private void Update()
    {
        gameObject.transform.position += -transform.forward * speed * Time.deltaTime;
    }
}
