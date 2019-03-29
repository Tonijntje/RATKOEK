using System.Collections;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public float distance = 3;
    public float throwForce = 500f;
    public GameObject audioEffectPickupFailed;
    public GameObject audioEffectPickup;
    public GameObject audioEffectThrow;
    public GameObject audioEffectDrop;

    private GameObject mainCamera;
    private bool carrying = false;
    private GameObject carriedObject;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        
    }

    void Update()
    {
        if (carrying)
        {
            carry(carriedObject);
            checkDrop();
            checkThrow();

        }
        else
        {
            checkPickup();
        }

    }

    //E to pick an object up, F to throw, E again to drop
    void checkPickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pickup();
        }
    }

    void checkDrop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dropObject(false);
        }
    }

    void checkThrow()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            dropObject(true);
        }
    }

    // carry our object with us
    void carry(GameObject o)
    {

        // setting a position does not work well with rigidbody and thus needs kinematic true to work well.
        // this is not the preferred way as this does not take colliding into account.
        //o.transform.position = Vector3.Lerp(   // make it smooth
        //o.transform.position,
        //mainCamera.transform.position + mainCamera.transform.forward * distance,
        //Time.deltaTime * smooth); 

        // instead of setting its position, we give it velocity towards the position we want.
        // now we can leave kinematics off, and everything is done using the rigidbody physics
        Vector3 moveTo = mainCamera.transform.position + mainCamera.transform.forward * distance;
        // give it a velocity towards the point where we want it to be
        o.GetComponent<Rigidbody>().velocity = (moveTo - o.transform.position) * 10;
        // now dont let it turn around and spin like crazy
        o.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void pickup()
    {
        // get middle of screen
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // we hit something! now check if the object should be able to be picked up.
            Pickupable p = hit.collider.GetComponent<Pickupable>();
            if (p != null)
            {
                //check for distance to hit object
                distance = Vector3.Distance(p.gameObject.transform.position, mainCamera.transform.position);
                Debug.Log("Picking up with a distance of" + distance);
                if (distance < 5f)
                {
                    //pick it up
                    carrying = true;
                    carriedObject = p.gameObject;
                    Rigidbody rg = carriedObject.GetComponent<Rigidbody>();
                    //rg.isKinematic = true;
                    rg.useGravity = false;
                    //rg.constraints =  RigidbodyConstraints.FreezeAll;
                    //rg.detectCollisions = true;
                    audioEffectPickup.GetComponent<AudioSource>().Play();
                }
                else
                {
                    Debug.Log("Too far off");
                    audioEffectPickupFailed.GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                Debug.Log("Not a pickupable object..");
                audioEffectPickupFailed.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            // shot in the dark.. nothing hit
            Debug.Log("Dit not hit anything..");
            audioEffectPickupFailed.GetComponent<AudioSource>().Play();
        }
    }

    void dropObject(bool doThrow)
    {
        carrying = false;
        Rigidbody rg = carriedObject.GetComponent<Rigidbody>();
        //rg.isKinematic = false;
        rg.useGravity = true;
        rg.velocity = Vector3.zero; // stop moving 
        //rg.constraints = RigidbodyConstraints.None;
        if (doThrow)
        {
            rg.AddForce(mainCamera.transform.forward * throwForce);
            audioEffectThrow.GetComponent<AudioSource>().Play();
        }
        else
        {
            audioEffectDrop.GetComponent<AudioSource>().Play();
        }
        carriedObject = null;
    }
    public void dropAny()
    {
        if (carrying)
        {
            dropObject(false);
        }
    }
}

