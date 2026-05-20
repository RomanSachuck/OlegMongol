using Main.CodeBase.Infrastructure.Services.APIService;
using Main.CodeBase.Infrastructure.Services.AssetManagment;
using Main.CodeBase.Infrastructure.Services.SceneLoadService;
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
            Container.BindInterfacesTo<APIClient>().AsSingle();
        }
    }
}