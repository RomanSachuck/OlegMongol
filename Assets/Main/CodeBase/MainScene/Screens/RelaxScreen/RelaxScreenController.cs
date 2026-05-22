namespace Main.CodeBase.MainScene.Screens.RelaxScreen
{
    public class RelaxScreenController : IScreenController
    {
        private RelaxScreenViewAbstract _viewAbstract;

        public bool Created { get; private set; }

        public void Initialize(ScreenViewAbstract screenViewAbstract)
        {
            _viewAbstract = screenViewAbstract as RelaxScreenViewAbstract;
            Created = true;
        }
        
        public void Open()
        {
            
        }

        public void Close()
        {
            
        }
    }
}