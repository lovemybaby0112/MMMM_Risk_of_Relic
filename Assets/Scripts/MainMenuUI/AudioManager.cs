using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    AudioSource backgroundMusic;
    public AudioSource selectButtonMusic;
    public AudioSource pressButtonMusic;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMasterVolume(Slider s)
    {
        AudioListener.volume = s.value;
    }

    public void SFXVolume(Slider s)
    {
        selectButtonMusic.volume = s.value;
        pressButtonMusic.volume = s.value;
    }
    public void BackgroundVolume(Slider s)
    {
        backgroundMusic = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();
        backgroundMusic.volume = s.value;
    }

}
