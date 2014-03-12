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

        private DataObject actualBaseObject;

        private Dictionary<string, bool> inheritValue = new Dictionary<string,bool>();

        public bool HasBaseObject { get { return actualBaseObject != null; } }

        public DataObject BaseObject
        {
            get { return actualBaseObject; }
            set
            {
                if (IsValidNewBaseObject(value))
                {
                    if (actualBaseObject != value)
                    {
                        actualBaseObject = value;

                        if (actualBaseObject != null)
                        {
                            UpdateInheritedValues();
                        }
                    }
                }
                else
                {
                    // set the base object to the old value
                    baseObject = actualBaseObject;
                    throw new ArgumentException(value.GetType().FullName + " is not assignable from " + this.GetType().FullName);
                }
            }
        }

        private bool IsValidNewBaseObject(DataObject newBaseObject)
        {
            return newBaseObject == null || newBaseObject.GetType().IsAssignableFrom(this.GetType());
        }

        private void UpdateInheritedValues()
        {
            inheritValue = new Dictionary<string, bool>();
            foreach (FieldInfo field in actualBaseObject.GetType().GetFields())
            {
                if (field.Name != "baseObject")
                {
                    inheritValue[field.Name] = true;
                }
            }
        }

        public void CommitNewBaseObject()
        {
            BaseObject = baseObject;
        }

        public void SetInheritValue(string valueName, bool inheritProperties)
        {
            inheritValue[valueName] = inheritProperties;
        }

        public bool InheritsValue(string valueName)
        {
            if (inheritValue.ContainsKey(valueName))
            {
                return actualBaseObject != null && inheritValue[valueName];
            }

            return false;
        }

        public bool HasField(string fieldName)
        {
            return GetType().GetField(fieldName) != null;
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
