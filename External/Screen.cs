using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RedM.External
{
    public static class Screen
    {
        public const float Width = 1280f;
        public const float Height = 720f;

        public const float AspectRatio = Width / Height;

        public const float ScaledWidth = Width * AspectRatio;

        public static Vector2 Resolution
        {
            get {
                int height = 0, width = 0;
                API.GetScreenResolution(ref width, ref height);
                return new Vector2(width, height);
            }
        }

        public static bool IsFadingIn => API.IsScreenFadingIn();
        public static bool IsFadingOut => API.IsScreenFadingOut();

        public static void FadeIn(int time)
        {
            API.DoScreenFadeIn(time);
        }

        public static void FadeOut(int time)
        {
            API.DoScreenFadeOut(time);
        }
    }
}
