using System;
using System.Numerics;
using Key_Quest.Engine.ECS.Components.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Key_Quest.Engine.ECS.Components;

public class SpriteRenderer : Component
{
    private Texture2D _sprite = null;
    
    private bool _flip = false;
    private float _layerDepth = 0f;
    
    private Spritesheet _spritesheet = null;
    private int _spriteIndex = 0;
    
    public bool Flip
    {
        get => _flip;
        set => _flip = value;
    }

    public float LayerDepth
    {
        get => _layerDepth;
        set => _layerDepth = value;
    }

    public Spritesheet Spritesheet
    {
        get => _spritesheet;
        set => _spritesheet = value;
    }

    public int SpriteIndex
    {
        get => _spriteIndex;
        set => _spriteIndex = value;
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

        if (GameObject.HasComponent<Animator>()) return;
        
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
            _layerDepth);
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
            _layerDepth);
    }
    
    public void DrawAnimation(Spritesheet spritesheet, Rectangle sprite)
    {
        Config.Batch.Draw(
            spritesheet.Texture,
            GameObject.Transform.Position - new Vector2(Config.CameraX, Config.CameraY),
            sprite,
            Color.White,
            0f,
            Vector2.Zero,
            GameObject.Transform.Scale,
            _flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            _layerDepth);
    }
}