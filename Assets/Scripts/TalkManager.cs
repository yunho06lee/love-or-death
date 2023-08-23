using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

enum PrintState
{
    Idle,
    Printing,
    Answering,
    Animating
}

public class TalkManager : MonoBehaviour
{
    // ReSharper disable InconsistentNaming
    
    
    //dialog information;
    private char tLineCode;         //선택지에 따른 분기 코드
    private int tIsDubbed;           //더빙 넘버
    private string tName;           //이름
    public string tText_first;     //대사 첫 줄(선택지)
    public string tText_second;    //대사 둘째 줄(선택지)
    private string characterSkin;   //캐릭터 스킨
    private string animationInvoke; //애니메이션 이름
    private string musicInvoke;     //음악 이름
    private string sceneInvoke;     //씬 이름
    
    public int tLocalID;           //n번째 대화
    public int stateOfDialog;
    
    //data received from dialog file
    private List<string> dialogData;
    private int dialogID;           //불러오는 대본의 번호

    //Text UI
    public Text talkText;
    public Text talkName;
    
    private StringBuilder curOutput;
    public float printRate = 0.05f;
    private int tStringID;
    
    //버튼 UI
    public GameObject button1;
    public GameObject button2;
    
    //선택지 관리 변수
    public int numberOfButtons;
    
    //현재의 선택지
    public char currentChoice;

    public GameObject Character;
    public GameObject BackDrop;
    
    private AudioSource audioSource;
    private Music music;

    public GameObject DubPrint;
    
    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        music = GetComponent<Music>();
        
        //버튼의 비활성화
        button1.SetActive(false);
        button2.SetActive(false);
        
        //init UI objects
        talkText.text = "";
        talkName.text = "";
        currentChoice = 'n';

        //list for dialog data
        tLocalID = 1;
        dialogData = new List<string>();
        //get id number from GameManager 
        dialogID = GameManager.instance.tSceneName;
        //호출 전 설정된 장면의 번호 확인
        GetData(dialogID);
        stateOfDialog = (int)PrintState.Idle;
        
        Debug.Log("Dialog Launch");
        InvokePrintText();
    }

    void Update()
    {
        //다음 문장으로 넘기기
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) && stateOfDialog == (int)PrintState.Idle)
        {
            tLocalID++;
            Debug.Log("Spacebar pressed");
            InvokePrintText();
        }
        
        //출력 중 스킵
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) && stateOfDialog == (int)PrintState.Printing && tStringID > 0)
        {
            talkText.text = tText_first;
            stateOfDialog = (int)PrintState.Idle;
        }
    }

    //호출된 ID의 전제 대화 데이터를 경로로부터 가져옴
    void GetData(int dialogSceneNumber)
    {
        try //check if dialog is in directory
        {
            StreamReader line = new StreamReader(Path.Combine(Application.streamingAssetsPath, $"Dialogs/dialog{GameManager.instance.tSceneName}.txt"));

            string input;
            
            //add each line of dialog to list
            while ((input = line.ReadLine()) != null)
                dialogData.Add(input);

            Debug.Log("Received dialog successfully");
        }
        catch //if unable to find next dialog file, break
        {
            Debug.Log("Unable to find dialog");
            //게임의 끝!
        }
    }

    //멤버 변수들을 초기화
    void LineInit()
    {
        string[] tmp = dialogData[tLocalID].Split('/');
        
        tLineCode = tmp[0][0];
        tIsDubbed = Convert.ToInt32(tmp[1]);
        tName = tmp[2];
        tText_first = tmp[3];
        tText_second = tmp[4];
        characterSkin = tmp[5];
        animationInvoke = tmp[6];
        musicInvoke = tmp[7];
        sceneInvoke = tmp[8];

        tText_first = InsertName(tText_first);
        tText_second = InsertName(tText_second);
    }
    
    //대화 및 효과 출력
    public void InvokePrintText()
    {
        if (tLocalID >= dialogData.Count)
        {
            Debug.Log("End of Dialog");
            GameManager.instance.tSceneName++;
            //대화 끝에 도달
            
            if (GameManager.instance.tSceneName >= 10)
                SceneManager.LoadScene("EndScene");
            else
                LoadingSceneController.LoadScene("DialogScene");
            
            return;
        }
        
        LineInit();

        if (characterSkin != "n")
            Character.GetComponent<CharacterSkinManager>().changeSkin(characterSkin);
        
        if(sceneInvoke != "n")
            BackDrop.GetComponent<BackdropSkinManager>().changeSkin(sceneInvoke);

        if (musicInvoke != "n")
        {
            music.StopMusic();
            music.PlayMusic(musicInvoke);
        }

        if (tLineCode == 'n')
            currentChoice = 'n';
        else if (tLineCode != currentChoice)
            return;
        
        //주인공의 텍스트에 대해서는 선택지로 출력
        if (tName == "me")
        {
            InvokePrintAnswer();
            return;
        }
        
        //애니메이션?
        
        //더빙?
        
        //음악?
        
        //일러스트 전환?
        //이 필요함
        
        
        //대사 출력 전 멤버 초기화
        talkText.text = "";
        tStringID = 0;
        
        //일반 대사 출력
        stateOfDialog = (int)PrintState.Printing;
        StartCoroutine(PrintDialog());
        
        if(tName != "narrative")
            talkName.text = tName;
        else
            talkName.text = "";
        
        tStringID = 0;
        curOutput = new StringBuilder("");
    }
    
    //선택지 및 효과 출력
    void InvokePrintAnswer()
    {
        DubPrint.GetComponent<Dub>().StopDub();
        
        stateOfDialog = (int)PrintState.Answering;
       
       LineInit();

       if (tText_second == "n")
       {
           //선택지 하나
           numberOfButtons = 1;
           button1.SetActive(true);
       }
       else
       {
           //선택지 둘
           numberOfButtons = 2;
           button1.SetActive(true);
           button2.SetActive(true);
       }
    }
    
    //대화를 출력하는 코루틴
    //PrintState가 Printing일 때만 작동
    IEnumerator PrintDialog()
    {
        DubPrint.GetComponent<Dub>().PlayDub($"{tIsDubbed}");
        //더빙
        
        while (true)
        {
            if (stateOfDialog != (int)PrintState.Printing)
            {
                //Debug.Log("stopped printing");
                yield break;
            }
            
            if (tStringID == 0)
                curOutput = new StringBuilder("");
            talkText.text = curOutput.ToString();
            if (tStringID >= tText_first.Length) //end of print
                stateOfDialog = (int)PrintState.Idle;
            else
                curOutput.Append(tText_first[tStringID++]);

            yield return new WaitForSeconds(printRate);
        }
    }

    string InsertName(string s)
    {
        foreach (char c in s)
        {
            if (c == '*')
            {
                s = s.Replace("*", GameManager.instance.playerName);
                break;
            }
        }

        return s;
    }
}
