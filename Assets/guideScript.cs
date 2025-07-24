using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guideScript : MonoBehaviour
{

    public GameObject[] guidebookPages;
    public GameObject backBtn;
    public GameObject fwdBtn;
    public int currentPage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goBack()
    {
        fwdBtn.SetActive(true);
        guidebookPages[currentPage].SetActive(false);
        currentPage--;
        guidebookPages[currentPage].SetActive(true);
        if (currentPage <= 0) backBtn.SetActive(false);

    }
    public void goForward()
    {
        backBtn.SetActive(true);
        guidebookPages[currentPage].SetActive(false);
        currentPage++;
        guidebookPages[currentPage].SetActive(true);
        if (currentPage >= guidebookPages.Length - 1) fwdBtn.SetActive(false);
    }
}
