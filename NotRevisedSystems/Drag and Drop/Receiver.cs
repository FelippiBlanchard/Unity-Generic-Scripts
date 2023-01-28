using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider2D))]
public class Receiver : MonoBehaviour
{
    [Header("Receiver Settings")]
    [SerializeField] public int receiverIndex;
    [SerializeField] protected Component componentSpecific;
    public ReceiverState currentState;

    [Header("Receiver Events")]
    [SerializeField] private UnityEvent onDraggerOver;
    [SerializeField] public UnityEvent onDraggerExit;
    [SerializeField] public UnityEvent onDrop;
    [SerializeField] public UnityEvent onGetComponentFromDrop;

    protected GameObject draggerObject;

    private void OnMouseEnter()
    {
        if (currentState == ReceiverState.ENABLED)
        {
            SelectReceiver();
        }

    }
    private void OnMouseExit()
    {
        if (currentState == ReceiverState.ENABLED)
        {
            UnselectReceiver();
        }
    }
    protected void SelectReceiver()
    {
        if (CanInteract())
        {
            DraggerUI.instance.SelectReceiver(this);
            onDraggerOver.Invoke();
        }
    }
    protected void UnselectReceiver()
    {
        if (CanInteract())
        {
            DraggerUI.instance.UnselectReceiver(this);
            onDraggerExit.Invoke();
        }
    }
    protected bool CanInteract()
    {
        return ((DraggerUI.instance != null) && (DraggerUI.instance.receiverIndex == receiverIndex));
    }
    public void Drop()
    {
        onDrop?.Invoke();
    }

    public void SetComponent(Component component)
    {
        componentSpecific = component;
        onGetComponentFromDrop.Invoke();
    }

    public void EnableReceiver()
    {
        SetReceiverState(ReceiverState.ENABLED);
    }
    public void DisableReceiver()
    {
        SetReceiverState(ReceiverState.DISABLED);
    }
    protected void SetReceiverState(ReceiverState state)
    {
        currentState = state;
    }
    public enum ReceiverState
    {
        ENABLED, DISABLED
    }
}