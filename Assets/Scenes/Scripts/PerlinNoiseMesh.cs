using UnityEngine;

public class PerlinNoiseMesh : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float threshold = 0.5f;
    [SerializeField, Range(0.05f, 0.3f)] private float scale = 0.1f;
    [SerializeField] private int gridSize = 100;
    [SerializeField] private float heightScale = 5f;
    private MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Procedural Terrain";

        Vector3[] vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        int[] triangles = new int[gridSize * gridSize * 6];

        int vertexIndex = 0;
        float invGridSize = 1f / gridSize;

        for (int x = 0; x <= gridSize; x++)
        {
            for (int z = 0; z <= gridSize; z++)
            {
                float noise = Mathf.PerlinNoise(x * scale, z * scale);
                float height = noise < threshold ? noise * heightScale : 0f;
                vertices[vertexIndex] = new Vector3(x * invGridSize - 0.5f, height, z * invGridSize - 0.5f);
                vertexIndex++;
            }
        }

        int triangleIndex = 0;
        int vertexOffset = 0;

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                triangles[triangleIndex + 0] = vertexOffset;
                triangles[triangleIndex + 1] = vertexOffset + gridSize + 1;
                triangles[triangleIndex + 2] = vertexOffset + 1;
                triangles[triangleIndex + 3] = vertexOffset + 1;
                triangles[triangleIndex + 4] = vertexOffset + gridSize + 1;
                triangles[triangleIndex + 5] = vertexOffset + gridSize + 2;

                vertexOffset++;
                triangleIndex += 6;
            }

            vertexOffset++;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        meshFilter.sharedMesh = mesh;
        meshFilter.sharedMesh.RecalculateBounds();
    }
}
