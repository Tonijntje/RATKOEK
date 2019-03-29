using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamera : MonoBehaviour
{
    public GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");    
    }

   
    void Update()
    {
        //"hand" follows camera rotation
        gameObject.transform.rotation = mainCamera.transform.rotation;
    }
}
