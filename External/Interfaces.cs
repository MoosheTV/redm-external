﻿using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace RedM.External
{
    public interface ISpatial
    {
        Vector3 Position { get; set; }
        Vector3 Rotation { get; set; }
    }

    public interface IExistable
    {
        bool Exists();
    }

    public interface IDeletable : IExistable
    {
        void Delete();
    }

    public abstract class PoolObject : INativeValue, IDeletable, IDisposable
    {
        protected PoolObject(int handle)
        {
            Handle = handle;
        }

        public int Handle { get; protected set; }
        public override ulong NativeValue
        {
            get { return (ulong)Handle; }
            set { Handle = unchecked((int)value); }
        }

        public abstract bool Exists();

        public abstract void Delete();

        public void Dispose()
        {
            Delete();
        }
    }
}
