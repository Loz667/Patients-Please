using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxSpawningScript : MonoBehaviour
{

    public interactScript interactScript;
    public GameObject box;
    public bool interaction;

    // Start is called before the first frame update
    void Start()
    {
        interactScript = GetComponent<interactScript>();
    }

    // Update is called once per frame
    void Update()
    {
        interaction = interactScript.interaction;
        if (interaction) spawnBox();
    }

    public void spawnBox() {
        Instantiate(box, transform.position + Vector3.up, Quaternion.identity);
    }
}
