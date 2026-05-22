using Cysharp.Threading.Tasks;

namespace Main.CodeBase.MainScene.Screens
{
    public abstract class ScreenControllerAbstract
    {
        public bool Created  { get; private set; }

        public virtual UniTask Initialize(ScreenViewAbstract view)
        {
            Created = true;
            return UniTask.CompletedTask;
        }
        public abstract void Open();
        public abstract void Close();
    }
}