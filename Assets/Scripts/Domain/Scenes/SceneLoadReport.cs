namespace Pong.Domain.Scenes
{
    public readonly struct SceneLoadReport
    {
        public readonly string SceneName;
        public readonly float Progress;
        public readonly bool IsDone;

        public SceneLoadReport(string sceneName, float progress, bool isDone)
        {
            SceneName = sceneName;
            Progress = progress;
            IsDone = isDone;
        }
    }
}

