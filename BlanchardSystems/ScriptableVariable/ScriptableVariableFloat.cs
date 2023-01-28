using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.ScriptableVariable
{
    [CreateAssetMenu(fileName = "Float", menuName = "BlanchardSystems/ScriptableVariable/Float", order = 1)]
    public class ScriptableVariableFloat : ScriptableObject
    {
        public float Value;
        
        public void AddValue(float value = 1)
        {
            this.Value += value;
        }

        public void SetValue(float value = 0)
        {
            this.Value = value;
        }
        
    }
}