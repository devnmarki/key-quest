using System;
using System.Collections.Generic;
using Key_Quest.Engine.ECS.Components;
using Key_Quest.Sandbox.Enums;

namespace Key_Quest.Sandbox.Components.Enemies;

public class EnemyComponent : Component
{
    public EnemyStates State { get; set; } = EnemyStates.Idle;

    public Dictionary<EnemyStates, Action> StateActions { get; set; } = new Dictionary<EnemyStates, Action>();
    
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (StateActions.TryGetValue(State, out var action))
            StateActions[State]();
    }
}