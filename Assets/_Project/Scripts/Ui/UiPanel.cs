using UnityEngine;

namespace com.ColorSelect
{
    public abstract class UiPanel : MonoBehaviour
    {
        public virtual void Activate(bool active)
        {
            gameObject.SetActive(active);
        }

        public virtual void Initialize()
        {
        }

        public virtual void Dispose()
        {
        }

        private void Awake()
        {
            Activate(false);
        }
    }
}