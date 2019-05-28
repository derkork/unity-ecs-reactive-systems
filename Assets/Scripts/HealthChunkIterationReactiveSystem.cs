using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class HealthChunkIterationReactiveSystem : ComponentSystem
{
    private EntityQuery _query;

    protected override void OnCreateManager()
    {
        _query = Entities.WithAll<Health>().ToEntityQuery();
    }

    protected override void OnUpdate()
    {
       
        var chunkComponentType = GetArchetypeChunkComponentType<Health>(true);

        using (var chunks = _query.CreateArchetypeChunkArray(Allocator.TempJob))
        {
            foreach (var chunk in chunks)
            {
                var healthVersion = chunk.GetComponentVersion(chunkComponentType);

                if (ChangeVersionUtility.DidChange(healthVersion, LastSystemVersion))
                {
                    Debug.Log("[CHUNK] Chunk contains entities with modified health.");
                    var healthItems = chunk.GetNativeArray(chunkComponentType);
                    foreach (var healthItem in healthItems)
                    {
                        Debug.Log($"[CHUNK] New health: {healthItem.Value}");
                    }
                }
            }
        }
    }
}