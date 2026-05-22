namespace Main.CodeBase.MainScene.Screens.BusinessScreen
{
    public class BusinessScreenController : ScreenControllerAbstract
    {
        private BusinessScreenView _view;
        
        public override void Initialize(ScreenViewAbstract view)
        {
            base.Initialize(view);
            
            _view = view as BusinessScreenView;
        }

        public override void Open()
        {
            _view.gameObject.SetActive(true);
        }

        public override void Close()
        {
            _view.gameObject.SetActive(false);
        }
    }
}