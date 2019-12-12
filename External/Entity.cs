using System;
using System.Security;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RedM.External
{
    public class Entity : PoolObject, IEquatable<Entity>, ISpatial
    {
        public Entity(int handle) : base(handle)
        {
        }

        public int Health
        {
            get => Function.Call<int>(Hash.GET_ENTITY_HEALTH, Handle) - 100;
        }

        public int MaxHealth
        {
            get => Function.Call<int>(Hash.GET_ENTITY_MAX_HEALTH, Handle) - 100;
            set => Function.Call(Hash.SET_ENTITY_MAX_HEALTH, Handle, value + 100);
        }

        public bool IsDead => Function.Call<bool>(Hash.IS_ENTITY_DEAD, Handle);

        public bool IsAlive => !IsDead;

        public Model Model => new Model(Function.Call<int>(Hash.GET_ENTITY_MODEL, Handle));

        public Blip Blip => new Blip(API.GetBlipFromEntity(Handle));

        public virtual Vector3 Position
        {
            get => Function.Call<Vector3>(Hash.GET_ENTITY_COORDS, Handle);
            set => Function.Call(Hash.SET_ENTITY_COORDS, Handle, value.X, value.Y, value.Z, false, false, false, true);
        }

        public Vector3 PositionNoOffset
        {
            set => Function.Call(Hash.SET_ENTITY_COORDS_NO_OFFSET, Handle, value.X, value.Y, value.Z, true, true, true);
        }

        public virtual Vector3 Rotation
        {
            get => Function.Call<Vector3>(Hash.GET_ENTITY_ROTATION, Handle);
            set => Function.Call(Hash.SET_ENTITY_ROTATION, Handle, value.X, value.Y, value.Z, 2, true);
        }

        public float Heading
        {
            get => Function.Call<float>(Hash.GET_ENTITY_HEADING, Handle);
            set => Function.Call(Hash.SET_ENTITY_ROTATION, Handle, value);
        }

        public Vector3 ForwardVector => Function.Call<Vector3>(Hash.GET_ENTITY_FORWARD_VECTOR, Handle);

        public bool IsPositionFrozen
        {
            set => Function.Call(Hash.FREEZE_ENTITY_POSITION, Handle, value);
        }

        public Vector3 Velocity
        {
            get => Function.Call<Vector3>(Hash.GET_ENTITY_VELOCITY, Handle);
            set => Function.Call(Hash.SET_ENTITY_VELOCITY, Handle, value.X, value.Y, value.Z);
        }

        public bool HasGravity
        {
            set => Function.Call(Hash.SET_ENTITY_HAS_GRAVITY, Handle, value);
        }

        public float HeightAboveGround => Function.Call<float>(Hash.GET_ENTITY_HEIGHT_ABOVE_GROUND, Handle);

        public int LodDistance
        {
            get => Function.Call<int>(Hash.GET_ENTITY_LOD_DIST, Handle);
            set => Function.Call(Hash.SET_ENTITY_LOD_DIST, Handle, value);
        }

        public bool IsVisible
        {
            get => Function.Call<bool>(Hash.IS_ENTITY_VISIBLE, Handle);
            set => Function.Call(Hash.SET_ENTITY_VISIBLE, Handle, value);
        }

        public bool IsInvincible
        {
            set => Function.Call(Hash.SET_ENTITY_INVINCIBLE, Handle, value);
        }

        public bool CanRagdoll
        {
            get => Function.Call<bool>(Hash.CAN_PED_RAGDOLL, Handle);
            set => Function.Call(Hash.SET_PED_CAN_RAGDOLL, Handle, value);
        }

        public EntityType EntityType => Function.Call<EntityType>(Hash.GET_ENTITY_TYPE, Handle);
        public bool IsOccluded => Function.Call<bool>(Hash.IS_ENTITY_OCCLUDED, Handle);

        public bool IsOnScreen => Function.Call<bool>(Hash.IS_ENTITY_ON_SCREEN, Handle);

        public bool IsUpright => Function.Call<bool>(Hash.IS_ENTITY_UPRIGHT, Handle);

        public bool IsUpsideDown => Function.Call<bool>(Hash.IS_ENTITY_UPSIDEDOWN, Handle);

        public bool IsInAir => Function.Call<bool>(Hash.IS_ENTITY_IN_AIR, Handle);

        public bool IsInWater => Function.Call<bool>(Hash.IS_ENTITY_IN_WATER, Handle);

        public bool IsOnFire => Function.Call<bool>(Hash.IS_ENTITY_ON_FIRE, Handle);

        public bool IsPersistent
        {
            get => Function.Call<bool>(Hash.IS_ENTITY_A_MISSION_ENTITY, Handle);
            set {
                if (value)
                {
                    Function.Call(Hash.SET_ENTITY_AS_MISSION_ENTITY, Handle, true);
                }
                else
                {
                    MarkAsNoLongerNeeded();
                }
            }
        }

        public int Opacity
        {
            get => Function.Call<int>(Hash.GET_ENTITY_ALPHA, Handle);
            set {
                if (value < 0 || value > 255)
                {
                    ResetOpacity();
                }
                else
                {
                    Function.Call(Hash.SET_ENTITY_ALPHA, Handle, value);
                }
            }
        }

        public bool HasCollided => Function.Call<bool>(Hash.HAS_ENTITY_COLLIDED_WITH_ANYTHING, Handle);

        public bool IsCollisionEnabled
        {
            get => Function.Call<bool>(Hash.GET_ENTITY_COLLISION_DISABLED, Handle);
            set => Function.Call(Hash.SET_ENTITY_COLLISION, Handle, value, false);
        }

        public bool PreRender
        {
            set => Function.Call(Hash.SET_ENTITY_ALWAYS_PRERENDER, Handle, value);
        }

        public float MotionBlur
        {
            set => Function.Call(Hash.SET_ENTITY_MOTION_BLUR, Handle, value);
        }

        public float Speed => Function.Call<float>(Hash.GET_ENTITY_SPEED, Handle);

        public Vector3 SpeedVector => Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, Handle);

        public void SetNoCollision(Entity entity, bool toggle)
        {
            Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, Handle, entity.Handle, toggle);
        }

        public bool HasBeenDamagedBy(Entity entity)
        {
            return Function.Call<bool>(Hash.HAS_ENTITY_BEEN_DAMAGED_BY_ENTITY, Handle, entity.Handle, true);
        }

        public Vector3 GetOffsetPosition(Vector3 relativeCoords)
        {
            return Function.Call<Vector3>(Hash.GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS, Handle, relativeCoords.X,
                relativeCoords.Y, relativeCoords.Z);
        }

        public Vector3 GetPositionOffset(Vector3 worldCoords)
        {
            return Function.Call<Vector3>(Hash.GET_OFFSET_FROM_ENTITY_GIVEN_WORLD_COORDS, Handle, worldCoords.X,
                worldCoords.Y, worldCoords.Z);
        }

        public void SetPosition(Vector3 pos, float heading)
        {
            Function.Call((Hash)0x203BEFFDBE12E96A, Handle, pos.X, pos.Y, pos.Z, heading % 360f, true, true);
        }

        public void SetPosition(Vector4 pos)
        {
            SetPosition(new Vector3(pos.X, pos.Y, pos.Z), pos.W);
        }

        public void AttachTo(Entity entity, Vector3 offset = default, Vector3 rotOffset = default)
        {
            Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, Handle, entity.Handle, -1, offset.X, offset.Y, offset.Z,
                rotOffset.X, rotOffset.Y, rotOffset.Z, false, false, false, false, 2, true);
        }

        public void Detach()
        {
            Function.Call(Hash.DETACH_ENTITY, Handle, true, true);
        }

        public bool IsAttached()
        {
            return Function.Call<bool>(Hash.IS_ENTITY_ATTACHED, Handle);
        }

        public bool IsAttachedTo(Entity entity)
        {
            return Function.Call<bool>(Hash.IS_ENTITY_ATTACHED_TO_ENTITY, Handle, entity.Handle);
        }

        public Entity GetEntityAttachedTo()
        {
            return FromHandle(Function.Call<int>(Hash.GET_ENTITY_ATTACHED_TO, Handle));
        }

        public void ResetOpacity()
        {
            Function.Call(Hash.RESET_ENTITY_ALPHA, Handle);
        }

        [SecuritySafeCritical]
        public void MarkAsNoLongerNeeded()
        {
            _MarkAsNoLongerNeeded();
        }

        [SecuritySafeCritical]
        private void _MarkAsNoLongerNeeded()
        {
            Function.Call(Hash.SET_ENTITY_AS_NO_LONGER_NEEDED, Handle, false, true);
            var handle = new OutputArgument(Handle);
            Function.Call(Hash.SET_ENTITY_AS_NO_LONGER_NEEDED, handle);
            Handle = handle.GetResult<int>();
        }

        public override bool Exists()
        {
            return Function.Call<bool>(Hash.DOES_ENTITY_EXIST, Handle);
        }

        public override void Delete()
        {
            Function.Call(Hash.DELETE_ENTITY, Handle);
        }

        public static Entity FromNetworkId(int netId)
        {
            return FromHandle(Function.Call<int>(Hash.NETWORK_GET_ENTITY_FROM_NETWORK_ID, netId));
        }

        public static Entity FromHandle(int handle)
        {
            switch (Function.Call<EntityType>(Hash.GET_ENTITY_TYPE, handle))
            {
                case EntityType.Ped:
                    return new Ped(handle);
                case EntityType.Vehicle:
                    return new Vehicle(handle);
                case EntityType.Object:
                    return new Prop(handle);
                default:
                    return null;
            }
        }

        public bool Equals(Entity entity)
        {
            return !ReferenceEquals(entity, null) && Handle == entity.Handle;
        }
        public override bool Equals(object obj)
        {
            return !ReferenceEquals(obj, null) && obj.GetType() == GetType() && Equals((Entity)obj);
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return left?.Equals(right) ?? ReferenceEquals(right, null);
        }
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }

    public enum EntityType
    {
        Ped = 1,
        Vehicle,
        Object
    }
}
