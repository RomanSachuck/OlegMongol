namespace Main.CodeBase.MainScene.Screens.InvestmentsScreen
{
    public class InvestmentsScreenController : ScreenControllerAbstract
    {
        private InvestmentsScreenView _view;
        
        public override void Initialize(ScreenViewAbstract view)
        {
            base.Initialize(view);
            
            _view = view as InvestmentsScreenView;
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