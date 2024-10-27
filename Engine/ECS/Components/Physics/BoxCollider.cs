using Key_Quest.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Key_Quest.Engine.ECS.Components.Physics;

public class BoxCollider : Component
{
    public Vector2 Size { get; set; }

    private bool debugMode = false;

    public BoxCollider(Vector2 size)
    {
        Size = size;
    }
    
    public Rectangle GetBounds()
    {
        return new Rectangle(
            (int)(GameObject.Transform.Position.X),
            (int)(GameObject.Transform.Position.Y),
            (int)Size.X,
            (int)Size.Y
        );
    }

    public bool CheckCollision(BoxCollider other)
    {
        return GetBounds().Intersects(other.GetBounds());
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (KeyboardHandler.IsPressed(Keys.Tab))
            debugMode = !debugMode;
    }

    public override void OnDraw()
    {
        base.OnDraw();

        if (debugMode)
        {
            Rectangle colliderRect = new Rectangle((int)GameObject.Transform.Position.X - (int)Config.CameraX, (int)GameObject.Transform.Position.Y - (int)Config.CameraY, (int)Size.X, (int)Size.Y);
            Config.Batch.Draw(Config.PixelTexture, colliderRect, Color.Blue * 0.5f);
        }
    }
}