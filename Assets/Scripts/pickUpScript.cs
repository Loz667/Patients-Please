using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pickUpScript : MonoBehaviour
{
    public GameObject playerCamera;
    public interactScript interactScript;
    public float dist; //distance to be held
    public bool interaction;
    public bool held;

    // Start is called before the first frame update
    void Start()
    {
        interactScript = GetComponent<interactScript>();
        playerCamera = GameObject.FindWithTag("CinemachineTarget");
    }

    // Update is called once per frame
    void Update()
    {
        interaction = interactScript.interaction;
        if (interaction)
        {
            held = !held;
        }

        if(held) 
        {
            interactScript.disabled = true;
            transform.position = playerCamera.transform.position + playerCamera.transform.forward * dist;
        }
        else interactScript.disabled = false;
    }
}
