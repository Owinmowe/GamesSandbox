using BottleGame.Data;

namespace BottleGame.Core
{
    /// <summary>
    /// Liquid structure used by BottleGame. Note that since its an structure you can reuse the same liquid
    /// several times for different purposes.
    /// </summary>
    public struct Liquid
    {
        /// <summary>Liquid amount.</summary>
        public int Amount;
        
        /// <summary>Liquid type data. This includes color.</summary>
        public LiquidTypeData TypeData;
    }
}

