using System;
using UnityEngine.UI;
using Zenject;

namespace Pong.View.UI
{
    public class SettingsMenuUI : IInitializable, IDisposable
    {
        private readonly Button _backButton;
        private readonly MenuSelector _menuSelector;

        [Inject]
        public SettingsMenuUI(Button backButton, MenuSelector menuSelector)
        {
            _backButton = backButton;
            _menuSelector = menuSelector;
        }

        public void Initialize()
        {
            _backButton.onClick.AddListener(OnBackClick);
        }

        public void Dispose()
        {
            _backButton.onClick.RemoveListener(OnBackClick);
        }

        private void OnBackClick()
        {
            _menuSelector.Display("Main");
        }
    }
}
