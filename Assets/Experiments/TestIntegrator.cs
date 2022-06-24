using System.Collections.Generic;
using UnityEngine;

public class TestIntegrator : MonoBehaviour
{

    [SerializeField] GameObject prefab;
    [SerializeField] MagnetBall firstBallToNumerate;
    // [SerializeField] MagnetBall firstObject;
    [SerializeField] List<MagnetBall> balls;

    [SerializeField] Vector3[] resultBalls;

    int counter = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void addBall()
    {
        var newPosition = resultBalls[++counter];
        var newObj = Instantiate(prefab);
        newObj.transform.position = newPosition;
    }

    public void numerate()
    {
        balls.Clear();
        // List<MagnetBall> balls = new();
        foreach (Transform t in transform)
        {
            var mb = t.GetComponent<MagnetBall>();
            if (mb && mb.counter != 0)
                balls.Add(mb);
        }

        resultBalls = new Vector3[balls.Count + 1];
        foreach (MagnetBall b in balls)
        {
            resultBalls[b.counter] = b.transform.position;
        }
        // firstBallToNumerate.startNumerate(numerated);
    }

    void AddObjectHex()
    {
        Utils.createGrid(20, 20, radius: 0.5f,
                    prefab: prefab, gameObject, transform.position);
    }

    public GameObject[] AddObjects(int peopleCount, Transform targetTransform, GameObject pf, float sizeMultiplicator)
    {

        // float xOffset = firstBallToNumerate.transform.position.x;
        Vector3 firstOffset = Vector3.zero;

        GameObject[] result = new GameObject[peopleCount];
        Vector3 newPosition;
        for (int i = 0; i < peopleCount; i++)
        {
            var b = Instantiate(pf);
            b.transform.parent = targetTransform;
            newPosition = i == 0 ? firstBallToNumerate.transform.position * sizeMultiplicator : resultBalls[i] * sizeMultiplicator;
            if (i == 0) firstOffset = newPosition;
            newPosition.x -= firstOffset.x;
            newPosition.z -= firstOffset.z;
            b.transform.position = newPosition;
            result[i] = b;
        }
        return result;
    }

    internal void fireMethod()
    {
        numerate();
        // AddObjects();
        // AddObjectHex();
        Debug.Log("fireMethod");
        // throw new NotImplementedException();
    }
}


