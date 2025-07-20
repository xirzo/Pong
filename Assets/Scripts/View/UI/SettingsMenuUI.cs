using System;
using Pong.Domain.Music;
using TMPro;
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
        private readonly Toggle _toggle;
        private readonly TMP_Dropdown _resolutionDropdown;
        private readonly MusicPlayer _musicPlayer;

        private Resolution[] _availableResolutions;

        [Inject]
        public SettingsMenuUI(Button backButton, MenuSelector menuSelector, Scrollbar scrollbar, Toggle toggle, TMP_Dropdown resolutionDropdown, MusicPlayer musicPlayer)
        {
            _backButton = backButton;
            _menuSelector = menuSelector;
            _scrollbar = scrollbar;
            _toggle = toggle;
            _resolutionDropdown = resolutionDropdown;
            _musicPlayer = musicPlayer;
        }

        public void Initialize()
        {
            _availableResolutions = Screen.resolutions;
            PopulateResolutionDropdown();

            _backButton.onClick.AddListener(OnBackClick);
            _scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
            _toggle.onValueChanged.AddListener(OnToggleChanged);
            _resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);

            _toggle.isOn = Screen.fullScreen;
        }

        public void Dispose()
        {
            _backButton.onClick.RemoveListener(OnBackClick);
            _scrollbar.onValueChanged.RemoveListener(OnScrollbarValueChanged);
            _toggle.onValueChanged.RemoveListener(OnToggleChanged);
            _resolutionDropdown.onValueChanged.RemoveListener(OnResolutionChanged);
        }

        private void OnBackClick()
        {
            _menuSelector.Display("Main");
        }

        private void OnScrollbarValueChanged(float val)
        {
            _musicPlayer.SetVolume(val);
        }

        private void OnToggleChanged(bool val)
        {
            Screen.fullScreen = val;
        }

        private void OnResolutionChanged(int index)
        {
            var selectedResolution = _availableResolutions[index];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }

        private void PopulateResolutionDropdown()
        {
            _resolutionDropdown.ClearOptions();

            var options = new System.Collections.Generic.List<string>();
            
            foreach (var resolution in _availableResolutions)
            {
                options.Add($"{resolution.width} x {resolution.height} @ {resolution.refreshRateRatio.numerator}/{resolution.refreshRateRatio.denominator}Hz");
            }

            _resolutionDropdown.AddOptions(options);

            var currentResolution = Screen.currentResolution;
            var currentIndex = 0;
            
            for (var i = 0; i < _availableResolutions.Length; i++)
            {
                if (_availableResolutions[i].width == currentResolution.width &&
                    _availableResolutions[i].height == currentResolution.height &&
                    _availableResolutions[i].refreshRateRatio.Equals(currentResolution.refreshRateRatio))
                {
                    currentIndex = i;
                    break;
                }
            }

            _resolutionDropdown.value = currentIndex;
            _resolutionDropdown.RefreshShownValue();
        }
    }
}
