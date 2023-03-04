using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class KeyboardInputSustem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<InputComponent>> _inputFilter = default;

        readonly EcsPoolInject<InputComponent> _inputPool = default;

        public void Run(IEcsSystems systems)
        {

            foreach (var entityIndex in _inputFilter.Value)
            {
                ref var inputComponent = ref _inputPool.Value.Get(entityIndex);
                inputComponent.Horizontal = Input.GetAxis("Horizontal");
                inputComponent.Vertical = Input.GetAxis("Vertical");
            }
        }
    }
}

