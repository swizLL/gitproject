using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalButton : MonoBehaviour {
    public void closeLoginPage()
    {
        UIController._instance.loginPage.SetActive(false);
        UIController._instance.startPage.SetActive(true);
    }
    public void closeRegisterPage()
    {
        UIController._instance.registerPage.SetActive(false);
        UIController._instance.startPage.SetActive(true);
    }
    public void backToSlctPage()
    {
        showPage(UIController._instance.characterSelPage);
        hidePage(UIController._instance.characterChangePage);
        UIController._instance.playerNameInputFld.text = "";
    }
    void showPage(GameObject needShow)
    {
        needShow.SetActive(true);
    }
    void hidePage(GameObject needHide)
    {
        needHide.SetActive(false);
    }
}
