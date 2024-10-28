using System;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Key_Quest.Sandbox.Components;

public class KnightComponent : Component
{
    private Rigidbody _rb;
    
    private float _input;

    public override void OnStart()
    {
        base.OnStart();

        _rb = GameObject.GetComponent<Rigidbody>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        HandleInputs();
        Move();
    }

    private void HandleInputs()
    {
        KeyboardHandler.GetState();

        if (KeyboardHandler.IsDown(Keys.Left))
        {
            _input = -1;
        } 
        else if (KeyboardHandler.IsDown(Keys.Right))
        {
            _input = 1;
        }
        else
        {
            _input = 0;
        }

        float jumpForce = 500f;
        if (KeyboardHandler.IsPressed(Keys.Z))
        {
            _rb.Velocity = new Vector2(_rb.Velocity.X, -jumpForce);
            Console.WriteLine("Player jumped!");
        }
    }

    private void Move()
    {
        float moveSpeed = 300f;
        _rb.Velocity = new Vector2(_input * moveSpeed, _rb.Velocity.Y);
    }

    private void Jump()
    {
        
    }
}