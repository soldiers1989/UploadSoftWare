using  System;
using  System.Runtime.InteropServices;
using  System.Resources;
using  System.IO;

namespace DY.FoodClientLib
{
    public class WavFilePlayer
    {
        //    flag  values  for  soundflags  argument  on  playsound
        public const int SND_SYNC = 0x0000;
        //  play  synchronously
        //  (default)
        public const int SND_ASYNC = 0x0001;
        //  play  asynchronously
        public const int SND_NODEFAULT = 0x0002;
        //  silence  (!default)
        //  if  sound  not  found
        public const int SND_MEMORY = 0x0004;
        //  pszSound  points  to
        //  a  memory  file
        public const int SND_LOOP = 0x0008;
        //  loop  the  sound  until
        //  next  sndPlaySound
        public const int SND_NOSTOP = 0x0010;
        //  don't  stop  any
        //  currently  playing
        //  sound
        public int snd_nowait = 0x00002000;
        //  don't  wait  if  the
        //  driver  is  busy
        public int SND_ALIAS = 0x00010000;
        //  name  is  a  Registry
        //  alias
        public int SND_ALIAS_ID = 0x00110000;
        //  alias  is  a  predefined
        //  ID
        public int SND_FILENAME = 0x00020000;
        //  name  is  file  name
        public int SND_RESOURCE = 0x00040004;
        //  name  is  resource  name
        //  or  atom
        public const int SND_PURGE = 0x0040;
        //  purge  non-static
        //  events  for  task
        public int SND_APPLICATION = 0x0080;
        //  look  for  application-
        //  specific  association

        [DllImport("Winmm.dll", CharSet = CharSet.Unicode)]
        public static extern bool PlaySound(string data, IntPtr hMod, UInt32 dwFlags);

        public void Winmm()
        {
        }

        public static void PlayWavResource(string wav)
        {
            PlaySound(wav, IntPtr.Zero, SND_ASYNC);
        }

        public static void StopPlay()
        {
            PlaySound(null, IntPtr.Zero, SND_PURGE);
        }
    }
}



