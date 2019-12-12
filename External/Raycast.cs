using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RedM.External
{
    public struct RaycastResult
    {
        public Entity HitEntity { get; }

        public Vector3 HitPosition { get; }

        public Vector3 SurfaceNormal { get; }

        public bool DitHit { get; }

        public bool DitHitEntity { get; }

        public int Result { get; }

        public RaycastResult(int handle)
        {
            var hitPos = new OutputArgument();
            var ditHit = new OutputArgument();
            var entity = new OutputArgument();
            var normal = new OutputArgument();

            Result = Function.Call<int>(Hash.GET_SHAPE_TEST_RESULT, handle, ditHit, hitPos, normal, entity);

            var entityId = entity.GetResult<int>();
            HitPosition = hitPos.GetResult<Vector3>();
            HitEntity = entityId == 0 || HitPosition == default ? null : new Entity(entityId);
            DitHitEntity = HitEntity != null && HitPosition != default && HitEntity.EntityType != 0;
            DitHit = ditHit.GetResult<bool>();
            SurfaceNormal = normal.GetResult<Vector3>();
        }
    }
}
