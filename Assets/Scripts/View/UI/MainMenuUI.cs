using Pong.Domain.Scenes;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pong.View.UI
{
    public class MainMenuUI : IInitializable, IDisposable
    {
        private readonly Button _playButton;
        private readonly Button _settingsButton;
        private readonly Button _exitButton;
        private readonly SceneLoader _sceneLoader;
        private readonly MenuSelector _menuSelector;

        [Inject]
        public MainMenuUI(Button playButton, Button settingsButton, Button exitButton, SceneLoader sceneLoader, MenuSelector menuSelector)
        {
            _playButton = playButton;
            _settingsButton = settingsButton;
            _exitButton = exitButton;
            _sceneLoader = sceneLoader;
            _menuSelector = menuSelector;
        }
        
        public void Initialize()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        public void Dispose()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }
        
        private void OnPlayButtonClicked()
        {
            _sceneLoader.LoadNext();
        }

        private void OnSettingsButtonClicked()
        {
            _menuSelector.Display("Settings");
        }
        
        private void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}