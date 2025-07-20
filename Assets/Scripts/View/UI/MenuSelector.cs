using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pong.View.UI
{
    public class MenuSelector : IInitializable
    {
        private readonly Dictionary<string, Menu> _menus;

        [Inject]
        public MenuSelector(Dictionary<string, Menu> menus)
        {
            _menus = menus;
        }

        public void Initialize()
        {
            foreach (var kv in _menus)
            {
                kv.Value.Hide();
            }
        }

        public void Display(string menu)
        {
            if (!_menus.ContainsKey(menu))
            {
                Debug.LogError($"Menu '{menu}' not found.");
                return;
            }
            
            foreach (var kv in _menus)
            {
                kv.Value.Hide();
            }

            _menus[menu].Show();
        }
    }
}