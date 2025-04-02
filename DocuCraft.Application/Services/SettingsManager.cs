namespace DocuCraft.Application.Services
{
    public sealed class SettingsManager
    {
        private static SettingsManager? _instance;
        private static readonly Lock _lock = new();

        public string Theme { get; private set; }
        public int FontSize { get; private set; }

        private SettingsManager()
        {
            // Значения по умолчанию
            Theme = "Light";
            FontSize = 12;
        }

        public static SettingsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock) _instance ??= new SettingsManager();
                }
                return _instance;
            }
        }

        public void SetTheme(string theme)
        {
            Theme = theme;
        }

        public void SetFontSize(int size)
        {
            FontSize = size;
        }
    }
}
