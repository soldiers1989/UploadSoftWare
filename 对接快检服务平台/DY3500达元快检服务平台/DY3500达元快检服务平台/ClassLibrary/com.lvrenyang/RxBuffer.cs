
using System;
namespace com.lvrenyang
{
    public class RxBuffer
    {
        int Read, Write, RxSize;
        byte[] buffer;

        public RxBuffer(int SIZE)
        {
            Read = Write = 0;
            RxSize = SIZE;
            buffer = new byte[RxSize];
        }

        public byte GetByte()
        {
            byte ch;
            lock (this)
            {
                ch = buffer[Read++];
                if (Read > (RxSize - 1))
                    Read = 0;
            }
            return ch;
        }

        public void PutByte(byte ch)
        {
            lock (this)
            {
                buffer[Write++] = ch;
                if (Write > RxSize - 1)
                    Write = 0;
            }
        }

        public void Clear()
        {
            lock (this)
            {
                Write = Read = 0;
            }
        }

        public bool IsEmpty()
        {
            return (Read == Write);
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] ReadData(int length)
        {
            byte[] Destion = new byte[length];
            Array.Copy(buffer, 0, Destion, 0, length);
            Console.WriteLine("数组长度：" + Destion.Length);
            return Destion;
        }
        /// <summary>
        ///复制数据到数组
        /// </summary>
        /// <param name="source"></param>
        /// <param name="desIndex"></param>
        public void CopyArray(byte[] source, int desIndex)
        {
            Write = desIndex + source.Length;//写入数据长度
            Console.WriteLine("写入长度：" + desIndex);
            Array.Copy(source, 0, buffer, desIndex, source.Length);
        }
    }
}