
using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.Profiling;
using Random = Unity.Mathematics.Random;

[BurstCompile]
public partial class EnemySystem : SystemBase
{

    [BurstCompile]
    protected override void OnStartRunning()
    {
        RequireForUpdate<ConfigData>();


    }
    [BurstCompile]
    protected override void OnUpdate()
    {
        Profiler.BeginSample("EnemyMove");
        var Config = SystemAPI.GetSingleton<ConfigData>();
        var dt = SystemAPI.Time.DeltaTime;
        var random = new Random(12345);
        Entities
            .WithAll<EnemyData>()
            .ForEach((TransformAspect transform) =>
            {
                var pos = transform.Position;
                if (math.length(pos) > Config.Range)
                {
                    pos = random.NextFloat3() * 10f;
                    pos.x += 50;
                    pos.z += 50;
                    pos.y = 0;
                }
                //// Unity.Mathematics.noise provides several types of noise functions.
                //// Here we use the Classic Perlin Noise (cnoise).
                //// The approach taken to generate a flow field from Perlin noise is detailed here:
                //// https://www.bit-101.com/blog/2021/07/mapping-perlin-noise-to-angles/
                var angle = (0.5f + noise.cnoise(pos / 10f)) * 4.0f * math.PI;
                //var angle = random.NextFloat();
                angle = random.NextBool() ? angle : -angle;
                var dir = float3.zero;
                math.sincos(angle, out dir.x, out dir.z);
                var move = dir * dt * 4.0f;
                transform.Position = pos + move;
                transform.Rotation = quaternion.RotateY(angle);


            }).ScheduleParallel();
        Profiler.EndSample();

    }

}
