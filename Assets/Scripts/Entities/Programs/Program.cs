using UnityEngine;

public abstract class Program : MonoBehaviour
{
    [HideInInspector] protected Computer hardware;

    protected void Start()
    {
        hardware = this.GetComponent<Computer>();
    }

    public abstract void ProgramState(bool state);
}
