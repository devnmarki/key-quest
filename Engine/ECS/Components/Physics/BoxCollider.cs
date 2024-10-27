using Microsoft.Xna.Framework;

namespace Key_Quest.Engine.ECS.Components.Physics;

public class BoxCollider : Component
{
    public Vector2 Size { get; set; }

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
}