using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField] private Camera CompilerCam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = CompilerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                PracticeEntity entity;
                if (hit.collider.gameObject.TryGetComponent(out entity))
                {
                    VariablesManager.instance.ChangeChosenEntity(entity);
                }
            }
        }
    }
}
