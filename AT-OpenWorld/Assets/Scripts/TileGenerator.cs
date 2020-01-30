using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{

    [SerializeField]
    private c_TerrainType[] terrainTypes;

    [SerializeField]
    NoiseMapGeneration mapGeneration;

    [SerializeField]
    private MeshRenderer tileRenderer;

    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private MeshCollider meshCollider;

    [SerializeField]
    private float mapScale;

    [SerializeField]
    private float heightMultiplier;

    [SerializeField]
    private AnimationCurve heightCurve;

    [SerializeField]
    private Wave[] waves;

    private void Start()
    {
        GenerateTile();

    }

    void GenerateTile()
    {
        //calculate depth and width based on vertices
        Vector3[] meshVertices = this.meshFilter.mesh.vertices;
        int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
        int tileWidth = tileDepth;

        //generate offset based on pos
        float offsetX = -this.gameObject.transform.position.x;
        float offsetZ = -this.gameObject.transform.position.z;
        //calculate offset


        float[,] heightMap = this.mapGeneration.GenerateMap(tileDepth, tileWidth, this.mapScale, offsetX, offsetZ, waves);


        //generate heightmap
        Texture2D tileTexture = BuildTexture(heightMap);
        this.tileRenderer.material.mainTexture = tileTexture;

        UpdateMeshVertices(heightMap);
          
    }

    private Texture2D BuildTexture(float[,] heightMap)
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);

        Color[] colourMap = new Color[tileDepth * tileWidth];
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                //transforms map index to array index

                int colourIndex = zIndex * tileWidth + xIndex;
                float height = heightMap[zIndex, xIndex];
                //Choose Terrain Type
                c_TerrainType terrain = ChooseTerrainType(height);

                colourMap[colourIndex] = terrain.Colour;
            }
        }

        //Create new texture and set its colour
        Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels(colourMap);
        tileTexture.Apply();

        return tileTexture;
   }

    private void UpdateMeshVertices(float[,] heightMap)
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);

        Vector3[] meshVertices = this.meshFilter.mesh.vertices;

        //Loops through all coordinates and updates each vertex
        int vertexIndex = 0;
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                float height = heightMap[zIndex, xIndex];

                Vector3 vertex = meshVertices[vertexIndex];

                //change Y value according to height

                meshVertices[vertexIndex] = new Vector3(vertex.x, height * this.heightCurve.Evaluate(height) * this.heightMultiplier, vertex.z);

                vertexIndex++;
            }
        }

        //Update each vertex
        this.meshFilter.mesh.vertices = meshVertices;
        this.meshFilter.mesh.RecalculateBounds();
        this.meshFilter.mesh.RecalculateNormals();

        //update collider
        this.meshCollider.sharedMesh = this.meshFilter.mesh;
    }
    
    c_TerrainType ChooseTerrainType(float height)
    {
        //checks if height is lower than the one for the expected type

        foreach(c_TerrainType terrainType in terrainTypes)
        {
            if (height < terrainType.height)
            {
                return terrainType;
            }

        }
        //else null
        return terrainTypes[terrainTypes.Length - 1];
    }
}

//Terrain Class (c for class smileyface )
[System.Serializable]
public class c_TerrainType
{
    public string name;
    public float height;
    public Color Colour;
}

