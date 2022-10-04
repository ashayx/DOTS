using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// This system should run after the transform system has been updated, otherwise the camera
// will lag one frame behind the tank and will jitter.
[UpdateInGroup(typeof(LateSimulationSystemGroup))]
[BurstCompile]
partial class CameraFllowSystem : SystemBase
{
    Entity Target;
    EntityQuery PlayerQuery;

    [BurstCompile]
    protected override void OnCreate()
    {
        PlayerQuery = GetEntityQuery(typeof(PlayerData));
        RequireForUpdate(PlayerQuery);
        //RequireForUpdate<CameraData>();
    }
    [BurstCompile]
    protected override void OnUpdate()
    {
        if (Target == Entity.Null || UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space))
        {
            var tanks = PlayerQuery.ToEntityArray(Allocator.Temp);
            Target = tanks[0];
        }
        //var data = SystemAPI.GetSingleton<CameraData>();
        //var camera = GetSingletonEntity<CameraData>();
        //var ts = GetComponent<LocalToWorldTransform>(camera);
        var tankTransform = GetComponent<LocalToWorld>(Target);

        //ts.Value.Position = tankTransform.Position + data.Offset;

        //SetComponent(camera, ts);
        //Debug.Log(ts.Value.Position);
        //ts.Value.Rotation
        CameraSingleton.Instance.SetTartget(tankTransform.Position);

    }
}