using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesGenerator : MonoBehaviour
{

    [SerializeField] float zSpaceBetweenLines = 85;
    [SerializeField] int linesCount = 100;
    [SerializeField] GameObject prefab;

    void Start()
    {
        for (int i = 0; i < linesCount; i++)
        {
            GameObject line = Instantiate(prefab);
            line.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + i * zSpaceBetweenLines);
            line.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
