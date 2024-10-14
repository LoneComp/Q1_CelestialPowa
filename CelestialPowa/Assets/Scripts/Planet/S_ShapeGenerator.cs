using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ShapeGenerator
{
    S_ShapeSettings settings;
    I_NoiseFilter[] noiseFilters;

    public S_ShapeGenerator(S_ShapeSettings _settings)
    {
        this.settings = _settings;
        noiseFilters = new I_NoiseFilter[_settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = S_NoiseFilterFactory.CreateNoiseFilter(_settings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0;
        float elevation = 0;

        if (noiseFilters.Length > 0)
        {
            firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
            
            if (settings.noiseLayers[0].enabled)
                elevation = firstLayerValue;
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (settings.noiseLayers[i].enabled)
            {
                float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }
        
        return pointOnUnitSphere * settings.planetRadius * (1 + elevation);
    }
}
