using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource ArrowSound;
    public AudioSource DyingSound;
    public AudioSource BowPullingSound;
    public AudioSource BackgroundMusic;
    public AudioSource FootStep;
    public AudioSource Clicking;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
}
