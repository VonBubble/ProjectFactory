/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 15:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using GameEngine;
using GameEngine.Environment;
using GameEngine.Factory;
using GameEngine.Factory.Component;
using GameEngine.Utils;
using SettlerLikeConsole.Renderer;

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
		
		public static readonly ConsoleKey BUILD_HARVESTER = ConsoleKey.H;
		public static readonly ConsoleKey BUILD_CONVEYOR = ConsoleKey.J;
		public static readonly ConsoleKey BUILD_BUILDER = ConsoleKey.K;
		
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
			if(input == UP)
				Move(0, -1);
			else if(input == DOWN)
				Move(0, 1);
			else if(input == LEFT)
				Move(-1, 0);
			else if(input == RIGHT)
				Move(1, 0);
			else if(input == BUILD_HARVESTER) {
				World.Instance.FactionList.GetFaction("Player").AddFactoryComponent(new Harvester(position));
			}
			else if(input == BUILD_CONVEYOR) {
				var neighboors = new Dictionary<Type, List<IFactoryComponent>>();
				foreach (Orientation direction in (Orientation[]) Enum.GetValues(typeof(Orientation)))
				{
					if(direction == Orientation.CENTER)
						continue;
					
					var cell = direction.GetNeighboor(position);
					if(cell != null && cell.FactoryComponent != null) {
						if(neighboors.ContainsKey(cell.FactoryComponent.GetType()) == false) { 
							neighboors.Add(cell.FactoryComponent.GetType(), new List<IFactoryComponent>());
						}
						if(neighboors[cell.FactoryComponent.GetType()] == null)
							neighboors[cell.FactoryComponent.GetType()] = new List<IFactoryComponent>();
						neighboors[cell.FactoryComponent.GetType()].Add(cell.FactoryComponent);
					}
				}
				
				Orientation orientation = Orientation.CENTER;
				if(neighboors.ContainsKey(typeof(Harvester))) {
					orientation = orientation.SetOrientationFromPosition(neighboors[typeof(Harvester)][0].Position, position);
				} else if(neighboors.ContainsKey(typeof(Builder))) {
					orientation = orientation.SetOrientationFromPosition(position, neighboors[typeof(Builder)][0].Position);
				} else if(neighboors.ContainsKey(typeof(Conveyor))) {
					var defaultOrientation = orientation;
					foreach (Conveyor conveyor in neighboors[typeof(Conveyor)]) {
						var conveyorFeedPosition = conveyor.Orientation.GetPositionOfTarget(position);
						if(conveyorFeedPosition.X != position.X && conveyorFeedPosition.Y != position.Y) {
							orientation = orientation.SetOrientationFromPosition(position, conveyor.Position);
							break;
						} else {
							defaultOrientation = orientation.SetOrientationFromPosition(conveyor.Position, position);
						}
					}
					if(orientation == Orientation.CENTER)
						orientation = defaultOrientation;
				}
				World.Instance.FactionList.GetFaction("Player").AddFactoryComponent(new Conveyor(position, orientation));
			}
			else if(input == BUILD_BUILDER) {
				World.Instance.FactionList.GetFaction("Player").AddFactoryComponent(new Builder(position));
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
