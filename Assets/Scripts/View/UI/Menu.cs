using UnityEngine;

namespace Pong.View.UI
{
    [System.Serializable]
    public class Menu
    {
        [SerializeField] private GameObject menuObject;
        public void Show()
        {
            menuObject.SetActive(true);
        }

        public void Hide()
        {
            menuObject.SetActive(false);
        }
    }
}