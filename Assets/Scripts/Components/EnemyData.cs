using Unity.Entities;
using Unity.Mathematics;

public struct EnemyData : IComponentData
{
    public float Speed;
    public float2 Input;
}
