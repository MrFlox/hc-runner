using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysGrouping : MonoBehaviour
{

    [SerializeField] List<GameObject> spheres;
    [SerializeField] GameObject targetSphere;

    void Awake()
    {
        foreach (Transform t in transform)
        {
            GameObject g = t.gameObject;
            if (targetSphere != g)
            {
                spheres.Add(g);
                Rigidbody r = g.AddComponent<Rigidbody>();
                r.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            }
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject g in spheres)
        {
            Vector3 target = targetSphere.transform.position - g.transform.position;
            // g.transform.Translate(target * Time.deltaTime * 100);
            g.GetComponent<Rigidbody>().velocity = target * Time.deltaTime * 100;

        }
    }
}
