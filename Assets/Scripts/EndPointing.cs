using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPointing : MonoBehaviour
{
    // ReSharper disable InconsistentNaming
    
    // change transparency of a canvas element
    
    public float fadeDuration = 1f;
    private CanvasGroup trans;
    private int animationState = 0;
    
    private TalkManager talkManager;
    
    void Start()
    {
        trans = GetComponent<CanvasGroup> ();
        trans.blocksRaycasts = false;
        trans.alpha = 0f;
        
        talkManager = FindObjectOfType<TalkManager>().GetComponent<TalkManager>();
    }

    private void Update()
    {
        if (talkManager.stateOfDialog == (int)PrintState.Idle)
        {
            if(animationState == 0)
                animationState = 1;
        }
        else
        {
            animationState = 0;
            trans.alpha = 0f;
        }
        
        if(animationState == 1)
            trans.alpha += Time.deltaTime / fadeDuration;
        else if(animationState == 2)
            trans.alpha -= Time.deltaTime / fadeDuration;

        if (trans.alpha >= 1 && animationState == 1)
        {
            animationState = 2;
            // Debug.Log($"animationState to 2, now {animationState} {trans.alpha}");
        }
        else if (trans.alpha <= 0 && animationState == 2)
        {
            animationState = 1;
            // Debug.Log($"animationState to 1, now {animationState} {trans.alpha}");
        }
    }
    
}
