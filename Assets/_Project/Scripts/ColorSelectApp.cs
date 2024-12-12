namespace com.ColorSelect
{
    public class ColorSelectApp
    {
        private readonly ILogger _logger;

        public ColorSelectApp(ILogger logger)
        {
            _logger = logger;
        }

        public void Initialize()
        {
        }

        public void StartGame()
        {
            _logger.LogRegular("Starting game");
        }
    }
}