using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BottleGame.Core
{
    /// <summary>
    /// Bottle struct for BottleGame. This struct has all the necessary data and functionality
    /// to add/remove different Liquid types and several fields to check the current state of
    /// a Bottle. 
    /// </summary>
    public struct Bottle
    {
        /// <summary>
        /// List of all internal liquids. Note that internally inside the Bottle struct liquids are a stack,
        /// so this functions does a conversion before retrieving the data.
        /// </summary>
        public List<Liquid> InternalLiquid => _internalLiquids.ToList();
        
        /// <summary>
        /// Top liquid from the bottle. Since the Liquid is a struct this returns a copy of the
        /// top liquid.
        /// </summary>
        public Liquid TopLiquid => _internalLiquids.Peek();
        
        /// <summary>
        /// Total liquid amount inside the bottle. This value is cached up, so it can be request
        /// several times with minimal performance lost.
        /// </summary>
        public int TotalLiquidAmount { get; private set; }
        
        /// <summary>
        /// Bottle max liquid amount. This value is set on Bottle construction and can't be overriden after.
        /// </summary>
        public int BottleMaxCapacity { get; }

        private readonly Stack<Liquid> _internalLiquids;

        /// <summary>
        /// Bottle constructor. After construction liquids can't be directly modified and max capacity
        /// can't be changed.
        /// <param name ="liquids">Starting liquids when bottle is constructed. An empty list is a valid
        /// value.</param>
        /// <param name ="bottleMaxCapacity">Bottle maximum liquid capacity. This value must be lower than
        /// the sum of starting liquids amount or it will be ignored and replaced by the amount of
        /// all starting liquids combined.</param>
        /// <returns>Bottle with given parameters.</returns>
        /// </summary>
        public Bottle(List<Liquid> liquids, int bottleMaxCapacity)
        {
            _internalLiquids = new Stack<Liquid>(liquids.Count);
            
            TotalLiquidAmount = 0;
            
            for (int i = 0; i < liquids.Count; i++)
            {
                _internalLiquids.Push(liquids[i]);
                TotalLiquidAmount += liquids[i].Amount;
            }

            if (TotalLiquidAmount > bottleMaxCapacity)
            {
                Debug.LogWarning("A bottle max capacity is lower than the total liquid starting inside.");
                Debug.LogWarning("Max capacity will be replaced with total liquid amount.");
                BottleMaxCapacity = TotalLiquidAmount;
            }
            else
            {
                BottleMaxCapacity = bottleMaxCapacity;
            }
        }

        /// <summary>
        /// This method is used to try to add a liquid to the Bottle.
        /// <param name ="liquidToAdd">Liquid that will be added to the bottle if possible.</param>
        /// <param name ="liquidExchangeAmount">Amount of liquid exchanged. This can be different than the
        /// provided amount by the liquid since the bottle could be full with only a fraction of the liquid
        /// amount. In case of method returning false this variable is not modified.</param>
        /// <returns>Returns true if liquid could be added to the bottle, otherwise false.</returns>
        /// </summary>
        public bool TryToAddLiquid(Liquid liquidToAdd, ref int liquidExchangeAmount)
        {
            if (TotalLiquidAmount == BottleMaxCapacity) return false;
            
            liquidExchangeAmount = AddLiquidToTop(liquidToAdd);
            return true;
        }

        /// <summary>
        /// This method is used to try to remove a liquid amount from a Bottle. If not liquid is present in a Bottle it
        /// will do nothing.
        /// <param name ="amount">Liquid amount that will be removed. If the liquid amount is superior or equal to the
        /// amount of liquid on the top liquid, the top liquid will be removed.</param>
        /// </summary>
        public void RemoveFromTopLiquid(int amount)
        {
            if (_internalLiquids.Count > 0)
            {
                Liquid liquid = _internalLiquids.Pop();
                
                int previousLiquidAmount = liquid.Amount;
                liquid.Amount -= amount;

                if (liquid.Amount > 0)
                {
                    _internalLiquids.Push(liquid);
                    TotalLiquidAmount -= amount;
                }
                else
                {
                    TotalLiquidAmount -= previousLiquidAmount;
                }
            }
        }
        
        private int AddLiquidToTop(Liquid liquidToAdd)
        {
            int liquidExchangeAmount = liquidToAdd.Amount;
            
            if (_internalLiquids.Count == 0)
            {
                // In case of bottle with no previous liquid we simply add it to the stack
                // without checking for previous liquids.
                
                TotalLiquidAmount += liquidExchangeAmount;
                
                // In case bottle overflow we remove the extra liquid amount from the total and liquid exchange amount.
                if (TotalLiquidAmount > BottleMaxCapacity)
                {
                    int overflowAmount = TotalLiquidAmount - BottleMaxCapacity;
                    TotalLiquidAmount -= overflowAmount;
                    liquidExchangeAmount = TotalLiquidAmount;
                }
                _internalLiquids.Push(liquidToAdd);
            }
            else
            {
                // In case of bottle with previous liquid we check if we can add it to the previous liquid or
                // add a new one if its not possible.
                Liquid topLiquid = _internalLiquids.Pop();

                // In case bottle overflow we remove the extra liquid amount from the total and liquid exchange amount.
                if (TotalLiquidAmount + liquidExchangeAmount > BottleMaxCapacity)
                {
                    int overflowAmount = TotalLiquidAmount + liquidExchangeAmount - BottleMaxCapacity;
                    liquidExchangeAmount -= overflowAmount;
                }

                if (topLiquid.TypeData.Equals(liquidToAdd.TypeData))
                {
                    // In case of same liquid type we add only one of them with both liquids amount combined.
                    topLiquid.Amount += liquidExchangeAmount;
                    TotalLiquidAmount += liquidExchangeAmount;
                    _internalLiquids.Push(topLiquid);
                }
                else
                {
                    // In case of different liquid type we add both to the stack.
                    TotalLiquidAmount += liquidExchangeAmount;
                    _internalLiquids.Push(topLiquid);
                    _internalLiquids.Push(liquidToAdd);
                }
            }
            
            return liquidExchangeAmount;
        }
    }
}
