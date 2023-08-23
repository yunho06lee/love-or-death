using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answering : MonoBehaviour
{
    public TalkManager talkManager;
    
    private CanvasGroup trans;

    private bool isShow = true;

    void Start()
    {
        trans = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (talkManager.stateOfDialog == (int)PrintState.Answering)
            trans.alpha = 0f;
        else
            trans.alpha = 1f;
    }
}
