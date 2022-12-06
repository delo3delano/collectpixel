using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelizedMesh : MonoBehaviour
{
    public List<Vector3Int> GridPoints = new List<Vector3Int>();
    public float HalfSize = 0.1f;
    public Vector3 LocalOrigin;
    [SerializeField] private GameObject newObject;

    private void Start()
    {
        //VoxelizeMesh(newObject.MeshFilter);
    }
    public Vector3 PointToPosition(Vector3Int point)
    {
        float size = HalfSize * 2f;
        Vector3 pos = new Vector3(HalfSize + point.x * size, HalfSize + point.y * size, HalfSize + point.z * size);
        return LocalOrigin + transform.TransformPoint(pos);
    }
    public void VoxelizeMesh(MeshFilter meshFilter)
    {
        if (!meshFilter.TryGetComponent(out MeshCollider meshCollider))
        {
            meshCollider = meshFilter.gameObject.AddComponent<MeshCollider>();
        }

        if (!meshFilter.TryGetComponent(out VoxelizedMesh voxelizedMesh))
        {
            voxelizedMesh = meshFilter.gameObject.AddComponent<VoxelizedMesh>();
        }

        Bounds bounds = meshCollider.bounds;
        Vector3 minExtents = bounds.center - bounds.extents;
        float halfSize = voxelizedMesh.HalfSize;
        Vector3 count = bounds.extents / halfSize;

        int xMax = Mathf.CeilToInt(count.x);
        int yMax = Mathf.CeilToInt(count.y);
        int zMax = Mathf.CeilToInt(count.z);

        voxelizedMesh.GridPoints.Clear();
        voxelizedMesh.LocalOrigin = voxelizedMesh.transform.InverseTransformPoint(minExtents);

        for (int x = 0; x < xMax; ++x)
        {
            for (int z = 0; z < zMax; ++z)
            {
                for (int y = 0; y < yMax; ++y)
                {
                    Vector3 pos = voxelizedMesh.PointToPosition(new Vector3Int(x, y, z));
                    if (Physics.CheckBox(pos, new Vector3(halfSize, halfSize, halfSize)))
                    {
                        voxelizedMesh.GridPoints.Add(new Vector3Int(x, y, z));
                    }
                }
            }
        }
    }
}

// [CustomEditor(typeof(VoxelizedMesh))]
// public class VoxelizedMeshEditor : MonoBehaviour
// {
//     void OnSceneGUI()
//     {
//         VoxelizedMesh voxelizedMesh = target as VoxelizedMesh;

//         Handles.color = Color.green;
//         float size = voxelizedMesh.HalfSize * 2f;

//         foreach (Vector3Int gridPoint in voxelizedMesh.GridPoints)
//         {
//             Vector3 worldPos = voxelizedMesh.PointToPosition(gridPoint);
//             Handles.DrawWireCube(worldPos, new Vector3(size, size, size));
//         }

//         Handles.color = Color.red;
//         if (voxelizedMesh.TryGetComponent(out MeshCollider meshCollider))
//         {
//             Bounds bounds = meshCollider.bounds;
//             Handles.DrawWireCube(bounds.center, bounds.extents * 2);
//         }
//     }
// }
