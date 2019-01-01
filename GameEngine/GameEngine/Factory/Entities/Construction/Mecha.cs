﻿/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 05/12/2018
 * Time: 15:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using GameEngine.Utils;
using GameEngine.Factory.Component.Behaviour.Pathfinding;
using GameEngine.Factory.Component;

namespace GameEngine.Factory.Entities.Construction
{
	/// <summary>
	/// Description of Mecha.
	/// </summary>
	[Serializable]
	public class Mecha: IMonetaryValue
	{
		private static Random RAND = new Random();
		
		private string name;
        private int value;
		private Vector2Int position;
		private Faction owner;
		private AStar ia;
		
		public Mecha() { }
		
		public Mecha(string name, Vector2Int position, Faction owner)
		{
			this.name = name;
			this.position = position;
			this.owner = owner;
		}
		
		public void Update() {
			int i = ia.Path.IndexOf(position);
			if(i > 0) {
				position = ia.Path[i - 1];
			}
			
//			var possibles = new List<Vector2Int>();
//			foreach (Orientation orientation in Enum.GetValues(typeof(Orientation))) {
//				var cell = orientation.GetNeighboor(position);
//				if(cell != null && cell.FactoryEntity == null) {
//					possibles.Add(cell.Position);
//				}
//			}
//			
//			if(possibles.Count == 0) {
//				new Debug(name + " stayed in place because it lacked the room to move.");
//				return;
//			}
//			
//			int choice = RAND.Next(possibles.Count);
//			new Debug(name + " moved from " + position + " to " + possibles[choice] + ".");
//			position = possibles[choice];
		}
		
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		
		public Vector2Int Position {
			get {
				return position;
			}
			set {
				position = value;
			}
		}
		
		public Faction Owner {
			get {
				return owner;
			}
		}
		
		public AStar IA {
			get {
				return ia;
			}
			set {
				ia = value;
			}
		}

        public int Value { get => value; set => this.value = value; }
    }
}
