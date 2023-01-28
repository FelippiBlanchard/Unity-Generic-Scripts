using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.ScriptableVariable
{
    [CreateAssetMenu(fileName = "Bool", menuName = "BlanchardSystems/ScriptableVariable/Bool", order = 1)]
    public class ScriptableVariableBool : ScriptableObject
    {
        public bool Value;
        
        public void SetValue(bool value)
        {
            this.Value = value;
        }

        public void InvertValue()
        {
            this.Value = !Value;
        }

    }
}