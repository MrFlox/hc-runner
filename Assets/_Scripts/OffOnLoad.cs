using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffOnLoad : MonoBehaviour


{

    void Awake()
    {
        foreach (Transform t in transform) t.gameObject.SetActive(false);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
