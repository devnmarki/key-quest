using System;
using System.Diagnostics;
using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Engine.LevelEditor;
using Key_Quest.Engine.SceneSystem;
using Key_Quest.Sandbox.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace Key_Quest.Sandbox.Scenes;

public class DefaultScene : Scene
{
    private TmxMap _map;
    private TilemapManager _tilemapManager;
    
    public override void Start()
    {
        base.Start();
        
        _map = new TmxMap("../../../Content/levels/test_level.tmx");
        Texture2D tileset = Config.Content.Load<Texture2D>("sprites/tilesets/tileset");
        
        _tilemapManager = new TilemapManager(_map, tileset);
        _tilemapManager.CreateColliders(new Vector2(0, 0));
        
        AddGameObject(new Knight(new Vector2(200, 50)));
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Render()
    {
        base.Render();
        
        _tilemapManager.Draw(new Vector2(0, 0));
    }
}