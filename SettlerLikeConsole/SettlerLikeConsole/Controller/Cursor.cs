/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 15:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using GameEngine;
using GameEngine.Environment;
using GameEngine.Factory;
using GameEngine.Factory.Component;
using GameEngine.Utils;
using GameEngine.Environment.Material;
using GameEngine.Factory.Entities;

namespace SettlerLikeConsole.Controller
{
	/// <summary>
	/// Description of Cursor.
	/// </summary>
	public static class Cursor
	{
		public static readonly ConsoleKey UP = ConsoleKey.Z;
		public static readonly ConsoleKey DOWN = ConsoleKey.S;
		public static readonly ConsoleKey LEFT = ConsoleKey.Q;
		public static readonly ConsoleKey RIGHT = ConsoleKey.D;
		
		public static readonly ConsoleKey ROTATE = ConsoleKey.R;
		public static readonly ConsoleKey ROTATE_INPUT = ConsoleKey.T;
		public static readonly ConsoleKey ROTATE_OUTPUT = ConsoleKey.Y;
		
		public static readonly ConsoleKey BUILD_HARVESTER = ConsoleKey.J;
		public static readonly ConsoleKey BUILD_CONVEYOR = ConsoleKey.K;
		public static readonly ConsoleKey BUILD_BUILDER = ConsoleKey.L;
		
		public static readonly ConsoleKey SAVE = ConsoleKey.O;
		public static readonly ConsoleKey LOAD = ConsoleKey.P;
		
		public static readonly char CHARACTER = 'X';
		private static Vector2Int position;
		
		public static void Set(int x, int y) {
			position = new Vector2Int(x, y);
			ControlPosition();
		}
		
		public static void Move(int directionX, int directionY) {
			position.X += directionX;
			position.Y += directionY;
			
			ControlPosition();
		}
		
		public static void Handle(ConsoleKey input) {
			if(input == SAVE) {
				Save.SerializeObject<World>(World.Instance, @"C:\Users\lcourtal\Documents\Games\save");
			} else if(input == LOAD) {
				World.Instance.LoadSave(Save.DeserializeObject<World>(@"C:\Users\lcourtal\Documents\Games\save"));
			} else if(input == UP)
				Move(0, -1);
			else if(input == DOWN)
				Move(0, 1);
			else if(input == LEFT)
				Move(-1, 0);
			else if(input == RIGHT)
				Move(1, 0);
			else if(input == ROTATE_INPUT) {
				var cell = GetFocusedCell();
				if(cell.FactoryEntity != null) {
					var grabber = cell.FactoryEntity.GetComponent<Grabber>();
					if(grabber != null) {
						grabber.Input = grabber.Input.Rotate(true);
					}
				}
	        }else if(input == ROTATE_OUTPUT) {
				var cell = GetFocusedCell();
				if(cell.FactoryEntity != null) {
					var grabber = cell.FactoryEntity.GetComponent<Grabber>();
					if(grabber != null) {
						grabber.Output = grabber.Output.Rotate(true);
					}
				}
	        } else if(input == ROTATE) {
//				var cell = GetFocusedCell();
//				if(cell.FactoryEntity != null && cell.FactoryEntity.GetType() == typeof(Conveyor)) {
//					(cell.FactoryEntity as Conveyor).Orientation = (cell.FactoryEntity as Conveyor).Orientation.Rotate();
//				}
			} else if(input == BUILD_HARVESTER) {
				var faction = World.Instance.FactionList.GetFaction("Player");
				var harvester = new FactoryEntity("Harvester", position, faction);
				harvester.AddComponent(new Generator(new Ressource("Iron", 1), harvester));
				var container = (Container)harvester.AddComponent(new Container(harvester));
				container.Ressource = new Ressource("Iron", 0);
				faction.AddFactoryEntity(harvester);
			}
			else if(input == BUILD_CONVEYOR) {
				var faction = World.Instance.FactionList.GetFaction("Player");
				var conveyor = new FactoryEntity("Conveyor", position, faction);
				conveyor.AddComponent(new Container(conveyor));
				var grabber = conveyor.AddComponent(new Grabber("Iron", 1, 2, conveyor));
				
				faction.AddFactoryEntity(conveyor);
//				var neighboors = new Dictionary<Type, List<IFactoryComponent>>();
//				foreach (Orientation direction in (Orientation[]) Enum.GetValues(typeof(Orientation)))
//				{
//					if(direction == Orientation.CENTER)
//						continue;
//					
//					var cell = direction.GetNeighboor(position);
//					if(cell != null && cell.FactoryComponent != null) {
//						if(neighboors.ContainsKey(cell.FactoryComponent.GetType()) == false) { 
//							neighboors.Add(cell.FactoryComponent.GetType(), new List<IFactoryComponent>());
//						}
//						if(neighboors[cell.FactoryComponent.GetType()] == null)
//							neighboors[cell.FactoryComponent.GetType()] = new List<IFactoryComponent>();
//						neighboors[cell.FactoryComponent.GetType()].Add(cell.FactoryComponent);
//					}
//				}
//				
//				Orientation orientation = Orientation.CENTER;
//				if(neighboors.ContainsKey(typeof(Harvester))) {
//					orientation = orientation.SetOrientationFromPosition(neighboors[typeof(Harvester)][0].Position, position);
//				} else if(neighboors.ContainsKey(typeof(Builder))) {
//					orientation = orientation.SetOrientationFromPosition(position, neighboors[typeof(Builder)][0].Position);
//				} else if(neighboors.ContainsKey(typeof(Conveyor))) {
//					var defaultOrientation = orientation;
//					foreach (Conveyor conveyor in neighboors[typeof(Conveyor)]) {
//						var conveyorFeedPosition = conveyor.Orientation.GetPositionOfTarget(position);
//						if(conveyorFeedPosition.X != position.X && conveyorFeedPosition.Y != position.Y) {
//							orientation = orientation.SetOrientationFromPosition(position, conveyor.Position);
//							break;
//						} else {
//							defaultOrientation = orientation.SetOrientationFromPosition(conveyor.Position, position);
//						}
//					}
//					if(orientation == Orientation.CENTER)
//						orientation = defaultOrientation;
//				}
//				//World.Instance.FactionList.GetFaction("Player").AddFactoryComponent(new Conveyor(position, orientation));
			}
			else if(input == BUILD_BUILDER) {
				var faction = World.Instance.FactionList.GetFaction("Player");
				var builder = new FactoryEntity("Builder", position, faction);
				builder.AddComponent(new Container(builder));
				faction.AddFactoryEntity(builder);
			}
			else if(input == ConsoleKey.Spacebar) {
				World.Instance.FactionList.Update();
			}
		}
		
		private static void ControlPosition() {
			int width = World.Instance.Terrain.Cells.GetLength(0);
			int height = World.Instance.Terrain.Cells.GetLength(1);
			
			if(position.X < 0)
				position.X = 0;
			else if(position.X >= width)
				position.X = width-1;
			
			if(position.Y < 0)
				position.Y = 0;
			else if(position.Y >= height)
				position.Y = height-1;
		}
		
		public static TerrainCell GetFocusedCell() {
			return World.Instance.Terrain.Cells[position.X, position.Y];
		}
		public static TerrainCell GetFocusedCell(int x, int y) {
			return World.Instance.Terrain.Cells[x, y];
		}
		
		public static Vector2Int Position {
			get {
				return position;
			}
		}
	}
}
