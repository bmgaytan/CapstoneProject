using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateMesh(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        
        int verticeScale = 10;
        bool flipDirection = true;


        MeshData meshData = new MeshData(width, height);
        int vert = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float finalPosX = (x - width / 2f) * verticeScale * (flipDirection ? -1 : 1);
                float finalPoxZ = (y - height / 2f) * verticeScale * (flipDirection ? -1 : 1);

                meshData.vertices[vert] = new Vector3(finalPosX, noiseMap[x, y], finalPoxZ);
                meshData.uvs[vert] = new Vector2(x / (float)width, y / (float)height);

                if (x < width - 1 && y < height - 1)
                {
                    meshData.AddTriangles(vert, vert + width + 1, vert + width);
                    meshData.AddTriangles(vert + width + 1, vert, vert + 1);

                }
                vert++;
            }
        }
        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    int triangleIndex;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshWidth];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
        uvs = new Vector2[meshWidth * meshHeight];
    }

    public void AddTriangles(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
