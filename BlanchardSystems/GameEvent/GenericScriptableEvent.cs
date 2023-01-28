using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BlanchardSystems.GameEvent
{
    public class GenericScriptableEvent<T> : ScriptableObject
    {
        [SerializeField] private UnityEvent<T> _event;
        [SerializeField] private bool _debugOnTrigger;
        [SerializeField] private string _debugMessage;

        public void Register(UnityAction<T> method)
        {
            _event.AddListener(method);
        }

        public void Trigger(T parameter)
        {
            _event.Invoke(parameter);
            if (_debugOnTrigger) Debug.Log($"DebugEvent: {_debugMessage}, parameter: {parameter.ToString()}");
        }

        public void Unregister(UnityAction<T> method)
        {
            _event.RemoveListener(method);
        }

        private void Awake()
        {
            _event = new UnityEvent<T>();
        }
    }
}