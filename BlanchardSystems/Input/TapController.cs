using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BlanchardSystems.Input
{
    public class TapController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector3> _onDrag;
        [SerializeField] private UnityEvent<Vector3> _onUp;
        private void OnMouseDrag()
        {
            var inputWorldPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            _onDrag.Invoke(inputWorldPosition);
        }

        private void OnMouseUp()
        {
            var inputWorldPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            _onUp.Invoke(inputWorldPosition);
        }
    }
}
