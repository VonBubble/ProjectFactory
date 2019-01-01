using UnityEngine;
using Assets.Scripts.Renderer.Environment;

namespace Assets.Scripts.Renderer
{
    public class WorldRenderer : MonoBehaviour
    {
        public Vector2Int size;
        public Material terrainMaterial;

        // Use this for initialization
        void Start()
        {
            var terrain = new GameObject("Terrain");
            terrain.transform.parent = this.transform;
            terrain.transform.position = Vector3.zero;
            var terrainRenderer = terrain.AddComponent<TerrainRenderer>();
            terrainRenderer.Generate(size.x, size.y, terrainMaterial);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
