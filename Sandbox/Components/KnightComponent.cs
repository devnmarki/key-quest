using System;
using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Graphics;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Engine.Input;
using Key_Quest.Engine.SceneSystem;
using Key_Quest.Sandbox.Components.Enemies;
using Key_Quest.Sandbox.GameObjects.Enemies;
using Key_Quest.Sandbox.GameObjects.Items.Weapons;
using Key_Quest.Sandbox.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Key_Quest.Sandbox.Components;

public class KnightComponent : Component
{
    private Rigidbody _rb;
    private SpriteRenderer _sr;
    private Animator _anim;
    
    private float _input;
    private float _moveSpeed;
    private float _jumpForce = 800f;
    private bool _isGrounded = false;

    private float _deathTimer = 0f;
    
    public override void OnStart()
    {
        base.OnStart();

        _rb = GameObject.GetComponent<Rigidbody>();
        _sr = GameObject.GetComponent<SpriteRenderer>();
        _anim = GameObject.GetComponent<Animator>();
        
        HandleCollisionIgnoreList();

        _moveSpeed = 300f;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        HandleInputs();
        Move();

        _rb.CollisionActions["bottom"] = other => _isGrounded = true;
        
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
        _rb.Velocity = new Vector2(_input * _moveSpeed, _rb.Velocity.Y);

        if (_input < 0f)
            _sr.Flip = true;
        else if (_input > 0f)
            _sr.Flip = false;
    }

    private void Jump()
    {
        _isGrounded = false;
        
        _rb.Velocity = new Vector2(_rb.Velocity.X, -_jumpForce);
    }

    private void HandleAnimations()
    {
        _anim.PlayAnimation(_input != 0 ? "walk" : "idle");
        
        if (!_isGrounded) 
            _anim.PlayAnimation("jump");
    }

    private void HandleCollisionIgnoreList()
    {
        foreach (GameObject go in SceneManager.CurrentScene.GameObjects)
        {
            if (!go.HasComponent<EnemyComponent>()) continue;
            
            if (!_rb.CollisionIgnoreList.Contains(go.GetType()))
                _rb.CollisionIgnoreList.Add(go.GetType());
        }
    }

    public void Respawn()
    {
        _deathTimer += (float)Config.Time.ElapsedGameTime.TotalSeconds;
        _moveSpeed = 0f;
        _rb.Velocity = Vector2.Zero;
        _jumpForce = 0f;
        
        // Play death animation

        if (_deathTimer >= 0.5f)
        {
            SceneManager.RefreshCurrentScene();
            _deathTimer = 0f;
        }
    }
}