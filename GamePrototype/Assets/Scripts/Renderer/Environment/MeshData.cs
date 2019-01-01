using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Renderer.Environment
{
    public class MeshData
    {
        public static readonly int VERTICES_MAX = 60000;

        private int width;
        private int height;

        private List<Vector3> vertices;
        private List<Vector3> normals;
        private List<Vector2> uvs;
        private List<int> triangles;

        public MeshData(int width, int height)
        {
            this.width = width;
            this.height = height;

            vertices = new List<Vector3>();
            normals = new List<Vector3>();
            uvs = new List<Vector2>();
            triangles = new List<int>();
        }
        
        public int AddVertice(float x, float y, float z)
        {
            Vector3 vertice = new Vector3(x, y, z);
            vertices.Add(vertice);
            normals.Add(Vector3.up);
            uvs.Add(new Vector2(x / (width - 1f), z / (height - 1f)));

            return vertices.Count - 1;
        }

        public void AddTriangle(int a, int b, int c)
        {
            triangles.Add(a);
            triangles.Add(b);
            triangles.Add(c);
        }

        public Mesh Mesh
        {
            get
            {
                Mesh mesh = new Mesh();
                mesh.vertices = vertices.ToArray();
                mesh.normals = normals.ToArray();
                mesh.uv = uvs.ToArray();
                mesh.triangles = triangles.ToArray();
                return mesh;
            }
        }
    }
}
