using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int activeLevelIndex = 0;
    List<GameObject> levels = new List<GameObject>();

    public static LevelManager Instance;
    void Awake()
    {
        Instance = this;
        int counter = 0;
        foreach (Transform t in transform)
        {
            t.gameObject.name = "Level_" + counter++;
            levels.Add(t.gameObject);
        }
    }



    void OnValidate()
    {
        if (levels.Count == 0) return;
        if (activeLevelIndex < 0) activeLevelIndex = 0;
        if (activeLevelIndex > levels.Count - 1) activeLevelIndex = levels.Count - 1;
        // if (activeLevelIndex < 0 || activeLevelIndex > levels.Count - 1) return;
        updateLevels();
    }

    public void setLevel(int level)
    {
        activeLevelIndex = level;
        updateLevels();
    }

    void updateLevels()
    {
        foreach (GameObject g in levels) g.SetActive(false);
        GameObject activeLevel = levels[activeLevelIndex];
        activeLevel.SetActive(true);
        Vector3 newPos = copyVector(activeLevel.transform.localPosition);
        newPos.x = 0;
        activeLevel.transform.localPosition = newPos;
    }
    Vector3 copyVector(Vector3 oVect) => new Vector3(oVect.x, oVect.y, oVect.z);
}
