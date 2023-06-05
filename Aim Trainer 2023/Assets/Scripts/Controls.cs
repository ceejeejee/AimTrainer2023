using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public KeyCode shoot = KeyCode.Mouse0;
    public KeyCode quit = KeyCode.Escape;
    public KeyCode restart = KeyCode.R;
    public KeyCode pause = KeyCode.Tab;

    public bool isPressingShoot()
    {
        return Input.GetKeyDown(shoot);
    }

    public bool isPressingQuit()
    {
        return Input.GetKeyDown(quit);
    }

    public bool isPressingRestart()
    {
        return Input.GetKeyDown(restart);
    }

    public bool isPressingPause(){
        return Input.GetKeyDown(pause);
    }
}