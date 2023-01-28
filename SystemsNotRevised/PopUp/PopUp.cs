using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PopUp : CanvasGroupController
{
    [Header("PopUp Settings")]
    [SerializeField] private float timeShowing;
    [SerializeField] private TMP_Text textBox;

    private static bool showing;
    private static string nextText;
    private Coroutine coroutine;

    public static PopUp instance;

    private void Start()
    {
        instance = this;
    }
    public void Message(string text)
    {
        nextText = text;
        
        if (showing)
        {
            StopCoroutine(coroutine);
        }
        
        coroutine = StartCoroutine(MessageCoroutine());
    }
    public IEnumerator MessageCoroutine()
    {
        if (showing)
        {
            Desactive();
            yield return new WaitForSeconds(timeFadeCanvasGroup);
        }

        textBox.text = nextText;
        Active();
        showing = true;
        yield return new WaitForSeconds(timeFadeCanvasGroup + timeShowing);
        Desactive();
        yield return new WaitForSeconds(timeFadeCanvasGroup);
        showing = false;
    }
}
