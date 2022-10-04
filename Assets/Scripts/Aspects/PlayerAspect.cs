
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct PlayerAspect : IAspect
{
    //public RefRW<TransformAspect> transformAspect;
    // An Entity field in an Aspect gives access to the Entity itself.
    // This is required for registering commands in an EntityCommandBuffer for example.
    public readonly Entity Self;

    readonly RefRO<PlayerData> Data;
    // Aspects can contain other aspects.
    readonly TransformAspect Transform;

    public float3 Position
    {
        get => Transform.Position;
        set => Transform.Position = value;
    }

    public float Speed
    {
        get => Data.ValueRO.Speed;
    }

    public float2 Input
    {
        get => Data.ValueRO.Input;
    }


}
