using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class CuboData
{
    public int puntos;
    public int vidas;
    public float posX, posY, posZ;
}

[System.Serializable]
public class PartidaData
{
    public CuboData[] cubitosData;
}

public class saveSystem : MonoBehaviour
{
    public GameObject[] cubitos;
    private cubitoManager cubiMana;

    private void Start()
    {
        cubiMana = FindObjectOfType<cubitoManager>();
    }
    public void RecibirCubitos(GameObject[] cubos)
    {
        cubitos = cubos;
        Debug.Log("Cubitos recibidos");
    }

    public void ActualizarArray()
    {
        cubitos = GameObject.FindGameObjectsWithTag("Player");
    }

    public void guardarPartida() //Guardamos los datos de la partida.
    {
        ActualizarArray();

        PartidaData partidaData = new PartidaData();
        partidaData.cubitosData = new CuboData[cubitos.Length];

        for (int i = 0; i < cubitos.Length; i++)
        {
            cubitoControll cubitoController = cubitos[i].GetComponent<cubitoControll>();

            if (cubitoController != null)
            {
                CuboData cuboData = new CuboData();
                cuboData.puntos = cubitoController.puntos;
                cuboData.vidas = cubitoController.vidas;

                cuboData.posX = cubitos[i].transform.position.x;
                cuboData.posY = cubitos[i].transform.position.y;
                cuboData.posZ = cubitos[i].transform.position.z;

                partidaData.cubitosData[i] = cuboData;
            }
        }

        string json = JsonUtility.ToJson(partidaData, true);
        string path = Application.persistentDataPath + "/savefile.json";
        File.WriteAllText(path, json);
        Debug.Log("Partida guardada en " + path);
    }

    //En caso de que escoja el botón de cargar la partida guardada
    public void cargarPartida()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PartidaData partidaData = JsonUtility.FromJson<PartidaData>(json);
            if (partidaData.cubitosData.Length != cubitos.Length)
            {
                Debug.LogError("El número de cubitos guardados no coincide con los actuales.");
                return;
            }

            for (int i = 0; i < cubitos.Length; i++)
            {
                CuboData cuboData = partidaData.cubitosData[i];
                cubitoControll cubitoController = cubitos[i].GetComponent<cubitoControll>();
                if(cubitoController != null)
                {
                    cubitoController.puntos = cuboData.puntos;
                    cubitoController.vidas = cuboData.vidas;
                    cubitos[i].transform.position = new Vector3(cuboData.posX, cuboData.posY, cuboData.posZ);
                }
                
            }

            Debug.Log("Partida cargada desde " + path);
        }
        else
        {
            Debug.LogError("No se encontró el archivo de guardado en " + path);
        }
    }

    public void reiniciarPartida()//En caso de que escoja el botón reiniciar partida
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Partida eliminada.");
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo para eliminar, pero se reinició la partida.");
        }

        ReiniciarEstadoJuego();
    }

    private void ReiniciarEstadoJuego() //Reiniciamos el juego con cubos nuevos
    {
        if(cubiMana != null)
        {
            cubiMana.EliminarCubisActuales();
            cubiMana.InstanciarCubitos();
        }
        else
        {
            Debug.Log("cubitoManager not found.");
        }
    }

    private void OnApplicationQuit()
    {
        guardarPartida();
    }
}
