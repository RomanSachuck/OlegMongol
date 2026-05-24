using Main.CodeBase.Infrastructure.Services.APIService;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.Infrastructure.Services.ConfigsService;
using Main.CodeBase.Infrastructure.Services.LocalizationService;
using Main.CodeBase.Infrastructure.Services.PersistentProgressService;
using Main.CodeBase.Infrastructure.Services.SceneLoadService;
using Main.CodeBase.Infrastructure.Services.TimeService;
using Zenject;

namespace Main.CodeBase.Infrastructure.ZenjectInstallers
{
    public class GlobalServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAPIClient();
            BindSceneLoader();
            BindAssetProvider();
            BindLocalization();
            BindConfigs();
            BindPersistentProgress();
            BindTimeService();
        }

        private void BindTimeService()
        {
            Container.BindInterfacesAndSelfTo<TimeService>().AsSingle();
        }

        private void BindPersistentProgress()
        {
            Container.BindInterfacesAndSelfTo<PersistentProgress>().AsSingle();
        }

        private void BindConfigs()
        {
            Container.BindInterfacesAndSelfTo<LocalConfigs>().AsSingle();
        }

        private void BindLocalization()
        {
            Container.BindInterfacesAndSelfTo<Localization>().AsSingle();
        }

        private void BindAssetProvider()
        {
            Container.BindInterfacesAndSelfTo<AssetsProvider>().AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
        }

        private void BindAPIClient()
        {
            Container.BindInterfacesAndSelfTo<APIClientYG>().AsSingle();
        }
    }
}