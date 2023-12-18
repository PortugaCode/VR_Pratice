using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // GameOjbet index 접근은 사실 좋은 방법은 아니다. (지금은 싱글 작업이고, 귀찮아서)
    // 직접참조가 더 확실하고 빠르다. But 협업을 할 때 index 접근 잘못하면 꼬일 확률이 있다.
    // 직접참조 걸 수 있는 것은 거는 것이 Unity 협업에 더 좋다.

    //Red or Blue Cube
    //public GameObject[] cubes;

    [SerializeField] private CubePooling cubePooling;

    //Spawn Points
    [SerializeField] private Transform[] points;

    // 리듬게임, 60/128
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

        // point 초기화 (childCount로 배열의 크기를 받기 때문에 에디터 상에서 변해도 잘 생성된다.)
        // 그냥 Index 접근은 위험한 행동, 이유가 있어야 한다.
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
            //audio manager 생성하면 Play 넣어주세요. todo(12.14.)
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
