using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;

public class OpeningNarrative : MonoBehaviour
{
    enum PrologState
    {
        Idle,
        Printing,
        Waiting
    }

    public Text prologText;
    
    private string forPrint;

    public int stateOfProlog;

    private int pStringID;
    
    public float printRate = 0.05f;
    
    private StringBuilder curOutput;

    public GameObject inputField;
    public Text txt;

    private string[] OpeningLines =
    {
        "특별 이벤트 개최중!",
        "당첨되시는 분에게는 특별한 상품이 주어집니다",
        "이벤트에 응모하기 위해서 이름을 입력해 주세요",
        "ask name",
        "성공적으로 접수되었습니다",
        "이벤트에 참여해 주셔서 감사합니다.",
        "당첨되신 분에게는 개별적으로 연락드리겠습니다."
    };
    private int openingLineID = 0;

    private CanvasGroup trans;

    private RectTransform rt;
    
    public RectTransform inputFieldRT;

    private void Start()
    {
        //입력창 끄기
        inputField.SetActive(false);
        stateOfProlog = (int)PrologState.Idle;
        trans = GetComponent<CanvasGroup>();
        rt = GetComponent<RectTransform>();
        prologText = GetComponent<Text>();
        openingLineID = 0;
        NextPage();
        
    }

    void NextPage()
    {
        Debug.Log("nxtpg");
        if (openingLineID >= OpeningLines.Length)
        {
            LoadingSceneController.LoadScene("DialogScene");
            return;
        }

        pStringID = 0;
        forPrint = OpeningLines[openingLineID];
        trans.alpha = 1f;
        Debug.Log(stateOfProlog);

        if (forPrint == "ask name")
        {
            GetName();
            return;
        }
        
        stateOfProlog = (int)PrologState.Printing;
        StartCoroutine(PrintDialog());
        openingLineID++;
    }

    void GetName()
    {
        inputField.SetActive(true);
        stateOfProlog = (int)PrologState.Waiting;
        rt.LeanSetLocalPosY(1000);
        //입력 대기
    }
    
    private void Update()
    {
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) && stateOfProlog == (int)PrologState.Idle)
        {
            rt.LeanSetLocalPosY(0);
            NextPage();
        }
        
        if(stateOfProlog == (int)PrologState.Waiting)
            trans.alpha = 0f;

        if (Input.GetKeyUp(KeyCode.Return) && stateOfProlog == (int)PrologState.Waiting)
        {
            GameManager.instance.playerName = txt.text;
            Debug.Log(GameManager.instance.playerName);
            
            inputField.SetActive(false);
            inputFieldRT.LeanSetLocalPosY(10000);
            Debug.Log(inputField.activeSelf);
            
            rt.LeanSetLocalPosY(0);
            stateOfProlog = (int)PrologState.Idle;
            openingLineID++;
            NextPage();
        }
    }

    IEnumerator PrintDialog()
    {
        Debug.Log("coroutine?");
        while (true)
        {
            if (stateOfProlog != (int)PrintState.Printing)
                yield break;

            if (pStringID == 0)
                curOutput = new StringBuilder("");
            prologText.text = curOutput.ToString();
            if (pStringID >= forPrint.Length) //end of print
                stateOfProlog = (int)PrintState.Idle;
            else
                curOutput.Append(forPrint[pStringID++]);

            yield return new WaitForSeconds(printRate);
        }
    }
}
