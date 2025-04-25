using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MiniScript.MSGS.Database
{
    public enum ValueTypeTag : byte
    {
        Int32,
        UInt32,
        Int64,
        UInt64,
        Byte,
        SByte,
        Int16,
        UInt16,
        Float,
        Double,
        ByteArray,
    }


    public class BaseRecord 
    {
        private List<(ValueTypeTag type, int offset)> layout = new();
        private byte[] data;
        private int position;

        public BaseRecord(int capacity = 256)
        {
            data = new byte[capacity];
            position = 0;
        }

        public int Count => layout.Count;

        public void AddValue(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value is int i)
                WritePrimitive(ValueTypeTag.Int32, BitConverter.GetBytes(i), 4);
            else if (value is uint ui)
                WritePrimitive(ValueTypeTag.UInt32, BitConverter.GetBytes(ui), 4);
            else if (value is long l)
                WritePrimitive(ValueTypeTag.Int64, BitConverter.GetBytes(l), 8);
            else if (value is ulong ul)
                WritePrimitive(ValueTypeTag.UInt64, BitConverter.GetBytes(ul), 8);
            else if (value is byte b)
                WritePrimitive(ValueTypeTag.Byte, new[] { b }, 1);
            else if (value is sbyte sb)
                WritePrimitive(ValueTypeTag.SByte, new[] { (byte)sb }, 1);
            else if (value is short s)
                WritePrimitive(ValueTypeTag.Int16, BitConverter.GetBytes(s), 2);
            else if (value is ushort us)
                WritePrimitive(ValueTypeTag.UInt16, BitConverter.GetBytes(us), 2);
            else if (value is float f)
                WritePrimitive(ValueTypeTag.Float, BitConverter.GetBytes(f), 4);
            else if (value is double d)
                WritePrimitive(ValueTypeTag.Double, BitConverter.GetBytes(d), 8);
            else if (value is byte[] arr)
                WriteByteArray(arr);
            else
                throw new NotSupportedException($"Type '{value.GetType()}' is not supported");
        }

        public object GetValue(int index)
        {
            var (type, offset) = layout[index];

            switch (type)
            {
                case ValueTypeTag.Int32: return BitConverter.ToInt32(data, offset);
                case ValueTypeTag.UInt32: return BitConverter.ToUInt32(data, offset);
                case ValueTypeTag.Int64: return BitConverter.ToInt64(data, offset);
                case ValueTypeTag.UInt64: return BitConverter.ToUInt64(data, offset);
                case ValueTypeTag.Byte: return data[offset];
                case ValueTypeTag.SByte: return (sbyte)data[offset];
                case ValueTypeTag.Int16: return BitConverter.ToInt16(data, offset);
                case ValueTypeTag.UInt16: return BitConverter.ToUInt16(data, offset);
                case ValueTypeTag.Float: return BitConverter.ToSingle(data, offset);
                case ValueTypeTag.Double: return BitConverter.ToDouble(data, offset);
                case ValueTypeTag.ByteArray:
                    int length = BitConverter.ToInt32(data, offset);
                    byte[] result = new byte[length];
                    Buffer.BlockCopy(data, offset + 4, result, 0, length);
                    return result;
                default:
                    throw new InvalidOperationException($"Unhandled type {type}");
            }
        }

        private void WritePrimitive(ValueTypeTag tag, byte[] bytes, int size)
        {
            EnsureCapacity(1 + size);
            data[position++] = (byte)tag;
            Buffer.BlockCopy(bytes, 0, data, position, size);
            layout.Add((tag, position));
            position += size;
        }

        private void WriteByteArray(byte[] bytes)
        {
            int length = bytes.Length;
            EnsureCapacity(1 + 4 + length);
            data[position++] = (byte)ValueTypeTag.ByteArray;
            BitConverter.TryWriteBytes(data.AsSpan(position, 4), length);
            Buffer.BlockCopy(bytes, 0, data, position + 4, length);
            layout.Add((ValueTypeTag.ByteArray, position));
            position += 4 + length;
        }

        private void EnsureCapacity(int additionalBytes)
        {
            if (position + additionalBytes > data.Length)
            {
                Array.Resize(ref data, data.Length * 2);
            }
        }
    }
}
