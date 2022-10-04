
using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Profiling;
using static UnityEditor.MaterialProperty;
using static UnityEngine.EventSystems.EventTrigger;
[BurstCompile]
public struct EnemyCreateJob : IJobFor
{
    public Entity Prototype;
    public int Row;
    public float EnemyDistance;
    public EntityCommandBuffer.ParallelWriter Ecb;

    [BurstCompile]
    public void Execute(int index)
    {
        var entity = Ecb.Instantiate(index, Prototype);
        //Debug.Log("index" + entity.Index);
        var postion = new float3(index % Row * EnemyDistance, 0, index / Row * EnemyDistance);
        Ecb.SetComponent(index, entity, new LocalToWorldTransform { Value = UniformScaleTransform.FromPosition(postion) });
    }
}
[BurstCompile]
public partial class EnemyCreateSystem : SystemBase
{
    [BurstCompile]
    protected override void OnStartRunning()
    {
        Profiler.BeginSample("EnemyCreateJob");
        RequireForUpdate<ConfigData>();

        var Config = SystemAPI.GetSingleton<ConfigData>();

        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        var job = new EnemyCreateJob
        {
            Prototype = Config.EnemyPrefab,
            Row = Config.EnemyRowCount,
            EnemyDistance = Config.EnemyDistance,
            Ecb = ecb.AsParallelWriter(),
        };
        //var handle = job.ScheduleParallel(Config.EnemyCount, 64, new JobHandle());
        var handle = job.ScheduleParallel(Config.EnemyCount, 64, new JobHandle());
        handle.Complete();

        ecb.Playback(EntityManager);
        ecb.Dispose();
        Profiler.EndSample();
        //EntityManager.DestroyEntity(Config.EnemyPrefab);
    }
    protected override void OnUpdate()
    {

    }

}
