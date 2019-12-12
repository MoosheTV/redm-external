using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RedM.External
{
    public class Model : INativeValue, IEquatable<Model>
    {
        public int Hash { get; private set; }

        public Model()
        {

        }

        public Model(int hash) : this()
        {
            Hash = hash;
        }

        public Model(string name) : this(Game.GenerateHash(name))
        {

        }

        public Model(PedHash hash) : this((int)hash)
        {

        }

        public Model(VehicleHash hash) : this((int)hash)
        {

        }

        public bool IsValid => Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_MODEL_VALID, (uint)Hash);

        public bool IsInCdImage => Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_MODEL_IN_CDIMAGE, (uint)Hash);

        public bool IsLoaded => Function.Call<bool>(CitizenFX.Core.Native.Hash.HAS_MODEL_LOADED, (uint)Hash);

        public bool IsCollisionLoaded => Function.Call<bool>(CitizenFX.Core.Native.Hash.HAS_COLLISION_FOR_MODEL_LOADED, (uint)Hash);

        public bool IsBoat => Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_THIS_MODEL_A_BOAT, (uint)Hash);

        public bool IsTrain => Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_THIS_MODEL_A_TRAIN, (uint)Hash);

        public bool IsPed
        {
            get {
                var uHash = (uint)Hash;
                foreach (uint hash in Enum.GetValues(typeof(PedHash)))
                {
                    if (hash == uHash)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsVehicle => Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_MODEL_A_VEHICLE, (uint)Hash);

        public Vector3 GetDimensions()
        {
            GetDimensions(out var right, out var left);
            return Vector3.Subtract(left, right);
        }

        public void GetDimensions(out Vector3 minimum, out Vector3 maximum)
        {
            var min = new OutputArgument();
            var max = new OutputArgument();
            Function.Call(CitizenFX.Core.Native.Hash.GET_MODEL_DIMENSIONS, (uint)Hash, min, max);
            minimum = min.GetResult<Vector3>();
            maximum = max.GetResult<Vector3>();
        }

        public void Request()
        {
            Function.Call(CitizenFX.Core.Native.Hash.REQUEST_MODEL, (uint)Hash);
        }

        public async Task<bool> Request(int timeout)
        {
            if (IsLoaded)
            {
                return true;
            }

            if (!IsInCdImage || !IsValid)
            {
                return false;
            }

            Request();

            var end = DateTime.UtcNow.AddMilliseconds(timeout);
            while (!IsLoaded)
            {
                await BaseScript.Delay(0);
                if (DateTime.UtcNow > end)
                {
                    return false;
                }
            }

            return true;
        }

        public void MarkAsNoLongerNeeded()
        {
            Function.Call(CitizenFX.Core.Native.Hash.SET_MODEL_AS_NO_LONGER_NEEDED, (uint)Hash);
        }

        public bool Equals(Model other)
        {
            return Hash == other?.Hash;
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() == GetType() && Equals((Model)obj);
        }

        public override int GetHashCode()
        {
            return Hash.GetHashCode();
        }

        public override ulong NativeValue
        {
            get => (ulong)Hash;
            set => Hash = unchecked((int)value);
        }

        public static implicit operator Model(int source)
        {
            return new Model(source);
        }

        public static implicit operator Model(string source)
        {
            return new Model(source);
        }

        public static bool operator ==(Model left, Model right)
        {
            return left?.Equals(right) ?? false;
        }

        public static bool operator !=(Model left, Model right)
        {
            return !(left?.Equals(right) ?? false);
        }
    }
}
