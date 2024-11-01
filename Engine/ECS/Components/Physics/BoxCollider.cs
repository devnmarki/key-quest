using Key_Quest.Engine.Input;
using Key_Quest.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Key_Quest.Engine.ECS.Components.Physics;

public class BoxCollider : Component
{
    public Vector2 Size { get; set; }
    public Vector2 Offset { get; set; }

    public Sides CollisionSide { get; set; } = Sides.None;
    
    public BoxCollider(Vector2 size)
    {
        Size = size;
        Offset = Vector2.Zero;
    }

    public BoxCollider(Vector2 size, Vector2 offset)
    {
        Size = size;
        Offset = offset;
    }
    
    public Rectangle GetBounds()
    {
        return new Rectangle(
            (int)(GameObject.Transform.Position.X + Offset.X),
            (int)(GameObject.Transform.Position.Y + Offset.Y),
            (int)Size.X,
            (int)Size.Y
        );
    }

    public bool CheckCollision(BoxCollider other)
    {
        return GetBounds().Intersects(other.GetBounds());
    }

    public override void OnDraw()
    {
        base.OnDraw();

        if (Config.DebugMode)
        {
            Rectangle colliderRect = new Rectangle(GetBounds().Left - (int)Config.CameraX, GetBounds().Top - (int)Config.CameraY, (int)Size.X, (int)Size.Y);
            Config.Batch.Draw(Config.PixelTexture, colliderRect, Color.Red * 0.5f);
        }
    }
}