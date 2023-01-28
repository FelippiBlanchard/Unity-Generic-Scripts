using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.ScriptableVariable
{
    [CreateAssetMenu(fileName = "String", menuName = "BlanchardSystems/ScriptableVariable/String", order = 1)]
    public class ScriptableVariableString : ScriptableObject
    {
        public string Value;

        public void SetValue(string value)
        {
            this.Value = value;
        }
    }
}