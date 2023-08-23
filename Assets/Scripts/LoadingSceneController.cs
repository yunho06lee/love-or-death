using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingSceneController : MonoBehaviour
{
    private static string nextScene;

    [SerializeField]
    Image ProgressBar;
    
    public Sprite[] img;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        while (ProgressBar.fillAmount < 1f)
        {
            yield return new WaitForSeconds(0.05f);

            ProgressBar.fillAmount += 0.02f;
        }
        
        SceneManager.LoadScene(nextScene);
    }
}
