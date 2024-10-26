using System;
using System.Diagnostics;
using Key_Quest.Engine.SceneSystem;
using Key_Quest.Sandbox.GameObjects;

namespace Key_Quest.Sandbox.Scenes;

public class DefaultScene : Scene
{
    public override void Start()
    {
        base.Start();
        
        AddGameObject(new Player());
    }

    public override void Update()
    {
        base.Update();
    }
}