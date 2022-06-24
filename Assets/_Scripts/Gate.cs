using System;
using UnityEngine;

public class Gate
{
    public GatesType type;
    public int value;
    public Gate(GatesType type)
    {
        this.type = type;
        this.value = getNumber(type);
    }

    public override string ToString()
    {
        return getSymbol() + value.ToString();
    }

    private int getNumber(GatesType type)
    {
        if (type == GatesType.Multiply || type == GatesType.Division)
        {
            return UnityEngine.Random.Range(2, 4);
        }
        return UnityEngine.Random.Range(1, 30);
    }

    private string getSymbol()
    {
        switch (type)
        {
            case GatesType.Addition:
                return "+";
            case GatesType.Multiply:
                return "*";//"✕";
            case GatesType.Division:
                return "/";//"÷";
            case GatesType.Substraction:
                return "-";
        }
        return "";
    }
}

public enum GatesType
{
    Multiply,
    Division,
    Addition,
    Substraction
}