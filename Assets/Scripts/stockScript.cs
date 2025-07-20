using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stockScript : MonoBehaviour
{

    public List<GameObject> stock;
    public GameObject shop;

    // Start is called before the first frame update
    void Start()
    {
        shop = GameObject.FindGameObjectWithTag("shop");
        Object[] folder = Resources.LoadAll("Prefabs/stockItems");
        foreach (GameObject obj in folder) { stock.Add(obj); }
        Instantiate(stock[Random.Range(0,stock.Count)], gameObject.transform.position, Quaternion.identity, shop.transform);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
