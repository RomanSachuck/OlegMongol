namespace Main.CodeBase.MainScene.Screens
{
    public abstract class ScreenControllerAbstract
    {
        public bool Created  { get; private set; }

        public virtual void Initialize(ScreenViewAbstract view)
        {
            Created = true;
        }
        public abstract void Open();
        public abstract void Close();
    }
}