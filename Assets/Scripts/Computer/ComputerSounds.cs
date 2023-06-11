using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip incorrectSound;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
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
}
