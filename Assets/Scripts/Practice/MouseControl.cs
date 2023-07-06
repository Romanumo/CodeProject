using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField] private Camera CompilerCam;
    [SerializeField] private Color onAbove;
    [SerializeField] private Color onSelected;
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
                Outline(entity, onAbove);
                if (Input.GetMouseButtonDown(0))
                {
                    VariablesManager.instance.ChangeChosenEntity(entity);
                    Outline(entity, onSelected);
                }
            }
        }
    }

    private void Outline(PracticeEntity entity, Color color)
    {
        if(previousEntity != null &&  previousEntity != entity)
            previousEntity.outline.OutlineColor = Color.black;

        if (color == onAbove && entity.outline.OutlineColor == onSelected)
            return;

        entity.outline.OutlineColor = color;
        previousEntity = entity;
    }
}
