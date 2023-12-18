using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour
{
    //�����θ� ���� ������ִ� ��
    [Range(1f, 100f)]
    public float speed = 20.0f;

    private void Update()
    {
        gameObject.transform.position += -transform.forward * speed * Time.deltaTime;
    }
}
