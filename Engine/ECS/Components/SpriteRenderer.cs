using System;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Key_Quest.Engine.ECS.Components;

public class SpriteRenderer : Component
{
    private Texture2D _sprite = null;
    private bool _flip = false;

    public bool Flip
    {
        get => _flip;
        set => _flip = value;
    }

    public SpriteRenderer(Texture2D sprite)
    {
        _sprite = sprite;
    }

    public override void OnDraw()
    {
        base.OnDraw();

        if (_sprite == null) return;
        
        if (!_flip)
        {
            Config.Batch.Draw(_sprite, new Vector2(GameObject.Transform.Position.X - Config.CameraX, GameObject.Transform.Position.Y - Config.CameraY), null, Color.White, 0f, Vector2.Zero, GameObject.Transform.Scale, SpriteEffects.None, 0f);
        }
        else
        {
            Config.Batch.Draw(_sprite, new Vector2(GameObject.Transform.Position.X - Config.CameraX, GameObject.Transform.Position.Y - Config.CameraY), null, Color.White, 0f, Vector2.Zero, GameObject.Transform.Scale, SpriteEffects.FlipHorizontally, 0f);
        }
            
    }
}