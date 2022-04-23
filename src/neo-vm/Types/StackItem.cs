// Copyright (C) 2016-2021 The Neo Project.
// 
// The neo-vm is free software distributed under the MIT software license, 
// see the accompanying file LICENSE in the main directory of the
// project or http://www.opensource.org/licenses/mit-license.php 
// for more details.
// 
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

#pragma warning disable CS0659

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Neo.VM.Types
{
    /// <summary>
    /// The base class for all types in the VM.
    /// </summary>
    public abstract class StackItem : IEquatable<StackItem>
    {
        /// <summary>
        /// Represents <see langword="false"/> in the VM.
        /// </summary>
        public static StackItem False { get; } = new Boolean(false);

        /// <summary>
        /// Indicates whether the object is <see cref="Null"/>.
        /// </summary>
        public bool IsNull => this is Null;

        /// <summary>
        /// Represents <see langword="null"/> in the VM.
        /// </summary>
        public static StackItem Null { get; } = new Null();

        /// <summary>
        /// Represents <see langword="true"/> in the VM.
        /// </summary>
        public static StackItem True { get; } = new Boolean(true);

        /// <summary>
        /// The type of this VM object.
        /// </summary>
        public abstract StackItemType Type { get; }

        /// <summary>
        /// Convert the VM object to the specified type.
        /// </summary>
        /// <param name="type">The type to be converted to.</param>
        /// <returns>The converted object.</returns>
        public virtual StackItem ConvertTo(StackItemType type)
        {
            if (type == Type) return this;
            if (type == StackItemType.Boolean) return GetBoolean();
            throw new InvalidCastException();
        }

        /// <summary>
        /// Copy the object and all its children.
        /// </summary>
        /// <returns>The copied object.</returns>
        public StackItem DeepCopy()
        {
            return DeepCopy(new Dictionary<StackItem, StackItem>(ReferenceEqualityComparer.Instance));
        }

        internal virtual StackItem DeepCopy(Dictionary<StackItem, StackItem> refMap)
        {
            return this;
        }

        public sealed override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is StackItem item) return Equals(item);
            return false;
        }

        public virtual bool Equals(StackItem? other)
        {
            return ReferenceEquals(this, other);
        }

        public virtual bool Equals(StackItem? other, ref uint limits)
        {
            throw new NotImplementedException();
        }

        internal virtual bool Equals(StackItem? other, ExecutionEngineLimits limits)
        {
            return Equals(other);
        }

        /// <summary>
        /// Wrap the specified <see cref="object"/> and return an <see cref="InteropInterface"/> containing the <see cref="object"/>.
        /// </summary>
        /// <param name="value">The wrapped <see cref="object"/>.</param>
        /// <returns></returns>
        public static StackItem FromInterface(object? value)
        {
            if (value is null) return Null;
            return new InteropInterface(value);
        }

        /// <summary>
        /// Get the boolean value represented by the VM object.
        /// </summary>
        /// <returns>The boolean value represented by the VM object.</returns>
        public abstract bool GetBoolean();

        /// <summary>
        /// Get the integer value represented by the VM object.
        /// </summary>
        /// <returns>The integer value represented by the VM object.</returns>
        public virtual BigInteger GetInteger()
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Get the <see cref="object"/> wrapped by this interface and convert it to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <returns>The wrapped <see cref="object"/>.</returns>
        public virtual T? GetInterface<T>() where T : class
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Get the readonly span used to read the VM object data.
        /// </summary>
        /// <returns></returns>
        public virtual ReadOnlySpan<byte> GetSpan()
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Get the <see cref="string"/> value represented by the VM object.
        /// </summary>
        /// <returns>The <see cref="string"/> value represented by the VM object.</returns>
        public virtual string? GetString()
        {
            return Utility.StrictUTF8.GetString(GetSpan());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(sbyte value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(byte value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(short value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(ushort value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(int value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(uint value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(long value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(ulong value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(BigInteger value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(bool value)
        {
            return value ? True : False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(byte[] value)
        {
            return (ByteString)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(ReadOnlyMemory<byte> value)
        {
            return (ByteString)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator StackItem(string value)
        {
            return (ByteString)value;
        }
    }
}
