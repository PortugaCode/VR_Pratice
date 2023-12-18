using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePooling : MonoBehaviour
{
    [SerializeField] private GameObject redcube;
    [SerializeField] private GameObject bluecube;

    public Queue<GameObject> redCubes = new Queue<GameObject>();
    public Queue<GameObject> blueCubes = new Queue<GameObject>();

    private void Awake()
    {
        for(int i = 0; i < 15; i++)
        {
            GameObject a = Instantiate(redcube, transform.position, Quaternion.identity);
            a.SetActive(false);
            a.transform.SetParent(gameObject.transform);
            redCubes.Enqueue(a);
        }
        for (int i = 0; i < 15; i++)
        {
            GameObject b = Instantiate(bluecube, transform.position, Quaternion.identity);
            b.SetActive(false);
            b.transform.SetParent(gameObject.transform);
            blueCubes.Enqueue(b);
        }
    }
}
