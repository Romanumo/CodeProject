using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] private GameObject dialogWindow;
    private AudioSource audio;
    bool inWindow = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (inWindow && Input.GetMouseButtonUp(0))
            WindowState(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WindowState(true);
            audio.Play();
        }
    }

    private void WindowState(bool state)
    {
        dialogWindow.SetActive(state);
        Time.timeScale = (state) ? 0.05f : 1;
        inWindow = state;

        if (!state)
            this.gameObject.SetActive(false);
    }
}
