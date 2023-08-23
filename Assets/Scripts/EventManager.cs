using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public TalkManager talkManager;

    public GameObject button1;
    public GameObject button2;

    private CanvasGroup trans1;
    private CanvasGroup trans2;
    
    public float fadeTime = 0.5f;
    
    public AudioSource audioSource;
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void ButtonClick()
    {
        talkManager.stateOfDialog = (int)PrintState.Idle;
        talkManager.tLocalID++;
        Debug.Log("Button pressed");

        trans1 = button1.GetComponent<CanvasGroup>();
        trans2 = button2.GetComponent<CanvasGroup>();
        
        //bScript1 = button1.GetComponent<TalkButton1>();
        //bScript2 = button2.GetComponent<TalkButton2>();
        
        talkManager.currentChoice = GameManager.instance.currentHover;

        //StartCoroutine(FadeOut());
        
        button1.SetActive(false);
        button2.SetActive(false);
        talkManager.InvokePrintText();
    }

    private float accumTime;
    
    private IEnumerator FadeOut()
    {
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            trans1.alpha = Mathf.Lerp(0.5f, 0f, accumTime / fadeTime);
            trans2.alpha = Mathf.Lerp(0.5f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        trans1.alpha = 0f;
        trans2.alpha = 0f;
        
    }
}
