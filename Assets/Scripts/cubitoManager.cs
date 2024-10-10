using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubitoManager : MonoBehaviour
{
    public GameObject cubitoPref;
    public int cubitoCount = 10;
    public float moveSpeed = 5;

    private List<GameObject> cubitos = new List<GameObject>();
    private bool isMoving = false;

    void Start()
    {
        for (int i = 0; i < cubitoCount; i++) //Instanciando los cubos en posiciones aleatorias
        {
            Vector3 randomPos = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));
            GameObject cubito = Instantiate(cubitoPref, randomPos, Quaternion.identity);
            cubitos.Add(cubito);

            cubito.GetComponent<Renderer>().material.color = Color.magenta; //Los cubitos en "Idle" tendrán este color.
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isMoving) //Click Mouse y Raycast
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(rayo, out hit))
            {
                Vector3 targetPos = hit.point;
                isMoving = true;
                StartCoroutine(MoveCubitos(targetPos));
            }
        }
        
    }

    IEnumerator MoveCubitos(Vector3 target)
    {
        foreach(GameObject cube in cubitos)
        {
            cube.GetComponent<Renderer>().material.color = Color.cyan;
        }

        bool CubitosReachTarget = false;
        //float separDistance = 3f; //Distancia de separación entre los cubos, porque se me aglomeraban después de coger la primera moneda y visualmente, sólo se veía un cubo algo deforme.

        while(!CubitosReachTarget)
        {
            CubitosReachTarget = true;

            foreach (GameObject cube in cubitos)
            {
                cube.transform.position = Vector3.MoveTowards(cube.transform.position, target, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(cube.transform.position, target) > 0.1f)
                {
                    CubitosReachTarget = false;
                }
            }
            //for (int i = 0; i < cubitos.Count; i++)
            //{
            //    GameObject cube = cubitos[i];
            //    Vector3 offset = new Vector3(Mathf.Sin(i + Time.time) * separDistance, 0, Mathf.Cos(i + Time.time) * separDistance);
            //    Vector3 moveTarget = target + offset;
            //    cube.transform.position = Vector3.MoveTowards(cube.transform.position, moveTarget, moveSpeed * Time.deltaTime);
            //    if (Vector3.Distance(cube.transform.position, moveTarget) > 0.1f)
            //    {
            //        CubitosReachTarget = false;
            //    }
            //}
            yield return null;
        }

        foreach (GameObject cube in cubitos)
        {
            cube.GetComponent<Renderer>().material.color = Color.magenta;
        }

        isMoving = false; 
    }

}
