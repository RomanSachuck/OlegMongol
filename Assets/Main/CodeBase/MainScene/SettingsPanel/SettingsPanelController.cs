using Main.CodeBase.Buttons;

namespace Main.CodeBase.MainScene.SettingsPanel
{
    public class SettingsPanelController
    {
        private SimpleButton _settingsButton;

        public void Initialize(SimpleButton settingsButton)
        {
            _settingsButton = settingsButton;
        }
    }
}