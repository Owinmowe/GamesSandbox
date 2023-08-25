using System;
using UnityEngine;
using BottleGame.Data;

namespace BottleGame.GameObjects
{
    /// <summary>
    /// Component controller class of the BottleGame. This class is responsible of receiving inputs and call the
    /// methods of BottleObjects based on player input.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {

        /// <summary>Event called from BottleController when the player tries to mix two bottles.</summary>
        public event Action<BottlesMixData> OnBottlesMixEvent; 

        [SerializeField, Tooltip("Depth of bottles when selected and dragged through the screen.")]
        private float zPosition = 5f;
        
        [SerializeField, Tooltip("Layer used by bottles for checking interactions.")]
        private LayerMask interactableMask;

        private BottleObject _selectedBottleObject; 
        private Camera CurrentCamera => Camera.main;
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                TryToSelectBottle();
            else if (Input.GetMouseButtonUp(0))
                TryToDeSelectBottle();
            else
                TryToDragBottle();
        }

        //In case of finding a mouse collision with a bottle, we select it. 
        private void TryToSelectBottle()
        {
            if (GetMouseBottleCollision(out BottleObject bottleObject))
            {
                _selectedBottleObject = bottleObject;
                _selectedBottleObject.ChangeColliderState(false);  
            }
        }

        //In case of selected bottle not being null, we try to mix it with a new BottleObject and then we deselect it. 
        private void TryToDeSelectBottle()
        {
            if (_selectedBottleObject)
            {
                if (GetMouseBottleCollision(out BottleObject bottleObject))
                {
                    BottlesMixData bottlesMixData = new BottlesMixData()
                    {
                        IndexBottleFrom = _selectedBottleObject.BottleIndex,
                        IndexBottleTo = bottleObject.BottleIndex
                    };
                    OnBottlesMixEvent?.Invoke(bottlesMixData);
                }
                    
                _selectedBottleObject.ReturnToStartPosition();
                _selectedBottleObject.ChangeColliderState(true);
                _selectedBottleObject = null;
            }
        }

        //In case of selected bottle not being null, we try to drag it trough the screen. 
        private void TryToDragBottle()
        { 
            if (_selectedBottleObject)
            {
                Vector3 mousePosition = CurrentCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = zPosition;
                _selectedBottleObject.transform.position = mousePosition;
            }
        }
        
        private bool GetMouseBottleCollision(out BottleObject bottleObject)
        {
            Ray ray = CurrentCamera.ScreenPointToRay(Input.mousePosition);
                
            if (Physics.Raycast(ray, out RaycastHit internalHit, 100, interactableMask))
            {
                if (internalHit.collider.TryGetComponent(out BottleObject newBottleObject))
                {
                    bottleObject = newBottleObject;
                    return true;
                }
            }
            
            bottleObject = null;
            return false;
        }
    }
}

