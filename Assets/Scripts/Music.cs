using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Music : MonoBehaviour
{
    private AudioSource audioSource;
    private string musicPath;
    private bool isPlaying;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        isPlaying = false;
    }

    public void PlayMusic(string fileName)
    {
        musicPath = Application.streamingAssetsPath + "/Music/" + fileName;
        isPlaying = true;
        StartCoroutine(PlayMusicCoroutine());
    }

    IEnumerator PlayMusicCoroutine()
    {
        while (isPlaying)
        {
            using (var www = new WWW("file:///" + musicPath + ".mp3"))
            {
                yield return www;

                audioSource.clip = www.GetAudioClip();
                audioSource.Play();

                // Wait for the clip to finish playing
                while (audioSource.isPlaying)
                {
                    yield return null;
                }
            }
        }
    }

    public void StopMusic()
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