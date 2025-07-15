using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputScript : MonoBehaviour
{
    public bool turn; // false = White's Turn, true = Black's Turn
    public bool pause;
    public bool gameOver;
    public int coins;
    public float speed;
    private Camera _mainCamera;
    private GameObject selected;
    public GameObject playerKing;
    public GameObject waveHandler;
    public GameObject coinsTxt;
    public GameObject loseScreen;
    public GameObject shop;


    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        pause = waveHandler.GetComponent<waveScript>().pause;
        coinsTxt.GetComponent<TextMeshProUGUI>().text = "Coins: " + coins;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (pause || gameOver) return;

        if (turn) enemyTurn();
        else
        {
            var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
            
            if (!rayHit.collider)
            {
                deselect();
                return;
            }
            
            var piece = rayHit.collider.gameObject;

            if (rayHit.collider.gameObject.GetComponent<pieceScript>() != null && piece.CompareTag("white") != turn && !rayHit.collider.gameObject.GetComponent<pieceScript>().hasMovedThisTurn) selectPiece(rayHit);
            else if (selected && rayHit.collider.gameObject.CompareTag("moveValid"))
            {
                GameObject[] oppPieces;
                movePiece(selected, rayHit.point);

                oppPieces = GameObject.FindGameObjectsWithTag("black");
                for (int i = 0; i < oppPieces.Length; i++)
                {
                    if (oppPieces[i].transform.position == selected.transform.position)
                    {
                        if (oppPieces[i].GetComponent<enemyScript>().pieceName == "Pawn") coins += 1;
                        if (oppPieces[i].GetComponent<enemyScript>().pieceName == "Knight") coins += 2;
                        if (oppPieces[i].GetComponent<enemyScript>().pieceName == "Bishop") coins += 3;
                        if (oppPieces[i].GetComponent<enemyScript>().pieceName == "Rook") coins += 5;
                        if (oppPieces[i].GetComponent<enemyScript>().pieceName == "Queen") coins += 8;

                        Destroy(oppPieces[i]);
                        
                    }
                }
                deselect();
            }
            else deselect();
        }
    }

    public void deselect()
    {
        if (selected != null)
        {
            selected.GetComponent<pieceScript>().select = false;
            selected.GetComponent<pieceScript>().hideValidMoves();
            selected = null;
        }
    }
    public void movePiece(GameObject piece, Vector3 moveTo)
    {
        var gridPos = new Vector3(Mathf.Floor(moveTo.x) + 0.5f, Mathf.Floor(moveTo.y) + 0.5f, moveTo.z);
        piece.transform.position = gridPos;

        if (piece.CompareTag("white"))
        {
            //Debug.Log("moving your piece");
            piece.GetComponent<pieceScript>().hasMoved = true;
            piece.GetComponent<pieceScript>().hasMovedThisTurn = true;
            var playerPieces = GameObject.FindGameObjectsWithTag("white");
            var moved = 0;
            foreach (GameObject obj in  playerPieces) if (obj.GetComponent<pieceScript>().hasMovedThisTurn == true) moved++;
            if (moved == playerPieces.Length)
            {
                foreach (GameObject obj in playerPieces) obj.GetComponent<pieceScript>().hasMovedThisTurn = false;
                turn = !turn;
            }
        }

        if (piece.CompareTag("black"))
        {
            piece.GetComponent<enemyScript>().hasMoved = true;
            //piece.GetComponent<enemyScript>().Ghost.transform.position = piece.transform.position;
            if (gridPos == playerKing.transform.position)
            {
                Destroy(playerKing);
                gameOver = true;
                loseScreen.SetActive(true);
            }
            var oppPieces = GameObject.FindGameObjectsWithTag("white");
            foreach (GameObject obj in oppPieces) if(gridPos == obj.transform.position) Destroy(obj);
            
        }



    }

    public void selectPiece(RaycastHit2D rayHit)
    {
        if (!rayHit.collider.gameObject.Equals(selected))
        {
            deselect();
            selected = rayHit.collider.gameObject;
            selected.GetComponent<pieceScript>().select = true;
            selected.GetComponent<pieceScript>().showValidMoves();
        }
    }

    public void enemyTurn()
    {
        var enemyPieces = GameObject.FindGameObjectsWithTag("black");
        for (int i = 0;  i < enemyPieces.Length; i++)
        {
            //Debug.Log(enemyPieces[i]);
            var validMoves = enemyPieces[i].GetComponent<enemyScript>().findValidMoves();
            if (validMoves != null)
            {
                Vector3 closestMove = enemyPieces[i].transform.position;
                float maxdist = (enemyPieces[i].transform.position - playerKing.transform.position).magnitude;
                for (int j = 0; j < validMoves.Count; j++)
                {
                    float dist = (validMoves[j] - playerKing.transform.position).magnitude;
                    if (dist < maxdist)
                    {
                        closestMove = validMoves[j];
                        maxdist = dist;
                    }
                }
                movePiece(enemyPieces[i], closestMove);
            }
        }
        waveHandler.GetComponent<waveScript>().nextTurn();
        turn = !turn;
    }

}


