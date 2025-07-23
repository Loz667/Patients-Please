using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum interactionType
{
    HOLD, PRESS, RELEASE
}

public class interactScript : MonoBehaviour
{

    public GameObject playerCamera;
    public GameObject UIInteract;
    public bool interaction;
    public bool disabled;
    public interactionType interactionType;
    public KeyCode interactKey;
    public float playerDist; //distance to the player
    public float range; //distance player must be within to interact
    public float hoverRadius; //how far out the UIInteract hovers
    public float defaultScale; //localsize when player is at distance 1u

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.FindWithTag("CinemachineTarget");
    }

    // Update is called once per frame
    void Update()
    {
        playerDist = (gameObject.transform.position - playerCamera.transform.position).magnitude;
        if (playerDist < range)
        {
            if (!disabled)
            {
                UIInteract.SetActive(true);
                UIInteract.transform.position = gameObject.transform.position + (playerCamera.transform.position - gameObject.transform.position).normalized * hoverRadius * playerDist;
                UIInteract.transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.transform.position);
                UIInteract.transform.localScale = Vector3.one * defaultScale * playerDist;
            }
            else UIInteract.SetActive(false);
            if (interactionType == interactionType.HOLD)
            {
                if (Input.GetKey(interactKey)) interaction = true;
                else interaction = false;
            }
            if (interactionType == interactionType.PRESS)
            {
                if (Input.GetKeyDown(interactKey)) interaction = true;
                else interaction = false;
            }
            if (interactionType == interactionType.RELEASE)
            {
                if (Input.GetKeyUp(interactKey)) interaction = true;
                else interaction = false;
            }
        }
        else UIInteract.SetActive(false);
    }
}
