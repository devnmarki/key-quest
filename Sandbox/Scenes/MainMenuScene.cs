using System;
using Key_Quest.Engine.SceneSystem;

namespace Key_Quest.Sandbox.Scenes;

public class MainMenuScene : Scene
{
    public override void Start()
    {
        base.Start();
        
        Console.WriteLine("Hello main menu!");
    }
}