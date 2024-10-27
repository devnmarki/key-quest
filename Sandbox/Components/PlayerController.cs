using System;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Key_Quest.Sandbox.Components;

public class PlayerController : Component
{
    private Rigidbody _rb;
    private Vector2 input = Vector2.Zero;
    
    public override void OnStart()
    {
        base.OnStart();

        _rb = GameObject.GetComponent<Rigidbody>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        if (KeyboardHandler.IsDown(Keys.A))
            input.X = -1f;
        else if (KeyboardHandler.IsDown(Keys.D))
            input.X = 1f;
        else
            input.X = 0f;

        if (KeyboardHandler.IsDown(Keys.W))
            input.Y = -1f;
        else if (KeyboardHandler.IsDown(Keys.S))
            input.Y = 1f;
        else
            input.Y = 0f;
        
        if (input.X != 0 && input.Y != 0)
        {
            input.X *= 0.7f;
            input.Y *= 0.7f;
        }
        
        float moveSpeed = 500f;
        _rb.Velocity = new Vector2(input.X * moveSpeed, input.Y * moveSpeed);
    }
}