using System.Collections.Generic;
using UnityEngine;

namespace com.ColorSelect
{
    public class ColorsService
    {
        private static readonly int ColorProperty = Shader.PropertyToID("_InstanceColor");

        private readonly Config _config;
        private readonly Dictionary<ColorDescription, MaterialPropertyBlock> _propertyBlocks;
        private readonly int[] _indices;

        public ColorsService(Config config)
        {
            _config = config;
            // + 1 length for wrong selection Fx color
            _propertyBlocks = new(_config.ColorDescriptions.Length + 1);
            _indices = new int[_config.ColorDescriptions.Length];
        }

        public void Initialize()
        {
            for (int i = 0; i < _config.ColorDescriptions.Length; i++)
            {
                _indices[i] = i;
                AddPropertyBlock(_config.ColorDescriptions[i]);
            }
            
            AddPropertyBlock(_config.WrongSelectionFxColor);
        }

        private void AddPropertyBlock(ColorDescription colorDescription)
        {
            MaterialPropertyBlock block = new();
            block.SetColor(ColorProperty, colorDescription.Color);
            _propertyBlocks.Add(colorDescription, block);
        }

        public void GetRandomColors(ColorDescription[] blocks)
        {
            _indices.Shuffle();

            for (var i = 0; i < blocks.Length; i++)
            {
                blocks[i] = _config.ColorDescriptions[_indices[i]];
            }
        }

        public MaterialPropertyBlock GetMaterialPropertyBlock(ColorDescription color)
        {
            return _propertyBlocks[color];
        }
    }
}