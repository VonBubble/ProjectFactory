using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Renderer.Environment
{
    public class TerrainRenderer: MonoBehaviour
    {        
        private GameEngine.Environment.Terrain terrain;

        public void Generate(int width, int height, Material material)
        {
            terrain = World.Instance.Terrain;
            terrain.GenerateNewMap(width, height);

            var meshData = new MeshData(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var elevation = 0;
                    var upper_left =  meshData.AddVertice(x + 0, elevation, y + 1);
                    var upper_right = meshData.AddVertice(x + 1, elevation, y + 1);
                    var lower_left =  meshData.AddVertice(x + 0, elevation, y + 0);
                    var lower_right = meshData.AddVertice(x + 1, elevation, y + 0);

                    meshData.AddTriangle(lower_left, upper_left, upper_right);
                    meshData.AddTriangle(lower_left, upper_right, lower_right);
                }
            }

            var meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer == null)
                meshRenderer = gameObject.AddComponent<MeshRenderer>();
            var meshFilter = GetComponent<MeshFilter>();
            if (meshFilter == null)
                meshFilter = gameObject.AddComponent<MeshFilter>();
            var meshCollider = GetComponent<MeshCollider>();
            if (meshCollider == null)
                meshCollider = gameObject.AddComponent<MeshCollider>();

            meshFilter.mesh = meshData.Mesh;
            meshCollider.sharedMesh = meshFilter.mesh;
            meshRenderer.material = material;
        }

        public void Update()
        {
            
        }
    }
}
