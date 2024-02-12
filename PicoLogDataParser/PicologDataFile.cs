using System;

namespace PicoLogDataParser
{
    public class PicologDataFile
    {
        public PicologDataHeader Header { get; set; }

        public List<PicologSample> Samples { get; set; }

        public PicologSettings Settings { get; set; }

        public PicologDataFile()
        {
            this.Header = new PicologDataHeader();
            this.Samples = new List<PicologSample>();
            this.Settings = new PicologSettings();
        }
    }
}
