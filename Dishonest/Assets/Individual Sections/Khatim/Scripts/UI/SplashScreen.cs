using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public float transitionTime;
    [SerializeField]
    private float time = 0;
    void Start()
    {
        time = 0;
        AudioManager.instance.SplashScreenAudio();
    }
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time >= transitionTime)
        {
            SceneManage.instance.MainMenu();
            AudioManager.instance.StopAudio();
			AudioManager.instance.MainMenuAudio();
        }
    }
}
