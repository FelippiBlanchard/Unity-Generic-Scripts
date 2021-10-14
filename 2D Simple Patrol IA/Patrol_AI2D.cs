using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Patrol_AI2D : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int angleRange;
    [SerializeField] private rayCastSettings raycastSettings;
     
    public PatrolMode currentMode;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if(currentMode == PatrolMode.ENABLED)
        {
            if(rb.velocity == new Vector2(0, 0))
            {
                StartRandomDirection();
            }
                VerifyRayCasts();
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }   

    public void DisablePatrol()
    {
        SetMode(PatrolMode.DISABLED);
    }
    public void EnablePatrol()
    {
        SetMode(PatrolMode.ENABLED);
    }

    public void VerifyRayCasts()
    {
        Debug.DrawRay(raycastSettings.upRayCast.transform.position, Vector2.up * raycastSettings.maxDistance, Color.white);
        Debug.DrawRay(raycastSettings.downRayCast.transform.position, Vector2.down * raycastSettings.maxDistance, Color.red);
        Debug.DrawRay(raycastSettings.rightRayCast.transform.position, Vector2.right * raycastSettings.maxDistance, Color.gray);
        Debug.DrawRay(raycastSettings.leftRayCast.transform.position, Vector2.left * raycastSettings.maxDistance, Color.blue);

        int layerMask = LayerMask.GetMask(raycastSettings.layerMask);

        if (Physics2D.Raycast(raycastSettings.upRayCast.transform.position, Vector2.up, raycastSettings.maxDistance, layerMask))
        {
            ChangeDirection(Direction.down);
        }
        if (Physics2D.Raycast(raycastSettings.downRayCast.transform.position, Vector2.down, raycastSettings.maxDistance, layerMask))
        {
            ChangeDirection(Direction.up);
        }
        if (Physics2D.Raycast(raycastSettings.rightRayCast.transform.position, Vector2.right, raycastSettings.maxDistance, layerMask))
        {
            ChangeDirection(Direction.left);
        }
        if (Physics2D.Raycast(raycastSettings.leftRayCast.transform.position, Vector2.left, raycastSettings.maxDistance, layerMask))
        {
            ChangeDirection(Direction.right);
        }
    }

    private void StartRandomDirection()
    {
        Direction direction = (Direction)Random.Range(0, 360);
        ChangeDirection(direction);
    }
    private void ChangeDirection(Direction direction)
    {
        int angle = Random.Range((int)direction - angleRange, (int)direction + angleRange);
        float angleRad = angle * Mathf.Deg2Rad;
        rb.velocity = new Vector2(speed * Mathf.Cos(angleRad), speed * Mathf.Sin(angleRad));
    }
    public enum Direction
    {
        up = 90,
        down = 270,
        left = 180,
        right = 0,
    }

    private void SetMode(PatrolMode mode)
    {
        currentMode = mode;

        switch (currentMode)
        {
            case PatrolMode.ENABLED:
                StartRandomDirection();
                return;
            case PatrolMode.DISABLED:
                rb.velocity = new Vector2(0, 0);
                return;
        }
    }
    public enum PatrolMode
    {
        ENABLED, DISABLED
    }
}

[System.Serializable]
public class rayCastSettings
{
    public string layerMask;
    public float maxDistance;

    public GameObject upRayCast;
    public GameObject downRayCast;
    public GameObject leftRayCast;
    public GameObject rightRayCast;

}
