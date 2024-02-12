using System;
using System.IO;
using System.Text;

namespace PicoLogDataParser
{
    public class PicoDataSampleReader
    {
        public Stream Stream { get; private set; }

        public uint SampleDataLength { get; private set; }

        public PicoDataSampleReader(Stream stream, uint sampleDataLength)
        {
            this.Stream = stream;
            this.SampleDataLength = sampleDataLength;
        }

        public PicologSample Read()
        {
            var reader = new BinaryReader(this.Stream, encoding: Encoding.UTF8, leaveOpen: true);
            var sample = new PicologSample();

            sample.SampleNo = reader.ReadUInt32();
            sample.Values = new List<uint>();

            var valueCount = (this.SampleDataLength / 4);

            for (int i = 0; i < valueCount - 1; i++)
            {
                sample.Values.Add(reader.ReadUInt32());
            }

            return sample;
        }   
    }
}
