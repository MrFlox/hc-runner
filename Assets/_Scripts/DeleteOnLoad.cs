using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnLoad : MonoBehaviour
{
    void Awake() => Destroy(gameObject);
}
