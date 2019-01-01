using Assets.Scripts.Renderer.Entities;
using Assets.Scripts.Renderer.Entities.Factory;
using Assets.Scripts.Utils;
using GameEngine;
using GameEngine.Factory;
using GameEngine.Factory.Component;
using GameEngine.Factory.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Watcher
{
    public class FactionWatcher: MonoBehaviour
    {
        public List<MeshOfEntity> factoryEntitiesPrefabs;
        private Dictionary<string, GameObject> hashMapPrefabs;
        private Dictionary<string, MeshOfEntity> hashMapMesh;
        private Dictionary<string, List<Matrix4x4>> hashMapPositions;
        public Mesh ressourceMesh;
        public Material ressourceMat;

        private Faction faction;
        private Dictionary<Vector3, GameObject> entities;

        public void Start()
        {
            hashMapPrefabs = new Dictionary<string, GameObject>();
            hashMapMesh = new Dictionary<string, MeshOfEntity>();
            foreach (var prefab in factoryEntitiesPrefabs)
            {
                hashMapMesh.Add(prefab.Name, prefab);
                hashMapPrefabs.Add(prefab.Name, prefab.GameObject);
            }
            factoryEntitiesPrefabs.Clear();

            faction = World.Instance.FactionList.GetFaction("Player");
            faction.EntityBuilt += HandleEntityBuilt;

            entities = new Dictionary<Vector3, GameObject>();
            hashMapPositions = new Dictionary<string, List<Matrix4x4>>();
        }

        public void Update()
        {
            //foreach (var entityName in hashMapPositions.Keys)
            //{
            //    Graphics.DrawMeshInstanced(hashMapMesh[entityName].mesh, 0, hashMapMesh[entityName].material, hashMapPositions[entityName]);
            //    //foreach (var rotation in hashMapPositions[entityName])
            //    //{
            //    //    Debug.Log("Entity rotated at " + new Vector3(
            //    //    rotation.GetColumn(0).magnitude,
            //    //    rotation.GetColumn(1).magnitude,
            //    //    rotation.GetColumn(2).magnitude
            //    //));
            //    //}
            //}
        }

        private Matrix4x4 GetMatrixFromEntity(FactoryEntity entity)
        {
            var matrix = new Matrix4x4();
            var rotation = Quaternion.identity;
            var putInto = entity.GetComponent<PutInto>();
            if(putInto != null)
            {
                rotation = OrientationConverter.ToQuaternion(putInto.Target);
            }
            matrix.SetTRS(new Vector3(entity.Position.X + 0.5f, 0, entity.Position.Y + 0.5f), rotation, new Vector3(0.75f, 0.25f, 0.75f));
            return matrix;
        }

        private void HandleEntityBuilt(object sender, PositionEventArgs args)
        {
            //var entity = (FactoryEntity)sender;
            //if (entity != null)
            //{
            //    var name = entity.Name;
            //    var position = entity.Position;
            //    //var meshOfEntity = hashMapMesh[name];
            //    if (hashMapPositions.ContainsKey(name) == false)
            //        hashMapPositions.Add(name, new List<Matrix4x4>());
            //    hashMapPositions[name].Add(GetMatrixFromEntity(entity));

            //    var putInto = entity.GetComponent<PutInto>();
            //    if(putInto != null)
            //    {
            //        putInto.RaiseRotatedEvent += HandleRotationEntity;
            //    }
            //}

            var entity = World.Instance.Terrain.GetTerrainCellAt(args.X, args.Y);
            var putInto = entity.FactoryEntity.GetComponent<PutInto>();
            var generator = entity.FactoryEntity.GetComponent<Generator>();
            var position = new Vector3(args.X + 0.5f, 0, args.Y + 0.5f);
            var quaternion = Quaternion.identity;
            if (putInto != null)
            {
                quaternion = OrientationConverter.ToQuaternion(putInto.Target);
                putInto.RaiseRotatedEvent += HandleRotationEntity;
            }
            if (generator != null)
            {
                generator.OnProgressMade += HandleEntityProgress;
            }
            var go = Instantiate(hashMapPrefabs[entity.FactoryEntity.Name], position, quaternion, transform);
            var handler = go.AddComponent<ComponentHandler>();
            handler.Initialize(entity.FactoryEntity);
            entities.Add(position, go);
            if (entity.FactoryEntity.GetComponent<IProgressableComponent>() != null)
            {
                var child = go.transform.Find("Ressource");
                var filter = child.gameObject.AddComponent<MeshFilter>();
                var renderer = child.gameObject.AddComponent<MeshRenderer>();
                filter.mesh = ressourceMesh;
                renderer.material = ressourceMat;
            }
        }

        private void HandleRotationEntity(object sender, PositionEventArgs args)
        {
            //var putInto = (PutInto)sender;
            //var position = new Vector3(args.X + 0.5f, 0, args.Y + 0.5f);
            //for (int i = 0; i < hashMapPositions[putInto.Parent.Name].Count; i++)
            //{
            //    var candidateMatrix = hashMapPositions[putInto.Parent.Name][i];
            //    Vector3 candidatePosition = candidateMatrix.GetColumn(3);
            //    if (candidatePosition.x != position.x || candidatePosition.z != position.z)
            //        continue;

            //    Quaternion candidateRotation = OrientationConverter.ToQuaternion(putInto.Target);
            //    // Extract new local scale
            //    Vector3 candidateScale = new Vector3(
            //        candidateMatrix.GetColumn(0).magnitude,
            //        candidateMatrix.GetColumn(1).magnitude,
            //        candidateMatrix.GetColumn(2).magnitude
            //    );
            //    candidateMatrix.SetTRS(candidatePosition, candidateRotation, candidateScale);

            //    candidatePosition.x += (i + 1);
            //    var matrix = new Matrix4x4();
            //    matrix.SetTRS(candidatePosition, candidateRotation, candidateScale);
            //    hashMapPositions[putInto.Parent.Name].Add(matrix);
            //    Debug.Log("Rotated to " + putInto.Target);
            //    break;
            //}
        }

        private void HandleEntityProgress(object sender, EventArgs args)
        {
            var entityGenerator = (Generator)sender;
        }

    }
}
