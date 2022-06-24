using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    public Vector3 target;
    [SerializeField] private Animator animator;
    public Vector3 direction;
    private float speed;

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawSphere(target, 1.0f);
    }

    private void Update()
    {
        if (!RunnerInputController.instance.touch)
        {
            animator.SetFloat("MoveSpeed", 0);

            return;
        }
        else
        {
            if (animator.GetFloat("MoveSpeed") == 0)
            {
                animator.SetFloat("MoveSpeed", 1);
                animator.SetFloat("Offset", UnityEngine.Random.Range(0.0f, 1.0f));
            }
        }
    }

    internal void setSpeed(float runSpeed) => this.speed = runSpeed;
}
