using Main.CodeBase.MainScene.Screens.BusinessScreen.Manager;
using Main.CodeBase.MainScene.Screens.BusinessScreen.WorkList;
using Main.CodeBase.StaticData.Repositories;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Main.CodeBase.Infrastructure.ZenjectInstallers
{
    public class BusinessScreenInstaller : MonoInstaller
    {
        [SerializeField] private BusinessListsPrefabsRepository _businessListsPrefabsRepository;
        [SerializeField] private Transform _businessListsParent;
        [SerializeField] private AssetReferenceGameObject _managerPanelPrefab;
        
        public override void InstallBindings()
        {
            BindBusinessListFactory();
            BindManagerPanelFactory();
        }

        private void BindManagerPanelFactory()
        {
            Container.BindInterfacesAndSelfTo<ManagerPanelFactory>().AsSingle()
                .WithArguments(_managerPanelPrefab);
        }

        private void BindBusinessListFactory()
        {
            Container.BindInterfacesAndSelfTo<WorkListFactory>().AsSingle()
                .WithArguments(_businessListsPrefabsRepository, _businessListsParent);
        }
    }
}