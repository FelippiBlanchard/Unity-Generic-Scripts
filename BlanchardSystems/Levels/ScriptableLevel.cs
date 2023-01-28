using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.Levels
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "BlanchardSystems/LevelSystem/ScriptableLevel")]
    public class ScriptableLevel : ScriptableObject
    {
        [Tooltip("Short description only for the editor")] [SerializeField]
        private string description;

        [Space]
        public GameObject levelPrefab;
        public GameObject canvasLevelPrefab;
        public GameObject soundLevelPrefab;

    }
}