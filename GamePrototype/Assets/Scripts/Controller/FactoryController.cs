using Assets.Scripts.Renderer.Environment;
using GameEngine;
using GameEngine.Environment.Material;
using GameEngine.Factory;
using GameEngine.Factory.Component;
using GameEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class FactoryController: MonoBehaviour
    {
        private FactoryEntity factoryToSpawn;

        public void SetFactoryToSpawn(string name)
        {
            factoryToSpawn = new FactoryEntity(name, GameEngine.Utils.Vector2Int.Null);
            PutInto putInto = null;
            switch (name)
            {
                case "Conveyor":
                    factoryToSpawn.AddComponent(new Container(factoryToSpawn));
                    putInto = (PutInto)factoryToSpawn.AddComponent(new PutInto("", 1, 1, factoryToSpawn));
                    putInto.Target = Orientation.NORTH;
                    break;
                case "Harvester":
                    factoryToSpawn.AddComponent(new Generator(3, new Ressource("Iron", 1), factoryToSpawn));
                    factoryToSpawn.AddComponent(new Container(factoryToSpawn));
                    putInto = (PutInto)factoryToSpawn.AddComponent(new PutInto("", 1, 1, factoryToSpawn));
                    putInto.Target = Orientation.NORTH;
                    break;
                default:
                    break;
            }
        }

        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                World.Instance.FactionList.Update();
            }

            if (Input.GetMouseButtonUp(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit))
                {
                    var go = hit.collider.gameObject;
                    int x = (int)(hit.point.x);
                    int y = (int)(hit.point.z);
                    if (go.GetComponent<TerrainRenderer>())
                    {
                        var cell = World.Instance.Terrain.GetTerrainCellAt(x, y);
                        if (cell.FactoryEntity == null)
                        {
                            factoryToSpawn.Position = cell.Position;
                            var faction = World.Instance.FactionList.GetFaction("Player");
                            faction.AddFactoryEntity(factoryToSpawn);
                            SetFactoryToSpawn(factoryToSpawn.Name);
                        }
                        else
                        {
                            var putInto = cell.FactoryEntity.GetComponent<PutInto>();
                            if (putInto != null)
                            {
                                putInto.Rotate();
                            }
                        }
                    }
                }
            }
        }
    }
}
