# Noise-module

No confusing fractional values like "0.001" to amplitude or frequency.
Using **IModule** interface you can change noise module on fly.

Example to use:


INoiseModule2D perlin = new PerlinModule2D(new OpenSimplexNoise(), 100, 25, 4);

INoiseModule3D ridged = new RidgedModule3D(new SimplexNoise(), 100, 50, 7);

INoiseModule3D billow = new BillowModule3D(new QualitativeNoise(), 200, 55, 5);
