using Main.CodeBase.Systems.WalletSystem;
using Zenject;

namespace Main.CodeBase.Infrastructure.ZenjectInstallers
{
    public class GlobalSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindWalletSystem();
        }

        private void BindWalletSystem()
        {
            Container.Bind<Wallet>().AsSingle();
        }
    }
}