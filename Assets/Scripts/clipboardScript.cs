using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

//Uses code for reading JSON files based on https://discussions.unity.com/t/how-to-read-json-file/625719/3

[System.Serializable] public class Subject
{
    public string Name;
    public string Age;
    public string Address;
    public string Symptoms;
    public string Diagnosis;
    public string Treatment;
    public string Notes;
}

[System.Serializable] public class Subjects
{
    public Subject[] subjects;
}

public class clipboardScript : MonoBehaviour
{
    public interactScript interactScript;
    public GameObject gameController;
    public GameObject clipboard;
    public GameObject clipboardUI;
    public GameObject NameTXT;
    public GameObject AgeTXT;
    public GameObject AddressTXT;
    public GameObject SymptomsTXT;
    public GameObject DiagnosisTXT;
    public GameObject TreatmentTXT;
    public GameObject NotesTXT;
    public bool interaction;
    public Subject subject;
    public int testNumber;
    
    public TextAsset subjectsJSON;

    // Start is called before the first frame update
    void Start()
    {
        interactScript = GetComponent<interactScript>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        clipboardUI = gameController.GetComponent<controllerScript>().clipboardUI;
       

        NameTXT = GameObject.FindGameObjectWithTag("subjectName");
        AgeTXT = GameObject.FindGameObjectWithTag("subjectAge");
        AddressTXT = GameObject.FindGameObjectWithTag("subjectAddress");
        SymptomsTXT = GameObject.FindGameObjectWithTag("subjectSymptoms");
        DiagnosisTXT = GameObject.FindGameObjectWithTag("subjectDiagnosis");
        TreatmentTXT = GameObject.FindGameObjectWithTag("subjectTreatment");
        NotesTXT = GameObject.FindGameObjectWithTag("subjectNotes");

        Subjects subjectsInJSON = JsonUtility.FromJson<Subjects>(subjectsJSON.text);

        int rand = UnityEngine.Random.Range(0, subjectsInJSON.subjects.Length);
        subject = subjectsInJSON.subjects[rand];
        
    }

    // Update is called once per frame
    void Update()
    {
        interaction = interactScript.interaction;
        if (interaction && clipboard.activeSelf != clipboardUI.activeSelf) toggle();
        if (gameController.GetComponent<controllerScript>().activeScreen != clipboardUI) clipboard.SetActive(true);
    }

    void toggle()
    {
        clipboard.SetActive(!clipboard.activeSelf);
        clipboardUI.SetActive(!clipboardUI.activeSelf);
        if (clipboardUI.activeSelf)
        {
            gameController.GetComponent<controllerScript>().changeActiveScreen(clipboardUI);
            NameTXT.GetComponent<TextMeshProUGUI>().text = subject.Name;
            AgeTXT.GetComponent<TextMeshProUGUI>().text = subject.Age;
            AddressTXT.GetComponent<TextMeshProUGUI>().text = subject.Address;
            SymptomsTXT.GetComponent<TextMeshProUGUI>().text = subject.Symptoms;
            DiagnosisTXT.GetComponent<TextMeshProUGUI>().text = subject.Diagnosis;
            TreatmentTXT.GetComponent<TextMeshProUGUI>().text = subject.Treatment;
            NotesTXT.GetComponent<TextMeshProUGUI>().text = subject.Notes;
        }
        else gameController.GetComponent<controllerScript>().deactivateCursor();
     
    }
}
