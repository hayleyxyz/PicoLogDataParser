using System;
using System.IO;
using System.Reflection.PortableExecutable;

namespace PicoLogDataParser
{
    internal class PicoDataFileReader
    {
        public Stream Stream { get; private set; }

        public PicoDataFileReader(Stream stream)
        {
            this.Stream = stream;
        }

        public PicologDataFile Read()
        {
            var reader = new BinaryReader(this.Stream);
            var file = new PicologDataFile();

            file.Header = (new PicoDataHeaderReader(this.Stream)).Read();

            for (int i = 0; i < file.Header.no_of_samples; i++)
            {
                file.Samples.Add((new PicoDataSampleReader(this.Stream, file.Header.sample_bytes)).Read());
            }

            reader.BaseStream.Seek(file.Header.header_bytes + 0x4000, SeekOrigin.Begin);

            file.Settings = (new PicoDataSettingsReader(this.Stream, file.Header.settings_bytes)).Read();

            return file;
        }   
    }
}
