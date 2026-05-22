namespace Main.CodeBase.Infrastructure.Services.SceneLoadService
{
    public interface ISceneLoader : ILoadingCurtain
    {
        void SetCurtain(LoadingCurtain loadingCurtain);
        void Load(SceneId sceneId);
    }
}