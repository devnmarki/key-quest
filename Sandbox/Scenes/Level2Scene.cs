using Key_Quest.Engine;
using Key_Quest.Engine.LevelEditor;
using Key_Quest.Engine.SceneSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace Key_Quest.Sandbox.Scenes;

public class Level2Scene : Scene
{
    private TmxMap _map;
    private TilemapManager _tilemapManager;
    
    public override void Start()
    {
        base.Start();
        
        _map = new TmxMap("../../../Content/levels/level_2.tmx");
        Texture2D tileset = Config.Content.Load<Texture2D>("sprites/tilesets/dungeon_tileset");
        
        _tilemapManager = new TilemapManager(_map, tileset);
        _tilemapManager.CreateColliders(Vector2.Zero);
        
        _tilemapManager.LoadGameObjects();
    }
    
    public override void Render()
    {
        base.Render();
        
        _tilemapManager.Draw(Vector2.Zero, 0.75f);
    }
}