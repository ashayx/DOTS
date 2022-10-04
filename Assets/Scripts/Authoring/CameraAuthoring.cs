using System.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CameraAuthoring : MonoBehaviour
{
    public float3 LookAtOffset;
    public float3 Offset;
}
class CameraBaker : Baker<CameraAuthoring>
{
    public override void Bake(CameraAuthoring authoring)
    {
        AddComponent(new CameraData
        {
            LookAtOffset = authoring.LookAtOffset,
            Offset = authoring.Offset,
        });
    }
}