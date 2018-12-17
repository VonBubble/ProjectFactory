/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 14/12/2018
 * Time: 10:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using GameEngine.Utils;
using System.Linq;
using System.Collections.Generic;

namespace GameEngine.Factory.Component.Behaviour.Pathfinding
{
	/// <summary>
	/// Description of Dijkstra.
	/// </summary>
	public class Dijkstra: IPathfinding
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
			
			var unvisitedNodes = new List<Vector2Int>();
			var distanceFromSource = new Dictionary<Vector2Int, int>();
			var previousNode = new Dictionary<Vector2Int, Vector2Int>();
			var width = World.Instance.Terrain.Cells.GetLength(0);
			var height = World.Instance.Terrain.Cells.GetLength(1);
			
			distanceFromSource.Add(origin, 0);
			previousNode.Add(origin, Vector2Int.Null);
			
			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					i++;
					var node = new Vector2Int { X = x, Y = y };
					unvisitedNodes.Add(node);
					if(node == origin)
						continue;
					
					distanceFromSource.Add(node, int.MaxValue);
					previousNode.Add(node, Vector2Int.Null);
				}
			}
			
			while(unvisitedNodes.Count > 0) {
				Vector2Int node = Vector2Int.Null;
				foreach (var next in unvisitedNodes) {
					if(node == Vector2Int.Null || distanceFromSource[next] < distanceFromSource[node])
						node = next;
				}
				
				unvisitedNodes.Remove(node);
				if(node == destination)
					break;
				
				foreach (Orientation orientation in Enum.GetValues(typeof(Orientation))) {
					var cell = orientation.GetNeighboor(node);
					if(cell == null)
						continue;
					
					int distance = distanceFromSource[node] + Vector2Int.Distance(cell.Position, node);
					if(distance < distanceFromSource[cell.Position]) {
						distanceFromSource[cell.Position] = distance;
						previousNode[cell.Position] = node;
					}
					i++;
				}
			}
			
			if(previousNode[destination] == Vector2Int.Null)
				return;
			
			Vector2Int step = destination;
			while(step != Vector2Int.Null) {
				i++;
				path.Add(step);
				step = previousNode[step];
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
