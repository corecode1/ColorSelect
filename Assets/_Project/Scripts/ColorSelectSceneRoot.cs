using UnityEngine;
using Zenject;

namespace com.ColorSelect
{
    public class ColorSelectSceneRoot : MonoBehaviour, IInitializable
    {
        private ColorSelectApp _app;

        [Inject]
        public void Construct(ColorSelectApp app)
        {
            _app = app;
        }

        public void Initialize()
        {
            _app.Initialize();
            _app.StartGame();
        }
    }
}