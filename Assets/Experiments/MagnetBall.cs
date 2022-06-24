using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MagnetBall : MonoBehaviour
{
    public int counter;
    [SerializeField] float forcePower = 10.0f;
    public Transform target;
    // ConstantForce force;
    Rigidbody body;
    public Vector3 initPos;

    internal void startNumerate(List<GameObject> list)
    {
        // throw new NotImplementedException();
    }

    // void Awake() => body = GetComponent<Rigidbody>();
    // void Awake() => force = GetComponent<ConstantForce>();

    void OnValidate()
    {

    }

    // void OnDrawGizmos()
    // {
    //     Handles.Label(transform.position, counter.ToString());
    //     if (counter == 0)
    //         GetComponent<MeshRenderer>().enabled = false;
    //     else GetComponent<MeshRenderer>().enabled = true;

    //     Gizmos.color = Color.cyan;
    //     Gizmos.DrawSphere(transform.position, .3f);
    // }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // return;
        // if (!target) return;
        // var dir = target.position - transform.position;
        // force.force = dir * forcePower;

        // body.velocity = dir;



    }
}
