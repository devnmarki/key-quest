using System;
using Key_Quest.Engine.SceneSystem;
using Key_Quest.Engine.Utils;
using Microsoft.Xna.Framework;

namespace Key_Quest.Engine.ECS.Components.Physics;

public class Rigidbody : Component
{
    public Vector2 Velocity = Vector2.Zero;
    public float Mass { get; set; } = 1f;
    public bool UseGravity { get; set; } = true;

    public Rigidbody(float mass = 1f, bool useGravity = true)
    {
        Mass = mass;
        UseGravity = useGravity;
    }

    public void ApplyForce(Vector2 force)
    {
        // F = ma -> a = F / m
        Vector2 acceleration = force / Mass;
        Velocity += acceleration;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (UseGravity)
        {
            ApplyForce(Config.GravityScale * Mass);
        }
        
        GameObject.Transform.Position.X += Velocity.X * (float)Config.Time.ElapsedGameTime.TotalSeconds;
        CheckCollision(Axis.Horizontal);
        
        GameObject.Transform.Position.Y += Velocity.Y * (float)Config.Time.ElapsedGameTime.TotalSeconds;
        CheckCollision(Axis.Vertical);
        
        Velocity *= 0.98f;
    }

    private void CheckCollision(Axis axis)
    {
        foreach (var gameObject in SceneManager.CurrentScene.GameObjects)
        {
            if (gameObject != GameObject)
            {
                foreach (var collider in GameObject.GetColliders())
                {
                    foreach (var otherCollider in gameObject.GetColliders())
                    {
                        if (collider.CheckCollision(otherCollider))
                        {
                            HandleCollision(collider, otherCollider, axis);
                        }
                    }
                }
            }
        }
    }
    
    private void HandleCollision(BoxCollider current, BoxCollider other, Axis axis)
    {
        if (axis == Axis.Horizontal)
        {
            if (current.GetBounds().Right >= other.GetBounds().Left && current.GetBounds().Left <= other.GetBounds().Left)
            {
                GameObject.Transform.Position.X = other.GameObject.Transform.Position.X - current.Size.X - current.Offset.X + other.Offset.X;
            }

            if (current.GetBounds().Left <= other.GetBounds().Right && current.GetBounds().Right >= other.GetBounds().Right)
            {
                GameObject.Transform.Position.X = other.GameObject.Transform.Position.X + other.Size.X - current.Offset.X + other.Offset.X;
            }
        }
        else
        {
            if (current.GetBounds().Bottom >= other.GetBounds().Top && current.GetBounds().Top <= other.GetBounds().Top - 0.1f)
            {
                GameObject.Transform.Position.Y = other.GameObject.Transform.Position.Y - current.Size.Y - current.Offset.Y + other.Offset.Y - 0.1f;
            }

            if (current.GetBounds().Top <= other.GetBounds().Bottom && current.GetBounds().Bottom >= other.GetBounds().Bottom)
            {
                GameObject.Transform.Position.Y = other.GameObject.Transform.Position.Y + other.Size.Y - current.Offset.Y + other.Offset.Y;
            }
            
            Velocity.Y = 0f;
        }
    }
}