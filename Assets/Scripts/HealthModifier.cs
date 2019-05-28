using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class HealthModifier : ComponentSystem
{
    private Entity _entity;
    private float _deltaSinceLastUpdate;
    private Random _random;

    protected override void OnCreateManager()
    {
        _random = new Random(321512);
    }

    protected override void OnStartRunning()
    {
        _entity = EntityManager.CreateEntity();
        EntityManager.AddComponentData(_entity, new Health {Value = 1000});
    }

    protected override void OnUpdate()
    {
        _deltaSinceLastUpdate += Time.deltaTime;
        if (_deltaSinceLastUpdate > 1f)
        {
            _deltaSinceLastUpdate -= 1f;

            var value = _random.NextInt(5000);
            Debug.Log($"[MODIFIER] Setting unit health to {value}");
            EntityManager.SetComponentData(_entity, new Health {Value = value});
        }
    }
}