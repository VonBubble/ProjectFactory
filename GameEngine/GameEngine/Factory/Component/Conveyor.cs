/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 17:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine.Utils;
using GameEngine.Environment.Material;
using GameEngine.Factory.Entities;

namespace GameEngine.Factory.Component
{
	/// <summary>
	/// Description of Conveyor.
	/// </summary>
	public class Conveyor: IFactoryComponent
	{
		private Vector2Int position;
		private string name;
		private Faction owner;
		private Ressource ressource;
		private Orientation orientation;
		private int delayUntilNextTick;
		
		private int timeSinceLastTick;
		
		public Conveyor(Vector2Int position, Orientation direction) {
			name = "Conveyor";
			this.position = position;
			this.delayUntilNextTick = 2;
			this.timeSinceLastTick = this.delayUntilNextTick;
			this.orientation = direction;
		}
		
		public void Update() {
			new Debug("Conveyor updated at " + position);
			if(this.ressource == null) {
				timeSinceLastTick = delayUntilNextTick;
				return;
			}
			
			timeSinceLastTick--;
			if(timeSinceLastTick > 0)
				return;
			
			timeSinceLastTick = delayUntilNextTick;
			var neighboor = orientation.GetNeighboor(position);
			if(neighboor != null && neighboor.FactoryComponent != null) {
				if(neighboor.FactoryComponent.GetType() == typeof(Conveyor)) {
					if((neighboor.FactoryComponent as Conveyor).Ressource == null) {
						(neighboor.FactoryComponent as Conveyor).Ressource = this.ressource;
						new Debug(this.ressource.Name + " moved from C" + this.position + " to C" + neighboor.Position + ".");
						this.ressource = null;
					}
				} else if((neighboor.FactoryComponent as Builder).Ressource == null) {
					(neighboor.FactoryComponent as Builder).Ressource = this.ressource;
					new Debug(this.ressource.Name + " feed from C" + this.position + " to B" + neighboor.Position + ".");
					this.ressource = null;
				}
			}
				
		}
		
		public override string ToString() {
			return name + " at " + position.X + ", " + position.Y;
		}
		
		public Vector2Int Position {
			get {
				return position;
			}
			set {
				position = value;
			}
		}
		
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		
		public Faction Owner {
			get {
				return owner;
			}
			set {
				owner = value;
			}
		}
		
		public Ressource Ressource {
			get {
				return ressource;
			}
			set {
				ressource = value;
				timeSinceLastTick = delayUntilNextTick;
			}
		}
		
		public Orientation Orientation {
			get {
				return orientation;
			}
		}
		
		public int DelayUntilNextTick {
			get {
				return delayUntilNextTick;
			}
		}
	}
}
