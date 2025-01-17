using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> onTrigger;
    bool isTriggered = false;

    void Update()
    {
        if (isTriggered && Input.GetMouseButtonUp(0))
            WindowState(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WindowState(true);
        }
    }

    private void WindowState(bool state)
    {
        onTrigger?.Invoke(state);
        Time.timeScale = (state) ? 0.05f : 1;
        isTriggered = state;

        if (!state)
            Destroy(this);      
    }
}
