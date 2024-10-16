using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubitoControll : MonoBehaviour
{
    public int puntos = 0;
    public int vidas = 7;
    public float moveSpeed = 5;
    private bool isMoving = false;

    private Renderer cubiRender;
    private Coroutine currentMoviC;

    void Start()
    {
        cubiRender = GetComponent<Renderer>();
        cubiRender.material.color = Color.magenta;
    }

    public void MoveCubito(Vector3 target)
    {
        currentMoviC = StartCoroutine(MoveCubitos(target));
        if (!isMoving)
        {
            StopCoroutine(currentMoviC);
            
        }
    }

    IEnumerator MoveCubitos(Vector3 target)
    {
        isMoving = true;
        cubiRender.material.color = Color.cyan;
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        cubiRender.material.color = Color.magenta;
        isMoving = false;
        currentMoviC = null;
    }

    public void gainPoints()
    {
        puntos++;
        Debug.Log(name + " ha ganado un punto! Puntos: " + puntos);
    }

    public void loseVidas()
    {
        vidas--;
        Debug.Log(name + " ha perdido una vida! Vidas restantes: " + vidas);
        if (vidas <= 0)
        {
            Debug.Log(name + " ha sido eliminado!");
            Destroy(gameObject);
        }
    }

    public void StopMoving()
    {
        if (currentMoviC != null)
        {
            StopCoroutine(currentMoviC);
            isMoving = false;
            currentMoviC = null;
        }
    }

    public void ResetColor()
    {
        cubiRender.material.color = Color.magenta;
    }
}
