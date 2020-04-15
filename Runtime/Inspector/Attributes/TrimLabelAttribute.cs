using System;

namespace StudioName.Runtime.Inspector.Attributes {
    
    /// <summary>
    /// Trims text from either the front or the back of a field / property label
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class TrimLabelAttribute : Attribute {
        /// <summary>
        /// Should we trim text from the front or the back of the label?
        /// </summary>
        public bool trimFromFront;
        /// <summary>
        /// Text to trim from the label
        /// </summary>
        public string textToTrim;
        
        public TrimLabelAttribute(string textToTrim, bool trimFromFront = true) {
            this.trimFromFront = trimFromFront;
            this.textToTrim = textToTrim;
        }
    }
}