using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

class ApplyVelocityParallelForSample : MonoBehaviour
{
    struct VelocityJob : IJobFor
    {
        [ReadOnly]
        public NativeArray<Vector3> velocity;
        public NativeArray<Vector3> position;
        public float deltaTime;

        public void Execute(int i)
        {
            position[i] = position[i] + velocity[i] * deltaTime;
        }
    }

    public void Update()
    {
        var position = new NativeArray<Vector3>(500, Allocator.Persistent);

        var velocity = new NativeArray<Vector3>(500, Allocator.Persistent);
        for (var i = 0; i < velocity.Length; i++)
            velocity[i] = new Vector3(0, 10, 0);

        var job = new VelocityJob()
        {
            deltaTime = Time.deltaTime,
            position = position,
            velocity = velocity
        };

        job.Run(position.Length);

        JobHandle sheduleJobDependency = new JobHandle();
        JobHandle sheduleJobHandle = job.Schedule(position.Length, sheduleJobDependency);

        JobHandle sheduleParralelJobHandle = job.ScheduleParallel(position.Length, 64, sheduleJobHandle);

        sheduleParralelJobHandle.Complete();

        Debug.Log(job.position[0]);

        position.Dispose();
        velocity.Dispose();
    }
}