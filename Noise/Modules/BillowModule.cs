/*
Copyright (c) Luchunpen (bwolf88).  All rights reserved.
Date: 02.06.2016 17:43:57
*/

using System;

namespace Nano3.Noise
{
    public class BillowModule1D : NoiseBaseModule, INoiseModule1D
    {
        private INoiseCore1D _noise;
        public override int Seed { get { return _noise.Seed; } }

        public BillowModule1D(INoiseCore1D noiseCore, double frequency, double amplitude, int octaves, double persistence = 0.5, double lacunarity = 2) 
            : base(frequency, amplitude, octaves, persistence, lacunarity)
        {
            if (noiseCore == null) { throw new ArgumentNullException("Noise core is null"); }
            _noise = noiseCore;
        }

        public double GetValue(double x)
        {
            double value = 0.0;
            double signal = 0.0;
            double curPersistence = 1.0;

            x *= _frequency;
            for (int currentOctave = 0; currentOctave < Octaves; currentOctave++)
            {
                signal = _noise.Noise1D(x);
                signal = 2.0 * Math.Abs(signal) - 1.0;
                value += signal * curPersistence;

                x *= Lacunarity;
                curPersistence *= Persistence;
            }
            value += 0.5;
            return value * Amplitude;
        }
    }

    public class BillowModule2D : NoiseBaseModule, INoiseModule2D
    {
        private INoiseCore2D _noise;
        public override int Seed { get { return _noise.Seed; } }

        public BillowModule2D(INoiseCore2D noiseCore, double frequency, double amplitude, int octaves, double persistence = 0.5, double lacunarity = 2)
            : base(frequency, amplitude, octaves, persistence, lacunarity)
        {
            if (noiseCore == null) { throw new ArgumentNullException("Noise core is null"); }
            _noise = noiseCore;
        }

        public double GetValue(double x, double y)
        {
            double value = 0.0;
            double signal = 0.0;
            double curPersistence = 1.0;

            x *= _frequency; y *= _frequency;
            for (int currentOctave = 0; currentOctave < Octaves; currentOctave++)
            {
                signal = _noise.Noise2D(x, y);
                signal = 2.0 * Math.Abs(signal) - 1.0;
                value += signal * curPersistence;

                x *= Lacunarity;
                y *= Lacunarity;
                curPersistence *= Persistence;
            }
            value += 0.5;
            return value * Amplitude;
        }
    }

    public class BillowModule3D : NoiseBaseModule, INoiseModule3D
    {
        private INoiseCore3D _noise;
        public override int Seed { get { return _noise.Seed; } }

        public BillowModule3D(INoiseCore3D noiseCore, double frequency, double amplitude, int octaves, double persistence = 0.5, double lacunarity = 2)
            : base(frequency, amplitude, octaves, persistence, lacunarity)
        {
            if (noiseCore == null) { throw new ArgumentNullException("Noise core is null"); }
            _noise = noiseCore;
        }

        public double GetValue(double x, double y, double z)
        {
            double value = 0.0;
            double signal = 0.0;
            double curPersistence = 1.0;

            x *= _frequency; y *= _frequency; z *= _frequency;
            for (int currentOctave = 0; currentOctave < Octaves; currentOctave++)
            {
                signal = _noise.Noise3D(x, y, z);
                signal = 2.0 * Math.Abs(signal) - 1.0;
                value += signal * curPersistence;

                x *= Lacunarity;
                y *= Lacunarity;
                z *= Lacunarity;
                curPersistence *= Persistence;
            }
            value += 0.5;
            return value * Amplitude;
        }
    }

    public class BillowModule4D : NoiseBaseModule, INoiseModule4D
    {
        private INoiseCore4D _noise;
        public override int Seed { get { return _noise.Seed; } }

        public BillowModule4D(INoiseCore4D noiseCore, double frequency, double amplitude, int octaves, double persistence = 0.5, double lacunarity = 2)
            : base(frequency, amplitude, octaves, persistence, lacunarity)
        {
            if (noiseCore == null) { throw new ArgumentNullException("Noise core is null"); }
            _noise = noiseCore;
        }

        public double GetValue(double x, double y, double z, double w)
        {
            double value = 0.0;
            double signal = 0.0;
            double curPersistence = 1.0;

            x *= _frequency; y *= _frequency; z *= _frequency; w *= _frequency;
            for (int currentOctave = 0; currentOctave < Octaves; currentOctave++)
            {
                signal = _noise.Noise4D(x, y, z, w);
                signal = 2.0 * Math.Abs(signal) - 1.0;
                value += signal * curPersistence;

                x *= Lacunarity;
                y *= Lacunarity;
                z *= Lacunarity;
                w *= Lacunarity;
                curPersistence *= Persistence;
            }
            value += 0.5;
            return value * Amplitude;
        }
    }
}
