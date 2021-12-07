using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPart : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private CircleCollider2D coll;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position, coll.radius, layerMask);
    }
}
