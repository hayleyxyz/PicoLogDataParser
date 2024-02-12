using System;
using System.Text;

namespace PicoLogDataParser
{
    public class PicologSettings
    {
        public byte[] Data { get; private set; }

        public PicologSettings()
        {
            //
        }

        public PicologSettings(byte[] data)
        {
            this.Data = data;
        }

        public override string ToString()
        {
            return Encoding.UTF8.GetString(this.Data);
        }
    }
}
