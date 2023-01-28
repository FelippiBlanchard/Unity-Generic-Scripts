using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.GameEvent
{
    [CreateAssetMenu(fileName = "NewVector3Event", menuName = "BlanchardSystems/GameEvent/ScriptableEventVector3")]
    public class ScriptableEventVector3 : GenericScriptableEvent<Vector3>
    {
    }
}