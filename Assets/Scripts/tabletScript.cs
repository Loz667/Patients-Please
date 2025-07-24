using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tabletScript : MonoBehaviour
{

    public GameObject activeScreen;
    public GameObject clipboardUI;
    public GameObject diagnosisTXT;
    public GameObject treatmentTXT;
    public Color selectedColor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToTabletScreen(GameObject newScreen)
    {
        activeScreen.SetActive(false);
        activeScreen = newScreen;
        activeScreen.SetActive(true);
    }

    public void assignDiagnosis(Button btn)
    {
        diagnosisTXT.GetComponentInChildren<TextMeshProUGUI>().text = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        toggleSelection(btn);
    }
    public void assignTreatment(Button btn)
    {
        treatmentTXT.GetComponentInChildren<TextMeshProUGUI>().text = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        toggleSelection(btn);
    }

    public void toggleSelection(Button btn)
    {
        Color normalBtn = btn.colors.normalColor;
        //Color selectedBtn = btn.colors.selectedColor;
        ColorBlock cb = btn.colors;

        if (normalBtn != selectedColor)
        {
            cb.normalColor = selectedColor;
        }
        else
        {
            cb.normalColor = Color.white;
        }
        cb.selectedColor = cb.normalColor;
        btn.colors = cb;
    }
}
