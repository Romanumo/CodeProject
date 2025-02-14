﻿using System;
using System.CodeDom.Compiler;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class Compiler : Program
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TextMeshProUGUI lineText;
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private SyntaxTheme syntaxTheme;
    private List<PracticeEntity> entities;

    public PracticeEntity chosenEntity { get; private set; }
    public Type entityType { get; private set; }

    private static Compiler _instance;
    public static Compiler instance { get => _instance; }

    public void Awake()
    {
        if (instance == null)
            _instance = this;

        entities = new List<PracticeEntity>();
    }

    private void Update()
    {
        //input.text = SyntaxHighlighter.HighlightCode(input.text, syntaxTheme);
    }

    public override void ProgramState(bool state)
    {
        foreach (PracticeEntity entity in entities)
        {
            entity.outline.enabled = state;
        }
    }

    public void ChangeChosenEntity(PracticeEntity entity)
    {
        entityType = entity.GetType();
        entity.outline.OutlineColor = Color.yellow;
        chosenEntity = entity;
    }

    public void Subscribe(PracticeEntity entity) => entities.Add(entity);

    public void Unsubscribe(PracticeEntity entity) => entities.Remove(entity);

    #region CoreCompiler
    public void Run()
    {
        string userCode = input.text;
        string code = @"
        using UnityEngine;

        public class Test
        {
            public static void Foo()
            {
                " + entityType.ToString() + " entity = Compiler.instance.chosenEntity as " + entityType.ToString() + @";" + userCode + @"
            }
        }";

        var assembly = Compile(code, debugText);
        var method = assembly.GetType("Test").GetMethod("Foo");
        var del = (Action)Delegate.CreateDelegate(typeof(Action), method);
        del.Invoke();
    }

    public static Assembly Compile(string source, TextMeshProUGUI debugText)
    {
        var options = new CompilerParameters();
        options.GenerateExecutable = false;
        options.GenerateInMemory = true;

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (!assembly.IsDynamic)
            {
                options.ReferencedAssemblies.Add(assembly.Location);
            }
        }

        var compiler = new CSharpCompiler.CodeCompiler();
        var result = compiler.CompileAssemblyFromSource(options, source);

        debugText.text = "";
        foreach (CompilerError err in result.Errors)
        {
            if (err.Line == 0)
                break;
            debugText.text += "On Line: " + (err.Line - 7) + ", Error: " + err.ErrorText + "\n";
        }

        return result.CompiledAssembly;
    } 
    #endregion

    #region UI
    public void EditorUpdate()
    {
        lineText.text = "";
        for (int i = 0; i < input.text.Split('\n').Length; i++)
        {
            lineText.text += i + 1 + "|\n";
        }
    }
    #endregion
}