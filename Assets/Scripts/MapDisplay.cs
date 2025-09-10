using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public GameObject terrainMesh;
    Vector3[] vertices;

    public void DrawTexture(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void GenerateMesh(float[,] noiseMap)
    {
        int mapWidth = noiseMap.GetLength(0);
        int mapHeight = noiseMap.GetLength(1);

        int verticeScale = 10;
        bool flipDirection = true;

        vertices = new Vector3[(mapWidth + 1) * (mapHeight + 1)];

        Mesh mesh = terrainMesh.GetComponent<MeshFilter>().sharedMesh;

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
