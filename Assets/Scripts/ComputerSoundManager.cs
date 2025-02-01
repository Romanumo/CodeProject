using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip computerNoise;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip incorrectSound;

    private AudioSource backgroundPlayer;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        CreateBackgroundPlayer();
    }

    public void ComputerState(bool state)
    {
        if (state)
            backgroundPlayer.Play();
        else
            backgroundPlayer.Stop();
    }

    public void ClickSound()
    {
        source.clip = clickSound;
        source.Play();  
    }

    public void CorrectSound()
    {
        source.clip = correctSound;
        source.Play();
    }

    public void IncorrectSound()
    {
        source.clip = incorrectSound;
        source.Play();
    }

    public void CreateBackgroundPlayer()
    {
        GameObject audioPlayer = new GameObject();
        audioPlayer.transform.parent = this.transform;
        audioPlayer.name = "BackgroundNoise";
        backgroundPlayer = audioPlayer.AddComponent<AudioSource>();

        backgroundPlayer.clip = computerNoise;
    }
}
