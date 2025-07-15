using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyScript : MonoBehaviour
{

    public int cost;
    public GameObject piece;
    public GameObject inputHandler;
    public GameObject waveHandler;
    public waveScript waveScript;
    public inputScript inputScript;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GameObject.FindGameObjectWithTag("inputHandler");
        waveHandler = GameObject.FindGameObjectWithTag("waveHandler");
        inputScript = inputHandler.GetComponent<inputScript>();
        waveScript = waveHandler.GetComponent<waveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buyPiece() 
    {
        if(inputScript.coins >= cost) 
        {
            inputScript.coins -= cost;
            var newPos = waveScript.randToGrid(new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-3.5f, 0f), 0));
            while (!waveScript.isPosEmpty(newPos)) { newPos = waveScript.randToGrid(new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(0, 3.5f), 0)); }
            Instantiate(piece, newPos, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
