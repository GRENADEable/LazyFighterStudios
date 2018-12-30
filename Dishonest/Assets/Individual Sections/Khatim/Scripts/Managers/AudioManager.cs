using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public AudioSource fxSource;
    public AudioSource mainmenuFx;
    public AudioSource gameOverFx;
    public AudioClip[] soundClips;
    public float audioStart;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void StopAudio()
    {
        fxSource.Stop();
        mainmenuFx.Stop();
        gameOverFx.Stop();
    }

    void AudioAcess(int index)
    {
        fxSource.PlayOneShot(soundClips[index]);
    }

    public void MainMenuAudio()
    {
        mainmenuFx.time = audioStart;
        mainmenuFx.Play();
        mainmenuFx.loop = true;
        //AudioAcess(0);
    }

    public void SplashScreenAudio()
    {
        AudioAcess(1);
    }
}
