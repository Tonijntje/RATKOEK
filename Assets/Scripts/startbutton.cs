using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startbutton : MonoBehaviour
{
  

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.B))
        {
            ClickStart();
        }

    }
    public void ClickStart()
    {
        SceneManager.LoadScene("hellyea");
    }
}

