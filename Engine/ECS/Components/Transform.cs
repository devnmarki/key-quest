using Microsoft.Xna.Framework;

namespace Key_Quest.Engine.ECS.Components;

public class Transform : Component
{
    public Vector2 Position;
    public Vector2 Scale;
    public float Rotation;
    
    public Transform() : base()
    {
        Position = Vector2.Zero;
        Scale = Vector2.One;
        Rotation = 0f;
    }
}