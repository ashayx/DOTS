using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float Speed = 10f;
    public float2 Input;

}

class PlayerBaker : Baker<PlayerAuthoring>
{
    public override void Bake(PlayerAuthoring authoring)
    {
        AddComponent(new PlayerData
        {
            Speed = authoring.Speed,
            Input = authoring.Input,
        });

    }
}

