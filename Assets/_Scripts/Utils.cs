using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{

    public static Vector3[] generateRadialPositions(Vector3 point, int rounds, float radiusOfObject = 6.0f)
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(point);
        int counter = 1;
        for (int r = 0; r < rounds; r++)
        {
            float num = radiusOfObject * (r + 1);
            float radius = num;
            for (int i = 0; i < num; i++)
            {
                /* Distance around the circle */
                var radians = 2 * MathF.PI / num * i;

                /* Get the vector direction */
                var vertcial = MathF.Sin(radians);
                var horizontal = MathF.Cos(radians);

                var spawnDir = new Vector3(horizontal, 0, vertcial);

                /* Get the spawn position */
                var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

                /* Now spawn */
                positions.Add(spawnPos);
                counter++;
            }
        }
        return positions.ToArray();
    }

    // xOff = 1, zOff = 1.85
    public static void createGrid_(int width, int height, float tileXOffset, float tileZOffset, GameObject prefab, GameObject targetLayer, Vector3 pos)
    {
        float sphereDiam = 4.0f;// prefab.transform.localScale.x;

        for (int x = 0; x <= width; x++)
        {
            for (int z = 0; z <= height; z++)
            {
                GameObject tempGo = Instantiate(prefab);
                //GameBall gb = tempGo.GetComponent<GameBall>();
                tempGo.transform.parent = targetLayer.transform;
                if (z % 2 == 0)
                {
                    tempGo.transform.position = new Vector3(x * tileXOffset * sphereDiam + pos.x, targetLayer.transform.position.y, z * tileZOffset * sphereDiam + pos.z);

                }
                else
                {
                    tempGo.transform.position = new Vector3(x * tileXOffset * sphereDiam + tileXOffset / 2 + pos.x, targetLayer.transform.position.y, (z * tileZOffset * sphereDiam) + pos.z);
                }
                //gb.xOffset = x * tileXOffset;
                //gb.zOffset = z * tileZOffset;
            }
        }
    }

    public static void createGrid(int width, int height, float radius, GameObject prefab, GameObject targetLayer, Vector3 pos)
    {

        Vector3[] poses = createGridPoses(width, height, radius: radius);

        foreach (Vector3 p in poses)
        {
            GameObject tempGo = Instantiate(prefab);
            tempGo.transform.parent = targetLayer.transform;
            tempGo.transform.position = p;
        }



    }

    private static Vector3[] createGridPoses(int width, int height, float radius)
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
