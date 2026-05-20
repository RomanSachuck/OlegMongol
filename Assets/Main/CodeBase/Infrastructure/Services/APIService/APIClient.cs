namespace Main.CodeBase.Infrastructure.Services.APIService
{
    public class APIClient : IAPIClient
    {
        private int _yieldCount;
        
        public bool IsInitialized 
        {
            get
            {
                _yieldCount++;
                return _yieldCount >= 250; //Это написано для теста задержки инициализации SDK
            }
        }
    }
}