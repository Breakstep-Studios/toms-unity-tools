using System;

namespace StudioName.Runtime.Inspector.Attributes {
    
    /// <summary>
    /// Marks a field value to be rounded to the nearest whole number.
    /// <para>Currently used in the <see cref="RoundedAttributeDrawer"/> to round Vector2 values to nearest whole number</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RoundedAttribute : Attribute { }

}
