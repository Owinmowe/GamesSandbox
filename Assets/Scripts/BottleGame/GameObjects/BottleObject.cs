using System.Collections.Generic;
using UnityEngine;
using BottleGame.Core;

namespace BottleGame.GameObjects
{
    /// <summary>
    /// Base Component class of the BottleGame.<br/>
    /// This class is responsible of Bottles Unity components interactions (like colliders or renderers).
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class BottleObject : MonoBehaviour
    {
        
        [SerializeField, Tooltip("Renderer responsible of drawing the liquid. The first assigned material of the renderer " +
                                 "MUST use the 'Unlit Liquid Shader' Shader.")]
        private Renderer liquidRenderer;
        
        /// <summary>
        /// Bottle Index of a BottleObject. This value represent the index of a Bottle in the GameLogic and
        /// its used to identify BottlesMixEvents.
        /// </summary>
        public int BottleIndex { get; private set; }
        
        private Vector3 _startPosition;
        private Collider _collider;
        
        private MaterialPropertyBlock _liquidMaterialPropertyBlock;
        
        private static readonly int BottleCapacity = Shader.PropertyToID("bottle_capacity");
        private static readonly int BottleTotalLiquidAmount = Shader.PropertyToID("bottle_total_liquid_amount");
        private static readonly int LiquidCount = Shader.PropertyToID("liquids_count");
        private static readonly int LiquidAmountArray = Shader.PropertyToID("liquid_amount_array");
        private static readonly int LiquidColorArray = Shader.PropertyToID("liquid_color_array");

        // This limit is hardcoded in the shader since shader don't have dynamic size arrays.
        private const int ShaderMaxColors = 6;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            _startPosition = transform.position;
        }

        /// <summary>
        /// This method is used to inject a Bottle logic struct data into a BottleObject. <br/>
        /// Using this information, the BottleObject creates a graphic interpretation
        /// of the Bottle.
        /// </summary>
        public void SetBottleData(Bottle bottle, int index)
        {
            BottleIndex = index;
            
            _liquidMaterialPropertyBlock ??= new MaterialPropertyBlock();
            
            List<Vector4> liquidColors = new List<Vector4>(bottle.InternalLiquid.Count);
            List<float> liquidsAmount = new List<float>(bottle.InternalLiquid.Count);

            for (int i = 0; i < bottle.InternalLiquid.Count; i++)
            {
                liquidColors.Add(bottle.InternalLiquid[i].TypeData.graphicColor);
                liquidsAmount.Add(bottle.InternalLiquid[i].Amount);
            }
            
            // Since the InternalLiquid property of the Bottle is stack transformed into a list we need to reverse
            // it to keep the same colors orders.
            
            liquidColors.Reverse();
            liquidsAmount.Reverse();

            // Since shaders can't have dynamic fields, we fill all the non used colors with
            // invalid liquids amount and clear colors.
            
            while (liquidColors.Count < ShaderMaxColors)
            {
                liquidColors.Add(Color.clear);
                liquidsAmount.Add(-1);
            }
            
            _liquidMaterialPropertyBlock.SetInt(BottleCapacity, bottle.BottleMaxCapacity);
            _liquidMaterialPropertyBlock.SetInt(BottleTotalLiquidAmount, bottle.TotalLiquidAmount);
            _liquidMaterialPropertyBlock.SetInt(LiquidCount, bottle.InternalLiquid.Count);
            _liquidMaterialPropertyBlock.SetVectorArray(LiquidColorArray, liquidColors);
            _liquidMaterialPropertyBlock.SetFloatArray(LiquidAmountArray, liquidsAmount);

            liquidRenderer.SetPropertyBlock(_liquidMaterialPropertyBlock);
        }

        /// <summary>
        /// This method is used to change a BottleObject collider state. <br/>
        /// Using this we can ignore certain collisions of a BottleObject.
        /// </summary>
        public void ChangeColliderState(bool newState) => _collider.enabled = newState;

        /// <summary>
        /// This method is used to return the BottleObject to the starting position.<br/>
        /// Used when a Bottle is mixed or when a game is reset.
        /// </summary>
        public void ReturnToStartPosition() => transform.position = _startPosition;
    }
}
