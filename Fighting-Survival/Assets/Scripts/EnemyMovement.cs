﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Collider EnemyRightHand;
    public Collider EnemyLiftHand;
    public Collider EnemyRightFoot;
    public Collider EnemyLiftFoot;

    private Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private bool CanAnimate;
    private int AttackMove;
    private bool GameOver;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetBool("IsMoving", true);
        CanAnimate = true;
        GameOver = false;
    }

    void FixedUpdate()
    {
        if (GameOver) return;
        if (GameManager.Instance.IsPlayerDead())
        {
            anim.SetTrigger("Winning");
            GameOver = true;
            return;
        }
        if(nav.enabled)
        {
            nav.SetDestination(player.position);
            DoAnimations();
        }
            
    }

    void AnimationStarted()
    {
        CanAnimate = false;
        EnableColliders(true);
    }

    void AnimationFinished()
    {
        CanAnimate = true;
        EnableColliders(false);
    }

    void EnableColliders(bool status)
    {
        EnemyRightHand.enabled = status;
        EnemyLiftHand.enabled = status;
        EnemyRightFoot.enabled = status;
        EnemyLiftFoot.enabled = status;
    }

    void DesableNav()
    {
        nav.enabled = false;
    }

    void EnableNav()
    {
        nav.enabled = true;
    }

    void DoAnimations()
    {
        anim.SetBool("IsMoving", IsMoving());
        if (!IsMoving())
        {
            if (CanAnimate)
            {
                AttackMove = Random.Range(1, 10);
                ResetAllTriggers();
                switch (AttackMove)
                {
                    case (1):
                        anim.SetTrigger("bk_push_left_A");
                        break;
                    case (2):
                        anim.SetTrigger("bk_rh_right_A");
                        break;
                    case (3):
                        anim.SetTrigger("bp_hook_right_A");
                        break;
                    case (4):
                        anim.SetTrigger("bp_upper_left_A");
                        break;
                    case (5):
                        anim.SetTrigger("hk_rh_right_A");
                        break;
                    case (6):
                        anim.SetTrigger("hk_side_left_A");
                        break;
                    case (7):
                        anim.SetTrigger("ho_hook_left_A");
                        break;
                    case (8):
                        anim.SetTrigger("hp_straight_A");
                        break;
                    case (9):
                        anim.SetTrigger("hp_straight_right_A");
                        break;
                    case (10):
                        anim.SetTrigger("hp_upper_right_A");
                        break;
                }
            }
        }
    }

    void ResetAllTriggers()
    {
        anim.ResetTrigger("bk_push_left_A");
        anim.ResetTrigger("bk_rh_right_A");
        anim.ResetTrigger("bp_hook_right_A");
        anim.ResetTrigger("bp_upper_left_A");
        anim.ResetTrigger("hk_rh_right_A");
        anim.ResetTrigger("hk_side_left_A");
        anim.ResetTrigger("ho_hook_left_A");
        anim.ResetTrigger("hp_straight_A");
        anim.ResetTrigger("hp_straight_right_A");
        anim.ResetTrigger("hp_upper_right_A");
    }

    bool IsMoving()
    {
        if (!nav.pathPending)
        {
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
