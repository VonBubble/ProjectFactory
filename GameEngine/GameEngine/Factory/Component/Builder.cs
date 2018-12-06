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
using GameEngine.Factory.Entities.Construction;

namespace GameEngine.Factory.Component
{
	/// <summary>
	/// Description of Builder.
	/// </summary>
	[Serializable]
	public class Builder: IFactoryComponent
	{
		private Vector2Int position;
		private string name;
		private Faction owner;
		private Ressource ressource;
		private int delayUntilNextTick;
		private int produced = 0;
		
		private int timeSinceLastTick;
		
		public Builder() { }
		
		public Builder(Vector2Int position) {
			name = "Builder";
			this.position = position;
			this.delayUntilNextTick = 3;
			this.timeSinceLastTick = this.delayUntilNextTick;
		}
		
		public void Update() {
			new Debug("Builder updated at " + position);
			if(this.ressource == null) {
				timeSinceLastTick = delayUntilNextTick;
				return;
			}
			
			timeSinceLastTick--;
			if(timeSinceLastTick > 0)
				return;
			
			timeSinceLastTick = delayUntilNextTick;
			new Debug("Builder used " + ressource.Name + " to produce: MK-2000_" + produced + ".");
			owner.AddUnit(new Mecha("MK-2000_" + produced, position, owner));
			this.ressource = null;
			produced++;
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
			}
		}
		
		public int DelayUntilNextTick {
			get {
				return delayUntilNextTick;
			}
		}
	}
}
