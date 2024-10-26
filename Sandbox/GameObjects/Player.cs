using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Key_Quest.Sandbox.GameObjects;

public class Player : GameObject
{
    private Texture2D _texture;
    
    protected override void Init()
    {
        base.Init();
        
        _texture = Config.Content.Load<Texture2D>("sprites/test/bob");

        Transform.Position = new Vector2(200, 500);
        Transform.Scale = new Vector2(6, 6);
        
        AddComponent(new SpriteRenderer(_texture));
    }
}