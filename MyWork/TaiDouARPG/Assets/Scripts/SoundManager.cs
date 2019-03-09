using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager _instance;

    private Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();
    public AudioClip[] audioClipArray;
    public AudioSource audioSource;
    public bool isQuiet = false;//是否静音
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        foreach(AudioClip audio in audioClipArray)
        {
            audioDict.Add(audio.name, audio);
        }
    }
    public void playerSound(string audioName)
    {
        if (isQuiet) return;
        AudioClip ac;
        if(audioDict.TryGetValue(audioName,out ac))
        {
            //AudioSource.PlayClipAtPoint(ac, Vector3.zero);
            this.audioSource.PlayOneShot(ac);
        }
    }
    public void Play(string audioName,AudioSource audioSource)
    {
        if (isQuiet) return;
        AudioClip ac;
        if (audioDict.TryGetValue(audioName, out ac))
        {
            audioSource.PlayOneShot(ac);
        }
    }
}
