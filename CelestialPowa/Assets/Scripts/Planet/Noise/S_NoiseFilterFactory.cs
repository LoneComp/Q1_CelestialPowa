using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class S_NoiseFilterFactory
{
    public static I_NoiseFilter CreateNoiseFilter(S_NoiseSettings _noiseSettings)
    {
        switch (_noiseSettings.filterType)
        {
            case S_NoiseSettings.FilterType.Simple :
                return new S_SimpleNoiseFilter(_noiseSettings);
            case S_NoiseSettings.FilterType.Rigid :
                return new S_RigidNoiseFilter(_noiseSettings);
        }
        return null;
    }
}
