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

    //Aparte de lo anterior nos queda añadir que el juego termine cuando solo quede un cubo (da igual que haya sido por puntos o por vida) y el sistema de guardado de datos.
    //EL SaveSystem requiere de un Canvas, un menú (accedemos por tecla), tres botones(Restart, Guardar, CargarDatos) y un script con el código correspondiente (la cosa es donde iría el script).
    void Start()
    {
        InstanciarCubitos();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //Click Mouse y Raycast
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(rayo, out hit))
            {
                Vector3 targetPos = hit.point;
                InstanciarCoins(targetPos);
                MueveCubitosHacia(targetPos);
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


}
