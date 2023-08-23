using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public bool isStarted = false;
    void Update()
    {
        if (Input.anyKeyDown && !isStarted)
        {
            LoadingSceneController.LoadScene("SetUsername");
            isStarted = true;
        }
    }
}
