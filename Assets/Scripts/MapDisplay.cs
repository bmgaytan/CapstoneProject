using System;
using Unity.Mathematics;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    Mesh mesh;
    float verticeOffsetX;
    float verticeOffsetZ;
    int verticeScale = 10;
    bool flipDirection = true;
    Vector3[] vertices;
    public Renderer textureRenderer;

    public Renderer terrainMesh;

    public void DrawTexture(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
    
    public void GenerateMesh(float[,] noiseMap, int mapWidth, int mapHeight)
    {
        mesh = new Mesh();
        terrainMesh.GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[(mapWidth + 1) * (mapHeight + 1)];

        verticeOffsetX = mapWidth / 2 * 10;
        verticeOffsetZ = mapWidth / 2 * 10;


        for (int z = 0, i = 0; z < mapHeight; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float finalPosX = (x - mapWidth / 2f) * verticeScale * (flipDirection ? -1 : 1);
                float finalPoxZ = (z - mapHeight / 2f) * verticeScale * (flipDirection ? -1 : 1);
                vertices[i] = new Vector3(finalPosX, noiseMap[x, z] * 20, finalPoxZ);
                i++;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
    }

    void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .9f);
        }
    }
}
