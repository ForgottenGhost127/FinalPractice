using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }
    //NO FUNCIONA PORQUE TIENES EL SCRIPT DONDE EL CANVAS, CAMBIA EL SCRIPT DE SITIO Y HAZ UNA REFERENCIA DEL CANVAS PARA PODER DESACTIVARLO, IDIOTA.

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Menu abierto");
            gameObject.SetActive(true);
        }
    }

    //public KeyCode toggleKey = KeyCode.M; // Tecla para abrir/cerrar el menú
    //private bool isMenuActive = false;    // Estado del menú

    //void Start()
    //{
    //    // Al iniciar el juego, el Canvas (que es el GameObject actual) está desactivado
    //    gameObject.SetActive(false);
    //}

    //void Update()
    //{
    //    // Si se presiona la tecla definida (por ejemplo, M), se alterna la visibilidad del menú
    //    if (Input.GetKeyDown(toggleKey))
    //    {
    //        isMenuActive = !isMenuActive;
    //        gameObject.SetActive(isMenuActive); // Activa o desactiva el Canvas (este GameObject)
    //    }
    //}
}
