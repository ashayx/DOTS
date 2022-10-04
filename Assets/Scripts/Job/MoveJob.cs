using Unity.Entities;
using Unity.Mathematics;

public partial struct MoveJob : IJobEntity
{
    // A regular EntityCommandBuffer cannot be used in parallel, a ParallelWriter has to be explicitly used.
    public EntityCommandBuffer.ParallelWriter ECB;
    // Time cannot be directly accessed from a job, so DeltaTime has to be passed in as a parameter.
    public float DeltaTime;

    // The ChunkIndexInQuery attributes maps the chunk index to an int parameter.
    // Each chunk can only be processed by a single thread, so those indices are unique to each thread.
    // They are also fully deterministic, regardless of the amounts of parallel processing happening.
    // So those indices are used as a sorting key when recording commands in the EntityCommandBuffer,
    // this way we ensure that the playback of commands is always deterministic.
    void Execute([ChunkIndexInQuery] int chunkIndex, ref PlayerAspect player)
    {
        var move = player.Input * player.Speed * DeltaTime;
        player.Position += new float3(move.x, 0, move.y);

        //var speed = math.lengthsq(player.Speed);
        //if (speed < 0.1f) ECB.DestroyEntity(chunkIndex, player.Self);
    }
}