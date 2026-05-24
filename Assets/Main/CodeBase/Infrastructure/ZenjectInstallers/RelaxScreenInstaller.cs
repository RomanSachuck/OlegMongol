using Main.CodeBase.MainScene.Screens.RelaxScreen;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.Infrastructure.ZenjectInstallers
{
    public class RelaxScreenInstaller : MonoInstaller
    {
        [SerializeField] private ScreenBackgroundsRepository _screenBackgroundsRepository;
        
        public override void InstallBindings()
        {
            BindHouseBgLoader();
        }

        private void BindHouseBgLoader()
        {
            Container.BindInterfacesAndSelfTo<HouseBgLoader>().AsSingle()
                .WithArguments(_screenBackgroundsRepository);
        }
    }
}