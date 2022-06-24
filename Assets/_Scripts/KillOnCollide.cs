using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnCollide : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "runner")
        {
            RunnerController r = other.GetComponent<RunnerController>();
            if (r.gameObject.activeSelf) PeoplesManager.instance.remove(other.gameObject);
        }
    }
}
