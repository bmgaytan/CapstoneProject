using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public GameObject terrainMesh;
    Vector3[] vertices;
    int mapWidth;
    int mapHeight;
    int[] triangles;
    Mesh mesh;
    MeshFilter meshFilter;

    public void DrawTexture(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void GenerateMesh(float[,] noiseMap)
    {
        mapWidth = noiseMap.GetLength(0);
        mapHeight = noiseMap.GetLength(1);

        int verticeScale = 10;
        bool flipDirection = true;

        vertices = new Vector3[(mapWidth + 1) * (mapHeight + 1)];

        meshFilter = terrainMesh.GetComponent<MeshFilter>();

        mesh = new Mesh();
        meshFilter.sharedMesh = mesh;

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

        triangles = new int[mapWidth * mapHeight * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < mapHeight; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                triangles[0 + tris] = vert + 0;
                triangles[1 + tris] = vert + mapWidth;
                triangles[2 + tris] = vert + 1;
                triangles[3 + tris] = vert + 1;
                triangles[4 + tris] = vert + mapWidth;
                triangles[5 + tris] = vert + mapWidth + 1;

                vert++;
                tris += 6;
            }
            vert++;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }
    void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        /*or (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .9f);
        }*/
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(vertices[0], 0.9f);
        Gizmos.DrawSphere(vertices[mapWidth - 1], 0.9f);
        Gizmos.DrawSphere(vertices[(mapHeight - 1) * (mapWidth + 1)], 0.9f);
        Gizmos.DrawSphere(vertices[mapHeight * (mapWidth - 1)], 0.9f);


    }

}
