namespace Pong.Domain.Scenes
{
    public readonly struct SceneLoadRequest
    {
        public readonly string SceneName;
        public readonly SceneLoadMode Mode;
        public readonly bool ActivateOnLoad;

        public SceneLoadRequest(string sceneName, SceneLoadMode mode = SceneLoadMode.Single, bool activateOnLoad = true)
        {
            SceneName = sceneName;
            Mode = mode;
            ActivateOnLoad = activateOnLoad;
        }
    }
}

