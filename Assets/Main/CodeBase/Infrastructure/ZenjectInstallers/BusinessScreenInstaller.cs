using Main.CodeBase.MainScene.Screens.BusinessScreen;
using Main.CodeBase.MainScene.Screens.BusinessScreen.WorkList;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using Zenject;

namespace Main.CodeBase.Infrastructure.ZenjectInstallers
{
    public class BusinessScreenInstaller : MonoInstaller
    {
        [SerializeField] private BusinessListsPrefabsRepository _businessListsPrefabsRepository;
        [SerializeField] private Transform _businessListsParent;
        
        public override void InstallBindings()
        {
            BindBusinessListFactory();
        }

        private void BindBusinessListFactory()
        {
            Container.Bind<WorkListFactory>().AsSingle()
                .WithArguments(_businessListsPrefabsRepository, _businessListsParent);
        }
    }
}