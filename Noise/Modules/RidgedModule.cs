/*
Copyright (c) Luchunpen (bwolf88).  All rights reserved.
Date: 02.06.2016 18:17:32
*/

using System;

namespace Nano3.Noise
{
    public class RidgedModule1D : NoiseBaseModule, INoiseModule1D
    {
        private INoiseCore1D _noise;
        public override int Seed { get { return _noise.Seed; } }

        private double[] _spectralWeight;

        public RidgedModule1D(INoiseCore1D noiseCore, double frequency, double amplitude, int octaves, double persistence = 0.5, double lacunarity = 2) 
            : base(frequency, amplitude, octaves, persistence, lacunarity)
        {
            if (noiseCore == null) { throw new ArgumentNullException("Noise core is null"); }
            _noise = noiseCore;
            _spectralWeight = CalculateSpectralWeights();
        }

        public double GetValue(double x)
        {
            x *= _frequency;

            double signal = 0.0;
            double value = 0.0;
            double weight = 1.0;

            double offset = 1.0;
            double gain = 2.0;

            for (int currentOctave = 0; currentOctave < Octaves; currentOctave++)
            {
                signal = _noise.Noise1D(x);

                // Make the ridges.
                signal = Math.Abs(signal);
                signal = offset - signal;

                // Square the signal to increase the sharpness of the ridges.
                signal *= signal; signal *= weight;
                // Weight successive contributions by the previous signal.
                weight = signal * gain;
                if (weight > 1.0) { weight = 1.0; }
                if (weight < 0.0) { weight = 0.0; }

                // Add the signal to the output value.
                value += (signal * _spectralWeight[currentOctave]);
                // Go to the next octave.
                x *= _lacunarity;
            }
            return ((value * 1.25) - 1.0) * _amplitude;
        }
    }

    public class RidgedModule2D : NoiseBaseModule, INoiseModule2D
    {
        private INoiseCore2D _noise;
        public override int Seed { get { return _noise.Seed; } }

        private double[] _spectralWeight;

        public RidgedModule2D(INoiseCore2D noiseCore, double frequency, double amplitude, int octaves, double persistence = 0.5, double lacunarity = 2)
            : base(frequency, amplitude, octaves, persistence, lacunarity)
        {
            if (noiseCore == null) { throw new ArgumentNullException("Noise core is null"); }
            _noise = noiseCore;
            _spectralWeight = CalculateSpectralWeights();
        }

        public double GetValue(double x, double y)
        {
            x *= _frequency; y *= _frequency;

            double signal = 0.0;
            double value = 0.0;
            double weight = 1.0;

            double offset = 1.0;
            double gain = 2.0;

            for (int currentOctave = 0; currentOctave < Octaves; currentOctave++)
            {
                signal = _noise.Noise2D(x, y);

                // Make the ridges.
                signal = Math.Abs(signal);
                signal = offset - signal;

                // Square the signal to increase the sharpness of the ridges.
                signal *= signal; signal *= weight;
                // Weight successive contributions by the previous signal.
                weight = signal * gain;
                if (weight > 1.0) { weight = 1.0; }
                if (weight < 0.0) { weight = 0.0; }

                // Add the signal to the output value.
                value += (signal * _spectralWeight[currentOctave]);
                // Go to the next octave.
                x *= _lacunarity; y *= _lacunarity;
            }
            return ((value * 1.25) - 1.0) * _amplitude;
        }
    }

    public class RidgedModule3D : NoiseBaseModule, INoiseModule3D
    {
        private INoiseCore3D _noise;
        public override int Seed { get { return _noise.Seed; } }

        private double[] _spectralWeight;

        public RidgedModule3D(INoiseCore3D noiseCore, double frequency, double amplitude, int octaves, double persistence = 0.5, double lacunarity = 2)
            : base(frequency, amplitude, octaves, persistence, lacunarity)
        {
            if (noiseCore == null) { throw new ArgumentNullException("Noise core is null"); }
            _noise = noiseCore;
            _spectralWeight = CalculateSpectralWeights();
        }

        public double GetValue(double x, double y, double z)
        {
            x *= _frequency; y *= _frequency; z *= _frequency;

            double signal = 0.0;
            double value = 0.0;
            double weight = 1.0;

            double offset = 1.0;
            double gain = 2.0;

            for (int currentOctave = 0; currentOctave < Octaves; currentOctave++)
            {
                signal = _noise.Noise3D(x, y, z);

                // Make the ridges.
                signal = Math.Abs(signal);
                signal = offset - signal;

                // Square the signal to increase the sharpness of the ridges.
                signal *= signal; signal *= weight;
                // Weight successive contributions by the previous signal.
                weight = signal * gain;
                if (weight > 1.0) { weight = 1.0; }
                if (weight < 0.0) { weight = 0.0; }

                // Add the signal to the output value.
                value += (signal * _spectralWeight[currentOctave]);
                // Go to the next octave.
                x *= _lacunarity; y *= _lacunarity; z *= _lacunarity;
            }
            return ((value * 1.25) - 1.0) * _amplitude;
        }
    }

    public class RidgedModule4D : NoiseBaseModule, INoiseModule4D
    {
        private INoiseCore4D _noise;
        public override int Seed { get { return _noise.Seed; } }

        private double[] _spectralWeight;

        public RidgedModule4D(INoiseCore4D noiseCore, double frequency, double amplitude, int octaves, double persistence = 0.5, double lacunarity = 2)
            : base(frequency, amplitude, octaves, persistence, lacunarity)
        {
            if (noiseCore == null) { throw new ArgumentNullException("Noise core is null"); }
            _noise = noiseCore;
            _spectralWeight = CalculateSpectralWeights();
        }

        public double GetValue(double x, double y, double z, double w)
        {
            x *= _frequency; y *= _frequency; z *= _frequency; w *= _frequency;

            double signal = 0.0;
            double value = 0.0;
            double weight = 1.0;

            double offset = 1.0;
            double gain = 2.0;

            for (int currentOctave = 0; currentOctave < Octaves; currentOctave++)
            {
                signal = _noise.Noise4D(x, y, z, w);

                // Make the ridges.
                signal = Math.Abs(signal);
                signal = offset - signal;

                // Square the signal to increase the sharpness of the ridges.
                signal *= signal; signal *= weight;
                // Weight successive contributions by the previous signal.
                weight = signal * gain;
                if (weight > 1.0) { weight = 1.0; }
                if (weight < 0.0) { weight = 0.0; }

                // Add the signal to the output value.
                value += (signal * _spectralWeight[currentOctave]);
                // Go to the next octave.
                x *= _lacunarity; y *= _lacunarity; z *= _lacunarity; w *= _lacunarity;
            }
            return ((value * 1.25) - 1.0) * _amplitude;
        }
    }
}
