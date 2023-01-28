using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems
{
    public class HoldScriptablesBetweenScenes : MonoBehaviour
    {
        [SerializeField] private List<ScriptableObject> _scriptableObjects;

        private void OnEnable()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
}
