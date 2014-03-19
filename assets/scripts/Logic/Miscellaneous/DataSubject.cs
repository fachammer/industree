using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Industree.Miscellaneous
{
    public abstract class DataSubject<TData> : MonoBehaviour where TData : DataObject
    {
        public TData data;
    }
}
