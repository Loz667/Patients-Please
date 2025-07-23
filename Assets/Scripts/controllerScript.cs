using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class controllerScript : MonoBehaviour
{

    public GameObject activeScreen;
    public GameObject tabletPromptUI;
    public GameObject guidePromptUI;
    public GameObject tabletUI;
    public GameObject guideUI;
    public GameObject clipboardUI;
    public GameObject player;
    public KeyCode openTabletKey;
    public KeyCode openGuideKey;
    public KeyCode toggleCursor;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        activeScreen = guideUI;
        tabletPromptUI.GetComponentInChildren<TextMeshProUGUI>().text = openTabletKey.ToString();
        guidePromptUI.GetComponentInChildren<TextMeshProUGUI>().text = openGuideKey.ToString();

        clipboardUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tabletPromptUI.activeSelf & Input.GetKeyDown(openTabletKey))
        {
            toggleCursorMode();
            toggleTablet();
        }

        if (Input.GetKeyDown(openGuideKey)) {
            toggleCursorMode();
            toggleGuide();
        }
        
        if (Input.GetKeyDown(toggleCursor)) toggleCursorMode();
    }

    public void changeActiveScreen(GameObject newScreen)
    {
        if(activeScreen) activeScreen.SetActive(false);
        activeScreen = newScreen;
        activeScreen.SetActive(true);
        activateCursor();
    }

    public void toggleTablet()
    {
        if (activeScreen == tabletUI) tabletUI.SetActive(!tabletUI.activeSelf);
        else changeActiveScreen(tabletUI);
        if (tabletUI.activeSelf) activateCursor();
        else deactivateCursor();
    }
    
    public void toggleGuide() 
    {
        if(activeScreen == guideUI) guideUI.SetActive(!guideUI.activeSelf);
        else changeActiveScreen(guideUI);
        if (guideUI.activeSelf) activateCursor();
        else deactivateCursor();
    }

    public void activateCursor() 
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = false;
    }

    public void deactivateCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;
    }

    public void toggleCursorMode()
    {
        if (Cursor.visible) deactivateCursor();
        else activateCursor();

    }

}
