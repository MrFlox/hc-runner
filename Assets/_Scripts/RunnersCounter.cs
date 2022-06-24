using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunnersCounter : MonoBehaviour
{
    [SerializeField] TextMeshPro text;

    // void Awake()
    // {
    //     mr = GetComponent<MeshRenderer>();
    // }
    // Start is called before the first frame update



    public void setCount(int value)
    {
        text.text = value.ToString();
    }

    public void flyWithText(string value)
    {

        text.text = value;
        Vector3 targetPos = new Vector3();
        iTween.MoveTo(gameObject, iTween.Hash(
                     "position", targetPos,
                     "islocal", true,
                     "time", 1f));

        //TODO: Fly!
    }

    void hideMy()
    {

    }

}
