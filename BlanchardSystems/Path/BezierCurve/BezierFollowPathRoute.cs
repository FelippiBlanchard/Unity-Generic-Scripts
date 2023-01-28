using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlanchardSystems.Path.BezierCurve
{
 public class BezierFollowPathRoute : MonoBehaviour
{
    [SerializeField] private BezierPathRoute _bezierPathRoute;

    private int _currentRoute;
    private float _tParam;
    private Vector2 _targetPosition;
    private bool _enabled;
    private bool _stopOnArrive;

    private Coroutine _currentCoroutine;

    public void Initialize(BezierPathRoute pathRoutes, float customTParam = 0, int customRoute = 0)
    {
        _bezierPathRoute = pathRoutes;

        _tParam = customTParam;
        _currentRoute = customRoute;
        
        SetPosition(GetControlPointsFromCurrentRoute());
    }

    public void StartMove()
    {
        _currentCoroutine = StartCoroutine(CO_StartMove());
    }
    private IEnumerator CO_StartMove()
    {
        _enabled = true;
        while (_enabled)
        {
            var p = GetControlPointsFromCurrentRoute();
            while (_tParam < 1 && _enabled)
            {
                SetPosition(p);
                yield return new WaitForEndOfFrame();
            }

            _tParam = 0f;
            _currentRoute++;
            if (_currentRoute >= _bezierPathRoute.Routes.Length) _currentRoute = 0;

            if (_stopOnArrive) _enabled = false;

        }
    }

    private void SetPosition(Vector2[] controlPoints)
    {
        _tParam += Time.deltaTime * _bezierPathRoute.Routes[_currentRoute].Speed * 0.1f;
        _targetPosition = Mathf.Pow(1 - _tParam, 3) * controlPoints[0]
                          + 3 * Mathf.Pow(1 - _tParam, 2) * _tParam * controlPoints[1]
                          + 3 * (1-_tParam) * Mathf.Pow(_tParam, 2) * controlPoints[2]
                          + Mathf.Pow(_tParam, 3) * controlPoints[3];
        transform.position = _targetPosition;
    }

    private Vector2[] GetControlPointsFromCurrentRoute()
    {
        var controlPoints = new Vector2[4];
        for (int i = 0; i < 4; i++)
        {
            controlPoints[i] = _bezierPathRoute.Routes[_currentRoute].RouteTransform.GetChild(i).position;
        }

        return controlPoints;
    }
}   
}
