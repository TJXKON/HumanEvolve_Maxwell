using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJump : MonoBehaviour
{
    public float jumpHeight = 1.2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Transform player;
    Rigidbody rb;
    BossDirection boss;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    public void JumpAttack()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;
        Vector2 jumpDirection = new Vector2(distanceFromPlayer, jumpHeight);

        if(IsGrounded())
            rb.AddForce(jumpDirection, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        Vector3 pos = groundCheck.position + Vector3.up * 0.9f;

        return Physics.CheckSphere(pos, 0.9f, groundLayer);
    }
}
