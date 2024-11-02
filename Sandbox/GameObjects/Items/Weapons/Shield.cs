using System;
using Key_Quest.Engine.ECS;
using Key_Quest.Engine.ECS.Components;

namespace Key_Quest.Sandbox.GameObjects.Items.Weapons;

public class Shield : GameObject
{
    protected override void Init()
    {
        base.Init();
        
        AddComponent(new SpriteRenderer(Assets.Sprites.Items.Shield) { LayerDepth = Globals.Layers.Items });
    }
}