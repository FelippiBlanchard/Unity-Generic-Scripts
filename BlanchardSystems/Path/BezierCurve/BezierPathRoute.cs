using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BlanchardSystems.Path.BezierCurve
{
    public class BezierPathRoute : MonoBehaviour
    {
        [FormerlySerializedAs("_routes")] [SerializeField]
        public VariableSpeedRoute[] Routes;

        [Serializable]
        public class VariableSpeedRoute
        {
            public Transform RouteTransform;
            public float Speed;
        }
    }
}