using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class pieceScript : MonoBehaviour
{

    public bool select;
    public bool hasMoved;
    public bool turn;
    public bool hasMovedThisTurn;
    public string pieceName;
    public GameObject selection;
    public GameObject validMove;
    public GameObject InputHandler;
    public inputScript inputScript;

    // Start is called before the first frame update
    void Start()
    {
        InputHandler = GameObject.FindGameObjectWithTag("inputHandler");
        inputScript = InputHandler.GetComponent<inputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (select) selection.SetActive(true);
        else selection.SetActive(false);

        turn = inputScript.turn;
    }

    public void showValidMoves()
    {
        var validMoves = findValidMoves();
        if (validMoves.Count != 0) foreach (Vector3 move in validMoves) Instantiate(validMove, move + Vector3.back, Quaternion.identity);
        else
        {
            hasMovedThisTurn = true;
            var myPieces = GameObject.FindGameObjectsWithTag("white");
            var moved = 0;
            foreach (GameObject obj in myPieces) if (obj.GetComponent<pieceScript>().hasMovedThisTurn) moved++;
            if (moved == myPieces.Length)
            {
                foreach (GameObject obj in myPieces) obj.GetComponent<pieceScript>().hasMovedThisTurn = false;
                inputScript.deselect();
                inputScript.turn = !turn;
            }
        }
    }

    public List<Vector3> findValidMoves()
    {
        List<Vector3> validMoves = new List<Vector3>();

        var curPos = gameObject.transform.position;
        var newPos = Vector3.zero;
        if (pieceName == "King")
        {
            // King move logic
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!(j == 0 && i == 0))
                    {
                        newPos = new Vector3(curPos.x + i, curPos.y + j, curPos.z);
                        //Debug.Log(newPos);
                        if (isMoveValid(newPos)) validMoves.Add(newPos);
                    }
                }
            }
        }
        if (pieceName == "Pawn")
        {
            var upLeft = new Vector3(-1, 1, 0);
            var upRight = new Vector3(1, 1, 0);
            var downLeft = new Vector3(-1, -1, 0);
            var downRight = new Vector3(1, -1, 0);
            // Pawn move logic
            if (gameObject.CompareTag("white")) //if white
            {
                newPos = new Vector3(curPos.x, curPos.y + 1, curPos.z);
                if (isMoveValid(newPos)) validMoves.Add(newPos);
                if (!hasMoved)
                {
                    newPos = new Vector3(curPos.x, curPos.y + 2, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                }

                var oppPieces = GameObject.FindGameObjectsWithTag("black");
                for (int i = 0; i < oppPieces.Length; i++)
                {
                    if (oppPieces[i].transform.position == curPos + upLeft)
                    {
                        newPos = curPos + upLeft;
                        if (isMoveValid(newPos)) validMoves.Add(newPos);
                    }
                    if (oppPieces[i].transform.position == curPos + upRight)
                    {
                        newPos = curPos + upRight;
                        if (isMoveValid(newPos)) validMoves.Add(newPos);
                    }
                }
            }
            else //if black
            {

                newPos = new Vector3(curPos.x, curPos.y - 1, curPos.z);
                if (isMoveValid(newPos)) validMoves.Add(newPos);
                if (!hasMoved)
                {
                    newPos = new Vector3(curPos.x, curPos.y - 2, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                }

                var oppPieces = GameObject.FindGameObjectsWithTag("white");
                for (int i = 0; i < oppPieces.Length; i++)
                {
                    if (oppPieces[i].transform.position == curPos + downLeft)
                    {
                        newPos = curPos + downLeft;
                        if (isMoveValid(newPos)) validMoves.Add(newPos);
                    }
                    if (oppPieces[i].transform.position == curPos + downRight)
                    {
                        newPos = curPos + downRight;
                        if (isMoveValid(newPos)) validMoves.Add(newPos);
                    }
                }
            }


        }
        if (pieceName == "Rook")
        {
            // Rook move logic
            for (int i = -8; i < 9; i++)
            {
                if (i != 0)
                {
                    newPos = new Vector3(curPos.x + i, curPos.y, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                    newPos = new Vector3(curPos.x, curPos.y + i, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                }
            }
        }
        if (pieceName == "Bishop")
        {
            //Bishop move logic
            for (int i = -8; i < 9; i++)
            {
                if (i != 0)
                {
                    newPos = new Vector3(curPos.x + i, curPos.y + i, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                    newPos = new Vector3(curPos.x - i, curPos.y + i, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                }
            }
        }
        if (pieceName == "Queen")
        {
            //Queen move logic
            for (int i = -8; i < 9; i++)
            {
                if (i != 0)
                {
                    newPos = new Vector3(curPos.x + i, curPos.y, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                    newPos = new Vector3(curPos.x, curPos.y + i, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                    newPos = new Vector3(curPos.x + i, curPos.y + i, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                    newPos = new Vector3(curPos.x - i, curPos.y + i, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                }
            }
        }
        if (pieceName == "Knight")
        {
            for (int i = -1; i < 2; i++)
            {
                if (i != 0)
                {
                    newPos = new Vector3(curPos.x + i, curPos.y + 2 * i, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                    newPos = new Vector3(curPos.x + 2 * i, curPos.y + i, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                    newPos = new Vector3(curPos.x - i, curPos.y + 2 * i, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                    newPos = new Vector3(curPos.x - 2 * i, curPos.y + i, curPos.z);
                    if (isMoveValid(newPos)) validMoves.Add(newPos);
                }
            }
        }

        return validMoves;
    }



    public bool isMoveValid(Vector3 pos)
    {
        var myPieces = GameObject.FindGameObjectsWithTag(gameObject.tag);
        var oppPieces = GameObject.FindGameObjectsWithTag("black");

        if (gameObject.CompareTag("white")) oppPieces = GameObject.FindGameObjectsWithTag("black");
        else oppPieces = GameObject.FindGameObjectsWithTag("white");

        var allPieces = myPieces.Concat(oppPieces).ToArray();

        // Check if the space if off the board
        if (pos.x < -3.5) return false;
        if (pos.x > 3.5) return false;
        if (pos.y < -3.5) return false;
        if (pos.y > 3.5) return false;

        // Check if the same teams piece is in the space
        for (int i = 0 ; i < myPieces.Length; i++)
        {
            if (pos == myPieces[i].transform.position) return false;
        }

        // Check if a piece obstructs the move
        if (pieceName != "Knight")
        {
            var curPos = gameObject.transform.position;
            var diffx = Mathf.Abs(pos.x - curPos.x);
            var diffy = Mathf.Abs(pos.y - curPos.y);
            var dist = Mathf.Max(diffx, diffy);

            var between = curPos;

            for (int i = 1; i < dist; i++)
            {
                if (dist != 0) between = Vector3.Lerp(curPos, pos, i / dist);
                for (int j = 0; j < allPieces.Length; j++)
                {
                    if (between == allPieces[j].transform.position && between != curPos)
                    {
                        //Debug.Log("Pos: " + pos + " Between: " + between + " i, j: " + i + ", " + j);
                        return false;
                    }
                }
            }
        }

        // Pawns are stupid
        if (pieceName == "Pawn")
        {
            var up = new Vector3(0, 1, 0);
            var down = new Vector3(0, -1, 0);

            for (int i = 0; i < allPieces.Length; i++)
            {
                var test = allPieces[i].transform.position;
                if (gameObject.CompareTag("white") && test == gameObject.transform.position + up && test == pos) return false;
                else if (gameObject.CompareTag("black") && test == gameObject.transform.position + down && test == pos) return false;
            }
        }

        return true;
    }

    public void hideValidMoves()
    {
        var validMoves = GameObject.FindGameObjectsWithTag("moveValid");
        for (int i = 0; i < validMoves.Length; i++)
        {
            Destroy(validMoves[i]);
        }
    }

}
