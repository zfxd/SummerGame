using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

[Serializable]
public class UnitStat
{
    // readonly to prevent accidental reassignment or nullification
    private readonly List<StatModifier> statModifiers;

    // publicly accessible readonly copy of the stat modifiers list
    public readonly ReadOnlyCollection<StatModifier> statModifiersPublic;
    
    public float baseValue; // Base value of the stat
    private float lastBaseValue; // Last known base value of the stat

    private float lastValue; // Last known value of the stat
    private bool isUpdated = true; // Whether lastValue is still correct (set to false when mod added/removed)

    // Current value of the stat (variable!! not method!!)
    // When requested, calculate if value is outdated and update it if needed
    public float value
    {
        get
        {   
            if (!isUpdated || lastBaseValue != baseValue)
            {
                lastBaseValue = baseValue;
                lastValue = CalculateFinalValue(); // Update lastValue if outdated
                isUpdated = true; // lastValue is now correct
            }

            return lastValue;
        }
    }
    
    // Constructors
    public UnitStat()
    {
        // Initialize the empty list of modifiers
        statModifiers = new List<StatModifier>();

        statModifiersPublic = statModifiers.AsReadOnly();
    }

    public UnitStat(float val): this()
    {
        baseValue = val;
    }

    // Calculate actual stat value based on base value and all modifiers
    private float CalculateFinalValue()
    {
        float finalValue = baseValue;

        float sumPercentAdd = 0;

        // Loop through list of modifiers and apply them
        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.type == StatModType.Flat)
            {
                finalValue += mod.value;
            }
            else if (mod.type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.value;

                // If end of list OR next mod is not PercentAdd type
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.type == StatModType.PercentMult)
            {
                finalValue += 1 + mod.value;
            }
        }

        // Round but stay as float
        return (float)Math.Round(finalValue, 4);
    }

    // Add a modifier to the list
    public void AddMod(StatModifier mod)
    {
        isUpdated = false;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModOrder); // Sort the list by order of application
    }
    
    // Remove a modifier from the list
    public bool RemoveMod(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isUpdated = false;
            return true;
        }

        return false;
    }

    // Remove all modifiers from given source from the list
    public bool RemoveAllModsFromSource(object src)
    {
        bool didRemove = false;

        // Traverse list in reverse to avoid having to shift after deletion
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].source == src)
            {
                statModifiers.RemoveAt(i);
                didRemove = true;
            }
        }

        return didRemove;
    }

    // Comparator to sort mod list by intended order of application
    private int CompareModOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order)
            return 1;
        else
            return 0;
    }
}