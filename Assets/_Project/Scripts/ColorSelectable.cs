using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace com.ColorSelect
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Collider))]
    public class ColorSelectable : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private ColorsService _colorsService;
        private ColorDescription? _color;

        public event Action<ColorSelectable, ColorDescription> OnSelected;

        [Inject]
        public void Construct(ColorsService colorsService)
        {
            _colorsService = colorsService;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_color == null)
            {
                return;
            }

            OnSelected?.Invoke(this, _color.Value);
        }

        public void SetColor(ColorDescription color)
        {
            _color = color;
            _meshRenderer.SetPropertyBlock(_colorsService.GetMaterialPropertyBlock(color));
        }

        protected virtual void OnSpawned(Vector3 position, ColorDescription color)
        {
            transform.localPosition = position;
            SetColor(color);
        }

        private void Reset()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public class Pool : MonoMemoryPool<Vector3, ColorDescription, ColorSelectable>
        {
            protected override void Reinitialize(
                Vector3 position,
                ColorDescription colorData,
                ColorSelectable item)
            {
                item.OnSpawned(position, colorData);
            }
        }
    }
}