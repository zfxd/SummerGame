public enum StatModType {Flat = 100, PercentAdd = 200, PercentMult = 300};

public class StatModifier
{
    public readonly float value;
    public readonly StatModType type;
    public readonly int order;
    public readonly object source;

    public StatModifier(float val, StatModType t, int ord, object src)
    {
        value = val;
        type = t;
        order = ord;
        source = src;
    }

    // No order, no source -> default order, null source
    public StatModifier(float val, StatModType t) : this(val, t, (int)t, null){}

    // No source -> null source
    public StatModifier(float val, StatModType t, int ord) : this(val, t, ord, null){}

    // No order -> default order
    public StatModifier(float val, StatModType t, object src) : this(val, t, (int)t, src){}

}