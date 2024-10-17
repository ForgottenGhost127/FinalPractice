using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubitoManager : MonoBehaviour
{
    public GameObject cubitoPref;
    public GameObject coinPref;
    public int cubitoCount = 10;

    private List<GameObject> cubitos = new List<GameObject>();
    private GameObject moneda;

    void Start()
    {
        InstanciarCubitos();

        saveSystem saveSystem = FindObjectOfType<saveSystem>();
        if (saveSystem != null)
        {
            saveSystem.RecibirCubitos(cubitos.ToArray());
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //Click Mouse, Menu y Raycast
        {
            MenuController menuC = FindObjectOfType<MenuController>();
            if(menuC != null && !menuC.IsMenuActive())
            {
                Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(rayo, out hit))
                {
                    Vector3 targetPos = hit.point;
                    InstanciarCoins(targetPos);
                    MueveCubitosHacia(targetPos);
                }
            }
            
        }
    }

    public void InstanciarCubitos()
    {
        for (int i = 0; i < cubitoCount; i++) //Instanciando los cubos en posiciones aleatorias
        {
            Vector3 randomPos = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));
            GameObject cubito = Instantiate(cubitoPref, randomPos, Quaternion.identity);
            cubitos.Add(cubito);
        }
    }

    private void InstanciarCoins(Vector3 position) //Instancia la moneda donde se ha detectado el click del ratón.
    {
        if (moneda != null)
        {
            Destroy(moneda);
        }
        moneda = Instantiate(coinPref, position, Quaternion.identity);
    }

    public void MueveCubitosHacia(Vector3 targetPos)
    {
        foreach (GameObject cubito in cubitos)
        {
            cubito.GetComponent<cubitoControll>().MoveCubito(targetPos);
        }
    }

    public void NotificarMonedaRecogida(cubitoControll cuboGanador)
    {
        List<GameObject> cubiEliminados = new List<GameObject>();
        foreach (GameObject cubito in cubitos)
        {
            cubitoControll cubitoController = cubito.GetComponent<cubitoControll>();
            cubitoController.StopMoving();
            if (cubitoController != cuboGanador)
            {
                cubitoController.loseVidas();
                if(cubitoController.vidas <= 0)
                {
                    cubiEliminados.Add(cubito);
                }
            }
        }

        foreach (GameObject cubo in cubiEliminados)
        {
            cubitos.Remove(cubo);
        }

        ReiniciarCubitos();

        saveSystem saveSystem = FindObjectOfType<saveSystem>();
        if (saveSystem != null)
        {
            saveSystem.RecibirCubitos(cubitos.ToArray());
        }
    }

    public void ReiniciarCubitos()
    {
        foreach (GameObject cubito in cubitos) //Se reinicia la posición de los cubitos... 
        {
            if(cubito != null)
            {
                Vector3 randomPos = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));
                cubito.transform.position = randomPos;
                cubito.GetComponent<cubitoControll>().ResetColor();
            }
        }
        
    }

    public void EliminarCubisActuales() //Borramos los cubos tanto de la escena como de la lista antes de reiniciar la partida completamente.
    {
        List<GameObject> cubosAEliminar = new List<GameObject>();
        foreach (GameObject cubito in cubitos)
        {
            cubosAEliminar.Add(cubito);
        }

        foreach (GameObject cubito in cubosAEliminar)
        {
            cubitos.Remove(cubito);
            Destroy(cubito);
        }

        cubitos.Clear();
    }


}
