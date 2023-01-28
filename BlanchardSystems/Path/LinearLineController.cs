using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.Path
{
    [ExecuteInEditMode]
    public class LinearLineController : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        private Transform[] _points;

        private void Update()
        {
            _points = gameObject.GetComponentsInChildren<Transform>();
            var length = _points.Length - 1;
            var positions = new Vector3[length];
            for (int i = 0; i < length; i++)
            {
                positions[i] = _points[i + 1].position;

            }

            _line.positionCount = length;
            _line.SetPositions(positions);

        }
    }
}