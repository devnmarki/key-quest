using System;
using System.Collections.Generic;
using Key_Quest.Engine;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Sandbox.Enums;
using Microsoft.Xna.Framework;

namespace Key_Quest.Sandbox.Components.Enemies;

public class UndeadKnightComponent : Component
{
    private EnemyComponent _enemyController;
    private Rigidbody _rb;

    private float _speed = 100f;
    private int _currentMovePointIndex = 0;

    private float _idleTime = 2f;
    private float _idleTimer;

    private List<Vector2> MovePoints = new List<Vector2>();
    
    public override void OnStart()
    {
        base.OnStart();

        _enemyController = GameObject.GetComponent<EnemyComponent>();
        _rb = GameObject.GetComponent<Rigidbody>();

        _enemyController.State = EnemyStates.Idle;
        
        _enemyController.StateActions[EnemyStates.Idle] = Idle;
        _enemyController.StateActions[EnemyStates.Patrol] = Patrol;
        _enemyController.StateActions[EnemyStates.Attack] = Attack;
        
        MovePoints.Add(GameObject.Transform.Position);
        MovePoints.Add(new Vector2(GameObject.Transform.Position.X + (float)GameObject.MapObject.Width * Config.GameScale, GameObject.Transform.Position.Y));

        GameObject.Transform.Position = MovePoints[0];
    }

    private void Idle()
    {
        _currentMovePointIndex++;

        if (_currentMovePointIndex >= MovePoints.Count)
        {
            _currentMovePointIndex = 0;
        }
        
        _enemyController.State = EnemyStates.Patrol;
    }
    
    private void Patrol()
    {
        GameObject.Transform.Position = Engine.ECS.GameObject.MoveTowards(GameObject.Transform.Position, MovePoints[_currentMovePointIndex], _speed * (float)Config.Time.ElapsedGameTime.TotalSeconds);

        if (GameObject.Transform.Position == MovePoints[_currentMovePointIndex])
        {
            _enemyController.State = EnemyStates.Idle;
        }
    }

    private void Attack()
    {
        
    }
}