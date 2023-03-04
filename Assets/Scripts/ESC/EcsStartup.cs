using Leopotam.EcsLite;
using UnityEngine;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;        
        IEcsSystems _systems;

        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _systems
                .Add(new KeyboardInputSustem())
                .Add(new MovementSystem())

#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif

            .Inject()
            .Init();
        }

        void Update () {
            // process systems here.
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}