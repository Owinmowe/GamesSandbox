using System.Collections.Generic;
using BottleGame.Core;
using BottleGame.Data;
using NUnit.Framework;
using UnityEngine;

namespace Tests.BottleGame.EditMode
{
    public class BottleTests
    {
        /// <summary>In this test we add a valid amount of liquid to an empty Bottle. </summary>
        [Test]
        public void CreateValidTest()
        {
            List<Liquid> liquids = new List<Liquid>();

            Liquid liquidToAdd = new Liquid()
            {
                Amount = 50,
                TypeData = new LiquidTypeData()
                {
                    graphicColor = Color.red
                }
            };
            liquids.Add(liquidToAdd);
            
            Bottle bottle = new Bottle(liquids, 100);
            
            Assert.IsTrue(bottle.TopLiquid.Amount == 50);
        }
        
        /// <summary>In this test we add a valid amount of liquid to an empty Bottle. </summary>
        [Test]
        public void CreateOverflowTest()
        {
            List<Liquid> liquids = new List<Liquid>();

            Liquid liquidToAdd = new Liquid()
            {
                Amount = 150,
                TypeData = new LiquidTypeData()
                {
                    graphicColor = Color.red
                }
            };
            liquids.Add(liquidToAdd);
            
            Bottle bottle = new Bottle(liquids, 100);
            
            Assert.IsTrue(bottle.TopLiquid.Amount == 150 && bottle.BottleMaxCapacity == 150);
        }
        
        /// <summary>In this test we add a valid amount of liquid to an empty Bottle. </summary>
        [Test]
        public void AddFromEmptyTest()
        {
            List<Liquid> liquids = new List<Liquid>();
            Bottle bottle = new Bottle(liquids, 100);

            Liquid liquidToAdd = new Liquid()
            {
                Amount = 50,
                TypeData = new LiquidTypeData()
                {
                    graphicColor = Color.red
                }
            };

            int liquidExchangeAmount = 0;
            bottle.TryToAddLiquid(liquidToAdd, ref liquidExchangeAmount);
            
            Assert.IsTrue(liquidExchangeAmount == 50);
        }

        /// <summary>In this test we add a valid amount of liquid to a used Bottle. </summary>
        [Test]
        public void AddValidTest()
        {
            List<Liquid> liquids = new List<Liquid>();
            Liquid liquidToAdd = new Liquid()
            {
                Amount = 50,
                TypeData = new LiquidTypeData()
                {
                    graphicColor = Color.red
                }
            };
            liquids.Add(liquidToAdd);
            
            Bottle bottle = new Bottle(liquids, 100);
            
            int liquidExchangeAmount = 0;
            
            Assert.IsTrue(bottle.TryToAddLiquid(liquidToAdd, ref liquidExchangeAmount));
        }

        /// <summary>In this test we add an overflowing amount of liquid to an empty Bottle. </summary>
        [Test]
        public void AddOverflowEmptyTest()
        {
            List<Liquid> liquids = new List<Liquid>();
            Bottle bottle = new Bottle(liquids, 100);

            Liquid liquidToAdd = new Liquid()
            {
                Amount = 101,
                TypeData = new LiquidTypeData()
                {
                    graphicColor = Color.red
                }
            };

            int liquidExchangeAmount = 0;
            bool liquidAddValid = bottle.TryToAddLiquid(liquidToAdd, ref liquidExchangeAmount);
            
            Assert.IsTrue(liquidAddValid && liquidExchangeAmount == 100);
        }
        
        /// <summary>In this test we add an overflowing amount of liquid to a used Bottle. </summary>
        [Test]
        public void AddOverflowUsedTest()
        {
            List<Liquid> liquids = new List<Liquid>();
            Liquid liquidToAdd = new Liquid()
            {
                Amount = 51,
                TypeData = new LiquidTypeData()
                {
                    graphicColor = Color.red
                }
            };
            liquids.Add(liquidToAdd);
            Bottle bottle = new Bottle(liquids, 100);


            int liquidExchangeAmount = 0;
            bool liquidAddValid = bottle.TryToAddLiquid(liquidToAdd, ref liquidExchangeAmount);
            
            Assert.IsTrue(liquidAddValid && liquidExchangeAmount == 49);
        }

        /// <summary>In this test we add a valid amount of liquid to a full Bottle. </summary>
        [Test]
        public void AddFullTest()
        {
            List<Liquid> liquids = new List<Liquid>();
            Liquid liquidToAdd = new Liquid()
            {
                Amount = 100,
                TypeData = new LiquidTypeData()
                {
                    graphicColor = Color.red
                }
            };
            liquids.Add(liquidToAdd);
            Bottle bottle = new Bottle(liquids, 100);


            int liquidExchangeAmount = 0;
            bool liquidAddValid = bottle.TryToAddLiquid(liquidToAdd, ref liquidExchangeAmount);
            
            Assert.IsTrue(!liquidAddValid && liquidExchangeAmount == 0);
        }

        /// <summary>In this test we remove a valid amount of liquid to a used Bottle. </summary>
        [Test]
        public void RemoveValidTest()
        {
            List<Liquid> liquids = new List<Liquid>();
            Liquid liquidToAdd = new Liquid()
            {
                Amount = 100,
                TypeData = new LiquidTypeData()
                {
                    graphicColor = Color.red
                }
            };
            liquids.Add(liquidToAdd);
            Bottle bottle = new Bottle(liquids, 100);
            
            bottle.RemoveFromTopLiquid(50);
            Assert.IsTrue(bottle.TotalLiquidAmount == 50 && bottle.InternalLiquid.Count == 1);
        }

        /// <summary>In this test we remove a valid amount of liquid to an empty Bottle. </summary>
        [Test]
        public void RemoveEmptyTest()
        {
            List<Liquid> liquids = new List<Liquid>();
            Bottle bottle = new Bottle(liquids, 100);
            bottle.RemoveFromTopLiquid(0);
            Assert.IsTrue(bottle.TotalLiquidAmount == 0 && bottle.InternalLiquid.Count == 0);
        }
    }
}