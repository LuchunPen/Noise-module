/*
Copyright (c) Luchunpen.
Date: 02.06.2016 16:10:52
*/

namespace Nano3.Noise
{
    public enum NoiseQuality: byte
    {
        Low,
        Standard,
        High
    }

    public interface INoiseCore1D
    {
        int Seed { get; }
        double Noise1D(double x);
    }
    public interface INoiseCore2D
    {
        int Seed { get; }
        double Noise2D(double x, double y);
    }
    public interface INoiseCore3D
    {
        int Seed { get; }
        double Noise3D(double x, double y, double z);
    }
    public interface INoiseCore4D
    {
        int Seed { get; }
        double Noise4D(double x, double y, double z, double w);
    }

    public interface INoiseModule1D
    {
        double GetValue(double x);
    }
    public interface INoiseModule2D
    {
        double GetValue(double x, double y);
    }
    public interface INoiseModule3D
    {
        double GetValue(double x, double y, double z);
    }
    public interface INoiseModule4D
    {
        double GetValue(double x, double y, double z, double w);

    }
}

