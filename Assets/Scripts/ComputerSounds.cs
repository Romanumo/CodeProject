using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

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
}
