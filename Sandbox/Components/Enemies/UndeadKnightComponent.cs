using System;
using System.Collections.Generic;
using Key_Quest.Engine;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Graphics;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Engine.SceneSystem;
using Key_Quest.Sandbox.Enums;
using Key_Quest.Sandbox.GameObjects;
using Key_Quest.Sandbox.GameObjects.Items.Weapons;
using Microsoft.Xna.Framework;

namespace Key_Quest.Sandbox.Components.Enemies;

public class UndeadKnightComponent : Component
{
    // Components
    private EnemyComponent _enemyController;
    private Rigidbody _rb;
    private SpriteRenderer _sr;
    private Animator _anim;

    // Patrolling AI
    private float _speed = 100f;
    private int _currentMovePointIndex = 0;
    private float _idleTime = 2f;
    private float _idleTimer;
    private List<Vector2> _movePoints = new List<Vector2>();
    private bool _flip = false;
    private Directions _direction = Directions.Right;
    
    // Attacking AI
    public GameObject Shield { get; set; }
    
    public override void OnStart()
    {
        base.OnStart();

        _enemyController = GameObject.GetComponent<EnemyComponent>();
        _rb = GameObject.GetComponent<Rigidbody>();
        _sr = GameObject.GetComponent<SpriteRenderer>();
        _anim = GameObject.GetComponent<Animator>();

        _enemyController.State = EnemyStates.Idle;
        
        _enemyController.StateActions[EnemyStates.Idle] = Idle;
        _enemyController.StateActions[EnemyStates.Patrol] = Patrol;
        _enemyController.StateActions[EnemyStates.Attack] = Attack;
        
        _movePoints.Add(GameObject.Transform.Position);
        _movePoints.Add(new Vector2(GameObject.Transform.Position.X + (float)GameObject.MapObject.Width * Config.GameScale, GameObject.Transform.Position.Y));

        GameObject.Transform.Position = _movePoints[0];

        _rb.CollisionIgnoreList.Add(typeof(Knight));
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        _sr.Flip = _flip;

        _anim.PlayAnimation(GameObject.Transform.Position == _movePoints[_currentMovePointIndex] ? "idle" : "walk");

        if (GameObject.Transform.Position == _movePoints[0])
            _direction = Directions.Right;
        else if (GameObject.Transform.Position == _movePoints[1])
            _direction = Directions.Left;

        HandleShield();
    }

    private void Idle()
    {
        _idleTimer += (float)Config.Time.ElapsedGameTime.TotalSeconds;

        if (_idleTimer >= _idleTime)
        {
            _currentMovePointIndex++;

            if (_currentMovePointIndex >= _movePoints.Count)
            {
                _currentMovePointIndex = 0;
            }
            
            _idleTimer = 0f;
            
            _enemyController.State = EnemyStates.Patrol;
        }
    }
    
    private void Patrol()
    {
        GameObject.Transform.Position = GameObject.MoveTowards(GameObject.Transform.Position, _movePoints[_currentMovePointIndex], _speed * (float)Config.Time.ElapsedGameTime.TotalSeconds);

        if (GameObject.Transform.Position == _movePoints[_currentMovePointIndex])
        {
            _flip = _direction != Directions.Left;
            _enemyController.State = EnemyStates.Idle;
        }
    }

    private void Attack()
    {
        // Attack logic
    }

    private void HandleShield()
    {
        // foreach (var frame in _anim.CurrentAnimation.Frames)
        // {
        //     if (frame % 2 == 0)
        //     else
        // }
        if (_anim.CurrentAnimation == _anim.GetAnimation("walk"))
        {
            if (_anim.CurrentAnimation.CurrentFrame % 2 == 0)
                Shield.Transform.Position.Y = GameObject.Transform.Position.Y + (1f * Config.GameScale);
            else
                Shield.Transform.Position.Y = GameObject.Transform.Position.Y;
        } 
        else
        {
            if (_anim.CurrentAnimation.CurrentFrame is 0 or 4)   
                Shield.Transform.Position.Y = GameObject.Transform.Position.Y + (2f * Config.GameScale);
            else if (_anim.CurrentAnimation.CurrentFrame == 1)
                Shield.Transform.Position.Y = GameObject.Transform.Position.Y + (2f * Config.GameScale);
            else if (_anim.CurrentAnimation.CurrentFrame == 2)
                Shield.Transform.Position.Y = GameObject.Transform.Position.Y + (3f * Config.GameScale);
        }
        
        if (_direction == Directions.Right)
            Shield.Transform.Position.X = GameObject.Transform.Position.X - (5f * Config.GameScale);
        else
            Shield.Transform.Position.X = GameObject.Transform.Position.X + (Config.GameScale);
    }
}