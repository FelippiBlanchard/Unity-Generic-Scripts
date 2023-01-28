#if DG 
    #define Tween
    using DG.Tweening;
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlanchardSystems.Animations
{
    public class AnimationOnClose : MonoBehaviour
    {

#if Tween
    [Header("Set Position")]
    [SerializeField] private bool setXposition;
    [SerializeField] private bool setYposition;
    [SerializeField] private Vector2 finalPosition;
    [SerializeField] private float setDuration;
    [SerializeField] private float spWaitDuration;

    [Header("Ease Type")]
    [SerializeField] private Ease ease = Ease.InOutSine;

    [Header("Bounce")]
    [Tooltip("Shake animation do always after Set Position")]
    [SerializeField] private bool doShake;
    [SerializeField] private float shakeDuration;
    [Tooltip("Shake animation do always after Set Position")]
    [SerializeField] private float shakeWaitDuration;
    [SerializeField] private Vector2 shakeScale1;
    [SerializeField] private Vector2 shakeScale2;

    [Header("Fade")]
    [SerializeField] private bool doFade;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeWaitDuration;
    private CanvasGroup canvasGroup;

    [Header("Desactive Game Object")]
    [SerializeField] private bool desactiveGameObject;
    [SerializeField] private float desactiveWaitDuration;


    private bool haveAnimator;

    public void Close()
    {
        InitializeFade();
        DesactiveAnimator();

        StartCoroutine(SetPosition());
        StartCoroutine(DoFade());
        StartCoroutine(DesactiveGameObject());
    }

    private void InitializeFade()
    {
        if (doFade)
        {
            InitializeCanvasGroup();
            canvasGroup.DOFade(1f, 0f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    private void InitializeCanvasGroup()
    {
        if (TryGetComponent<CanvasGroup>(out CanvasGroup cg))
        {
            canvasGroup = cg;
        }
        else
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private IEnumerator SetPosition()
    {

        if (setXposition || setYposition)
        {
            yield return new WaitForSeconds(spWaitDuration);

            if (setXposition) transform.DOLocalMoveX(finalPosition.x, setDuration).SetEase(ease);
            if (setYposition) transform.DOLocalMoveY(finalPosition.y, setDuration).SetEase(ease);

            yield return new WaitForSeconds(setDuration);
        }

        if (haveAnimator)
        {
            ActiveAnimator();
        }

    }

    private IEnumerator DoFade()
    {
        if (doFade)
        {
            yield return new WaitForSeconds(fadeWaitDuration);
            canvasGroup.DOFade(0f, fadeDuration);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }


    private IEnumerator DesactiveGameObject()
    {
        if (desactiveGameObject)
        {
            yield return new WaitForSeconds(desactiveWaitDuration);
            gameObject.SetActive(false);
        }
    }


    private void DesactiveAnimator()
    {
        if (TryGetComponent<Animator>(out Animator a))
        {
            haveAnimator = true;
            GetComponent<Animator>().enabled = false;
        }
    }

    private void ActiveAnimator()
    {
        GetComponent<Animator>().enabled = true;
    }

#else
        private void Awake()
        {
            Debug.Log("DG Tween library need to be imported and defined");
        }

#endif
    }
}