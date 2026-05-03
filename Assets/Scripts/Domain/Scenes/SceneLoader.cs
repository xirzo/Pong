using System;
using R3;
using UnityEngine.SceneManagement;
using Zenject;

namespace Pong.Domain.Scenes
{
    public sealed class SceneLoader
    {
        private readonly ZenjectSceneLoader _zenjectSceneLoader;

        public SceneLoader(ZenjectSceneLoader zenjectSceneLoader)
        {
            _zenjectSceneLoader = zenjectSceneLoader;
        }

        public Observable<SceneLoadReport> LoadScene(SceneLoadRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SceneName))
            {
                throw new ArgumentException("Scene name is required.", nameof(request));
            }

            var subject = new Subject<SceneLoadReport>();
            var mode = request.Mode == SceneLoadMode.Additive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            var operation = _zenjectSceneLoader.LoadSceneAsync(request.SceneName, mode);

            if (operation == null)
            {
                throw new InvalidOperationException($"Failed to start loading scene '{request.SceneName}'.");
            }

            operation.allowSceneActivation = request.ActivateOnLoad;

            operation.completed += _ =>
            {
                subject.OnNext(new SceneLoadReport(request.SceneName, operation.progress, true));
                subject.OnCompleted();
            };

            return subject;
        }

        public Observable<SceneUnloadReport> UnloadScene(string sceneName)
        {
            if (string.IsNullOrWhiteSpace(sceneName))
            {
                throw new ArgumentException("Scene name is required.", nameof(sceneName));
            }

            var subject = new Subject<SceneUnloadReport>();
            var scene = SceneManager.GetSceneByName(sceneName);
            if (!scene.IsValid())
            {
                throw new InvalidOperationException($"Scene '{sceneName}' is not loaded or does not exist.");
            }

            var operation = SceneManager.UnloadSceneAsync(scene);
            if (operation == null)
            {
                throw new InvalidOperationException($"Failed to start unloading scene '{sceneName}'.");
            }

            operation.completed += _ =>
            {
                subject.OnNext(new SceneUnloadReport(sceneName, operation.progress, true));
                subject.OnCompleted();
            };

            return subject;
        }
    }
}
