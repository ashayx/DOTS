using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial class PlayerSystem : SystemBase
{
    protected override void OnCreate()
    {

    }
    [BurstCompile]
    protected override void OnUpdate()
    {
        var dt = SystemAPI.Time.DeltaTime;
        var input = float2.zero;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        //Debug.Log($"input {input}");

        Entities
            .WithAll<PlayerData>()
            .ForEach((ref PlayerData player) =>
            {
                player.Input = input;

            }).ScheduleParallel();
    }
}
