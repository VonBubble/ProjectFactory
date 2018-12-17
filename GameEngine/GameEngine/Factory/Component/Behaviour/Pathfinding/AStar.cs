/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 14/12/2018
 * Time: 10:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using GameEngine.Utils;

namespace GameEngine.Factory.Component.Behaviour.Pathfinding
{
	/// <summary>
	/// Description of AStar.
	/// </summary>
	public class AStar: IPathfinding
	{
		public int i;
		private Vector2Int origin;
		private Vector2Int destination;
		private List<Vector2Int> path = new List<Vector2Int>();
		private Thread thread;
		
		public void Start() {
			if(thread == null)
				thread = new Thread(Update);
			
			thread.Start();
		}
		public void Update() {
			i = 0;
			path.Clear();
			if(origin == destination)
				return;
			
			var visited = new List<Vector2Int>();
			var waitingVisit = new List<Vector2Int>();
			waitingVisit.Add(origin);
			
			var originToNode = new Dictionary<Vector2Int, Vector2Int>();
			var distanceFromStart = new Dictionary<Vector2Int, int>();
			var distanceByNode = new Dictionary<Vector2Int, int>();
			var width = World.Instance.Terrain.Cells.GetLength(0);
			var height = World.Instance.Terrain.Cells.GetLength(1);
			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					var position = new Vector2Int { X = x, Y = y };
					distanceFromStart.Add(position, Int32.MaxValue);
					distanceByNode.Add(position, Int32.MaxValue);
					i++;
				}
			}
			distanceFromStart[origin] = 0;
			distanceByNode[origin] = Vector2Int.Distance(origin, destination);
			originToNode.Add(origin, Vector2Int.Null);
			
			while(waitingVisit.Count > 0) {
				var current = Vector2Int.Null;
				foreach (var node in waitingVisit) {
					if(current == Vector2Int.Null || distanceByNode[node] < distanceByNode[current])
						current = node;
				}
				if(current == destination)
					break;
				
				waitingVisit.Remove(current);
				visited.Add(current);
				
				foreach (Orientation orientation in Enum.GetValues(typeof(Orientation))) {
					var cell = orientation.GetNeighboor(current);
					if(cell == null || visited.Contains(cell.Position))
						continue;
					
					// The distance from start to a neighbor
					var currentFromStart = distanceFromStart[current] + Vector2Int.Distance(current, cell.Position);
		
					if(waitingVisit.Contains(cell.Position) == false)	// Discover a new node
						waitingVisit.Add(cell.Position);
					else if(currentFromStart >= distanceFromStart[cell.Position])
		                continue;       
		
		            // This path is the best until now. Record it!
		            originToNode[cell.Position] = current;
		            distanceFromStart[cell.Position] = currentFromStart;
		            distanceByNode[cell.Position] = currentFromStart + Vector2Int.Distance(cell.Position, destination);
		            i++;
				}
			}
			
			var step = destination;
			while(step != Vector2Int.Null) {
				path.Add(step);
				step = originToNode[step];
				i++;
			}
		}
		
		public Vector2Int Origin {
			get {
				return origin;
			}
			set {
				origin = value;
			}
		}
		public Vector2Int Destination {
			get {
				return destination;
			}
			set {
				destination = value;
			}
		}
		
		public List<Vector2Int> Path {
			get {
				return path;
			}
		}
		
		public Thread Thread {
			get {
				if(thread == null) {
					thread = new Thread(Update);
					thread.Start();
				}
				
				return thread;
			}
		}
	}
}
