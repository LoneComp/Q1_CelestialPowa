using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_RigidNoiseFilter : I_NoiseFilter
{
    private S_NoiseSettings noiseSettings;
    private S_Noise noise = new S_Noise();

    public S_RigidNoiseFilter(S_NoiseSettings _noiseSettings)
    {
        this.noiseSettings = _noiseSettings;
    }
    
    public float Evaluate(Vector3 _point)
    {
        float weight = 1;
        float noiseValue = 0;
        float frequency = noiseSettings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < noiseSettings.numLayer; i++)
        {
            float v = 1 - Mathf.Abs(noise.Evaluate(_point * frequency + noiseSettings.centre));
            v *= v;
            v *= weight;
            weight = v;
            
            noiseValue += v * amplitude;
            frequency *= noiseSettings.roughness;
            amplitude *= noiseSettings.persistence;
        }
        
        noiseValue = Mathf.Max(0, noiseValue - noiseSettings.minValue);
        return noiseValue * noiseSettings.strength;
    }
}
