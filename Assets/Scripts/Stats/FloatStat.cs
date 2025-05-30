using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatStat 
{
    [SerializeField] private float baseValue;

    public List<float> modifiers;

    public float GetValue()
    {
        float finalValue = baseValue;

        foreach (float modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;
    }

    public void SetValue(float _value)
    {
        baseValue = _value;
    }

    public void AddModifier(float _modifier)
    {
        modifiers.Add(_modifier);
    }

    public void RemoveModifier(float _modifier)
    {
        modifiers.Remove(_modifier);
    }
}
