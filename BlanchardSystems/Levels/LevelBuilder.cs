using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlanchardSystems.ScriptableVariable;


namespace BlanchardSystems.Levels
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private bool _wantDebugLevel;
        [SerializeField] private ScriptableLevel _debugLevel;

        [Space]
        [SerializeField] private ScriptableVariableString _currentChosenLevel;

        

        private void OnEnable()
        {
            if (_wantDebugLevel)
            {
                InitializeLevel(_debugLevel);
            }
        }

        private void InitializeLevel(ScriptableLevel level)
        {

        }

        private void SetupLevel()
        {
            
        }
        
        
    }
    
}