using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public GameObject Handle;
    public interactScript interactScript;
    public bool interaction;
    public bool isOpen;
    public Quaternion startingRotation;
    public int openDir;

    // Start is called before the first frame update
    void Start()
    {
        interactScript = Handle.GetComponent<interactScript>();
        startingRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        interaction = interactScript.interaction;
        if (interaction) { toggleDoor(); }
    }

    public void toggleDoor() 
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            gameObject.transform.rotation = startingRotation * Quaternion.Euler(0, -90 * openDir, 0);
        }
        else 
        {
            gameObject.transform.rotation = startingRotation;
        }
    }
}
