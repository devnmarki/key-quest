using System;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Graphics;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Key_Quest.Sandbox.Components;

public class KnightComponent : Component
{
    private Rigidbody _rb;
    private SpriteRenderer _sr;
    private Animator _anim;
    
    private float _input;
    private bool _isGrounded = false;

    public override void OnStart()
    {
        base.OnStart();

        _rb = GameObject.GetComponent<Rigidbody>();
        _sr = GameObject.GetComponent<SpriteRenderer>();
        _anim = GameObject.GetComponent<Animator>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        HandleInputs();
        Move();

        _rb.CollisionActions["bottom"] = () => _isGrounded = true;
        
        HandleAnimations();
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

        if (KeyboardHandler.IsDown(Keys.Z) && _isGrounded)
        {
            Jump();
        }
    }

    private void Move()
    {
        float moveSpeed = 300f;
        _rb.Velocity = new Vector2(_input * moveSpeed, _rb.Velocity.Y);

        if (_input < 0f)
            _sr.Flip = true;
        else if (_input > 0f)
            _sr.Flip = false;
    }

    private void Jump()
    {
        _isGrounded = false;
        
        float jumpForce = 800f;
        _rb.Velocity = new Vector2(_rb.Velocity.X, -jumpForce);
    }

    private void HandleAnimations()
    {
        _anim.PlayAnimation(_input != 0 ? "walk" : "idle");
        
        if (!_isGrounded) 
            _anim.PlayAnimation("jump");
    }
}