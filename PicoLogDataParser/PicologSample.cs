using System;
using System.Collections.Generic;

namespace PicoLogDataParser
{
    public class PicologSample
    {
        public uint SampleNo { get; set; }
        public List<uint> Values { get; set; }

        public PicologSample()
        {
            this.Values = new List<uint>();
        }
    }
}
