using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    [SerializeField] private AudioClip[] clips= new AudioClip[10];
    private AudioSource audioSource;
    private bool isFinal = false;
    [SerializeField]private AudioClip mainBG;


    public void PlaySound(string name, float volumeScale)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(getSound(name),volumeScale);
    }
    public void PlayFinalMusic()
    {
        isFinal = true;
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(getSound("FinalBG"),1);
    }
    private AudioClip getSound(string _name)
    {
        AudioClip _myAudioClip =  clips[0];
        foreach (AudioClip i in clips)
        {
            if(_name == i.name)
            {
                _myAudioClip = i;
            }
        }
        return _myAudioClip;
    }
    void Start()
    {
       audioSource = FindObjectOfType<AudioSource>();
       audioSource.loop = true;
    }
    void FixedUpdate()
    {
        if(!audioSource.isPlaying)
        {
           audioSource.clip = mainBG;
           audioSource.Play();
           
        }
        if(isFinal)
        {
            audioSource.Stop();
        }
    }
}
