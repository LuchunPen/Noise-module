/*
Copyright (c) Luchunpen.
Date: 02/06/2016 13:32
*/

using System;

namespace Nano3.Noise
{
    public class QualitativeNoise : INoiseCore1D, INoiseCore2D, INoiseCore3D
    {
        private int _seed; public int Seed { get { return _seed; } }
        private HashF hash;
        private const int B = 256;
        private int[] m_perm = new int[B + B];

        public QualitativeNoise() : this (0) { }

        public QualitativeNoise(int seed)
        {
            _seed = seed;
            hash = new XXHash(seed);
            int i, j, k;
            for (i = 0; i < B; i++)
            {
                m_perm[i] = i;
            }

            while (--i != 0)
            {
                k = m_perm[i];
                j = hash.Range(0, B, i);
                m_perm[i] = m_perm[j];
                m_perm[j] = k;
            }

            for (i = 0; i < B; i++)
            {
                m_perm[B + i] = m_perm[i];
            }
        }

        private double FADE(double t) { return t * t * t * (t * (t * 6.0f - 15.0f) + 10.0f); }

        private double LERP(double t, double a, double b) { return (a) + (t) * ((b) - (a)); }

        private double GRAD1(int hash, double x)
        {
            int h = hash % 16;
            double grad = 1.0f + (h % 8);
            if ((h % 8) < 4) grad = -grad;
            return (grad * x);
        }

        private double GRAD2(int hash, double x, double y)
        {
            int h = hash % 16;
            double u = h < 4 ? x : y;
            double v = h < 4 ? y : x;
            int hn = h % 2;
            int hm = (h / 2) % 2;
            return ((hn != 0) ? -u : u) + ((hm != 0) ? -2.0f * v : 2.0f * v);
        }

        private double GRAD3(int hash, double x, double y, double z)
        {
            int h = hash % 16;
            double u = (h < 8) ? x : y;
            double v = (h < 4) ? y : (h == 12 || h == 14) ? x : z;
            int hn = h % 2;
            int hm = (h / 2) % 2;
            return ((hn != 0) ? -u : u) + ((hm != 0) ? -v : v);
        }

        public double Noise1D(double x)
        {
            return Noise1D(x, NoiseQuality.Standard);
        }
        public double Noise2D(double x, double y)
        {
            return Noise2D(x, y, NoiseQuality.Standard);
        }
        public double Noise3D(double x, double y, double z)
        {
            return Noise3D(x, y, z, NoiseQuality.Standard);
        }

        public double Noise1D(double x, NoiseQuality quality)
        {
            //returns a noise value between -0.5 and 0.5
            int ix0, ix1;
            double fx0, fx1;
            double s, n0, n1;

            ix0 = NoiseHelper.FastFloor(x);

            // Fractional part of x & y
            if (quality == NoiseQuality.Low) { fx0 = x - ix0; }
            else if (quality == NoiseQuality.Standard) { fx0 = MathEx.SCurve3(x - ix0); }
            else { fx0 = MathEx.SCurve5(x - ix0); }

            fx1 = fx0 - 1.0f;
            ix1 = (ix0 + 1) & 0xff;
            ix0 = ix0 & 0xff;       // Wrap to 0..255

            s = FADE(fx0);

            n0 = GRAD1(m_perm[ix0], fx0);
            n1 = GRAD1(m_perm[ix1], fx1);
            return 0.188f * LERP(s, n0, n1);
        }
        public double Noise2D(double x, double y, NoiseQuality quality)
        {
            //returns a noise value between -0.75 and 0.75
            int ix0, iy0, ix1, iy1;
            double fx0, fy0, fx1, fy1, s, t, nx0, nx1, n0, n1;

            ix0 = NoiseHelper.FastFloor(x);   // Integer part of x
            iy0 = NoiseHelper.FastFloor(y);   // Integer part of y

            // Fractional part of x & y
            if (quality == NoiseQuality.Low) { fx0 = x - ix0; fy0 = y - iy0; }
            else if (quality == NoiseQuality.Standard) { fx0 = MathEx.SCurve3(x - ix0); fy0 = MathEx.SCurve3(y - iy0); }
            else { fx0 = MathEx.SCurve5(x - ix0); fy0 = MathEx.SCurve5(y - iy0); }

            fx1 = fx0 - 1.0f;
            fy1 = fy0 - 1.0f;
            ix1 = (ix0 + 1) & 0xff; // Wrap to 0..255
            iy1 = (iy0 + 1) & 0xff;
            ix0 = ix0 & 0xff;
            iy0 = iy0 & 0xff;

            t = FADE(fy0);
            s = FADE(fx0);

            nx0 = GRAD2(m_perm[ix0 + m_perm[iy0]], fx0, fy0);
            nx1 = GRAD2(m_perm[ix0 + m_perm[iy1]], fx0, fy1);

            n0 = LERP(t, nx0, nx1);

            nx0 = GRAD2(m_perm[ix1 + m_perm[iy0]], fx1, fy0);
            nx1 = GRAD2(m_perm[ix1 + m_perm[iy1]], fx1, fy1);

            n1 = LERP(t, nx0, nx1);

            return 0.507f * LERP(s, n0, n1);
        }
        public double Noise3D(double x, double y, double z, NoiseQuality quality)
        {
            //returns a noise value between -1.5 and 1.5
            int ix0, iy0, ix1, iy1, iz0, iz1;
            double fx0, fy0, fz0, fx1, fy1, fz1;
            double s, t, r;
            double nxy0, nxy1, nx0, nx1, n0, n1;

            ix0 = NoiseHelper.FastFloor(x); // Integer part of x
            iy0 = NoiseHelper.FastFloor(y); // Integer part of y
            iz0 = NoiseHelper.FastFloor(z); // Integer part of z

            // Fractional part of x, y, z
            if (quality == NoiseQuality.Low) { fx0 = x - ix0; fy0 = y - iy0; fz0 = z - iz0; }
            else if (quality == NoiseQuality.Standard) { fx0 = MathEx.SCurve3(x - ix0); fy0 = MathEx.SCurve3(y - iy0); fz0 = MathEx.SCurve3(z - iz0); }
            else { fx0 = MathEx.SCurve5(x - ix0); fy0 = MathEx.SCurve5(y - iy0); fz0 = MathEx.SCurve5(z - iz0); }

            fx1 = fx0 - 1.0f;
            fy1 = fy0 - 1.0f;
            fz1 = fz0 - 1.0f;
            ix1 = (ix0 + 1) & 0xff; // Wrap to 0..255
            iy1 = (iy0 + 1) & 0xff;
            iz1 = (iz0 + 1) & 0xff;
            ix0 = ix0 & 0xff;
            iy0 = iy0 & 0xff;
            iz0 = iz0 & 0xff;

            r = FADE(fz0);
            t = FADE(fy0);
            s = FADE(fx0);

            nxy0 = GRAD3(m_perm[ix0 + m_perm[iy0 + m_perm[iz0]]], fx0, fy0, fz0);
            nxy1 = GRAD3(m_perm[ix0 + m_perm[iy0 + m_perm[iz1]]], fx0, fy0, fz1);
            nx0 = LERP(r, nxy0, nxy1);

            nxy0 = GRAD3(m_perm[ix0 + m_perm[iy1 + m_perm[iz0]]], fx0, fy1, fz0);
            nxy1 = GRAD3(m_perm[ix0 + m_perm[iy1 + m_perm[iz1]]], fx0, fy1, fz1);
            nx1 = LERP(r, nxy0, nxy1);

            n0 = LERP(t, nx0, nx1);

            nxy0 = GRAD3(m_perm[ix1 + m_perm[iy0 + m_perm[iz0]]], fx1, fy0, fz0);
            nxy1 = GRAD3(m_perm[ix1 + m_perm[iy0 + m_perm[iz1]]], fx1, fy0, fz1);
            nx0 = LERP(r, nxy0, nxy1);

            nxy0 = GRAD3(m_perm[ix1 + m_perm[iy1 + m_perm[iz0]]], fx1, fy1, fz0);
            nxy1 = GRAD3(m_perm[ix1 + m_perm[iy1 + m_perm[iz1]]], fx1, fy1, fz1);
            nx1 = LERP(r, nxy0, nxy1);

            n1 = LERP(t, nx0, nx1);

            return 0.936f * LERP(s, n0, n1);
        }
    }
}
