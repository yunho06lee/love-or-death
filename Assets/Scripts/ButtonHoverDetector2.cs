using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverDetector2 : MonoBehaviour
{
    public TalkManager talkManager;
    
    public GameObject button2;
    
    private TalkButton2 bScript2;

    private void Start()
    {
        bScript2 = button2.GetComponent<TalkButton2>();
    }

    public void OnPointerEnter()
    {
        // 마우스가 버튼 위에 올라왔을 때 실행할 코드 작성
        if (talkManager.stateOfDialog == (int)PrintState.Answering)
        {
            bScript2.trans.alpha = 0.7f;
            GameManager.instance.currentHover = 'b';
        }
    }

    public void OnPointerExit()
    {
        // 마우스가 버튼에서 벗어났을 때 실행할 코드 작성
        if (talkManager.stateOfDialog == (int)PrintState.Answering)
        {
            bScript2.trans.alpha = 1f;
            GameManager.instance.currentHover = 'n';
        }
    }
}
