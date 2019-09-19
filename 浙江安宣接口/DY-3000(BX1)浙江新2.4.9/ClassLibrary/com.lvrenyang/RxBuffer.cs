
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
            lock(this)
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
    }
}
