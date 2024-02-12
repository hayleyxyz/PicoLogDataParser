using System;
using System.IO;
using System.Text;

namespace PicoLogDataParser
{
    public class PicoDataSettingsReader
    {
        public Stream Stream { get; private set; }

        private uint settingsDataLength;

        public PicoDataSettingsReader(Stream stream, uint settingsDataLength)
        {
            this.Stream = stream;
            this.settingsDataLength = settingsDataLength;
        }

        public PicologSettings Read()
        {
            var reader = new BinaryReader(this.Stream, encoding: Encoding.UTF8, leaveOpen: true);
            var settings = new PicologSettings(reader.ReadBytes((int)this.settingsDataLength));

            return settings;
        }
    }
}
