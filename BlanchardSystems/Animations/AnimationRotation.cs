using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.Animations
{
    public class AnimationRotation : MonoBehaviour
    {

        [SerializeField] private bool _active = true;
        [SerializeField] private float _speed;

        private void Update()
        {
            if (_active)
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
        }
    }
}