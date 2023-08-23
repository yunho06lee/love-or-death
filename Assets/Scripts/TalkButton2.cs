using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkButton2 : MonoBehaviour
{
    // ReSharper disable InconsistentNaming
    
    private RectTransform rectTransform;
    
    //버튼의 번호
    public CanvasGroup trans;
    
    private TalkManager talkManager;

    //버튼의 텍스트
    public Text bText;
    
    void OnEnable()
    {
        Debug.Log("button2 start");
        rectTransform = GetComponent<RectTransform>();
        
        trans = GetComponent<CanvasGroup>();
        trans.alpha = 1f;
        
        talkManager = FindObjectOfType<TalkManager>().GetComponent<TalkManager>();
        
        SetPosition(talkManager.tText_second);
    }
    
    //선택지의 갯수와 두 선택지를 담은 배열을 받음
    void SetPosition(string txt)
    {
        rectTransform.LeanSetLocalPosY(-150f);
        bText.text = txt;
    }
}