using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace assets.scripts.Miscellaneous
{
    public abstract class DataClient<T> : MonoBehaviour where T : MonoBehaviour
    {
        [HideInInspector]
        public T data;
        public bool getDataFromMaster;

        protected virtual void Start()
        {
            if (getDataFromMaster)
            {
                data = GameObject.FindGameObjectWithTag(Tags.dataMaster).GetComponent<T>();
            }
            else
            {
                data = GetComponent<T>();
            }
        }
    }
}
