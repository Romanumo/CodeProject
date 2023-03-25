using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField] private Camera CompilerCam;
    private PracticeEntity previousEntity;

    void Update()
    {
        Ray ray = CompilerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            PracticeEntity entity;
            if (hit.collider.gameObject.TryGetComponent(out entity))
            {
                if (Input.GetMouseButtonDown(0))
                    VariablesManager.instance.ChangeChosenEntity(entity);

                Outline(entity);
            }
        }
    }

    private void Outline(PracticeEntity entity)
    {
        if(previousEntity != null &&  previousEntity != entity)
            previousEntity.outline.OutlineColor = Color.black;

        entity.outline.OutlineColor = Color.gray;
        previousEntity = entity;
    }
}
