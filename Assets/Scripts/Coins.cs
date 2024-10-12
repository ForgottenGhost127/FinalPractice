using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) //Detectamos colisión del cubo con la moneda y llamamos a las funciones de los otros scripts.
        {
            cubitoControll cuboGanador = other.GetComponent<cubitoControll>();
            cuboGanador.gainPoints();

            cubitoManager manager = FindObjectOfType<cubitoManager>();
            manager.NotificarMonedaRecogida(cuboGanador);

            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0); // Visual, moneda gira en su eje Y
    }
}
