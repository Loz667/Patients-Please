using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitScript : MonoBehaviour
{
    public GameObject Tablet;
    public GameObject TabletUI;
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        TabletUI = gameController.GetComponent<controllerScript>().tabletUI;
        Tablet = gameController.GetComponent<controllerScript>().tabletPromptUI;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Tablet.SetActive(false);
            TabletUI.SetActive(false);
        }
    }
}
