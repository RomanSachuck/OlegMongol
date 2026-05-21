using Main.CodeBase.Infrastructure.Services.APIService;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.Infrastructure.Services.LocalizationService;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;
using Main.CodeBase.Infrastructure.Services.SceneLoadService;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.Infrastructure.ZenjectInstallers
{
    public class GlobalServicesInstaller : MonoInstaller
    {
        [SerializeField] private StartingProgressRepository _startingProgressRepository;
        
        public override void InstallBindings()
        {
            BindAPIClient();
            BindSceneLoader();
            BindAssetProvider();
            BindLocalization();
            BindConfigs();
            BindPersistentProgress();
        }

        private void BindPersistentProgress()
        {
            Container.BindInterfacesTo<PersistentProgress>().AsSingle();
        }

        private void BindConfigs()
        {
            Container.BindInterfacesTo<LocalConfigs>()
                .AsSingle().WithArguments(_startingProgressRepository);
        }

        private void BindLocalization()
        {
            Container.Bind<Localization>().AsSingle();
        }

        private void BindAssetProvider()
        {
            Container.BindInterfacesTo<AssetsProvider>().AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
        }

        private void BindAPIClient()
        {
            Container.BindInterfacesTo<APIClientYG>().AsSingle();
        }
    }
}