using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beslagTarget : MonoBehaviour
{
    public Material pannenKoekMaterial;
    public Material beslagMaterial1;
    public Material beslagMaterial2;
    public Material beslagMaterial3;

    bool isPannekoek = false;
    int nrIngredients = 0;

    private Rigidbody rg;
    private Renderer rend;
    private Material mat;

    void Start()
    {
        rg = gameObject.GetComponent<Rigidbody>();
        mat = gameObject.GetComponent<Material>();
        rg.constraints = RigidbodyConstraints.FreezeAll; //freeze untill pancake


        
    }

    public void OnCollisionEnter(Collision coll)
    {
        Debug.Log("collision with" + coll.gameObject.name);
        if (isPannekoek)
        {
            GameObject.FindWithTag("GameController").GetComponent<CountDownTimer>().YouWin();
        }
        if (coll.gameObject.tag == "ingredient")
        {
            coll.gameObject.active = false; //ingredient dissapears
            GameObject.FindWithTag("GameController").GetComponent<PickupObject>().dropAny();
            nrIngredients++;
            Debug.Log("ingredient " + coll.gameObject.name + " is in. Now has " + nrIngredients + " ingredients.");

            gameObject.transform.localScale += new Vector3(0, 0.1f, 0); //make beslag thicker

            switch(nrIngredients)
            {
                case 1:
                    gameObject.GetComponent<MeshRenderer>().material = beslagMaterial1;
                    break;
                case 2:
                    gameObject.GetComponent<MeshRenderer>().material = beslagMaterial2;
                    break;
                case 3:
                    gameObject.GetComponent<MeshRenderer>().material = beslagMaterial3;
                    break;

            }

            if (nrIngredients >=3)
            {
                rg.constraints = RigidbodyConstraints.None; //unlock constraints, we got pancake
            }

        }

        if (coll.gameObject.name == "spatel")
        {
            if (nrIngredients >=3)
            {
                rg.velocity = new Vector3(0, 20.0f, 0);
                rg.angularVelocity = new Vector3(0, 0, 5f);
                isPannekoek = true;
                Debug.Log("is pannekoek!");
                gameObject.GetComponent<MeshRenderer>().material = pannenKoekMaterial;
                //flip the pancake in the air, we're done here.
            }
        }
    }
}
