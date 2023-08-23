using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
   // ReSharper disable InconsistentNaming
   
   //싱글톤 패턴을 위한 인스턴스
   public static GameManager instance = null;
   
   //ui management 데이터
   public int tSceneName = 1;

   public VideoPlayer video;
   
   //플레이어가 설정한 이름
   public string playerName = "";
   
   //선택 시도하는 버튼
   public char currentHover = 'n';

   void Start()
   {
      // 싱글톤 패턴
      if (instance == null)
      {
         instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else if (instance == this)
      {
         Destroy(gameObject);
      }
      
      //video = GetComponent<VideoPlayer>();
      //video.Play();
   }
   
   void Update()
   {
      
   }
   
   void Talk(int id, int idx)
   {
      
   }

   void CallDialogScene(int dialogID)
   {
      //set tSceneName
   }

   IEnumerator LaunchScene(float time = 0)
   {
      yield return new WaitForSeconds(time); //로딩 화면 필요
      
      LoadingSceneController.LoadScene("SetUsername");
   }
   
   
}
