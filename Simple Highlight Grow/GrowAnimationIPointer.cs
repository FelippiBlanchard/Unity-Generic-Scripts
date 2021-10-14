using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class GrowAnimationIPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float timeAnimation;
    [Range(1f,2.5f)]
    [SerializeField] private float maxSize;
    [SerializeField] private Mode currentMode;
    [SerializeField] private State currentState;
    [SerializeField] private bool isMinSize1f;

    private Vector3 initialScale;
    private Coroutine coroutine;
    private void OnEnable()
    {
        SetInitialScale();
    }
    private void SetInitialScale()
    {
        if (isMinSize1f)
        {
            initialScale = new Vector3(1, 1, 1);
        }
        else
        {
            initialScale = transform.localScale;
        }
    }

    private void OnMouseEnter()
    {
        if (!isUI())
        {
            if (Enabled())
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = StartCoroutine(CoroutineAnimateUp());
            }
        }
    }
    private void OnMouseExit()
    {
        if (!isUI())
        {
            if (Enabled())
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = StartCoroutine(CoroutineAnimateDown());
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isUI())
        {
            if (Enabled())
            {
                AnimateUp();
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isUI())
        {
            if (Enabled())
            {
                AnimateDown();
            }
        }
    }
    public void AnimateUp()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(CoroutineAnimateUp());
    }
    public void AnimateDown()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(CoroutineAnimateDown());
    }

    private IEnumerator CoroutineAnimateUp()
    {
        transform.DOScale(maxSize*initialScale, timeAnimation);
        yield return new WaitForSeconds(timeAnimation);
        coroutine = null;
    }
    private IEnumerator CoroutineAnimateDown()
    {
        transform.DOScale(initialScale, timeAnimation);
        yield return new WaitForSeconds(timeAnimation);
        coroutine = null;
    }

    private bool Enabled()
    {
        if(currentState == State.ENABLED)
        {
            return true;
        }
        return false;
    }
    public void ActiveGrower()
    {
        currentState = State.ENABLED;
    }
    public void DesactiveGrower()
    {
        currentState = State.DISABLED;
    }
    private enum State
    {
        ENABLED, DISABLED
    }

    private bool isUI()
    {
        if(Mode.UI== currentMode)
        {
            return true;
        }
        return false;
    }
    private enum Mode
    {
        UI, World
    }
}
