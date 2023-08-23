using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dub : MonoBehaviour
{
    private AudioSource audioSource;
    private string musicPath;
    private bool isPlaying;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        isPlaying = false;
    }

    public void PlayDub(string fileName)
    {
        musicPath = Application.streamingAssetsPath + "/Dub/" + fileName;
        isPlaying = true;
        StartCoroutine(PlayMusicCoroutine());
    }

    IEnumerator PlayMusicCoroutine()
    {
        using (var www = new WWW("file:///" + musicPath + ".mp3"))
        {
            Debug.Log("더빙을 시도했다67ㅅ");
            yield return www;
            
            Debug.Log("더빙을 시ㅛ68효도했다");
            audioSource.clip = www.GetAudioClip();
            audioSource.Play();
            
            Debug.Log("더빙을 시도했다");
            // Wait for the clip to finish playing
            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }
    }

    public void StopDub()
    {
        if (isPlaying)
        {
            isPlaying = false;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}