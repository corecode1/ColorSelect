using UnityEngine;
using Zenject;

namespace com.ColorSelect
{
    public class CommonInstaller : MonoInstaller<CommonInstaller>
    {
        [SerializeField] private ColorSelectSceneRoot _colorSelectSceneRoot;
        [SerializeField] private Config _config;
        [SerializeField] private ColorSelectable _colorSelectablePrefab;
        [SerializeField] private MatchView _matchView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ColorSelectSceneRoot>().FromInstance(_colorSelectSceneRoot);
            Container.Bind<Config>().FromInstance(_config);
            Container.Bind<ILogger>().To<Logger>().AsSingle();
            Container.Bind<ColorSelectApp>().AsSingle();
            Container.Bind<DefaultGameMode>().AsSingle();
            Container.Bind<ColorsService>().AsSingle();
            Container.Bind<MatchView>().FromInstance(_matchView);

            Container.BindMemoryPool<ColorSelectable, ColorSelectable.Pool>()
                .WithInitialSize(_config.SelectablesCount)
                .FromComponentInNewPrefab(_colorSelectablePrefab)
                .UnderTransformGroup("ColorSelectables");

            Container.BindMemoryPool<MatchModel, MatchModel.Pool>()
                .WithInitialSize(_config.ExpectedMatchesPoolSize);
        }
    }
}