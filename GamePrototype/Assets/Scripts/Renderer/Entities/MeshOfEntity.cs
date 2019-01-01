using GameEngine.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Renderer.Entities
{
    [Serializable]
    public class MeshOfEntity
    {
        public Mesh mesh;
        public Material material;
        public GameObject gameObject;
        public string Name;

        public MeshOfEntity() { }

        public MeshOfEntity(Mesh mesh, Material material)
        {
            this.mesh = mesh;
            this.material = material;
        }

        public GameObject GameObject
        {
            get
            {
                return gameObject;
            }
            set
            {
                gameObject = value;
            }
        }
    }
}
