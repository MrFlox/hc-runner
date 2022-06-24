using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class GatesManager : MonoBehaviour
{
    static public GatesManager Instance;
    [SerializeField] List<GatesController> gates = new List<GatesController>();

    // void OnGUI()
    // {
    //     // if (!btnTexture)
    //     // {
    //     //     Debug.LogError("Please assign a texture on the inspector");
    //     //     return;
    //     // }

    //     // if (GUI.Button(new Rect(10, 10, 50, 50)))
    //     //     Debug.Log("Clicked the button with an image");

    //     if (GUI.Button(new Rect(0, 0, 200, 200), "Click"))
    //         Debug.Log("Clicked the button with text");
    // }

    void OnValidate()
    {
        // // Debug.Log("OnValidate");
        // gates.Clear();
        // foreach (Transform t in transform)
        // {
        //     GatesTrigger tr = t.GetComponent<GatesTrigger>();
        //     gates.Add(tr);
        //     tr.updateView();
        // }


    }

    void Awake()
    {
        Instance = this;
        gates.Clear();
        foreach (Transform t in transform)
        {
            GatesController tr = t.GetComponent<GatesController>();
            gates.Add(tr);
        }
    }

    public void showAll()
    {
        foreach (GatesController t in gates) t.gameObject.SetActive(true);
    }
}
