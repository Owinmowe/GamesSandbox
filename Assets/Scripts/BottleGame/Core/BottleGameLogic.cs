using System;
using System.Collections.Generic;
using Utilities;
using GamesSandbox.Core;
using BottleGame.Data;
using BottleGame.Data.Configuration;
using UnityEngine;

namespace BottleGame.Core
{
    /// <summary>
    /// Core GameLogic class for BottleGame. This class implements all the Gameplay logic needed for the game.
    /// </summary>
    public class BottleGameLogic : IGameLogic
    {
        /// <summary>
        /// This event is called either when the gameplay is started by the StartGame method just after making a valid
        /// puzzle.
        /// </summary>
        public event Action OnGameStarted;
        
        /// <summary>
        /// This event is called either when the gameplay is completed or when its externally cancelled by
        /// the StopGame method. 
        /// </summary>
        public event Action<PostGameData> OnGameEnded;
        
        private readonly GameplayConfigurationData _currentGameplayData;
        private float _gameStartTime;

        /// <summary>
        /// Current logic bottles in the BottleGameLogic.
        /// The bottles only get set after calling the StartGame method. 
        /// </summary>
        public List<Bottle> CurrentBottles { get; private set; }
        
        /// <summary>
        /// Current game state. <br/>
        /// True after OnGameStarted event is called and false after GameEnded event is called. 
        /// </summary>
        public bool GameActive { get; private set; }
        
        /// <summary>
        /// Method used to mix two bottles. This method tries to add the the top liquid from the "FromBottle"
        /// to the "ToBottle" and if the add was valid, it removes the added amount from the "FromBottle".<br/>
        /// If this mix makes all bottles have only one type of internal liquid, the OnGameEnded event is called. 
        /// </summary>
        public BottleGameLogic(GameplayConfigurationData gameplayConfigurationData)
        {
            _currentGameplayData = gameplayConfigurationData;
        }
              
        /// <summary>
        /// This method initialize the game using the GameplayConfigurationData set in the constructor
        /// and calls the OnGameStarted event.
        /// </summary>
        public void StartGame()
        {
            CurrentBottles = GetValidBottlesPuzzle(_currentGameplayData);
            
            _gameStartTime = Time.time;
            
            OnGameStarted?.Invoke();
            GameActive = true;
        }

        /// <summary>This method calls the OnGameEnded event. </summary>
        public void StopGame()
        {
            BottleGamePostGameData postGameData = new BottleGamePostGameData
            {
                TimeToComplete = Time.time - _gameStartTime
            };
            
            OnGameEnded?.Invoke(postGameData);
            GameActive = false;
        }

        /// <summary>
        /// Method used to mix two bottles. This method tries to add the the top liquid from the "FromBottle"
        /// to the "ToBottle" and if the add was valid, it removes the added amount from the "FromBottle".<br/>
        /// If this mix makes all bottles have only one type of internal liquid, the OnGameEnded event is called. 
        /// </summary>
        public void MixTwoBottlesLiquid(BottlesMixData bottlesMixData)
        {
            Bottle fromBottle = CurrentBottles[bottlesMixData.IndexBottleFrom];
            Bottle toBottle = CurrentBottles[bottlesMixData.IndexBottleTo];

            int liquidAddAmount = 0;
            if (toBottle.TryToAddLiquid(fromBottle.TopLiquid, ref liquidAddAmount))
            {
                fromBottle.RemoveFromTopLiquid(liquidAddAmount);
                
                ReplaceBottle(bottlesMixData.IndexBottleFrom, fromBottle);
                ReplaceBottle(bottlesMixData.IndexBottleTo, toBottle);
                
            }
            
            if(HasGameEnded())
                StopGame();
        }

        private void ReplaceBottle(int bottleIndex, Bottle bottle) => CurrentBottles[bottleIndex] = bottle;

        private bool HasGameEnded()
        {
            foreach (var bottle in CurrentBottles)
            {
                if (!IsBottleDone(bottle))
                    return false;
            }
            return true;
        }

        private bool IsBottleDone(Bottle bottle)
        {
            bool bottleFull = bottle.InternalLiquid.Count == 1 && bottle.TotalLiquidAmount == bottle.BottleMaxCapacity;
            bool bottleEmpty = bottle.TotalLiquidAmount == 0;
            return bottleFull || bottleEmpty;
        }

        private List<Bottle> GetValidBottlesPuzzle(GameplayConfigurationData gameplayConfigurationData)
        {
            List<LiquidTypeData> possibleLiquidTypes = gameplayConfigurationData.possibleLiquids.liquidsTypeData;
            int fullBottlesAmount = gameplayConfigurationData.fullBottlesAmount;
            int emptyBottlesAmount = gameplayConfigurationData.emptyBottlesAmount;
            int bottlesSlotsAmount = gameplayConfigurationData.bottlesSlotsAmount;
            int bottlesMaxCapacity = gameplayConfigurationData.bottlesMaxCapacity;
            int liquidAmountPerSlot = bottlesMaxCapacity / bottlesSlotsAmount;
            
            List<Bottle> returnBottles = new List<Bottle>(fullBottlesAmount + emptyBottlesAmount);
            
            List<Liquid> allPuzzleLiquid = new List<Liquid>(fullBottlesAmount * bottlesSlotsAmount);
            for (int i = 0; i < fullBottlesAmount; i++)
            {
                for (int j = 0; j < bottlesSlotsAmount; j++)
                {
                    Liquid newLiquid = new Liquid()
                    {
                        Amount = liquidAmountPerSlot,
                        TypeData = possibleLiquidTypes[j]
                    };
                    allPuzzleLiquid.Add(newLiquid);
                }
            }
            
            StaticFunctions.ShuffleCollection(allPuzzleLiquid);

            for (int i = 0; i < fullBottlesAmount; i++)
            {
                List<Liquid> liquidListToAdd = new List<Liquid>();

                for (int j = 0; j < bottlesSlotsAmount; j++)
                {
                    Liquid liquidToAdd = allPuzzleLiquid[i * bottlesSlotsAmount + j];

                    if (liquidListToAdd.Count > 0 && liquidListToAdd[^1].TypeData == liquidToAdd.TypeData)
                    {
                        liquidToAdd.Amount += liquidListToAdd[^1].Amount;
                        liquidListToAdd[^1] = liquidToAdd;
                    }
                    else
                    {
                        liquidListToAdd.Add(liquidToAdd);
                    }
                }

                Bottle newBottle = new Bottle(liquidListToAdd, bottlesMaxCapacity);
                
                returnBottles.Add(newBottle);
            }

            for (int i = 0; i < emptyBottlesAmount; i++)
            {
                List<Liquid> liquidListToAdd = new List<Liquid>();
                Bottle newBottle = new Bottle(liquidListToAdd, bottlesMaxCapacity);
                returnBottles.Add(newBottle);
            }
            
            return returnBottles;
        }
    }
}
