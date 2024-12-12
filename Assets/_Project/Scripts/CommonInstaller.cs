using UnityEngine;
using Zenject;

namespace com.ColorSelect
{
    public class CommonInstaller : MonoInstaller<CommonInstaller>
    {
        [SerializeField] private ColorSelectSceneRoot _colorSelectSceneRoot;
        [SerializeField] private Config _config;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ColorSelectSceneRoot>().FromInstance(_colorSelectSceneRoot);
            Container.Bind<Config>().FromInstance(_config);
            Container.Bind<ILogger>().To<Logger>().AsSingle();
            Container.Bind<ColorSelectApp>().AsSingle();
        }
    }
}