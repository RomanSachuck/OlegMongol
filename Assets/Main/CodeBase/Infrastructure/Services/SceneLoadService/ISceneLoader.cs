namespace Main.CodeBase.Infrastructure.Services.SceneLoadService
{
    public interface ISceneLoader
    {
        void SetCurtain(LoadingCurtain loadingCurtain);
        void Load(SceneId sceneId);
        void ShowCurtain();
        void HideCurtain();
    }
}