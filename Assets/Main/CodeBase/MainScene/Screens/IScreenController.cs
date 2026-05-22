namespace Main.CodeBase.MainScene.Screens
{
    public interface IScreenController
    {
        bool Created  { get; }
        void Initialize(ScreenViewAbstract viewAbstract);
        void Open();
        void Close();
    }
}