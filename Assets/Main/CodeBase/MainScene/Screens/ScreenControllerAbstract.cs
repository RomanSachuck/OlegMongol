using Cysharp.Threading.Tasks;

namespace Main.CodeBase.MainScene.Screens
{
    public abstract class ScreenControllerAbstract
    {
        public abstract UniTask Initialize(ScreenViewAbstract view);
        public abstract void Open();
        public abstract void Close();
    }
}