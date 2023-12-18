using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // GameOjbet index ������ ��� ���� ����� �ƴϴ�. (������ �̱� �۾��̰�, �����Ƽ�)
    // ���������� �� Ȯ���ϰ� ������. But ������ �� �� index ���� �߸��ϸ� ���� Ȯ���� �ִ�.
    // �������� �� �� �ִ� ���� �Ŵ� ���� Unity ������ �� ����.

    //Red or Blue Cube
    //public GameObject[] cubes;

    [SerializeField] private CubePooling cubePooling;

    //Spawn Points
    [SerializeField] private Transform[] points;

    // �������, 60/128
    public float bpm = 128.0f;
    [SerializeField] private float beat 
    { 
        get
        {
            return 60f / bpm;
        }
    }

    public float Timer;
    //======================================================================

    private void Start()
    {
        cubePooling = GameObject.FindGameObjectWithTag("Respawn").GetComponent<CubePooling>();

        // point �ʱ�ȭ (childCount�� �迭�� ũ�⸦ �ޱ� ������ ������ �󿡼� ���ص� �� �����ȴ�.)
        // �׳� Index ������ ������ �ൿ, ������ �־�� �Ѵ�.
        points = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            points[i] = transform.GetChild(i);
        }
        Timer = 0.0f;
    }

    private void Update()
    {
        if(Timer > beat)
        {
            int randomIndex = Random.Range(0, 2);
            GameObject cube = SetCube(randomIndex);

            float y = Random.Range(0.5f, 1.5f);

            cube.transform.position = new Vector3(points[randomIndex].transform.position.x, 
                y, points[randomIndex].transform.position.z);

            cube.transform.Rotate(transform.forward, 90f * Random.Range(0, 4));

            Timer -= beat;
            //audio manager �����ϸ� Play �־��ּ���. todo(12.14.)
            if(!AudioManager.Instance.isNowPlay)
            {
                AudioManager.Instance.PlayMusic();
            }
        }
        Timer += Time.deltaTime;
    }

    private GameObject SetCube(int i)
    {
        if (i.Equals(0))
        {
            GameObject cube = cubePooling.redCubes.Dequeue();
            cube.SetActive(true);
            return cube;
        }
        else
        {
            GameObject cube = cubePooling.blueCubes.Dequeue();
            cube.SetActive(true);
            return cube;
        }
    }
}
