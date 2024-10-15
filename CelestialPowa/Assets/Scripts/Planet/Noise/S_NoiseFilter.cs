using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SimpleNoiseFilter : I_NoiseFilter
{
    private S_NoiseSettings noiseSettings;
    private S_Noise noise = new S_Noise();

    public S_SimpleNoiseFilter(S_NoiseSettings _noiseSettings)
    {
        this.noiseSettings = _noiseSettings;
    }
    
    public float Evaluate(Vector3 _point)
    {
        float noiseValue = 0;
        float frequency = noiseSettings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < noiseSettings.numLayer; i++)
        {
            float v = noise.Evaluate(_point * frequency + noiseSettings.centre);
            noiseValue += (v + 1) * 0.5f * amplitude;
            frequency *= noiseSettings.roughness;
            amplitude *= noiseSettings.persistence;
        }
        
        noiseValue = Mathf.Max(0, noiseValue - noiseSettings.minValue);
        return noiseValue * noiseSettings.strength;
    }
}
