using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public abstract class PracticeEntity : MonoBehaviour
{
    protected EntityInfo _info;
    public EntityInfo info { get => _info; }

    public Outline outline { get; private set; }

    protected void Start()
    {
        outline = GetComponent<Outline>();
        Compiler.instance?.Subscribe(this);

        InitInfo();
    }

    private void OnDestroy()
    {
        Compiler.instance?.Unsubscribe(this);
    }

    private void InitInfo()
    {
        _info = new EntityInfo(gameObject.name, this.GetType().ToString());
        MethodInfo[] methodInfos = this.GetType().GetMethods();
        foreach (MethodInfo methodInfo in methodInfos)
        {
            if (!methodInfo.IsPublic)
                continue;

            if (methodInfo.ReturnType == _info.GetType())
                break;

            _info.methods.Add(methodInfo.ToString());
        }

        FieldInfo[] fieldInfos = this.GetType().GetFields();
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            _info.variables.Add(fieldInfo.Name);
        }
    }
}

public class EntityInfo
{
    public string name;
    public string type;
    public List<string> variables;
    public List<string> methods;

    public EntityInfo(string name, string type)
    {
        this.name = name;
        this.type = type;
        variables = new List<string>();
        methods = new List<string>();
    }
}