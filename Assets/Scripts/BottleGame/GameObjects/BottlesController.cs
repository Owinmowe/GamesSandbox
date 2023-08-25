using System;
using System.Collections.Generic;
using BottleGame.Core;
using UnityEngine;

namespace BottleGame.GameObjects
{
    /// <summary>
    /// Component controller class of the BottleGame. This class is responsible of spawning, updating and destroying
    /// all necessary BottleObjects (engine representation of Bottle class) in the game. 
    /// </summary>
    public class BottlesController : MonoBehaviour
    {
        [SerializeField, Tooltip("BottleObject Prefab used for spawning bottles engine representations.")]
        private BottleObject bottleObjectPrefab;

        [SerializeField, Tooltip("The total area size for the bottles. This will be divided by the amount of " +
                                 "bottles per row and the amount of rows necessary for all bottles.")] 
        private Vector2 bottlesAreaSize;
        
        [SerializeField, Tooltip("The amount of bottles per row of bottles.")] 
        private int bottlesPerRow;

        private BottleObject[] _currentBottleObjects;
        
        /// <summary> This method instantiates a list of Bottle logic objects as BottleObjects. </summary>
        public void InitializeBottles(List<Bottle> bottles)
        {
            _currentBottleObjects = new BottleObject[bottles.Count];

            Vector3[] positions = CalculateAllBottlesPositions(bottles);
            
            for (int i = 0; i < bottles.Count; i++)
            {
                BottleObject newBottle = Instantiate(bottleObjectPrefab, positions[i], Quaternion.identity, transform);
                _currentBottleObjects[i] = newBottle;
                _currentBottleObjects[i].SetBottleData(bottles[i], i);
            }
        }
        
        /// <summary>
        /// This method updates the current existing BottleObjects. <br/>
        /// This means that it does not Destroy or Instantiate new GameObjects.
        /// </summary>
        public void UpdateCurrentBottles(List<Bottle> bottles)
        {
            for (int i = 0; i < bottles.Count; i++)
            {
                _currentBottleObjects[i].SetBottleData(bottles[i], i);
            }
        }
        
        /// <summary>This method destroys all current BottleObjects GameObject from the scene.</summary>
        public void DeInitializeBottles()
        {
            foreach (var bottleObject in _currentBottleObjects)
            {
                Destroy(bottleObject.gameObject);
            }
            _currentBottleObjects = null;
        }

        private Vector3[] CalculateAllBottlesPositions(List<Bottle> bottles)
        {
            Vector3[] positions = new Vector3[bottles.Count];

            int rowsAmount = Mathf.CeilToInt(bottles.Count / (float)bottlesPerRow);
            
            float distanceBetweenColumn = bottlesAreaSize.x / bottlesPerRow;
            float distanceBetweenRows = bottlesAreaSize.y / rowsAmount;

            float startColumnPosition = transform.position.x - bottlesAreaSize.x / 2 + distanceBetweenColumn / 2;
            float startRowPosition = transform.position.y + bottlesAreaSize.y / 2 - distanceBetweenRows / 2;
            
            int currentColumn = 0;
            int currentRow = 0;
            
            for (int i = 0; i < bottles.Count; i++)
            {
                positions[i].x = startColumnPosition + distanceBetweenColumn * currentColumn;
                positions[i].y = startRowPosition - currentRow * distanceBetweenRows;
                positions[i].z = 5;
                
                currentColumn++;
                if (currentColumn >= bottlesPerRow)
                {
                    currentColumn = 0;
                    currentRow++;
                }
            }
            
            return positions;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, bottlesAreaSize);
        }
    }
}