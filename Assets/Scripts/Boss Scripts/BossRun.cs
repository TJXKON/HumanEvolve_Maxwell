using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
    public float speed = 3f;
    public float attackRange = 3f;

    private Transform player;
    Rigidbody rb;
    BossDirection boss;
    BossJump jump;
    BossProjectile projectile;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        boss = animator.GetComponent<BossDirection>();
        jump = animator.GetComponent<BossJump>();
        projectile = animator.GetComponent<BossProjectile>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.WatchPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) > 15f)
        {
            animator.SetTrigger("Jump");
            jump.JumpAttack();

        }
        else if (Vector2.Distance(player.position, rb.position) <= 15f && Vector2.Distance(player.position, rb.position) > 8f)
        {
            if(projectile.isCooldown == false)
            {
                animator.SetTrigger("Projectile");
                projectile.ProjectileAttack();

            }
        }
        else if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Projectile");
    }
}
