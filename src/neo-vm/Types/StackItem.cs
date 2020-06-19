#pragma warning disable CS0659

using Neo.VM.Collections;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Neo.VM.Types
{
    public abstract class StackItem : IEquatable<StackItem>
    {
        public static StackItem False { get; } = new Boolean(false);
        public bool IsNull => this is Null;
        public static StackItem Null { get; } = new Null();
        public static StackItem True { get; } = new Boolean(true);
        public abstract StackItemType Type { get; }

        public virtual StackItem ConvertTo(StackItemType type)
        {
            if (type == Type) return this;
            if (type == StackItemType.Boolean) return ToBoolean();
            throw new InvalidCastException();
        }

        public StackItem DeepCopy()
        {
            return DeepCopy(new Dictionary<StackItem, StackItem>(ReferenceEqualityComparer.Default));
        }

        internal virtual StackItem DeepCopy(Dictionary<StackItem, StackItem> refMap)
        {
            return this;
        }

        public sealed override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is StackItem item) return Equals(item);
            return false;
        }

        public virtual bool Equals(StackItem other)
        {
            return ReferenceEquals(this, other);
        }

        public static StackItem FromInterface(object value)
        {
            if (value is null) return Null;
            return new InteropInterface(value);
        }

        public abstract bool ToBoolean();

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
