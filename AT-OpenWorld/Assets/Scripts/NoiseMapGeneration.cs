using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMapGeneration : MonoBehaviour
{
  public float [,] GenerateMap( int mDepth, int mWidth, float scale, float offsetX, float offsetZ, Wave[] waves)
    {
        //creates map with empty values
        float[,] noiseMap = new float[mDepth, mWidth];

        for (int zIndex = 0; zIndex < mDepth; zIndex++)
       {
            for(int xIndex = 0; xIndex < mWidth; xIndex++)
            {
                float sampleX = (xIndex + offsetX) / scale;
                float sampleZ = (zIndex +offsetZ) / scale;
                //float noise = 0f;
                float noise = Mathf.PerlinNoise(sampleX, sampleZ);
                float normalisation = 0f;

                foreach (Wave wave in waves)
                {
                    //generate noise using perlin for each wave
                    noise += wave.amplitude * Mathf.PerlinNoise(sampleX * wave.frequency + wave.seed, sampleZ * wave.frequency + wave.seed);
                    normalisation += wave.amplitude;
                }

                noise /= normalisation;
                noiseMap[zIndex, xIndex] = noise;
            }
        }
        return noiseMap;
    }
}

[System.Serializable]
public class Wave
{
    public float seed;
    public float frequency;
    public float amplitude;
}
