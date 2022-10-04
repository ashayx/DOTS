using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float Speed = 10f;
}

class EnemyBaker : Baker<EnemyAuthoring>
{
    public override void Bake(EnemyAuthoring authoring)
    {
        AddComponent(new EnemyData
        {
            Speed = authoring.Speed,
        });

    }
}

