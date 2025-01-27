using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VariablesManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;

    private static VariablesManager _instance;
    public static VariablesManager instance { get => _instance; }

    private void Awake()
    {
        if (instance == null)
            _instance = this;
    }

    public void ShowInfo(PracticeEntity entity)
    {
        infoText.text = "";
        infoText.text += entity.info.name + " (" + entity.info.type + ")" + "\n";
        infoText.text += "Type 'entity.' and use methods" + "\n";
        infoText.text += "----------Methods-------------\n";
        foreach (string method in entity.info.methods)
        {
            infoText.text += method + "\n";
        }
        infoText.text += "----------Variables----------\n";
        foreach (string variable in entity.info.variables)
        {
            infoText.text += variable + "\n";
        }
        infoText.text += "\n";
    }

    public void ChangeChosenEntity(PracticeEntity entity)
    {
        Compiler.instance.ChangeChosenEntity(entity);
        ShowInfo(entity);
    }
}