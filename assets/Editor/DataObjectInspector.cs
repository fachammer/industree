using assets.scripts.Miscellaneous;
using assets.scripts.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataObject), true)]
public class DataObjectInspector : Editor
{
    private const string BASE_OBJECT_NAME = "baseObject";

    public override void OnInspectorGUI()
    {
        DataObject dataObject = target as DataObject;

        serializedObject.Update();

        SetInheritedValues(dataObject);

        foreach(FieldInfo field in target.GetType().GetFields()){
            DrawPropertyGUI(dataObject, field);
        }

        serializedObject.ApplyModifiedProperties();

        CheckInputValidity(dataObject);
    }

    private void CheckInputValidity(DataObject dataObject)
    {
        try
        {
            dataObject.CommitNewBaseObject();
        }
        catch (ArgumentException e)
        {
            Debug.LogException(e);
        }
    }

    private void SetInheritedValues(DataObject dataObject)
    {
        DataObject baseObject = dataObject.BaseObject;

        if (baseObject != null)
        {
            foreach (FieldInfo field in baseObject.GetType().GetFields())
            {
                if (dataObject.InheritsValue(field.Name))
                {
                    object baseObjectValue = baseObject.GetType().GetField(field.Name).GetValue(baseObject);
                    field.SetValue(dataObject, baseObjectValue);
                }
            }
        }
    }

    private void DrawPropertyGUI(DataObject dataObject, FieldInfo field)
    {
        SerializedProperty property = serializedObject.FindProperty(field.Name);

        EditorGUILayout.BeginHorizontal();

        GUI.enabled = IsGUIEnabledForPropertyOfDataObject(dataObject, field);
        EditorGUILayout.PropertyField(property, new GUIContent(field.Name));
        GUI.enabled = true;

        if (field.Name != BASE_OBJECT_NAME && dataObject.HasBaseObject && dataObject.BaseObject.HasField(field.Name))
        {
            dataObject.SetInheritValue(field.Name, GetToggleButtonState(dataObject.InheritsValue(field.Name)));
        }   

        EditorGUILayout.EndHorizontal();
    }

    private bool IsGUIEnabledForPropertyOfDataObject(DataObject dataObject, FieldInfo field)
    {
        return !dataObject.InheritsValue(field.Name) || dataObject.BaseObject == null;
    }

    private bool GetToggleButtonState(bool currentValue)
    {
        return EditorGUILayout.Toggle(new GUIContent("", "inherit from base object"), currentValue, GUILayout.Width(15));
    }
}
