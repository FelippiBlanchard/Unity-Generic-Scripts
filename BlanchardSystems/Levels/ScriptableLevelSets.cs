using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.Levels
{
    [CreateAssetMenu(fileName = "NewLevelSets", menuName = "BlanchardSystems/LevelSystem/ScriptableLevelSets")]
    public class ScriptableLevelSets : ScriptableObject
    {
        public List<LevelSet> levelSets;

        [Serializable]
        public class LevelSet
        {
            public int setID;

            [Tooltip("Short description only for the editor")] [SerializeField]
            private string description;

            public List<ScriptableLevel> levels;
        }
    }
}