using Main.CodeBase.Systems.WalletSystem;
using Main.CodeBase.Systems.WorkSystem;
using Zenject;

namespace Main.CodeBase.Infrastructure.ZenjectInstallers
{
    public class GlobalSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindWalletSystem();
            BindWorkSystem();
        }

        private void BindWorkSystem()
        {
            Container.BindInterfacesAndSelfTo<WorkController>().AsSingle();
        }

        private void BindWalletSystem()
        {
            Container.BindInterfacesAndSelfTo<Wallet>().AsSingle();
        }
    }
}