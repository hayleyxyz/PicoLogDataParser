using System;
using System.IO;

namespace PicoLogDataParser
{
    internal class Program
    {
        public struct PicoLogHeader
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

        struct PicoSample
        {
            public uint index;
            public uint[] values;
        }

        static void Main(string[] args)
        {
            string filePath = args[0];

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var picoReader = new PicoDataFileReader(stream);

            var file = picoReader.Read();

            return;

            // Create an instance of the struct
            PicoLogHeader header = new PicoLogHeader();
            var samples = new List<PicoSample>();
            byte[] settings;

            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    // Read each field from the binary file
                    header.header_bytes = reader.ReadUInt16();
                    header.signature = reader.ReadChars(40);
                    header.version = reader.ReadUInt32();
                    header.no_of_parameters = reader.ReadUInt32();
                    header.parameters = new ushort[250];
                    for (int i = 0; i < header.parameters.Length; i++)
                    {
                        header.parameters[i] = reader.ReadUInt16();
                    }
                    header.sample_no = reader.ReadUInt32();
                    Console.WriteLine($"sample_no: {header.sample_no} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.no_of_samples = reader.ReadUInt32();
                    Console.WriteLine($"no_of_samples: {header.no_of_samples} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.max_samples = reader.ReadUInt32();
                    Console.WriteLine($"max_samples: {header.max_samples} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.interval = reader.ReadUInt32();
                    Console.WriteLine($"interval: {header.interval} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.interval_units = reader.ReadUInt16();
                    Console.WriteLine($"interval_units: {header.interval_units} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.trigger_sample = reader.ReadUInt32();
                    Console.WriteLine($"trigger_sample: {header.trigger_sample} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.triggered = reader.ReadUInt16();
                    Console.WriteLine($"triggered: {header.triggered} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.first_sample = reader.ReadUInt32();
                    Console.WriteLine($"first_sample: {header.first_sample} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.sample_bytes = reader.ReadUInt32();
                    Console.WriteLine($"sample_bytes: {header.sample_bytes} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.settings_bytes = reader.ReadUInt32();
                    Console.WriteLine($"settings_bytes: {header.settings_bytes} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.start_date = reader.ReadUInt32(); // This is wrong, it should be Picodate
                    Console.WriteLine($"start_date: {header.start_date} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.start_time = reader.ReadUInt32(); // This is wrong, it should be Fulltime
                    Console.WriteLine($"start_time: {header.start_time} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.minimum_time = reader.ReadUInt32();
                    Console.WriteLine($"minimum_time: {header.minimum_time} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.maximum_time = reader.ReadUInt32();
                    Console.WriteLine($"maximum_time: {header.maximum_time} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.notes = reader.ReadChars(1000);
                    Console.WriteLine($"notes: {header.notes} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.current_time = reader.ReadUInt32();
                    Console.WriteLine($"current_time: {header.current_time} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.stopAfter = reader.ReadUInt16();
                    Console.WriteLine($"stopAfter: {header.stopAfter} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.maxTimeUnit = reader.ReadUInt16();
                    Console.WriteLine($"maxTimeUnit: {header.maxTimeUnit} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.maxSampleTime = reader.ReadUInt32();
                    Console.WriteLine($"maxSampleTime: {header.maxSampleTime} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.startTimeMsAccuracy = reader.ReadUInt32(); // This is wrong, it should be Fulltime
                    Console.WriteLine($"startTimeMsAccuracy: {header.startTimeMsAccuracy} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.previousTimeMsAccuracy = reader.ReadUInt32(); // This is wrong, it should be Fulltime
                    Console.WriteLine($"previousTimeMsAccuracy: {header.previousTimeMsAccuracy} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.noOfDays = reader.ReadUInt32();
                    Console.WriteLine($"noOfDays: {header.noOfDays} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    header.spare = new byte[58];
                    Console.WriteLine($".spare: {header.spare} {reader.BaseStream.Position} 0x{reader.BaseStream.Position.ToString("x2")}");
                    for (int i = 0; i < header.spare.Length; i++)
                    {
                        header.spare[i] = reader.ReadByte();
                    }

                    Console.WriteLine($"Position: {reader.BaseStream.Position}");

                    for (int i = 0; i < header.no_of_samples; i++)
                    {
                        var numberOfValuesToRead = (header.sample_bytes / 4) - 1;

                        var sample = new PicoSample
                        {
                            index = reader.ReadUInt32(),
                            values = new uint[numberOfValuesToRead]
                        };

                        for (int j = 0; j < numberOfValuesToRead; j++)
                        {
                            sample.values[j] = reader.ReadUInt32();
                        }

                        samples.Add(sample);

                        Console.WriteLine($"Sample {sample.index} {String.Join(" ", sample.values)} ");
                    }

                    reader.BaseStream.Seek(header.header_bytes + 0x4000, SeekOrigin.Begin); // TODO: Is it always 0x4000?

                    settings = reader.ReadBytes((int)header.settings_bytes);

                    Console.WriteLine($"Settings: {System.Text.Encoding.UTF8.GetString(settings)}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
