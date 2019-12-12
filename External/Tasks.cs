using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

#pragma warning disable 4014
namespace RedM.External
{
    public sealed class Tasks
    {
        private Ped Ped { get; }

        internal Tasks(Ped ped)
        {
            Ped = ped;
        }

        public void StandStill(int duration)
        {
            Function.Call(Hash.TASK_STAND_STILL, Ped.Handle, duration);
        }

        public void Jump()
        {
            Function.Call(Hash.TASK_JUMP, Ped.Handle, false);
        }

        public void Cower(int duration)
        {
            Function.Call(Hash.TASK_COWER, Ped.Handle, duration);
        }

        public void Whistle()
        {
            Function.Call((Hash)0xD6401A1B2F63BED6, Ped.Handle, 869278708, 1971704925);
        }

        public void HandsUp(int duration, Ped facingPed = null)
        {
            Function.Call(Hash.TASK_HANDS_UP, Ped.Handle, duration, facingPed == null ? 0 : facingPed.Handle, -1, false);
        }

        public void KnockOut(float angle, bool immediately)
        {
            Function.Call((Hash)0xF90427F00A495A28, Ped.Handle, angle, immediately);
        }

        public void KnockOutAndHogtied(float angle, bool immediately)
        {
            Function.Call((Hash)0x42AC6401ABB8C7E5, Ped.Handle, angle, immediately);
        }

        public void Combat(Ped target)
        {
            Function.Call(Hash.TASK_COMBAT_PED, Ped.Handle, target.Handle, 0, 0);
        }

        public void ReviveTarget(Ped target)
        {
            Function.Call((Hash)0x356088527D9EBAAD, Ped.Handle, target.Handle, -1516555556);
        }

        public void SeekCoverFrom(Ped target, int duration)
        {
            Function.Call(Hash.TASK_SEEK_COVER_FROM_PED, Ped.Handle, target.Handle, duration,
                false, false, false);
        }

        public void SeekCoverFrom(Vector3 pos, int duration)
        {
            Function.Call(Hash.TASK_SEEK_COVER_FROM_POS, Ped.Handle, pos.X, pos.Y, pos.Z, duration,
                false, false, false);
        }

        public void StandGuard(Vector3 pos = default)
        {
            pos = pos == default ? Ped.Position : pos;
            Function.Call(Hash.TASK_STAND_GUARD, Ped.Handle, pos.X, pos.Y, pos.Z, Ped.Heading, "DEFEND");
        }

        public void Rob(Ped target, int duration, int flag = 18)
        {
            Function.Call((Hash)0x7BB967F85D8CCBDB, Ped.Handle, target.Handle, flag, duration);
        }

        public void Climb()
        {
            Function.Call(Hash.TASK_CLIMB, Ped.Handle);
        }

        public void Flock()
        {
            Function.Call((Hash)0xE0961AED72642B80, Ped.Handle);
        }

        public void Duck(int duration)
        {
            Function.Call((Hash)0xA14B5FBF986BAC23, Ped.Handle, duration);
        }

        public void EnterCover()
        {
            Function.Call((Hash)0x4972A022AE6DAFA1, Ped.Handle);
        }

        public void ExitCover()
        {
            Function.Call((Hash)0x2BC4A6D92D140112, Ped.Handle);
        }

        public void EnterTransport()
        {
            Function.Call((Hash)0xAEE3ADD08829CB6F, Ped.Handle);
        }

        public void EnterVehicle(Vehicle vehicle, VehicleSeat seat = VehicleSeat.Any, int timeout = 5000, float speed = 1f, int flag = 0)
        {
            Function.Call((Hash)0xC20E50AA46D09CA8, Ped.Handle, vehicle.Handle, timeout, (int)seat, speed, flag, 0);
        }

        public void ExitVehicle(Vehicle vehicle, LeaveVehicleFlags flag = LeaveVehicleFlags.None)
        {
            Function.Call((Hash)0xD3DBCE61A490BE02, Ped.Handle, vehicle.Handle, (int)flag);
        }

        public void MountAnimal(Ped animal, int timeout = -1)
        {
            Function.Call((Hash)0x92DB0739813C5186, Ped.Handle, animal.Handle, timeout, -1, 2f, 1, 0, 0);
        }

        public void StartScenarioInPlace(string scenario, bool playEnterAnim = true, int p4 = -1082130432)
        {
            Function.Call(Hash._TASK_START_SCENARIO_IN_PLACE, Ped.Handle, Game.GenerateHash(scenario), -1, playEnterAnim, false, p4, false);
        }

        public void DismountAnimal(Ped animal)
        {
            Function.Call((Hash)0x48E92D3DDE23C23A, Ped.Handle, animal.Handle, 0, 0, 0, 0);
        }

        public void HitchAnimal(Ped animal, int flag = 0)
        {
            Function.Call((Hash)0x9030AD4B6207BFE8, Ped.Handle, animal.Handle, flag);
        }

        public void DriveToCoord(Vehicle vehicle, Vector3 pos, float speed, float radius = 6f, VehicleDrivingFlags drivingMode = VehicleDrivingFlags.Default)
        {
            Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, Ped.Handle, vehicle.Handle, pos.X, pos.Y, pos.Z, speed,
                radius, vehicle.Model.Hash, drivingMode);
        }

        public void FollowToEntity(Entity entity, float speed, Vector3 offset = default, int timeout = -1, float stoppingRange = 3f, bool keepFollowing = true)
        {
            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, Ped.Handle, entity.Handle, offset.X, offset.Y,
                offset.Z, speed, timeout, stoppingRange, keepFollowing);
        }

        public void GoToWhistle(Entity target, int flag = 3)
        {
            Function.Call((Hash)0xBAD6545608CECA6E, Ped.Handle, target.Handle, flag);
        }

        public void LeadHorse(Ped horse)
        {
            Function.Call((Hash)0x9A7A4A54596FE09D, Ped.Handle, horse.Handle);
        }

        public void FlyAway(Entity awayFrom = null)
        {
            Function.Call((Hash)0xE86A537B5A3C297C, awayFrom == null ? 0 : awayFrom.Handle);
        }

        public void WalkAway(Entity awayFrom = null)
        {
            Function.Call((Hash)0x04ACFAC71E6858F9, awayFrom == null ? 0 : awayFrom.Handle);
        }

        public void ReactToShockingEvent()
        {
            Function.Call((Hash)0x452419CBD838065B, 0, Ped.Handle, 0);
        }

        public void ReactTo(Entity target, EventReaction reaction, float p2 = 7.5f, float p3 = 0f, int flag = 4)
        {
            Function.Call((Hash)0xC4C32C31920E1B70, Ped.Handle, target.Handle, (int)reaction, p2, p3, flag);
        }

        public void WanderAround()
        {
            Function.Call(Hash.TASK_WANDER_STANDARD, Ped.Handle, 1193033728, 0);
        }

        public void HuntAnimal(Ped target)
        {
            Function.Call((Hash)0x4B39D8F9D0FE7749, target.Handle, Ped.Handle, 1);
        }

        public void PlayAnimation(string animDict, string animName)
        {
            PlayAnimation(animDict, animName, 8f, -8f, -1, AnimationFlags.None, 0f);
        }
        public void PlayAnimation(string animDict, string animName, float speed, int duration, float playbackRate)
        {
            PlayAnimation(animDict, animName, speed, -speed, duration, AnimationFlags.None, playbackRate);
        }
        public void PlayAnimation(string animDict, string animName, float blendInSpeed, int duration, AnimationFlags flags)
        {
            PlayAnimation(animDict, animName, blendInSpeed, -8f, duration, flags, 0f);
        }

        public async Task PlayAnimation(string animDict, string animName, float blendInSpeed, float blendOutSpeed, int duration,
            AnimationFlags flags, float playbackRate, float timeout = 1000f)
        {
            if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, animDict))
            {
                Function.Call(Hash.REQUEST_ANIM_DICT, animDict);
            }

            var end = DateTime.UtcNow.AddMilliseconds(timeout);
            while (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, animDict))
            {
                if (DateTime.UtcNow >= end)
                {
                    return;
                }
                await BaseScript.Delay(0);
            }

            Function.Call(Hash.TASK_PLAY_ANIM, Ped.Handle, animDict, animName, blendInSpeed, blendOutSpeed,
                duration, (int)flags, playbackRate, false, false, false);
        }

        public void ClearAnimation(string animDict, string animName, float blendOutSpeed = -8f)
        {
            Function.Call(Hash.STOP_ANIM_TASK, Ped.Handle, animDict, animName, blendOutSpeed);
        }

        public void LookAt(Entity lookAt, int duration)
        {
            Function.Call(Hash.TASK_LOOK_AT_ENTITY, Ped.Handle, lookAt.Handle, duration, 0, 2);
        }

        public void LookAt(Vector3 pos, int duration)
        {
            Function.Call(Hash.TASK_LOOK_AT_COORD, Ped.Handle, pos.X, pos.Y, pos.Z, duration, 0, 2);
        }

        public void ClearLookAt()
        {
            Function.Call(Hash.TASK_CLEAR_LOOK_AT, Ped.Handle);
        }

        public void AimGunAt(Entity entity, int duration)
        {
            Function.Call(Hash.TASK_AIM_GUN_AT_ENTITY, Ped.Handle, entity.Handle, duration, false, 1);
        }

        public void AimGunAt(Vector3 pos, int duration)
        {
            Function.Call(Hash.TASK_AIM_GUN_AT_COORD, Ped.Handle, pos.X, pos.Y, pos.Z, duration, false, 0);
        }

        public void ShootAt(Entity target, int duration, FiringPattern pattern)
        {
            Function.Call(Hash.TASK_SHOOT_AT_ENTITY, Ped.Handle, target.Handle, duration, (uint)pattern);
        }

        public void ShootAt(Vector3 pos, int duration, FiringPattern pattern)
        {
            Function.Call(Hash.TASK_SHOOT_AT_COORD, Ped.Handle, pos.X, pos.Y, pos.Z, duration, (uint)pattern);
        }

        public void ShootWithWeapon(uint weaponHash)
        {
            Function.Call((Hash)0x08AA95E8298AE772, Ped.Handle, weaponHash);
        }

        public void TurnTo(Entity entity, int duration)
        {
            Function.Call(Hash.TASK_TURN_PED_TO_FACE_ENTITY, Ped.Handle, entity.Handle, duration, -1082130432, -1082130432, -1082130432);
        }

        public void TurnTo(Vector3 pos, int duration)
        {
            Function.Call(Hash.TASK_TURN_PED_TO_FACE_COORD, Ped.Handle, pos.X, pos.Y, pos.Z, duration, -1082130432, -1082130432, -1082130432);
        }

        public void EveryoneLeaveVehicle(bool inOrder = false)
        {
            if (Ped.CurrentVehicle == null) return;
            Function.Call(inOrder ? (Hash)0x6F1C49F275BD25B3 : Hash.TASK_EVERYONE_LEAVE_VEHICLE,
                Ped.CurrentVehicle.Handle, false);
        }

        public void WarpIntoVehicle(Vehicle vehicle, VehicleSeat seat)
        {
            Function.Call(Hash.TASK_WARP_PED_INTO_VEHICLE, Ped.Handle, vehicle.Handle, (int)seat);
        }

        public void Pause(int duration)
        {
            Function.Call(Hash.TASK_PAUSE, Ped.Handle, duration);
        }

        public void ClearSecondary()
        {
            Function.Call(Hash.CLEAR_PED_SECONDARY_TASK, Ped.Handle);
        }

        public void ClearAll()
        {
            Function.Call(Hash.CLEAR_PED_TASKS, Ped.Handle);
        }

        public void ClearAllImmediately()
        {
            Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Ped.Handle);
        }
    }

    [Flags]
    public enum LeaveVehicleFlags
    {
        None = 0,
        WarpOut = 16,
        LeaveDoorOpen = 256,
        BailOut = 4096,
        MoveFromPassenger = 262144
    }

    [Flags]
    public enum VehicleDrivingFlags
    {
        Default = AvoidVehicles | AvoidEmptyVehicles | StopBeforePeds | StopBeforeVehicles | AvoidPeds | AvoidObjects,
        StopBeforeVehicles = 1 << 0,
        StopBeforePeds = 1 << 1,
        AvoidVehicles = 1 << 2,
        AvoidEmptyVehicles = 1 << 3,
        AvoidPeds = 1 << 4,
        AvoidObjects = 1 << 5,
        Unk6 = 1 << 6,
        ObeyTrafficStops = 1 << 7,
        UseBlinkers = 1 << 8,
        AllowCuttingTraffic = 1 << 9,
        Reverse = 1 << 10,
        Unk11 = 1 << 11,
        Unk12 = 1 << 12,
        Unk13 = 1 << 13,
        Unk14 = 1 << 14,
        Unk15 = 1 << 15,
        Unk16 = 1 << 16,
        Unk17 = 1 << 17,
        TakeShortestPath = 1 << 18,
        AvoidOffroad = 1 << 19,
        Unk20 = 1 << 20,
        Unk21 = 1 << 21,
        IgnoreRoads = 1 << 22,
        Unk23 = 1 << 23,
        IgnoreAllPathing = 1 << 24,
        Unk25 = 1 << 25,
        Unk26 = 1 << 26,
        Unk27 = 1 << 27,
        Unk28 = 1 << 28,
        AvoidHighways = 1 << 29,
        Unk30 = 1 << 30
    }

    public enum EventReaction
    {
        TaskCombatHigh = 1103872808,
        TaskCombatMedium = 623557147,
        TaskCombatReact = -1342511871,
        TaskCombatPanic = -996719768,
        DefaultShocked = -372548123,
        DefaultPanic = 1618376518,
        DefaultCurious = -1778605437,
        DefaultBrave = 1781933509,
        DefaultAngry = 1345150177,
        DefaultDefuse = -1675652957,
        DefaultScared = -1967172690,
        FleeHumanMajorThreat = -2111647205,
        FleeScared = 759577278
    }

    [Flags]
    public enum AnimationFlags
    {
        None = 0,
        Loop = 1,
        StayInEndFrame = 2,
        UpperBodyOnly = 16,
        AllowRotation = 32,
        CancelableWithMovement = 128,
        RagdollOnCollision = 4194304
    }

    public enum FiringPattern : uint
    {
        Default,
        FullAuto = 3337513804u,
        BurstFire = 3607063905u,
        BurstInCover = 40051185u,
        BurstFireDriveby = 3541198322u,
        FromGround = 577037782u,
        DelayFireByOneSec = 2055493265u,
        SingleShot = 1566631136u,
        BurstFirePistol = 2685983626u,
        BurstFireSMG = 3507334638u,
        BurstFireRifle = 2624893958u,
        BurstFireMG = 3044263348u,
        BurstFirePumpShotGun = 12239771u,
        BurstFireHeli = 2437838959u,
        BurstFireMicro = 1122960381u,
        BurstFireBursts = 1122960381u,
        BurstFireTank = 3804904049u
    }
}
