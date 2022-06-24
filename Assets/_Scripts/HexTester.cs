using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTester : MonoBehaviour
{
    public float testRadius = 1;
    Vector3[] positions;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        positions = createGrid(4, 4, radius: testRadius);
    }

    private void OnValidate()
    {
        positions = createGrid(4, 4, radius: testRadius);
    }

    private void OnDrawGizmos()
    {
        if (positions.Length > 0)
            foreach (Vector3 pos in positions)
                Gizmos.DrawSphere(pos, testRadius);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3[] createGrid(int width, int height, float radius)
    {

        List<Vector3> listResult = new List<Vector3>();
        float tileXOffset = 2;
        float tileZOffset = 1.8f;

        for (int x = 0; x <= width; x++)
        {
            for (int z = 0; z <= height; z++)
            {

                Vector3 newPos = new Vector3();
                if (z % 2 == 0)
                {
                    newPos = new Vector3(x * tileXOffset * radius, 0, z * tileZOffset * radius);
                }
                else
                {
                    newPos = new Vector3((x * tileXOffset + tileXOffset / 2) * radius, 0, (z * tileZOffset * radius));
                }
                listResult.Add(newPos);
            }
        }

        Vector3[] result = new Vector3[listResult.Count];

        for (int i = 0; i < listResult.Count; i++)
            result[i] = listResult[i];

        return result;
    }
}
