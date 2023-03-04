using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    public class MovementSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<InputComponent, VelocityComponent, PlayerComponent>> _filter = default;

        readonly EcsPoolInject<InputComponent> _inputPool = default;
        readonly EcsPoolInject<VelocityComponent> _velocityPool = default;
        readonly EcsPoolInject<PlayerComponent> _playerComponentPool = default;
        public void Run(IEcsSystems systems)
        {
            foreach (var entityIndex in _filter.Value)
            {
                ref var inputComponent = ref _inputPool.Value.Get(entityIndex);
                ref var velocityComponent = ref _velocityPool.Value.Get(entityIndex);
                ref var playerComponent = ref _playerComponentPool.Value.Get(entityIndex);

                playerComponent.playerObject = GameObject.FindGameObjectWithTag("Player");
                velocityComponent.value.x = inputComponent.Horizontal * 5 * Time.deltaTime;
                velocityComponent.value.y = inputComponent.Jump ? 5 : velocityComponent.value.y;
            }
        }
    }
}

