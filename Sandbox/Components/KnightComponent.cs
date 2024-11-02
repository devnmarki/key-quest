using System;
using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Graphics;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Engine.Input;
using Key_Quest.Engine.SceneSystem;
using Key_Quest.Sandbox.Components.Enemies;
using Key_Quest.Sandbox.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Key_Quest.Sandbox.Components;

public class KnightComponent : Component
{
    private Rigidbody _rb;
    private SpriteRenderer _sr;
    private Animator _anim;
    
    private Vector2 _input = Vector2.Zero;
    private float _moveSpeed;
    private float _jumpForce = 800f;
    private bool _isGrounded = false;

    private float _deathTimer = 0f;

    private float _climbSpeed = 200f;
    
    public override void OnStart()
    {
        base.OnStart();

        _rb = GameObject.GetComponent<Rigidbody>();
        _sr = GameObject.GetComponent<SpriteRenderer>();
        _anim = GameObject.GetComponent<Animator>();
        
        HandleCollisionIgnoreList();

        _rb.OnCollision = OnCollision;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        HandleInputs();
        Move();

        _rb.CollisionActions["bottom"] = other => _isGrounded = true;
        
        HandleAnimations();

        _moveSpeed = 300f;

        _rb.UseGravity = true;
    }

    private void HandleInputs()
    {
        KeyboardHandler.GetState();

        if (KeyboardHandler.IsDown(Keys.Left))
        {
            _input.X = -1;
        } 
        else if (KeyboardHandler.IsDown(Keys.Right))
        {
            _input.X = 1;
        }
        else
        {
            _input.X = 0;
        }
        
        if (KeyboardHandler.IsDown(Keys.Up))
            _input.Y = -1f;
        else if (KeyboardHandler.IsDown(Keys.Down))
            _input.Y = 1f;
        else
            _input.Y = 0f;

        if (KeyboardHandler.IsDown(Keys.Z) && _isGrounded)
        {
            Jump();
        }
    }

    private void Move()
    {
        _rb.Velocity = new Vector2(_input.X * _moveSpeed, _rb.Velocity.Y);

        if (_input.X < 0f)
            _sr.Flip = true;
        else if (_input.X > 0f)
            _sr.Flip = false;
    }

    private void Jump()
    {
        _isGrounded = false;
        
        _rb.Velocity = new Vector2(_rb.Velocity.X, -_jumpForce);
    }

    private void HandleAnimations()
    {
        _anim.PlayAnimation(_input.X != 0 ? "walk" : "idle");
        
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
        
        _rb.CollisionIgnoreList.Add(typeof(Door));
        _rb.CollisionIgnoreList.Add(typeof(Key));
        _rb.CollisionIgnoreList.Add(typeof(Ladders));
    }

    public void Respawn()
    {
        _deathTimer += (float)Config.Time.ElapsedGameTime.TotalSeconds;
        _moveSpeed = 0f;
        _rb.Velocity = Vector2.Zero;
        _jumpForce = 0f;

        Assets.Sounds.Death.Play();
        
        SceneManager.RefreshCurrentScene();
    }

    private void OnCollision(GameObject other)
    {
        if (other is Key key)
            Pickup(key);

        if (other is Door door && !door.GetComponent<DoorComponent>().Locked)
            SceneManager.ChangeScene(door.MapObject.Properties["Level"]);
        
        if (other is Ladders)
            ClimbLadders();
    }

    private void Pickup(Key key)
    {
        if (GameObject.FindGameObjectByTag("door") is not Door door) return;
        door.GetComponent<DoorComponent>().Locked = false;

        Assets.Sounds.KeyPickup.Play();
        
        SceneManager.CurrentScene.RemoveGameObject(key);
    }

    private void ClimbLadders()
    {
        _rb.UseGravity = false;
        _isGrounded = true;
        _moveSpeed = 150f;

        _rb.Velocity = new Vector2(_rb.Velocity.X, _input.Y * _climbSpeed);

        _anim.PlayAnimation("idle");
    }
}