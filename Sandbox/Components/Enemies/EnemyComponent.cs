using System;
using System.Collections.Generic;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Engine.ECS.Components.Physics;
using Key_Quest.Engine.Utils;
using Key_Quest.Sandbox.Enums;
using Key_Quest.Sandbox.GameObjects;
using Key_Quest.Sandbox.Interfaces;

namespace Key_Quest.Sandbox.Components.Enemies;

public class EnemyComponent : Component
{
    public EnemyStates State { get; set; } = EnemyStates.Idle;

    public Dictionary<EnemyStates, Action> StateActions { get; set; } = new Dictionary<EnemyStates, Action>();

    private Rigidbody _rb;

    public override void OnStart()
    {
        base.OnStart();

        _rb = GameObject.GetComponent<Rigidbody>();

        _rb.OnCollision = HandleCollision;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (StateActions.TryGetValue(State, out var action))
            StateActions[State]();
    }

    private void HandleCollision(GameObject other)
    {
        if (other is Knight knight)
        {
            knight.GetComponent<KnightComponent>()?.Respawn();
        }
    }
}