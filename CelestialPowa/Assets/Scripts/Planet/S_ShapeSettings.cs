using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SC_ShapeSettings", menuName = "Celestial Powa/Planet/ShapeSettings")]
public class S_ShapeSettings : ScriptableObject
{
    public float planetRadius = 10f;
    public Vector3 seed;
    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enabled;
        public bool useFirstLayerAsMask;
        public S_NoiseSettings noiseSettings;
    }

    public void UpdateSeed()
    {
        for (int i = 0; i < noiseLayers.Length; i++)
        {
            noiseLayers[i].noiseSettings.centre = seed; 
        }
    }
}
