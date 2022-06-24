using System;
using TMPro;
using UnityEngine;

public class GatesController : MonoBehaviour
{
    [SerializeField] bool doubleGates = true;
    public Gate leftGate, rightGate;
    [SerializeField] TextMeshPro leftText, rightText;


    GatesType getGatesType(Array values) => (GatesType)values.GetValue(UnityEngine.Random.Range(0, values.Length - 1));

    void Awake()
    {

        Array values = GatesType.GetValues(typeof(GatesType));

        leftGate = new Gate(type: getGatesType(values));
        leftText.text = leftGate.ToString();
        if (doubleGates)
        {
            rightGate = new Gate(type: getGatesType(values));
            rightText.text = rightGate.ToString();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "runner")
        {
            gameObject.SetActive(false);
            if (doubleGates)
            {
                Gate activeGate = getActiveGates();
                PeoplesManager.instance.ApplyGates(activeGate);
            }
            else
            {
                PeoplesManager.instance.ApplyGates(leftGate);
            }
            //TODO: сделать нормальный вылетающий текст
            //Debug.Log("Active Modifier: " + activeGate.type + " " + activeGate.value);
        }
    }

    private Gate getActiveGates() => PeoplesManager.instance.roadSide == RoadSide.Left ? leftGate : rightGate;

}
