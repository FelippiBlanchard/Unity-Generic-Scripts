using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.ScriptableVariable
{
    [CreateAssetMenu(fileName = "Int", menuName = "BlanchardSystems/ScriptableVariable/Int", order = 1)]
    public class ScriptableVariableInt : ScriptableObject
    {
        public int Value;

        public void AddValue(int value = 1)
        {
            this.Value += value;
        }

        public void SetValue(int value = 0)
        {
            this.Value = value;
        }
    }
}