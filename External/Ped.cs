using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RedM.External
{
    public class Ped : Entity
    {
        public Tasks Tasks { get; }
        public Ped(int handle) : base(handle)
        {
            Tasks = new Tasks(this);
        }

        public Gender Gender => Function.Call<bool>(Hash.IS_PED_MALE, Handle) ? Gender.Male : Gender.Female;
        public bool IsJumping => Function.Call<bool>(Hash.IS_PED_JUMPING, Handle);
        public bool IsInMeleeCombat => Function.Call<bool>(Hash.IS_PED_IN_MELEE_COMBAT, Handle);
        public bool IsInCombat => Function.Call<bool>(Hash.IS_PED_IN_COMBAT, Handle);
        public bool IsClimbing => Function.Call<bool>(Hash.IS_PED_CLIMBING, Handle);
        public bool IsPlayer => Function.Call<bool>(Hash.IS_PED_A_PLAYER, Handle);
        public bool IsHuman => Function.Call<bool>(Hash.IS_PED_HUMAN, Handle);
        public bool IsFleeing => Function.Call<bool>(Hash.IS_PED_FLEEING, Handle);
        public bool IsGettingUp => Function.Call<bool>(Hash.IS_PED_GETTING_UP, Handle);
        public bool IsGettingIntoVehicle => Function.Call<bool>(Hash.IS_PED_GETTING_INTO_A_VEHICLE, Handle);
        public bool IsInVehicle => Function.Call<bool>(Hash.IS_PED_IN_VEHICLE, Handle);
        public bool IsOnFoot => Function.Call<bool>(Hash.IS_PED_ON_FOOT, Handle);
        public bool IsOnMount => Function.Call<bool>(Hash.IS_PED_ON_MOUNT, Handle);

        public Vehicle CurrentVehicle => (Vehicle)FromHandle(Function.Call<int>(Hash.GET_VEHICLE_PED_IS_IN, Handle, false));
        public Vehicle LastVehicle => (Vehicle)FromHandle(Function.Call<int>(Hash.GET_VEHICLE_PED_IS_IN, Handle, true));

        public int HealthCore
        {
            get => GetCoreValue(PedCore.Health);
            set => SetCoreValue(PedCore.Health, value);
        }

        public int StaminaCore
        {
            get => GetCoreValue(PedCore.Stamina);
            set => SetCoreValue(PedCore.Stamina, value);
        }

        public int DeadEyeCore
        {
            get => GetCoreValue(PedCore.DeadEye);
            set => SetCoreValue(PedCore.DeadEye, value);
        }

        public float Scale
        {
            set => Function.Call((Hash)0x25ACFC650B65C538, Handle, value);
        }

        public string PromptName
        {
            set => Function.Call(Hash._SET_PED_PROMPT_NAME, Handle, value);
        }

        public Weapon CurrentWeaponLeftHand
        {
            get {
                var wpnOut = new OutputArgument();
                Function.Call(Hash.GET_CURRENT_PED_WEAPON, Handle, wpnOut, true, true);
                return new Weapon(this, wpnOut.GetResult<WeaponHash>());
            }
        }
        public Weapon CurrentWeaponRightHand
        {
            get {
                var wpnOut = new OutputArgument();
                Function.Call(Hash.GET_CURRENT_PED_WEAPON, Handle, wpnOut, true, false);
                return new Weapon(this, wpnOut.GetResult<WeaponHash>());
            }
        }


        public int Outfit
        {
            set => Function.Call((Hash)0x77ff8d35eec6bbc4, Handle, value, 0);
        }

        public int GetCoreValue(PedCore core)
        {
            return Function.Call<int>((Hash)0x36731AC041289BB1, Handle, (int)core);
        }

        public void SetCoreValue(PedCore core, int value)
        {
            Function.Call((Hash)0xc6258f41d86676e0, Handle, (int)core, MathUtil.Clamp(value, 0, 100));
        }

        public void GiveWeapon(WeaponHash weapon, int ammoCount, bool equipNow = false, bool isLeftHanded = false, float condition = 0.0f)
        {
            Function.Call((Hash)0x5E3BDDBCB83F3D84, Handle, (uint)weapon, ammoCount, equipNow, true, 1, false,
                0.5f, 1f, 752097756, isLeftHanded, condition);
        }

        public void GiveAllWeapons(int ammo = 200)
        {
            foreach (WeaponHash hash in Enum.GetValues(typeof(WeaponHash)))
            {
                var wpn = new Weapon(this, hash);
                if (wpn.Group == WeaponGroup.Animal)
                {
                    continue;
                }
                GiveWeapon(wpn, ammo);
            }
        }

    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum PedCore
    {
        Health = 0,
        Stamina,
        DeadEye
    }

}
