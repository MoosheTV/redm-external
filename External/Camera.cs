using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RedM.External
{
    public sealed class Camera : PoolObject
    {

        public Camera(int handle) : base(handle)
        {
        }

        public bool IsActive
        {
            get => Function.Call<bool>(Hash.IS_CAM_ACTIVE, Handle);
            set => Function.Call(Hash.SET_CAM_ACTIVE, Handle, value);
        }

        public Vector3 Position
        {
            get => Function.Call<Vector3>(Hash.GET_CAM_COORD, Handle);
            set => Function.Call((Hash)0xF9EE7D419EE49DE6, Handle, value.X, value.Y, value.Z);
        }

        public Vector3 Rotation
        {
            get => Function.Call<Vector3>(Hash.GET_CAM_ROT, Handle, 2);
            set => Function.Call((Hash)0x63DFA6810AD78719, Handle, value.X, value.Y, value.Z, 2);
        }
        public bool IsInterpolating => Function.Call<bool>(Hash.IS_CAM_INTERPOLATING, Handle);

        // TODO: Find Camera Matrices in memory and re-implement in extra-natives-rdr3
        /*public Matrix Matrix
		{
			get {
				Vector3 rightVector = new Vector3();
				Vector3 forwardVector = new Vector3();
				Vector3 upVector = new Vector3();
				Vector3 position = new Vector3();

				API.GetCamMatrix( Handle, ref rightVector, ref forwardVector, ref upVector, ref position );

				return new Matrix(
					rightVector.X, rightVector.Y, rightVector.Z, 0.0f,
					forwardVector.X, forwardVector.Y, forwardVector.Z, 0.0f,
					upVector.X, upVector.Y, upVector.Z, 0.0f,
					position.X, position.Y, position.Z, 1.0f
				);
			}
		}*/

        public Vector3 ForwardVector => GameMath.RotationToDirection(Rotation);
        //public Vector3 UpVector => Matrix.Up;
        //public Vector3 RightVector => Matrix.Right;

        public Vector3 Direction
        {
            get => ForwardVector;
            set {
                value.Normalize();
                Vector3 vector1 = new Vector3(value.X, value.Y, 0f);
                Vector3 vector2 = new Vector3(value.Z, vector1.Length(), 0f);
                Vector3 vector3 = Vector3.Normalize(vector2);
                Rotation = new Vector3((float)(System.Math.Atan2(vector3.X, vector3.Y) * 57.295779513082323f), Rotation.Y, (float)(System.Math.Atan2(value.X, value.Y) * -57.295779513082323f));
            }
        }

        public float FieldOfView
        {
            get => Function.Call<float>(Hash.GET_CAM_FOV, Handle);
            set => Function.Call(Hash.SET_CAM_FOV, Handle, value);
        }

        public void PointAt(int entityHandle, Vector3 offset = default)
        {
            Function.Call(Hash.POINT_CAM_AT_ENTITY, Handle, entityHandle, offset.X, offset.Y, offset.Z, true);
        }

        public void PointAt(Vector3 target)
        {
            Function.Call(Hash.POINT_CAM_AT_COORD, Handle, target.X, target.Y, target.Z);
        }

        public void InterpTo(Camera to, int duration, bool easePosition = true, bool easeRotation = true)
        {
            Function.Call(Hash.SET_CAM_ACTIVE_WITH_INTERP, to.Handle, Handle, duration, easePosition ? 1 : 0, easeRotation ? 1 : 0);
        }
        /*public Vector3 GetOffsetPosition( Vector3 offset ) {
			return Vector3.TransformCoordinate( offset, Matrix );
		}

		public Vector3 GetPositionOffset( Vector3 worldCoords ) {
			return Vector3.TransformCoordinate( worldCoords, Matrix.Invert( Matrix ) );
		}*/

        public override bool Exists()
        {
            return Function.Call<bool>(Hash.DOES_CAM_EXIST, Handle);
        }

        public override void Delete()
        {
            Function.Call(Hash.DESTROY_CAM, Handle);
        }
    }

    public static class GameplayCamera
    {
        public static Vector3 Position => Function.Call<Vector3>(Hash.GET_GAMEPLAY_CAM_COORD);

        public static Vector3 Rotation => Function.Call<Vector3>(Hash.GET_GAMEPLAY_CAM_ROT, 2);

        public static float FieldOfView => Function.Call<float>(Hash.GET_GAMEPLAY_CAM_FOV);

        public static Vector3 ForwardVector
        {
            get {
                var rotation = (float)(Math.PI / 180f) * Rotation;
                return Vector3.Normalize(new Vector3((float)-Math.Sin(rotation.Z) * (float)Math.Abs(Math.Cos(rotation.X)), (float)Math.Cos(rotation.Z) * (float)Math.Abs(Math.Cos(rotation.X)), (float)Math.Sin(rotation.X)));
            }
        }

        public static float RelativePitch => Function.Call<float>(Hash.GET_GAMEPLAY_CAM_RELATIVE_PITCH);

        public static float RelativeHeading => Function.Call<float>(Hash.GET_GAMEPLAY_CAM_RELATIVE_HEADING);

        public static float Zoom => Function.Call<float>(Hash._ANIMATE_GAMEPLAY_CAM_ZOOM);

        public static bool IsRendering => Function.Call<bool>(Hash.IS_GAMEPLAY_CAM_RENDERING);

        public static bool IsLookingBehind => Function.Call<bool>(Hash.IS_GAMEPLAY_CAM_LOOKING_BEHIND);

        public static bool IsShaking => Function.Call<bool>(Hash.IS_GAMEPLAY_CAM_SHAKING);
    }
}
