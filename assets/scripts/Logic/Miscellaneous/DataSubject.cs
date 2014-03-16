using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace assets.scripts.Miscellaneous
{
    public abstract class DataSubject<T> : MonoBehaviour where T : DataObject
    {
        public T data;
    }
}
