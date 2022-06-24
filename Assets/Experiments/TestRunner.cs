using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestRunner : MonoBehaviour
{
    TestIntegrator integrator;
    // Start is called before the first frame update

    public void fireMethod()
    {
        if (!integrator) integrator = GetComponent<TestIntegrator>();
        integrator.fireMethod();
    }


    public void addBall()
    {
        if (!integrator) integrator = GetComponent<TestIntegrator>();
        integrator.addBall();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

// [CustomEditor(typeof(TestRunner))]
// public class CustomInspector : Editor

// {

//     // public override void OnInspectorGUI()
//     // {
//     //     TestIntegrator myTarget = (TestIntegrator)target;

//     //     // myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
//     //     // EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
//     // }

//     public override void OnInspectorGUI()
//     {
//         TestRunner myScript = (TestRunner)target;
//         if (GUILayout.Button("Run Numerate"))
//         {
//             myScript.fireMethod();
//             // myScript.BuildObject();
//         }
//         if (GUILayout.Button("addBall"))
//         {
//             myScript.addBall();
//             // myScript.BuildObject();
//         }
//     }
// }