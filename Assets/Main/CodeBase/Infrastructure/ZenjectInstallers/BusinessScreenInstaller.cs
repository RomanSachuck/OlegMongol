using Main.CodeBase.MainScene.Screens.BusinessScreen.BusinessUpgrade;
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
        [SerializeField] private AssetReferenceGameObject _businessUpgradePanelPrefab;
        
        public override void InstallBindings()
        {
            BindBusinessListFactory();
            BindBusinessUpgradePanelFactory();
        }

        private void BindBusinessUpgradePanelFactory()
        {
            Container.BindInterfacesAndSelfTo<BusinessUpgradePanelFactory>().AsSingle()
                .WithArguments(_businessUpgradePanelPrefab);
        }

        private void BindBusinessListFactory()
        {
            Container.BindInterfacesAndSelfTo<WorkListFactory>().AsSingle()
                .WithArguments(_businessListsPrefabsRepository, _businessListsParent);
        }
    }
}