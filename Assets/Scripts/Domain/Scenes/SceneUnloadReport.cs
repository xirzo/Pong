namespace Pong.Domain.Scenes
{
    public readonly struct SceneUnloadReport
    {
        public readonly string SceneName;
        public readonly float Progress;
        public readonly bool IsDone;

        public SceneUnloadReport(string sceneName, float progress, bool isDone)
        {
            SceneName = sceneName;
            Progress = progress;
            IsDone = isDone;
        }
    }
}

