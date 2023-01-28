using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.Path.BezierCurve
{
    [ExecuteInEditMode]
    public class BezierLineRendererController : MonoBehaviour
    {
        [SerializeField] private bool _enableDebug = false;

        [SerializeField] private int _qtyVertex;
        [SerializeField] private LineRenderer _lineRenderer;

        [Header("Control Point Parameter")] [SerializeField]
        private Transform[] _controlPoints;

        private Vector2[] _controlPositions;
        [SerializeField] private bool _pickFromGizmos;
        [SerializeField] private BezierRouteGizmos _bezierRouteGizmos;

        private void Start()
        {
            if (_pickFromGizmos)
                _controlPoints = _bezierRouteGizmos.ControlPoints;
            _controlPositions = new Vector2[_controlPoints.Length];
            for (int i = 0; i < _controlPoints.Length; i++)
            {
                _controlPositions[i] = _controlPoints[i].position;
            }
        }

        private void Update()
        {
            if (!_enableDebug) return;

            for (int i = 0; i < _controlPoints.Length; i++)
            {
                _controlPositions[i] = _controlPoints[i].position;
            }

            _lineRenderer.positionCount = _qtyVertex;
            for (int i = 0; i < _qtyVertex; i++)
            {
                _lineRenderer.SetPosition(i, GetPosition(i / (_qtyVertex - 1f)));
            }
        }

        private Vector2 GetPosition(float tParam)
        {
            return Mathf.Pow(1 - tParam, 3) * _controlPositions[0]
                   + 3 * Mathf.Pow(1 - tParam, 2) * tParam * _controlPositions[1]
                   + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * _controlPositions[2]
                   + Mathf.Pow(tParam, 3) * _controlPositions[3];
        }
    }
}