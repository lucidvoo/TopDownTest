using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stores one int var for easy and decoupled sharing data between scripts

[CreateAssetMenu(fileName = "newInt", menuName = "SO/Int Variable", order = 20)]
public class IntVariableSO : ScriptableObject
{
    [SerializeField] private bool useMinValue;
    [SerializeField] private int minValue = 0;
    [Space]
    [SerializeField] private bool useMaxValue;
    [SerializeField] private int maxValue = 100;
    [Space]
    [SerializeField] private int defaultValue = 0;
    
    private int value;

    public int Value 
    { 
        get => value;
        set
        {
            if (useMinValue && value < minValue)
            {
                this.value = minValue;
            }
            else if (useMaxValue && value > maxValue)
            {
                this.value = maxValue;
            }
            else
            {
                this.value = value;
            }
        }
    }

    private void OnEnable()
    {
        Reset();
    }

    public void Reset() => Value = defaultValue;

    public bool IsZero() => Value == 0;

}
