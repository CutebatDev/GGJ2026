using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioClip pixelTrack;
    [SerializeField] private AudioClip spaceTrack;
    [SerializeField] private AudioClip punkTrack;
    private float currentTime;
    private Dictionary<int, AudioClip> trackDictionary;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMusic.clip = pixelTrack;
        backgroundMusic.Play();
        trackDictionary.Add(1, pixelTrack);
        trackDictionary.Add(2, spaceTrack);
        trackDictionary.Add(3, punkTrack);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private float GetClipTime()
    {
        return backgroundMusic.time;
    }

    public void ChangeAudioTrack(int layerNum)
    {
        Debug.Log("reached method");
        currentTime = GetClipTime();
        
        backgroundMusic.Stop();
        
        backgroundMusic.clip = trackDictionary.GetValueOrDefault(layerNum);
        
        backgroundMusic.time = currentTime;

        backgroundMusic.Play();
    }
    
}
