using System;
using System.Collections.Generic;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;

namespace Key_Quest.Engine.SceneSystem;

public class SceneManager
{
    private static Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
    private static Scene _currentScene = null;

    public static Scene CurrentScene
    {
        get => _currentScene;
    }

    public static void AddScene(string sceneName, Scene scene)
    {
        _scenes.Add(sceneName, scene);
    }

    public static void RefreshCurrentScene()
    {
        _currentScene.GameObjects.Clear();
        _currentScene.Start();
        foreach (GameObject gameObject in _currentScene.GameObjects)
        {
            foreach (Component component in gameObject.Components)
            {
                component.OnStart();
            }
        }
    }

    public static void ChangeScene(string sceneName)
    {
        Scene newScene = _scenes[sceneName];
        if (newScene != null)
        {
            if (_currentScene != newScene)
            {
                _currentScene = newScene;
                RefreshCurrentScene();
            }
        }
        else
        {
            throw new Exception("Scene " + sceneName + " is not found!");
        }
    }

    public static void UpdateCurrentScene()
    {
        _currentScene?.Update();
    }

    public static void RenderCurrentScene()
    {
        _currentScene?.Render();
    }
}