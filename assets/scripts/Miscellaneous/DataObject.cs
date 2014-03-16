using assets.scripts.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace assets.scripts.Miscellaneous
{
    public abstract class DataObject : MonoBehaviour
    {
        public DataObject baseObject;

        [SerializeField]
        private DataObject actualBaseObject;

        [SerializeField]
        private List<string> inheritValues;

        public bool HasBaseObject { get { return actualBaseObject != null; } }

        public DataObject BaseObject
        {
            get { return actualBaseObject; }
            set
            {
                if (IsValidNewBaseObject(value))
                {
                    HandleValidBaseObject(value);
                }
                else
                {
                    HandleInvalidBaseObject(value);
                }
            }
        }

        private void HandleInvalidBaseObject(DataObject newValue)
        {
            // set the base object to the old value
            baseObject = actualBaseObject;
            throw new ArgumentException(newValue.GetType().FullName + " is not assignable from " + this.GetType().FullName);
        }

        private void HandleValidBaseObject(DataObject newValue)
        {
            if (actualBaseObject != newValue)
            {
                actualBaseObject = newValue;
                HandleNewBaseObject();
            }
        }

        private void HandleNewBaseObject()
        {
            if (actualBaseObject != null)
            {
                SetAllValuesToInherited();
            }
            else
            {
                inheritValues.Clear();
            }
        }

        private void SetAllValuesToInherited()
        {
            foreach (FieldInfo field in actualBaseObject.GetType().GetFields())
            {
                if (field.Name != "baseObject")
                {
                    AddInheritValueIfNotExists(field.Name);
                }
            }
        } 

        private bool IsValidNewBaseObject(DataObject newBaseObject)
        {
            return newBaseObject == null || newBaseObject.GetType().IsAssignableFrom(this.GetType());
        }

        public void CommitNewBaseObject()
        {
            BaseObject = baseObject;
        }        

        public bool HasField(string fieldName)
        {
            return GetType().GetField(fieldName) != null;
        }

        public bool InheritsValue(string valueName)
        {
            return inheritValues.Contains(valueName);
        }

        public void SetInheritsValue(string valueName, bool inherit)
        {
            if (inherit)
            {
                AddInheritValueIfNotExists(valueName);
            }

            else
            {
                RemoveInheritValueIfExists(valueName);
            }
        }

        private void AddInheritValueIfNotExists(string valueName)
        {
            if (!inheritValues.Contains(valueName))
            {
                inheritValues.Add(valueName);
            }
        }

        private void RemoveInheritValueIfExists(string valueName)
        {
            if (inheritValues.Contains(valueName))
            {
                inheritValues.Remove(valueName);
            }
        }

        [MenuItem("Assets/Create/DataObjectPrefab")]
        public static void CreateGameObjectPrefab()
        {
            GameObject go = new GameObject("dataObject");
            PrefabUtility.CreatePrefab("Assets/DataObjects/dataObject.prefab", go);
            GameObject.DestroyImmediate(go);
        }
    }
}
