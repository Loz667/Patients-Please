using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopScript : MonoBehaviour
{

    public GameObject stockSpawner;
    public GameObject waveHandler;
    private GameObject[] stockPos;
    

    // Start is called before the first frame update
    void Start()
    {
        stockPos = GameObject.FindGameObjectsWithTag("stockPos");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newStock()
    {
        var oldStock = GameObject.FindGameObjectsWithTag("stock");
        foreach (GameObject obj in oldStock) Destroy(obj);

        Instantiate(stockSpawner, stockPos[0].transform.position, Quaternion.identity, gameObject.transform);
        Instantiate(stockSpawner, stockPos[1].transform.position, Quaternion.identity, gameObject.transform);
        Instantiate(stockSpawner, stockPos[2].transform.position, Quaternion.identity, gameObject.transform);
    }

    public void closeShop()
    {
        waveHandler.GetComponent<waveScript>().pause = false;
        gameObject.SetActive(false);
    }

}
