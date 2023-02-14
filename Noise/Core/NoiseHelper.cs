/*
Copyright (c) Luchunpen.
Date: 02.06.2016 16:34:44
*/

using System;

namespace Nano3.Noise
{
    public static class NoiseHelper
    {
        public static int FastFloor(double x)
        {
            int xi = (int)x;
            return x < xi ? xi - 1 : xi;
        }

        public static double SeamlessNoise(double x, double y, double dx, double dy, INoiseModule4D noise)
        {
            double s = x;
            double t = y;

            double nx = (Math.Cos(s * 2.0f * Math.PI) * dx / (2.0f * Math.PI));
            double ny = (Math.Cos(t * 2.0f * Math.PI) * dy / (2.0f * Math.PI));
            double nz = (Math.Sin(s * 2.0f * Math.PI) * dx / (2.0f * Math.PI));
            double nw = (Math.Sin(t * 2.0f * Math.PI) * dy / (2.0f * Math.PI));

            return noise.GetValue(nx, ny, nz, nw);
        }
    }
}
