using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
[BurstCompile]
public partial struct PlayerMoveSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
    }

    public void OnDestroy(ref SystemState state)
    {
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        var cannonBallJob = new MoveJob
        {
            // Note the function call required to get a parallel writer for an EntityCommandBuffer.
            ECB = ecb.AsParallelWriter(),
            // Time cannot be directly accessed from a job, so DeltaTime has to be passed in as a parameter.
            DeltaTime = SystemAPI.Time.DeltaTime
        };
        cannonBallJob.ScheduleParallel();
    }




}
