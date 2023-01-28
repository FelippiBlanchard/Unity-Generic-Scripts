using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BlanchardSystems.GameEvent
{
    [CreateAssetMenu(fileName = "NewVoidEvent", menuName = "BlanchardSystems/GameEvent/ScriptableEventVoid")]
    public class ScriptableEventVoid : ScriptableObject
    {
        [SerializeField] private UnityEvent _event;
        [SerializeField] private bool _debugOnTrigger;
        [SerializeField] private string _debugMessage;

        public void Register(UnityAction method)
        {
            _event.AddListener(method);
        }

        public void Trigger()
        {
            _event.Invoke();
            if (_debugOnTrigger) Debug.Log($"DebugEvent: {_debugMessage}");
        }

        public void Unregister(UnityAction method)
        {
            _event.RemoveListener(method);
        }

        private void Awake()
        {
            _event = new UnityEvent();
        }
    }
}