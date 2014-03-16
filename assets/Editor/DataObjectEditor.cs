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
public class DataObjectEditor : Editor
{
    private const string BASE_OBJECT_NAME = "baseObject";
    private const string INHERIT_VALUES_NAME = "inheritValues";

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DataObject dataObject = target as DataObject;

        SetInheritedValues(dataObject);

        foreach(FieldInfo field in target.GetType().GetFields()){
            DrawPropertyGUI(dataObject, field);
        }

        serializedObject.ApplyModifiedProperties();

        CheckInputValidity(dataObject);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
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
        EditorGUIUtility.LookLikeControls();

        EditorGUILayout.BeginHorizontal();

        GUI.enabled = IsGUIEnabledForPropertyOfDataObject(dataObject, field);
        
        EditorGUILayout.PropertyField(property);

        GUI.enabled = field.Name != BASE_OBJECT_NAME && dataObject.HasBaseObject && dataObject.BaseObject.HasField(field.Name);
        
        dataObject.SetInheritsValue(field.Name, GetToggleButtonState(dataObject.InheritsValue(field.Name)));

        GUI.enabled = true;
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
