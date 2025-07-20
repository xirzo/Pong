using Zenject;

namespace Pong.View.UI
{
    public class MenuStarter : IInitializable
    {
        private readonly MenuSelector _menuSelector;

        [Inject]
        public MenuStarter(MenuSelector menuSelector)
        {
            _menuSelector = menuSelector;
        }

        public void Initialize()
        {
            _menuSelector.Display("Main");
        }
    }
}