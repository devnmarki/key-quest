using System;
using Key_Quest.Engine;
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
        
        if (KeyboardHandler.IsDown(Keys.Left))
            input.X = -1f;
        else if (KeyboardHandler.IsDown(Keys.Right))
            input.X = 1f;
        else
            input.X = 0f;

        float jumpForce = 500f;
        if (KeyboardHandler.IsPressed(Keys.Z))
        {
            _rb.Velocity = new Vector2(_rb.Velocity.X, -jumpForce);
        }
        
        float moveSpeed = 200f;
        _rb.Velocity = new Vector2(input.X * moveSpeed, _rb.Velocity.Y);
    }
}