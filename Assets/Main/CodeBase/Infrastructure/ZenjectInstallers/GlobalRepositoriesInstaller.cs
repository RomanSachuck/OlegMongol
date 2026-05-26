using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.Infrastructure.ZenjectInstallers
{
    public class GlobalRepositoriesInstaller : MonoInstaller
    {
        [SerializeField] private IconsRepository _iconsRepository;
        
        public override void InstallBindings()
        {
            BindIconsRepository();
        }

        private void BindIconsRepository()
        {
            Container.BindInterfacesAndSelfTo<IconsRepository>().FromInstance(_iconsRepository).AsSingle();
        }
    }
}