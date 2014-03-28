using System;
using UnityEngine;

namespace Industree.Input.Internal
{
    internal class UnityAxis : MonoBehaviour, IAxis
    {
        public string name;

        private float value = 0f;

        public float Value
        {
            get { return value; }
            private set
            {
                if (this.value != value)
                {
                    float oldValue = this.value;
                    this.value = value;
                    Change(oldValue, this.value);
                }
            }
        }

        public event Action<float, float> Change = (oldValue, newValue) => { };

        private void Update()
        {
            Value = UnityEngine.Input.GetAxisRaw(name);
        }
    }
}
