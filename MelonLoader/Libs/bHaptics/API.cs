﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace MelonLoader
{
    public static class bHaptics
    {
        private static int MaxBufferSize = 20;
        private static bool _waserror = false;
        public static bool WasError { get => _waserror; internal set { if (value == true) MelonLogger.Warning("Disabling bHaptics API..."); _waserror = value; } }
        internal static void Start() { if (!_waserror) bHaptics_NativeLibrary.Initialise("MelonLoader", MelonUtils.GameName.Replace(" ", "_")); }
        internal static void Quit() { if (_waserror) return; bHaptics_NativeLibrary.TurnOff(); bHaptics_NativeLibrary.Destroy(); }

        public static bool IsPlaying() => (!_waserror && bHaptics_NativeLibrary.IsPlaying());
        public static bool IsPlaying(string key) => (!_waserror && bHaptics_NativeLibrary.IsPlayingKey(key));
        public static bool IsPlaying(PositionType type) => (!_waserror && bHaptics_NativeLibrary.IsDevicePlaying(type));

        public static bool IsFeedbackRegistered(string key) => (!_waserror && bHaptics_NativeLibrary.IsFeedbackRegistered(key));

        public static void RegisterTactFileStr(string key, string tactFileStr) { if (!_waserror) bHaptics_NativeLibrary.RegisterFeedbackFromTactFile(key, tactFileStr); }
        public static void RegisterTactFileStrReflected(string key, string tactFileStr) { if (!_waserror) bHaptics_NativeLibrary.RegisterFeedbackFromTactFileReflected(key, tactFileStr); }

        public static void SubmitRegistered(string key) { if (!_waserror) bHaptics_NativeLibrary.SubmitRegistered(key); }
        public static void SubmitRegistered(string key, int startTimeMillis) => bHaptics_NativeLibrary.SubmitRegisteredStartMillis(key, startTimeMillis);
        public static void SubmitRegistered(string key, string altKey, ScaleOption option) { if (!_waserror) bHaptics_NativeLibrary.SubmitRegisteredWithOption(key, altKey, option.Intensity, option.Duration, 1f, 1f); }
        public static void SubmitRegistered(string key, string altKey, ScaleOption sOption, RotationOption rOption) { if (!_waserror) bHaptics_NativeLibrary.SubmitRegisteredWithOption(key, altKey, sOption.Intensity, sOption.Duration, rOption.OffsetX, rOption.OffsetY); }

        public static void TurnOff() { if (!_waserror) bHaptics_NativeLibrary.TurnOff(); }
        public static void TurnOff(string key) { if (!_waserror) bHaptics_NativeLibrary.TurnOffKey(key); }

        public static void Submit(string key, PositionType position, byte[] bytes, int durationMillis)
        {
            if (_waserror)
                return;
            int bytes_size = bytes.Length;
            if (bytes_size != MaxBufferSize)
            {
                byte[] newbytes = new byte[MaxBufferSize];
                for (int i = 0; i < bytes_size; i++)
                    newbytes[i] = bytes[i];
            }
            bHaptics_NativeLibrary.SubmitByteArray(key, position, bytes, MaxBufferSize, durationMillis);
        }
        public static void Submit(string key, PositionType position, List<DotPoint> points, int durationMillis)
        {
            if (_waserror)
                return;
            byte[] bytes = new byte[MaxBufferSize];
            for (var i = 0; i < points.Count; i++)
            {
                DotPoint point = points[i];
                if ((point.Index < 0) || (point.Index > MaxBufferSize))
                    continue;
                bytes[point.Index] = (byte)point.Intensity;
            }
            bHaptics_NativeLibrary.SubmitByteArray(key, position, bytes, MaxBufferSize, durationMillis);
        }
        public static void Submit(string key, PositionType position, List<PathPoint> points, int durationMillis)
        {
            if (_waserror)
                return;
            PathPoint[] pathPoints = points.ToArray();
            bHaptics_NativeLibrary.SubmitPathArray(key, position, pathPoints, pathPoints.Length, durationMillis);
        }

        public static FeedbackStatus GetCurrentFeedbackStatus(PositionType pos)
        {
            if (_waserror)
                return default;
            FeedbackStatus status;
            bHaptics_NativeLibrary.TryGetResponseForPosition(pos, out status);
            return status;
        }

        public enum PositionType
        {
            All = 0, Left = 1, Right = 2,
            Vest = 3,
            Head = 4,
            Racket = 5,
            HandL = 6,
            HandR = 7,
            FootL = 8,
            FootR = 9,
            ForearmL = 10, ForearmR = 11,
            VestFront = 201, VestBack = 202,
            GloveLeft = 203, GloveRight = 204,
            Custom1 = 251, Custom2 = 252, Custom3 = 253, Custom4 = 254
        }

        public class RotationOption
        {
            public RotationOption(float offsetX, float offsetY)
            {
                OffsetX = offsetX;
                OffsetY = offsetY;
            }
            public float OffsetX, OffsetY;
            public override string ToString() => "RotationOption { OffsetX=" + OffsetX.ToString() +
                       ", OffsetY=" + OffsetY.ToString() + " }";
        }

        public class ScaleOption
        {
            public ScaleOption(float intensity, float duration)
            {
                Intensity = intensity;
                Duration = duration;
            }
            public float Intensity, Duration;
            public override string ToString() => "ScaleOption { Intensity=" + Intensity.ToString() +
                       ", Duration=" + Duration.ToString() + " }";
        }

        public class DotPoint
        {
            public DotPoint(int index, int intensity)
            {
                if ((index < 0) || (index > MaxBufferSize))
                    throw new Exception("Invalid argument index : " + index);
                Intensity = MelonUtils.Clamp(intensity, 0, 100);
                Index = index;
            }
            public int Index, Intensity;
            public override string ToString() => "DotPoint { Index=" + Index.ToString() +
                       ", Intensity=" + Intensity.ToString() + " }";
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PathPoint
        {
            public PathPoint(float x, float y, int intensity, int motorCount = 3)
            {
                X = MelonUtils.Clamp(x, 0f, 1f);
                Y = MelonUtils.Clamp(y, 0f, 1f);
                Intensity = MelonUtils.Clamp(intensity, 0, 100);
                MotorCount = MelonUtils.Clamp(motorCount, 0, 3);
            }
            public float X, Y;
            public int Intensity;

            // Number of maximum motors to vibrate
            // if 0 means default motor count, now 3
            public int MotorCount;

            public override string ToString() => "PathPoint { X=" + X.ToString() +
                       ", Y=" + Y.ToString() +
                       ", MotorCount=" + MotorCount.ToString() +
                       ", Intensity=" + Intensity.ToString() + " }";
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FeedbackStatus
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public int[] values;
        };
    }
}