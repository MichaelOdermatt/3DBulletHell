using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangels;
    Vector2[] uvs;
    private float[,] noiseMap;
    private Color[] colorMap;

    public int xSize = 100;
    public int zSize = 100;
    public float heightMultiplier = 6.0f;
    public float noiseScale = 15;
    public AnimationCurve meshHeightCurve;
    public int octaves = 3;
    public float persistance = 0.5f;
    public float lacunarity = 2.5f;
    public bool autoUpdate = true;
    public int seed;
    public Vector2 offset;
    public TerrainType[] regions;

    public void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        mesh = new Mesh();    
        GetComponent<MeshFilter>().mesh = mesh;

        noiseMap = NoiseGenerator.GenerateNoiseMap(xSize + 1, zSize + 1, seed, noiseScale, octaves, persistance, lacunarity, offset, meshHeightCurve);

        createShape();
        flatshading();
        setColorMap();
        updateMesh();
        drawTexture();
    }

    void createShape()
    {

        // set the vertices for the mesh

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = noiseMap[x, z] * heightMultiplier;

                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        // set the triangles for the mesh

        triangels = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangels[tris + 0] = vert;
                triangels[tris + 1] = vert + xSize + 1;
                triangels[tris + 2] = vert + 1;
                triangels[tris + 3] = vert + 1;
                triangels[tris + 4] = vert + xSize + 1;
                triangels[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }

            vert++;
        }

        // set the uvs for the mesh

        uvs = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2((float)x / xSize,(float)z / zSize);
                i++;
            }
        }

    }

    public void updateAll()
    {
        noiseMap = NoiseGenerator.GenerateNoiseMap(xSize + 1, zSize + 1, seed, noiseScale, octaves, persistance, lacunarity, offset, meshHeightCurve);

        createShape();
        //flatshading();
        setColorMap();
        updateMesh();
        drawTexture();
    }

    void updateMesh()
    {
        //noiseMap = NoiseGenerator.GenerateNoiseMap(xSize + 1, zSize + 1, seed, noiseScale, octaves, persistance, lacunarity, offset, meshHeightCurve);

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangels;
        mesh.colors = colorMap;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
    }

    void flatshading()
    {
        Vector3[] flatShadedVerticies = new Vector3[triangels.Length];
        Vector2[] flatShadedUvs = new Vector2[triangels.Length];

        for (int i = 0; i < triangels.Length; i++)
        {
            flatShadedVerticies[i] = vertices[triangels[i]];
            flatShadedUvs[i] = uvs[triangels[i]];
            triangels[i] = i;
        }

        vertices = flatShadedVerticies;
        uvs = flatShadedUvs;
    }
    
    public void setColorMap()
    {
        colorMap = new Color[vertices.Length];

        for (int z = 0; z <= zSize ; z++)
        {
            for (int x = 0; x <= xSize ; x++)
            {
                float currentHeight = noiseMap[x, z];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[z * xSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
    }

    public void drawTexture()
    {
        Texture2D texture = new Texture2D(xSize, zSize);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        texture.SetPixels(colorMap);
        texture.Apply();

        //textureRenderer.sharedMaterial.mainTexture = texture;
        GetComponent<MeshRenderer>().sharedMaterial.mainTexture = texture;
    }

}

[System.Serializable]
public struct TerrainType
{
    public string Name;
    public float height;
    public Color color;
}
