using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceCube : MonoBehaviour
{
    private Transform sliceObject; // 자르는 기준이 되는 오브젝트
    public GameObject target; // 잘리는 오브젝트
    public Material cross; // 잘리는 단면에 대한 Mat
    [SerializeField] private CubePooling cubePooling;

    public float cutForce = 2000f; // 잘릴 때 우아하게 보이기 위해서 힘을 준다.

    //막대봉 관련 변수
    private Vector3 presiousPos; // 이전위치
    public LayerMask layer;

    private void Update()
    {
        RaycastHit hit; // target
       if(Physics.Raycast(transform.position, transform.forward, out hit , 5, layer))
        {
            Debug.Log(Vector3.Angle(transform.position - presiousPos, hit.transform.up));
            if (Vector3.Angle(transform.position - presiousPos, hit.transform.up) >= 130.0f)
            {
                //Slice를 만들어주세요
                SliceObj(hit.transform.gameObject);
            }
        }

        presiousPos = transform.position;
    }

    public void SliceObj(GameObject target)
    {
        sliceObject = target.transform.GetChild(1);

        //SlicedHull hull = target.Slice(sliceObject.position, sliceObject.up);
        Vector3 slice_normal = Vector3.Cross(transform.position - presiousPos, transform.forward);
        SlicedHull hull = target.Slice(sliceObject.position, slice_normal);

        if (hull != null)
        {
            GameObject Upperhull = hull.CreateUpperHull(target, cross);
            GameObject Lowerhull = hull.CreateLowerHull(target, cross);

            Upperhull.transform.position = target.transform.position;
            Lowerhull.transform.position = target.transform.position;

            if (target.transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    GameObject g = target.transform.GetChild(i).gameObject;
                    if (g.transform.CompareTag("Slice_obj")) continue;
                    SlicedHull hull_c = g.Slice(sliceObject.position, slice_normal);

                    if(hull_c != null)
                    {
                        GameObject upper_c = hull_c.CreateUpperHull(g, cross, Upperhull);
                        GameObject lower_c = hull_c.CreateLowerHull(g, cross, Lowerhull);
                    }
                }
            }
            
            if(target.CompareTag("RedCube"))
            {
                target.SetActive(false);
                cubePooling.redCubes.Enqueue(target);
            }
            else if(target.CompareTag("BlueCube"))
            {
                target.SetActive(false);
                cubePooling.blueCubes.Enqueue(target);
            }
            //Destroy(target); //추후 오브젝트 풀 넣어주쇼
            //빵빵 터지게 효과 넣어주세요
            SetupSlice_Componet(Upperhull);
            SetupSlice_Componet(Lowerhull);

            Destroy(Upperhull, 1.0f);
            Destroy(Lowerhull, 1.0f);
        }
    }

    private void SetupSlice_Componet(GameObject g)
    {
        Rigidbody rb = g.AddComponent<Rigidbody>();
        MeshCollider c = g.AddComponent<MeshCollider>();

        c.convex = true;
        rb.AddExplosionForce(cutForce, g.transform.position, 1.0f); //폭탄 터지는 듯한 효과
    }
}
