using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMethods : MonoBehaviour
{
    public GameObject GamemodeManager;

    public void QuitButton(){
        Application.Quit();
    }

    public void RestartButton(){
        GamemodeManager.GetComponent<SevenBALLS>().roundEnd();
    }

    public void ChangeGMButton(){

    }
}
