using System;

namespace PicoLogDataParser
{
    public class PicologDataHeader
    {
        public ushort header_bytes;             // Length, in bytes, of this header
        public char[] signature;                // "PicoLog for Windows"
        public uint version;
        public uint no_of_parameters;           // Number of parameters recorded
        public ushort[] parameters;             // Array of parameters
        public uint sample_no;                  // Same as no_of_samples, unless wraparound occurred
        public uint no_of_samples;              // Number of samples recorded so far
        public uint max_samples;
        public uint interval;                   // Sample interval
        public ushort interval_units;           // 0=femtoseconds, 4=milliseconds, 5=seconds, 6=minutes, 7=hours
        public uint trigger_sample;
        public ushort triggered;
        public uint first_sample;
        public uint sample_bytes;               // Length of each sample record
        public uint settings_bytes;             // Length of settings text after samples (copy of .pls file)
        public uint start_date;
        public uint start_time;
        public uint minimum_time;
        public uint maximum_time;
        public char[] notes;
        public uint current_time;
        public ushort stopAfter;
        public ushort maxTimeUnit;
        public uint maxSampleTime;
        public uint startTimeMsAccuracy;    // Time when sampling starts in real-time collection mode.
        public uint previousTimeMsAccuracy; // Time when the last sample was taken in real-time mode.
        public uint noOfDays;                   // The number of days elapsed since sampling in real-time collection mode started.
        public byte[] spare;
    }
}
