using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class waveScript : MonoBehaviour
{

    public int turn = 1;
    public int wave;
    public int shopRate;
    public int shopTurn;
    public bool pause;
    public GameObject shop;
    public GameObject inputHandler;
    public GameObject shopCountTxt;
    public GameObject waveCountTxt;
    public List<GameObject> easyEnemies;
    public List<GameObject> medEnemies;
    public List<GameObject> hardEnemies;

    // Start is called before the first frame update
    void Start()
    {
        Object[] easyFolder = Resources.LoadAll("Prefabs/easyEnemyPieces");
        Object[] medFolder = Resources.LoadAll("Prefabs/medEnemyPieces");
        Object[] hardFolder = Resources.LoadAll("Prefabs/hardEnemyPieces");
        foreach (GameObject obj in easyFolder)
        {
            easyEnemies.Add(obj);
        }
        foreach (GameObject obj in medFolder)
        {
            medEnemies.Add(obj);
        }
        foreach (GameObject obj in hardFolder)
        {
            hardEnemies.Add(obj);
        }
        newWave("easy");
        updateCounts();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextTurn() 
    {
        if (!inputHandler.GetComponent<inputScript>().gameOver)
        {
            turn++;
            updateCounts();
            if (turn % 3 == 0) newWave("easy");
            if (turn % 5 == 0) newWave("med");
            if (turn % 7 == 0) newWave("hard");
            if (turn - shopTurn == shopRate)
            {
                shop.SetActive(true);
                shop.GetComponent<shopScript>().newStock();
                pause = true;
                shopTurn = turn;
                shopRate++;
            }
        }
    }

    public void updateCounts() 
    {
        waveCountTxt.GetComponent<TextMeshProUGUI>().text = "Next Wave in\n" + turnsUntilWave() + " Turns";
        shopCountTxt.GetComponent<TextMeshProUGUI>().text = "Next Shop in\n" + (shopTurn + shopRate - turn) + " Turns";
    }


    public int turnsUntilWave() 
    {
        var easy = 3 - turn % 3;
        var med = 5 - turn % 5;
        var hard = 7 - turn % 7;

        return Mathf.Min(easy, med, hard);
    }

    public void newWave(string diff)
    {
        float spawnRate = 2;
        if (diff == "easy" && turn != 0) spawnRate = wave / 3;
        if (diff == "med") spawnRate = wave / 5;
        if (diff == "hard") spawnRate = wave / 7;

        for (int i = 0; i <= spawnRate; i++)
        {
            var newPos = randToGrid(new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(0, 3.5f), 0));
            GameObject enemy = null;
            while (!isPosEmpty(newPos)) { newPos = randToGrid(new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(0, 3.5f), 0)); }
            if (diff == "easy")
            {
                var index = Random.Range(0, easyEnemies.Count);
                enemy = easyEnemies[index];
            }
            if (diff == "med") 
            {
                var index = Random.Range(0, medEnemies.Count);
                enemy = medEnemies[index];
            }
            if (diff == "hard")
            {
                var index = Random.Range(0, hardEnemies.Count);
                enemy = hardEnemies[index];
            }
            if (enemy) Instantiate(enemy, newPos, Quaternion.identity);
        }
        wave++;
    }

    public bool isPosEmpty(Vector3 pos)
    {
        var enemyPieces = GameObject.FindGameObjectsWithTag("black");
        var playerPieces = GameObject.FindGameObjectsWithTag("white");
        var allPieces = playerPieces.Concat(enemyPieces);

        foreach (var piece in allPieces)
        {
            if (piece.transform.position == pos) return false;
        }

        return true;
    }
    public Vector3 randToGrid(Vector3 rand)
    {
        Vector3 gridPos = new Vector3(Mathf.Floor(rand.x) + 0.5f, Mathf.Floor(rand.y) + 0.5f, 0);
        return gridPos;
    }
}
