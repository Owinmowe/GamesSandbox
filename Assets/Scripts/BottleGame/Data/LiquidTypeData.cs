using UnityEngine;

namespace BottleGame.Data
{
    [System.Serializable]
    public struct LiquidTypeData
    {
        /// <summary>Liquid color. Used by the Liquid Shader to draw different colors in bottles.</summary>
        public Color graphicColor;

        /// <summary>
        /// This method replaces the default Equals comparison since we use the graphicColor variable
        /// for comparisons.
        /// </summary>
        public bool Equals(LiquidTypeData other)
        {
            return graphicColor.Equals(other.graphicColor);
        }

        /// <summary>
        /// This method replaces the default Equals comparison since we use the graphicColor variable
        /// for comparisons.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is LiquidTypeData other && Equals(other);
        }

        /// <summary>
        /// This method replaces the default GetHashCode since we use the graphicColor variable as a way
        /// to compare colors.
        /// </summary>
        public override int GetHashCode()
        {
            return graphicColor.GetHashCode();
        }

        /// <summary>
        /// This operator overload replaces the default "==" comparison since we use the graphicColor variable
        /// for comparisons.
        /// </summary>
        public static bool operator ==(LiquidTypeData obj1, LiquidTypeData obj2)
        {
            return obj1.GetHashCode() == obj2.GetHashCode();
        }

        /// <summary>
        /// This operator overload replaces the default "!=" comparison since we use the graphicColor variable
        /// for comparisons.
        /// </summary>
        public static bool operator !=(LiquidTypeData obj1, LiquidTypeData obj2)
        {
            return obj1.GetHashCode() != obj2.GetHashCode();
        }
    }
}