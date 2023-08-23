using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkButton1 : MonoBehaviour
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
        Debug.Log("button1 start");
        rectTransform = GetComponent<RectTransform>();
        
        trans = GetComponent<CanvasGroup>();
        trans.alpha = 1f;
        
        talkManager = FindObjectOfType<TalkManager>().GetComponent<TalkManager>();
        
        SetPosition(talkManager.tText_first);
    }
    
    //선택지의 갯수와 두 선택지를 담은 배열을 받음
    void SetPosition(string txt)
    {
        if (talkManager.numberOfButtons == 1)
        {
            rectTransform.LeanSetLocalPosY(0f);
            bText.text = txt;
        }
        else
        {
            rectTransform.LeanSetLocalPosY(200f);
            bText.text = txt;
        }
    }
}
