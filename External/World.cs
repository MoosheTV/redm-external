using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RedM.External
{
    public static class World
    {

        private static WeatherType _currentWeather;
        public static WeatherType CurrentWeather
        {
            get => GetCurrentWeatherType();
            set {
                _currentWeather = value;
                Function.Call(Hash._SET_WEATHER_TYPE_TRANSITION, GetCurrentWeatherType(), value, 1f);
            }
        }

        private static WeatherType _nextWeather;
        public static WeatherType NextWeather
        {
            get {
                GetCurrentWeatherType();
                return _nextWeather;
            }
        }

        public static int CurrentDay => API.GetClockDayOfMonth();

        public static int CurrentMonth => API.GetClockMonth();

        public static int CurrentYear => API.GetClockYear();

        public static bool IsWaypointActive => Function.Call<bool>((Hash)0x202B1BBFC6AB5EE4);

        public static Vector3 WaypointPosition => Function.Call<Vector3>((Hash)0x29B30D07C3F7873B);


        public static TimeSpan CurrentTime
        {
            get => new TimeSpan(API.GetClockHours(), API.GetClockMinutes(), API.GetClockSeconds());
            set => API.SetClockTime(value.Hours, value.Minutes, value.Seconds);
        }

        public static void SetClockDate(int day, int month, int year)
        {
            API.SetClockDate(day, month, year);
        }

        private static WeatherType GetCurrentWeatherType()
        {
            var currentWeather = new OutputArgument();
            var nextWeather = new OutputArgument();
            var percent = new OutputArgument();
            Function.Call(Hash._GET_WEATHER_TYPE_TRANSITION, currentWeather, nextWeather, percent);
            _currentWeather = currentWeather.GetResult<WeatherType>();
            _nextWeather = nextWeather.GetResult<WeatherType>();
            var pct = percent.GetResult<float>();
            if (pct >= 0.5f)
            {
                return _nextWeather;
            }
            return _currentWeather;
        }

        public static Camera RenderingCamera
        {
            get => new Camera(API.GetRenderingCam());
            set {
                if (value == null)
                {
                    Function.Call(Hash.RENDER_SCRIPT_CAMS, false, false, 3000, true, false);
                }
                else
                {
                    value.IsActive = true;
                    Function.Call(Hash.RENDER_SCRIPT_CAMS, true, false, 3000, true, false);
                }
            }
        }

        public static Camera CreateCamera(Vector3 pos, Vector3 rot, float fov = -1f)
        {
            if (fov <= 0f)
            {
                fov = Function.Call<float>(Hash.GET_GAMEPLAY_CAM_FOV);
            }
            var handle = Function.Call<int>(Hash.CREATE_CAM_WITH_PARAMS, "DEFAULT_SCRIPTED_CAMERA", pos.X, pos.Y,
                pos.Z, rot.X, rot.Y, rot.Z, fov, true, 2);
            return new Camera(handle);
        }

        public static Prompt CreatePrompt(Control control, string text, int holdDownTime = 0)
        {
            var handle = Function.Call<int>(Hash._PROMPT_REGISTER_BEGIN);
            Function.Call((Hash)0xf4a5c4509bf923b1, handle, 0);
            var prompt = new Prompt(handle)
            {
                Control = control,
                Text = text,
                HoldTime = holdDownTime
            };
            Function.Call(Hash._PROMPT_REGISTER_END, handle);
            return prompt;
        }

        public static Blip CreateBlip(Vector3 position, BlipType type)
        {
            var blip = Function.Call<int>((Hash)0x554d9d53f696d002, type, position.X, position.Y, position.Z);
            return new Blip(blip);
        }

        public static async Task<Ped> CreatePed(Model model, Vector3 position, float heading = 0f, bool isNet = true, bool isMission = true)
        {
            if (!await model.Request(4000))
            {
                return null;
            }
            var id = Function.Call<int>((Hash)0xD49F9B0955C367DE, model.Hash, position.X, position.Y, position.Z, heading,
                isNet, !isMission, 0, 0);
            Function.Call((Hash)0x283978A15512B2FE, id, true);
            return id == 0 ? null : (Ped)Entity.FromHandle(id);
        }

        public static async Task<Vehicle> CreateVehicle(Model model, Vector3 position, float heading = 0f, bool isNet = true, bool isMission = true)
        {
            if (!await model.Request(4000))
            {
                return null;
            }
            var id = Function.Call<int>((Hash)0xAF35D0D2583051B0, model.Hash, position.X, position.Y, position.Z, heading,
                isNet, !isMission);
            return id == 0 ? null : (Vehicle)Entity.FromHandle(id);
        }

        public static async Task<Prop> CreateProp(Model model, Vector3 position, Vector3 rotation, bool dynamic, bool placeOnGround, bool isNetworked)
        {
            if (!await model.Request(4000))
            {
                return null;
            }

            var prop = new Prop(Function.Call<int>(Hash.CREATE_OBJECT, model.Hash, position.X, position.Y, position.Z, isNetworked, true, dynamic, true, true));

            if (placeOnGround)
                Function.Call(Hash.PLACE_OBJECT_ON_GROUND_PROPERLY, prop.Handle);

            if (prop != null)
            {
                prop.Rotation = rotation;
            }

            return prop;
        }

        public static RaycastResult Raycast(Vector3 source, Vector3 target, IntersectOptions options, Entity ignoreEntity = null)
        {
            return new RaycastResult(Function.Call<int>(Hash._START_SHAPE_TEST_RAY, source.X, source.Y, source.Z,
                target.X, target.Y, target.Z, (int)options, ignoreEntity == null ? 0 : ignoreEntity.Handle, 7));
        }

        public static RaycastResult Raycast(Vector3 source, Vector3 direction, float maxDist, IntersectOptions options, Entity ignoreEntity = null)
        {
            var target = source + direction * maxDist;
            return new RaycastResult(Function.Call<int>(Hash._START_SHAPE_TEST_RAY, source.X, source.Y, source.Z,
                target.X, target.Y, target.Z, (int)options, ignoreEntity == null ? 0 : ignoreEntity.Handle, 7));
        }

        public static RaycastResult CrosshairRaycast(float maxDist, IntersectOptions options, Entity ignoreEntity = null)
        {
            var source = GameplayCamera.Position;
            var rotation = (float)(Math.PI / 180.0) * GameplayCamera.Rotation;
            var forward = Vector3.Normalize(new Vector3((float)-Math.Sin(rotation.Z) * (float)Math.Abs(Math.Cos(rotation.X)), (float)Math.Cos(rotation.Z) * (float)Math.Abs(Math.Cos(rotation.X)), (float)Math.Sin(rotation.X)));
            var target = source + forward * maxDist;
            return Raycast(source, target, options, ignoreEntity);
        }

        public static Vector2 World3dToScreen2d(Vector3 pos)
        {
            OutputArgument outX = new OutputArgument(), outY = new OutputArgument();
            return Function.Call<bool>(Hash.GET_SCREEN_COORD_FROM_WORLD_COORD, pos.X, pos.Y, pos.Z, outX, outY) ?
                new Vector2(outX.GetResult<float>(), outY.GetResult<float>()) : Vector2.Zero;
        }
    }

    [Flags]
    public enum IntersectOptions
    {
        Everything = -1,
        Map = 1,
        MissionEntities,
        Peds1 = 12,
        Objects = 16,
        Unk1 = 32,
        Unk2 = 64,
        Unk3 = 128,
        Vegetation = 256,
        Unk4 = 512
    }

    public enum WeatherType : uint
    {
        Overcast = 0xBB898D2D,
        Rain = 0x54A69840,
        Fog = 0xD61BDE01,
        Snowlight = 0x23FB812B,
        Thunder = 0xB677829F,
        Blizzard = 0x27EA2814,
        Snow = 0xEFB6EFF6,
        Misty = 0x5974E8E5,
        Sunny = 0x614A1F91,
        HighPressure = 0xF5A87B65,
        Clearing = 0x6DB1A50D,
        Sleet = 0xCA71D7C,
        Drizzle = 0x995C7F44,
        Shower = 0xE72679D5,
        SnowClearing = 0x641DFC11,
        OvercastDark = 0x19D4F1D9,
        Thunderstorm = 0x7C1C4A13,
        Sandstorm = 0xB17F6111,
        Hurricane = 0x320D0951,
        Hail = 0x75A9E268,
        Whiteout = 0x2B402288,
        GroundBlizzard = 0x7F622122
    }
}
