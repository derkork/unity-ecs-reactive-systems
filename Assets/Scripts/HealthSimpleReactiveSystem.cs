using Unity.Entities;
using UnityEngine;

public class HealthSimpleReactiveSystem : ComponentSystem
{
    private EntityQuery _query;

    protected override void OnCreateManager()
    {
        _query = Entities.WithAll<Health>().ToEntityQuery();
        _query.SetFilterChanged(ComponentType.ReadOnly<Health>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, ref Health healthItem) =>
        {
            Debug.Log($"[SIMPLE] New health: {healthItem.Value}");
        });
    }
}