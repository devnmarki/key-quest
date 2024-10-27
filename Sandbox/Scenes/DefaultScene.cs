using System;
using System.Diagnostics;
using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Engine.SceneSystem;
using Key_Quest.Sandbox.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Key_Quest.Sandbox.Scenes;

public class DefaultScene : Scene
{
    public override void Start()
    {
        base.Start();
        
        AddGameObject(new Player());

        GameObject bob = new GameObject();
        bob.Transform.Position = new Vector2(200, 200);
        bob.Transform.Scale = new Vector2(6);
        bob.AddComponent(new SpriteRenderer(Config.Content.Load<Texture2D>("sprites/test/bob")));
        bob.AddComponent(new Rigidbody(1f, false));
        bob.AddComponent(new BoxCollider(new Vector2(48, 48)));
        AddGameObject(bob);
        
        GameObject bob2 = new GameObject();
        bob2.Transform.Position = new Vector2(248, 200);
        bob2.Transform.Scale = new Vector2(6);
        bob2.AddComponent(new SpriteRenderer(Config.Content.Load<Texture2D>("sprites/test/bob")));
        bob2.AddComponent(new Rigidbody(1f, false));
        bob2.AddComponent(new BoxCollider(new Vector2(48, 48)));
        AddGameObject(bob2);
    }

    public override void Update()
    {
        base.Update();
    }
}