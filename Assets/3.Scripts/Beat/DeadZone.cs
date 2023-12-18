using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private CubePooling cubePooling;

    private void Awake()
    {
        cubePooling = GameObject.FindGameObjectWithTag("Respawn").GetComponent<CubePooling>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("RedCube"))
        {
            collision.gameObject.SetActive(false);
            cubePooling.redCubes.Enqueue(collision.gameObject);
            //Destroy(collision.gameObject);
        }
        else if(collision.transform.CompareTag("BlueCube"))
        {
            collision.gameObject.SetActive(false);
            cubePooling.blueCubes.Enqueue(collision.gameObject);
        }
    }
}
