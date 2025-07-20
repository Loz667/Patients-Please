using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class controllerScript : MonoBehaviour
{

    public GameObject tabletPromptUI;
    public GameObject tabletUI;
    public GameObject player;
    public KeyCode openTabletKey;
    public KeyCode toggleCursor;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        tabletPromptUI.GetComponentInChildren<TextMeshProUGUI>().text = openTabletKey.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (tabletPromptUI.activeSelf & Input.GetKeyDown(openTabletKey))
        {
            toggleTablet();
            toggleCursorMode();
        }
        if (Input.GetKeyDown(toggleCursor)) toggleCursorMode();
    }

    public void toggleTablet()
    {
        tabletUI.SetActive(!tabletUI.activeSelf);
    }
    
    public void toggleCursorMode()
    {
        Cursor.visible = !Cursor.visible;
        player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = !Cursor.visible;
        if (Cursor.visible) Cursor.lockState = CursorLockMode.Confined;
        else Cursor.lockState = CursorLockMode.Locked;

    }

}
