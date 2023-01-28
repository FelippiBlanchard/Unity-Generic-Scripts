using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class DraggerUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IDropHandler
{
    [Header("Drag Interact Only With")]
    [SerializeField] public int receiverIndex;

    [Header("Dragger Screen Space")]
    [SerializeField] private ScreenSpaceMode mode;

    [Header("Dragger Events")]
    [SerializeField] private UnityEvent onPointerDown;
    [SerializeField] private UnityEvent onDraggerOver;
    [SerializeField] private UnityEvent onDraggerExit;
    [SerializeField] private UnityEvent onDrop;
    [SerializeField] private UnityEvent onDropAnywhere;

    private Receiver currentReceiver;
    private DraggerState currentState;
    public static DraggerUI instance;


    protected virtual void FollowCursor()
    {
        Vector3 position;
        switch (mode)
        {
            case ScreenSpaceMode.OVERLAY:
                position = Input.mousePosition;
                position.z = 0f;
                transform.position = position;
                return;
            case ScreenSpaceMode.CAMERA:
                position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0f;
                transform.position = position;
                return;
            case ScreenSpaceMode.WORLD:
                position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0f;
                transform.position = position;
                return;
        }
    }
    public virtual void AutoDestroy()
    {
        Destroy(gameObject);
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        switch (currentState)
        {
            case DraggerState.ENABLED:
                onPointerDown.Invoke();
                instance = this;
                return;
            case DraggerState.DISABLED:
                return;
        }
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        switch (currentState)
        {
            case DraggerState.ENABLED:
                FollowCursor();
                return;
            case DraggerState.DISABLED:
                return;
        }
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        switch (currentState)
        {
            case DraggerState.ENABLED:
                instance = null;
                Drop();
                return;
            case DraggerState.DISABLED:
                return;
        }
    }
    protected virtual bool CanDrop()
    {
        if (currentReceiver != null)
        {
            return true;
        }
        return false;
    }
    protected virtual void Drop()
    {
        if (CanDrop())
        {
            currentReceiver.Drop();
            onDrop.Invoke();
        }
        else
        {
            onDropAnywhere.Invoke();
        }
    }

    public virtual void SelectReceiver(Receiver receiver)
    {
        currentReceiver = receiver;
        onDraggerOver.Invoke();
    }
    public virtual void UnselectReceiver(Receiver receiver)
    {
        if (currentReceiver == receiver)
        {
            currentReceiver = null;
            onDraggerExit.Invoke();
        }
    }
    public virtual void SetComponentOnDrop(Component component)
    {
        currentReceiver?.SetComponent(component);
    }

    public void EnableDragger()
    {
        SetDraggerState(DraggerState.ENABLED);
    }
    public void DisableDragger()
    {
        SetDraggerState(DraggerState.DISABLED);
    }
    protected void SetDraggerState(DraggerState state)
    {
        currentState = state;
    }
    public enum DraggerState { ENABLED, DISABLED}
    public enum ScreenSpaceMode{ OVERLAY, CAMERA, WORLD}
}

