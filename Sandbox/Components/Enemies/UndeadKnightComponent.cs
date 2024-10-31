using System;
using System.Collections.Generic;
using Key_Quest.Engine;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Graphics;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Sandbox.Enums;
using Microsoft.Xna.Framework;

namespace Key_Quest.Sandbox.Components.Enemies;

public class UndeadKnightComponent : Component
{
    private EnemyComponent _enemyController;
    private Rigidbody _rb;
    private SpriteRenderer _sr;
    private Animator _anim;

    private float _speed = 100f;
    private int _currentMovePointIndex = 0;
    private float _idleTime = 2f;
    private float _idleTimer;
    private List<Vector2> _movePoints = new List<Vector2>();
    private bool _flip = false;
    
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
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        _sr.Flip = _flip;
        
        if (GameObject.Transform.Position == _movePoints[_currentMovePointIndex])
            _anim.PlayAnimation("idle");
        else
            _anim.PlayAnimation("walk");
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
        GameObject.Transform.Position = Engine.ECS.GameObject.MoveTowards(GameObject.Transform.Position, _movePoints[_currentMovePointIndex], _speed * (float)Config.Time.ElapsedGameTime.TotalSeconds);

        if (GameObject.Transform.Position == _movePoints[_currentMovePointIndex])
        {
            _flip = !_flip;
            _enemyController.State = EnemyStates.Idle;
        }
    }

    private void Attack()
    {
        
    }
}