﻿using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour
{
    public void Start() { }
    public void Update() { }

    public void SetSingleMode()
    {
        GameObject.Find("GameController")
            .GetComponent<Controller>()
            .setMode(Controller.PlayMode.SINGLE);

        Application.LoadLevel(1);
    }

    public void SetMultiMode()
    {
        GameObject.Find("GameController")
            .GetComponent<Controller>()
            .setMode(Controller.PlayMode.MULTI);

        Application.LoadLevel(1);
    }
}
