using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace HCC.Structs.Jobs
{

    public struct RaycastFinder
    {
        #region Fields
        private float3 _rayOrign;
        private float _distance;
        private RaycastHit _hit;

        NativeArray<RaycastHit> _hits;
        NativeArray<RaycastCommand> _commands;
        #endregion

        #region Properties
        public RaycastHit Hit { get { return _hit; } }
        #endregion

        #region Functions


        #endregion

        #region Methods
        public RaycastFinder(float3 origin, float distance) 
        {   
            _rayOrign = origin;
            _distance = distance;
               
            _hit = new RaycastHit();
            _hits = new NativeArray<RaycastHit>();
            _commands = new NativeArray<RaycastCommand>();

            Find();
        
        }

        private void Find() 
        {
            _hits = new NativeArray<RaycastHit>(2, Allocator.TempJob);

            _commands = new NativeArray<RaycastCommand>(1, Allocator.TempJob);

            Ray ray = Camera.main.ViewportPointToRay(_rayOrign);


            _commands[0] = new RaycastCommand(ray.origin, ray.direction, QueryParameters.Default,_distance);


            JobHandle handle = RaycastCommand.ScheduleBatch(_commands, _hits, 1, default(JobHandle));

            handle.Complete();

            RaycastHit hits = _hits[0];

            if (_hits.Length < 0)
            {
                _hits.Dispose();
                _commands.Dispose();
                return;
            }

            _hit = hits;
            _hits.Dispose();
            _commands.Dispose();

        }

        #endregion
    }
}
