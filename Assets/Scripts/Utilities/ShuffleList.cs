using System.Collections;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Partial Utility class. This class is a collection of static utility functions for all possible uses.
    /// </summary>
    public static partial class StaticFunctions
    {
        
        /// <summary>
        /// Static method that receives a collection of any type and shuffles it by applying
        /// the <a href="https://en.wikipedia.org/wiki/Fisher–Yates_shuffle">Fisher-Yates shuffle.</a>.
        /// </summary>
        public static void ShuffleCollection (IList list)
        {
            int count = list.Count;
            int last = count - 1;
            for (int i = 0; i < last; ++i)
            {
                int r = Random.Range(i, count);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }
    }
}
