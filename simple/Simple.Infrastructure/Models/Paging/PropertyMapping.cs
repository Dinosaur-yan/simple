using System;
using System.Collections.Generic;
using System.Text;

public class PropertyMapping
{
    public string TargetProperty { get; private set; }
    public bool IsRevent { get; private set; }

    public PropertyMapping(string targetProperty,
        bool revent = false)
    {
        TargetProperty = targetProperty;
        IsRevent = revent;
    }
}
