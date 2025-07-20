using System;
using Pong.Domain.Music;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Pong.View.UI
{
    public class SettingsMenuUI : IInitializable, IDisposable
    {
        private readonly Button _backButton;
        private readonly MenuSelector _menuSelector;
        private readonly Scrollbar _scrollbar;
        private readonly MusicPlayer _musicPlayer;

        [Inject]
        public SettingsMenuUI(Button backButton, MenuSelector menuSelector, Scrollbar scrollbar, MusicPlayer musicPlayer)
        {
            _backButton = backButton;
            _menuSelector = menuSelector;
            _scrollbar = scrollbar;
            _musicPlayer = musicPlayer;
        }

        public void Initialize()
        {
            _backButton.onClick.AddListener(OnBackClick);
            _scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
        }

        public void Dispose()
        {
            _backButton.onClick.RemoveListener(OnBackClick);
            _scrollbar.onValueChanged.RemoveListener(OnScrollbarValueChanged);
        }

        private void OnBackClick()
        {
            _menuSelector.Display("Main");
        }
        
        private void OnScrollbarValueChanged(float val)
        {
            _musicPlayer.SetVolume(val);
        }
    }
}
