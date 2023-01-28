#if DG 
    #define Tween
    using DG.Tweening;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlanchardSystems.Animations
{
    public class AnimationOnEnable : MonoBehaviour
    {
#if Tween
    [Header("Set Position")]
    [SerializeField] private bool setXposition;
    [SerializeField] private bool setYposition;
    [SerializeField] private Vector2 startPosition;
    private float finalXPosition;
    private float finalYPosition;
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

    private bool haveAnimator;
    private bool alreadyShaked;
    private void Awake()
    {
        GetPositions();
    }
    private void OnEnable()
    {
        InitializeFade();
        DesactiveAnimator();
        
        StartCoroutine(SetPosition());
        StartCoroutine(DoFade());
    }

    private void InitializeFade()
    {
        if (doFade)
        {
            InitializeCanvasGroup();
            canvasGroup.DOFade(0f, 0f);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
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

    private void GetPositions()
    {
        finalXPosition = transform.localPosition.x;
        finalYPosition = transform.localPosition.y;
    }

    private IEnumerator SetPosition()
    {

        if(setXposition || setYposition)
        {
            if (setXposition)
            {
                var localposition = transform.localPosition;
                transform.localPosition = new Vector3(startPosition.x, localposition.y, localposition.z);
            }
            if (setYposition)
            {
                var localposition = transform.localPosition;
                transform.localPosition = new Vector3(localposition.x, startPosition.y, localposition.z);
            }
            yield return new WaitForSeconds(spWaitDuration);

            if(setXposition) transform.DOLocalMoveX(finalXPosition, setDuration).SetEase(ease);
            if(setYposition) transform.DOLocalMoveY(finalYPosition, setDuration).SetEase(ease);

            yield return new WaitForSeconds(setDuration);
        }

        yield return new WaitForSeconds(shakeWaitDuration);
        yield return ShakeCoroutine();

        if (haveAnimator)
        {
            ActiveAnimator();
        }

    }


    private IEnumerator ShakeCoroutine()
    {
        if (doShake)
        {
            if (!alreadyShaked)
            {
                alreadyShaked = true;

                transform.DOScaleX(shakeScale1.x, shakeDuration / 3);
                transform.DOScaleY(shakeScale1.y, shakeDuration / 3);

                yield return new WaitForSeconds(shakeDuration / 3);

                transform.DOScaleX(shakeScale2.x, shakeDuration / 3);
                transform.DOScaleY(shakeScale2.y, shakeDuration / 3);

                yield return new WaitForSeconds(shakeDuration / 3);

                transform.DOScaleX(1f, shakeDuration / 3);
                transform.DOScaleY(1f, shakeDuration / 3);

                yield return new WaitForSeconds(shakeDuration / 3);
            }

        }
    }

    private IEnumerator DoFade()
    {
        if (doFade)
        {
            yield return new WaitForSeconds(fadeWaitDuration);
            canvasGroup.DOFade(1f, fadeDuration);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
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