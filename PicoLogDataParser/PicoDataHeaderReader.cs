﻿using System;
using System.IO;
using System.Text;

namespace PicoLogDataParser
{
    public class PicoDataHeaderReader
    {
        public Stream Stream { get; private set; }

        public PicoDataHeaderReader(Stream stream)
        {
            this.Stream = stream;
        }

        public PicologDataHeader Read()
        {
            var reader = new BinaryReader(this.Stream, encoding: Encoding.UTF8, leaveOpen: true);
            var header = new PicologDataHeader();

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

            return header;
        }   
    }
}
