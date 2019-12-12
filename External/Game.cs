using CitizenFX.Core.Native;

namespace RedM.External
{
    public static class Game
    {
        private static Player _player;
        public static Player Player
        {
            get {
                var handle = API.PlayerId();
                if (_player != null && handle == _player.Handle)
                {
                    return _player;
                }
                _player = new Player(handle);
                return _player;
            }
        }

        public static Ped PlayerPed => Player.Character;

        public static int PlayerMoney
        {
            get => Function.Call<int>((Hash)0x0C02DABFA3B98176);
            set {
                var source = PlayerMoney;
                var target = value;
                if (target < source)
                {
                    Function.Call((Hash)0x466BC8769CF26A7A, source - target);
                }
                else
                {
                    Function.Call((Hash)0xBC3422DC91667621, target - source);
                }
            }
        }

        public static bool IsPauseMenuActive => Function.Call<bool>(Hash.IS_PAUSE_MENU_ACTIVE);


        public static bool IsCinematicModeEnabled
        {
            set => Function.Call(Hash.SET_CINEMATIC_BUTTON_ACTIVE, value);
        }

        public static bool IsCinematicModeActive
        {
            set => Function.Call(Hash.SET_CINEMATIC_MODE_ACTIVE, value);
        }

        public static int GenerateHash(string name)
        {
            return Function.Call<int>(Hash.GET_HASH_KEY, name);
        }

        public static bool IsControlPressed(int index, Control control)
        {
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, index, (uint)control);
        }

        public static bool IsControlJustPressed(int index, Control control)
        {
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, index, (uint)control);
        }

        public static bool IsControlJustReleased(int index, Control control)
        {
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_RELEASED, index, (uint)control);
        }

        public static bool IsEnabledControlPressed(int index, Control control)
        {
            return Function.Call<bool>(Hash.IS_CONTROL_PRESSED, index, (uint)control);
        }

        public static bool IsEnabledControlJustPressed(int index, Control control)
        {
            return Function.Call<bool>(Hash.IS_CONTROL_JUST_PRESSED, index, (uint)control);
        }

        public static bool IsEnabledControlReleased(int index, Control control)
        {
            return !IsPauseMenuActive && Function.Call<bool>(Hash.IS_CONTROL_RELEASED, index, (uint)control);
        }

        public static bool IsEnabledControlJustReleased(int index, Control control)
        {
            return Function.Call<bool>(Hash.IS_CONTROL_JUST_RELEASED, index, (uint)control);
        }

        public static bool IsControlEnabled(int index, Control control)
        {
            return Function.Call<bool>(Hash.IS_CONTROL_ENABLED, index, (uint)control);
        }

        public static float GetControlNormal(int index, Control control)
        {
            return Function.Call<float>(Hash.GET_CONTROL_NORMAL, index, (uint)control);
        }

        public static void SetControlNormal(int index, Control control, float value)
        {
            Function.Call(Hash._SET_CONTROL_NORMAL, index, (uint)control, value);
        }

        public static void DisableControlThisFrame(int index, Control control)
        {
            Function.Call(Hash.DISABLE_CONTROL_ACTION, index, control, true);
        }

        public static float GetDisabledControlNormal(int index, Control control)
        {
            return Function.Call<float>(Hash.GET_DISABLED_CONTROL_NORMAL, index, (uint)control);
        }

        public static void Pause(bool value)
        {
            Function.Call(Hash.SET_GAME_PAUSED, value);
        }

        public static void PauseClock(bool value)
        {
            Function.Call(Hash.PAUSE_CLOCK, value);
        }

        public static bool DoesGXTEntryExist(string entry)
        {
            return Function.Call<bool>(Hash.DOES_TEXT_LABEL_EXIST, entry);
        }

        public static string GetGXTEntry(string entry)
        {
            return DoesGXTEntryExist(entry) ? Function.Call<string>(Hash._GET_LABEL_TEXT, entry) : string.Empty;
        }
    }
}
