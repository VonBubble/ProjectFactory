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
using GameEngine.Factory.Entities;
using GameEngine.Environment.Material;

namespace GameEngine.Factory.Component
{
	/// <summary>
	/// Description of Harvester.
	/// </summary>
	[Serializable]
	public class Harvester: IFactoryComponent
	{
		private Vector2Int position;
		private string name;
		private Faction owner;
		private Ressource ressource;
		private int delayUntilNextTick;
		
		private int timeSinceLastTick;
		
		public Harvester(Vector2Int position) {
			name = "Harvester";
			this.position = position;
			this.ressource = new Ressource("Iron");
			this.delayUntilNextTick = 3;
			this.timeSinceLastTick = this.delayUntilNextTick;
		}
		
		public void Update() {
			new Debug("Harseter updated at " + position);
			timeSinceLastTick--;
			if(timeSinceLastTick > 0)
				return;
			
			timeSinceLastTick = delayUntilNextTick;
			foreach (Orientation direction in (Orientation[]) Enum.GetValues(typeof(Orientation)))
			{
				var neighboor = direction.GetNeighboor(position);
				if(neighboor != null && neighboor.FactoryComponent != null 
				   && neighboor.FactoryComponent.GetType() == typeof(Conveyor)) {
					if((neighboor.FactoryComponent as Conveyor).Ressource == null) {
						(neighboor.FactoryComponent as Conveyor).Ressource = new Ressource(this.ressource.Name);
						new Debug(this.ressource.Name + " moved from H" + this.position + " to C" + neighboor.Position + ".");
						break;
					}
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
		}
		
		public int DelayUntilNextTick {
			get {
				return delayUntilNextTick;
			}
		}
	}
}
