using Unity.Entities;
using Unity.Mathematics;

public partial struct CameraData: IComponentData
{
    public float3 LookAtOffset;
    public float3 Offset;
}