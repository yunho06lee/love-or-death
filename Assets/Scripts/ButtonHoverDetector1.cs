using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverDetector1 : MonoBehaviour
{
    public TalkManager talkManager;
    
    public GameObject button1;

    private TalkButton1 bScript1;

    private void Start()
    {
        bScript1 = button1.GetComponent<TalkButton1>(); 
    }

    public void OnPointerEnter()
    {
        // 마우스가 버튼 위에 올라왔을 때 실행할 코드 작성
        if (talkManager.stateOfDialog == (int)PrintState.Answering)
        {
            bScript1.trans.alpha = 0.7f;
            GameManager.instance.currentHover = 'a';
        }
    }

    public void OnPointerExit()
    {
        // 마우스가 버튼에서 벗어났을 때 실행할 코드 작성
        if (talkManager.stateOfDialog == (int)PrintState.Answering)
        {
            bScript1.trans.alpha = 1f;
            GameManager.instance.currentHover = 'n';
        }
    }
}
