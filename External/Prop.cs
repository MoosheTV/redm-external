using CitizenFX.Core.Native;

namespace RedM.External
{
    public sealed class Prop : Entity
    {
        public Prop(int handle) : base(handle)
        {

        }

        public override bool Exists()
        {
            return base.Exists() && API.GetEntityType(Handle) == 3;
        }

        public static bool Exists(Prop prop)
        {
            return !ReferenceEquals(prop, null) && prop.Exists();
        }
    }
}
