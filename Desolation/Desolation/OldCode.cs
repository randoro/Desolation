using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{
    class OldCode
    {

        //public static float[,] createNoise(int xPos, int yPos, int width, int height)
        //{
        //    Console.WriteLine("Noice:");


        //    float[,] testNoise = GenerateWhiteNoise(xPos, yPos, width, height);
        //    float[,] testNoise2 = GenerateWhiteNoise(xPos, yPos, width, height);

        //    for (int i = 0; i < height; i++)
        //    {
        //        for (int j = 0; j < 16; j++)
        //        {
        //            testNoise2[j, i] = testNoise[width + j - 16, i];
        //        }
        //    }
        //    float[,] perlinNoise = GeneratePerlinNoise(testNoise, 4);

        //    float[,] perlinNoise2 = GeneratePerlinNoise(testNoise2, 4);


        //    float[,] testNoise3 = new float[width * 2, height];
        //    for (int i = 0; i < height; i++)
        //    {
        //        for (int j = 0; j < width; j++)
        //        {
        //            testNoise3[j, i] = perlinNoise[j, i];
        //            testNoise3[j + width, i] = perlinNoise2[j, i];
        //        }
        //    }

        //    //float[,] perlinNoise3 = GeneratePerlinNoise(testNoise3, 6);
        //    //float[,] perlinNoise = GeneratePerlinNoise(testNoise, 6);

        //    //for (int i = 0; i < width; i++)
        //    //{
        //    //    for (int j = 0; j < height; j++)
        //    //    {
        //    //        Console.Write("[" + perlinNoise[i, j] + "] ");
        //    //    }
        //    //    Console.WriteLine("");
        //    //}



        //    //int[,] oGridCells = { { 1, 2 }, { 3, 4 } };
        //    //float[] oResult = new float[width * height];
        //    //System.Buffer.BlockCopy(perlinNoise, 0, oResult, 0, width * height * 4);
        //    return testNoise3;
        //}



        //private static float[,] GenerateWhiteNoise(int xPos, int yPos, int width, int height)
        //{

        //    float[,] noise = new float[width, height];
        //    uint seed = Globals.getUniqueNumber(Globals.getUniquePositiveFromAny(xPos * 16), Globals.getUniquePositiveFromAny(yPos * 16));
        //    Random random = new Random((int)seed);

        //    for (int i = 0; i < height; i++)
        //    {
        //        for (int j = 0; j < width; j++)
        //        {
        //            noise[j, i] = (float)random.NextDouble() % 1f;
        //        }
        //    }

        //    return noise;
        //}



        //private static float[,] GenerateSmoothNoise(float[,] baseNoise, int octave)
        //{
        //    int width = baseNoise.GetLength(0);
        //    int height = baseNoise.GetLength(1);

        //    float[,] smoothNoise = new float[width, height];

        //    int samplePeriod = 1 << octave; // calculates 2 ^ k
        //    float sampleFrequency = 0.8f / samplePeriod;

        //    for (int i = 0; i < width; i++)
        //    {
        //        //calculate the horizontal sampling indices
        //        int sample_i0 = (i / samplePeriod) * samplePeriod;
        //        int sample_i1 = (sample_i0 + samplePeriod) % width; //wrap around
        //        float horizontal_blend = (i - sample_i0) * sampleFrequency;

        //        for (int j = 0; j < height; j++)
        //        {
        //            //calculate the vertical sampling indices
        //            int sample_j0 = (j / samplePeriod) * samplePeriod;
        //            int sample_j1 = (sample_j0 + samplePeriod) % height; //wrap around
        //            float vertical_blend = (j - sample_j0) * sampleFrequency;

        //            //blend the top two corners
        //            float top = Interpolate(baseNoise[sample_i0, sample_j0],
        //               baseNoise[sample_i1, sample_j0], horizontal_blend);

        //            //blend the bottom two corners
        //            float bottom = Interpolate(baseNoise[sample_i0, sample_j1],
        //               baseNoise[sample_i1, sample_j1], horizontal_blend);

        //            //final blend
        //            smoothNoise[i, j] = Interpolate(top, bottom, vertical_blend);
        //        }
        //    }

        //    return smoothNoise;
        //}

        //private static void makeNoise(int seed, float frequency, int width, int height, bool horizontal)
        //{
        //    float[] line = noise(seed, frequency, width);
        //    for (int j = 0; j < height; j++)
        //    {
        //        for (int i = 0; i < width; i++)
        //        {
        //            float newf = 0;
        //            if (horizontal)
        //            {
        //                newf = values[i, j] + line[i] / 2;
        //            }
        //            else
        //            {
        //                newf = values[i, j] + line[j] / 2;
        //            }
        //            values[i, j] = newf;
        //        }
        //    }
        //}

        //public static float[,] noises(int width, int height)
        //{
        //    for (int j = 0; j < height; j++)
        //    {
        //        for (int i = 0; i < width; i++)
        //        {
        //            values[i, j] = 0.5f;
        //        }
        //    }
        //    Random rand = new Random(0);
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int bol = rand.Next(0, 2);
        //        makeNoise(i, 4.2f + i * 0.11f, width, height, bol == 1 ? true : false);
        //    }
        //    //makeNoise(0, 4.0f, width, height, true);
        //    //makeNoise(1, 3.0f, width, height, true);
        //    //makeNoise(2, 0.4f, width, height, true);
        //    //makeNoise(3, 0.1f, width, height, true);
        //    //makeNoise(4, 1.0f, width, height, true);
        //    //makeNoise(0, 0.1f, width, height, false);
        //    //makeNoise(1, 3.0f, width, height, false);
        //    //makeNoise(2, 5.0f, width, height, false);
        //    //makeNoise(3, 2.0f, width, height, false);
        //    //makeNoise(4, 0.2f, width, height, false);

        //    return values;
        //}

        //public static float Interpolate(float x0, float x1, float alpha)
        //{
        //    return x0 * (1 - alpha) + alpha * x1;
        //}


        //private static float[,] GeneratePerlinNoise(float[,] baseNoise, int octaveCount)
        //{
        //    int width = baseNoise.GetLength(0);
        //    int height = baseNoise.GetLength(1);

        //    float[][,] smoothNoise = new float[octaveCount][,]; //an array of 2D arrays containing

        //    float persistance = 0.5f;

        //    //generate smooth noise
        //    for (int i = 0; i < octaveCount; i++)
        //    {
        //        smoothNoise[i] = GenerateSmoothNoise(baseNoise, i);
        //    }

        //    float[,] perlinNoise = new float[width, height];
        //    float amplitude = 1.0f;
        //    float totalAmplitude = 0.0f;

        //    //blend noise together
        //    for (int octave = octaveCount - 1; octave >= 0; octave--)
        //    {
        //        amplitude *= persistance;
        //        totalAmplitude += amplitude;

        //        for (int i = 0; i < width; i++)
        //        {
        //            for (int j = 0; j < height; j++)
        //            {
        //                perlinNoise[i, j] += smoothNoise[octave][i, j] * amplitude;
        //            }
        //        }
        //    }

        //    //normalisation
        //    for (int i = 0; i < width; i++)
        //    {
        //        for (int j = 0; j < height; j++)
        //        {
        //            perlinNoise[i, j] /= totalAmplitude;
        //        }
        //    }

        //    return perlinNoise;
        //}

        //public static Color GetColor(Color gradientStart, Color gradientEnd, byte t)
        //{
        //    float t2 = (float)t / 256f;
        //    float mod = t2 % 0.2f;
        //    float r = t2 - mod;
        //    float u = 1 - r;

        //    Color color = new Color(
        //       (int)(gradientStart.R * u + gradientEnd.R * r),
        //       (int)(gradientStart.G * u + gradientEnd.G * r),
        //       (int)(gradientStart.B * u + gradientEnd.B * r));

        //    return color;
        //}

        //public static float[] noise(int seed, float freq, int length)
        //{
        //    Random rand = new Random(seed);
        //    float phase = (float)(rand.NextDouble() * 2 * Math.PI);
        //    float[] returnValue = new float[length];
        //    for (int i = 0; i < length; i++)
        //    {
        //        returnValue[i] = (float)(Math.Sin(2 * Math.PI * freq * i / length + phase));
        //    }

        //    return returnValue;
        //}



        //public static float[,] DiamondSquare(int x1, int y1, int x2, int y2, float range, int level)
        //{
        //    if (level == 9)
        //    {
        //        for (int i = 0; i < x2 - x1 + 1; i++)
        //        {
        //            for (int j = 0; j < y2 - y1 + 1; j++)
        //            {
        //                values[i, j] = 0.5f;
        //            }
        //        }
        //    }
        //    if (level < 1)
        //    {
        //        return values;
        //    }


        //    Random rand = new Random(0);
        //    // diamonds
        //    for (int i = x1 + level; i < x2; i += level)
        //        for (int j = y1 + level; j < y2; j += level)
        //        {
        //            float a = values[i - level, j - level];
        //            float b = values[i, j - level];
        //            float c = values[i - level, j];
        //            float d = values[i, j];
        //            float e = values[i - level / 2, j - level / 2] = (a + b + c + d) / 4 + (float)rand.NextDouble() * range;
        //        }

        //    // squares
        //    for (int i = x1 + 2 * level; i < x2; i += level)
        //        for (int j = y1 + 2 * level; j < y2; j += level)
        //        {
        //            float a = values[i - level, j - level];
        //            float b = values[i, j - level];
        //            float c = values[i - level, j];
        //            float d = values[i, j];
        //            float e = values[i - level / 2, j - level / 2];

        //            float f = values[i - level, j - level / 2] = (a + c + e + values[i - 3 * level / 2, j - level / 2]) / 4 + (float)rand.NextDouble() * range;
        //            float g = values[i - level / 2, j - level] = (a + b + e + values[i - level / 2, j - 3 * level / 2]) / 4 + (float)rand.NextDouble() * range;
        //        }

        //    return DiamondSquare(x1, y1, x2, y2, range / 2, level / 2);
        //    //return null;
        //}
    }
}
