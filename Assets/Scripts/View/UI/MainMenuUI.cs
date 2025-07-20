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
        private readonly Button _exitButton;
        private readonly SceneLoader _sceneLoader;

        [Inject]
        public MainMenuUI(Button playButton, Button exitButton, SceneLoader sceneLoader)
        {
            _playButton = playButton;
            _exitButton = exitButton;
            _sceneLoader = sceneLoader;
        }
        
        public void Initialize()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
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
        
        private void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}