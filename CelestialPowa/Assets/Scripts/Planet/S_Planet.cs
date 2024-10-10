using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Planet : MonoBehaviour
{
    [Header("Settings")] 
    [Range(2, 256)] 
    public int resolution = 2;
    public enum FaceRenderMask {All, Top, Bottom, Left, Right, Front, Back};
    public FaceRenderMask faceRenderMask;
    [Space(10)]
    [Header("References")]
    public S_ShapeSettings shapeSettings;
    public S_ColourSettings colourSettings;
    [Space(10)]
    
    [SerializeField, HideInInspector]
    private MeshFilter[] meshFilters;
    [HideInInspector]
    public bool shapeFoldoutSetting;
    [HideInInspector]
    public bool colorFoldoutSetting;
    private S_TerrainFaces[] terrainFaces;
    
    S_ShapeGenerator shapeGenerator;
    
    private void Initialize()
    {
        shapeGenerator = new S_ShapeGenerator(shapeSettings);
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new S_TerrainFaces[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back }; 

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObject = new GameObject("PlanetMesh");
                meshObject.transform.SetParent(transform);
            
                meshObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            terrainFaces[i] = new S_TerrainFaces(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }
    
    public void OnShapeSettingsChanged()
    {
        Initialize();
        GenerateMesh();
    }

    public void OnColourSettingsChanged()
    {
        Initialize();
        GenerateColors();
    }
    
    private void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }
    }
    private void GenerateColors()
    {
        foreach (MeshFilter m in meshFilters)
        {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colourSettings.planetColor;
        }
    }
}
