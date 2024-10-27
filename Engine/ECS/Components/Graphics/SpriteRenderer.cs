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
    
    private Spritesheet _spritesheet = null;
    private int _spriteIndex = 0;
    
    public bool Flip
    {
        get => _flip;
        set => _flip = value;
    }

    public SpriteRenderer(Texture2D sprite)
    {
        _sprite = sprite;
        _spritesheet = null;
    }

    public SpriteRenderer(Spritesheet spritesheet, int sprite)
    {
        _spritesheet = spritesheet;
        _spriteIndex = sprite;
        _sprite = null;
    }
    
    public override void OnDraw()
    {
        base.OnDraw();

        if (_sprite != null)
        {
            DrawSingleSprite();
        }
        else if (_spritesheet != null && _spriteIndex >= 0 && _spriteIndex < _spritesheet.Sprites.Count)
        {
            DrawFromSpritesheet();
        }
    }

    private void DrawSingleSprite()
    {
        Config.Batch.Draw(
            _sprite,
            GameObject.Transform.Position - new Vector2(Config.CameraX, Config.CameraY),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            GameObject.Transform.Scale,
            _flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            0f);
    }

    private void DrawFromSpritesheet()
    {
        Config.Batch.Draw(
            _spritesheet.Texture,
            GameObject.Transform.Position - new Vector2(Config.CameraX, Config.CameraY),
            _spritesheet.Sprites[_spriteIndex],
            Color.White,
            0f,
            Vector2.Zero,
            GameObject.Transform.Scale,
            _flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            0f);
    }
}