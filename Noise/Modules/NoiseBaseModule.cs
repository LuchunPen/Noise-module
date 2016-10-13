/*
Copyright (c) Luchunpen (bwolf88).  All rights reserved.
Date: 02.06.2016 17:30:53
*/

using System;

namespace Nano3.Noise
{
    public abstract class NoiseBaseModule
    {
        protected static readonly int _MAX_OCTAVES = 30;
        protected static readonly int _MIN_OCTAVES = 1;
        protected static readonly double _MIN_LACUNARITY = 0;
        protected static readonly double _MIN_PERSISTENCE = 0;

        public abstract int Seed { get; }

        protected double _frequency;
        public double Frequency { get { return  _frequency == 0 ? 0 : 1 / _frequency; } }

        protected double _amplitude;
        public double Amplitude { get { return _amplitude; } }

        protected int _octaves;
        public int Octaves { get { return _octaves; } }

        protected double _persistence;
        public double Persistence { get { return _persistence; } }

        protected double _lacunarity;
        public double Lacunarity { get { return _lacunarity; } }

        public NoiseBaseModule(double frequency, double amplitude, int octaves, double persistence = 0.5, double lacunarity = 2.0)
        {
            _frequency = frequency == 0 ? 0 : 1 / frequency;
            _amplitude = amplitude;
            _octaves = octaves > _MAX_OCTAVES ? _MAX_OCTAVES : octaves < _MIN_OCTAVES ? _MIN_OCTAVES : octaves;
            _persistence = persistence < _MIN_PERSISTENCE ? _MIN_PERSISTENCE : persistence;
            _lacunarity = lacunarity < _MIN_LACUNARITY ? _MIN_LACUNARITY : lacunarity;
        }

        protected double[] CalculateSpectralWeights()
        {
            double h = 1.0;

            double frequency = 1.0;
            double[] spectral = new double[_MAX_OCTAVES];
            for (int i = 0; i < _MAX_OCTAVES; i++)
            {
                // Compute weight for each frequency.
                spectral[i] = Math.Pow(frequency, -h);
                frequency *= Lacunarity;
            }
            return spectral;
        }
    }
}
