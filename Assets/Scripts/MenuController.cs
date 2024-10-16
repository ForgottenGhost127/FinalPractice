using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public GameObject canvasMenu;
    private bool isMenuActive = false;
    void Start()
    {
        canvasMenu.SetActive(false);
    }
    
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isMenuActive = !isMenuActive;
            canvasMenu.SetActive(isMenuActive);
        }
    }

    public bool IsMenuActive()
    {
        return isMenuActive;
    }

}
