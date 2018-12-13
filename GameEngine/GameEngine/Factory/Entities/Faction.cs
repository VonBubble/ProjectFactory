/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 17:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GameEngine.Factory.Entities.Construction;

namespace GameEngine.Factory.Entities
{
	/// <summary>
	/// Description of Faction.
	/// </summary>
	[Serializable]
	public class Faction
	{
		private string name;
		private FactoryLayer factoryLayer;
		private List<Mecha> units;
		
		public Faction() {
			units = new List<Mecha>();
		}
		
		public Faction(string name)
		{
			this.name = name;
			factoryLayer = new FactoryLayer();
			units = new List<Mecha>();
		}
		
		public void Update(){
			new Debug("Faction[" + name + "].Update() called");
			factoryLayer.Update();
			UpdateUnits();
		}
		
		private void UpdateUnits() {
			foreach (var unit in units) {
				unit.Update();
			}
		}
		
		public void AddFactoryEntity(FactoryEntity factoryEntity) {
			factoryEntity.Owner = this;
			factoryLayer.AddFactoryEntity(factoryEntity);
			if(World.Instance.Terrain.Cells[factoryEntity.Position.X, factoryEntity.Position.Y] != null)
				World.Instance.Terrain.Cells[factoryEntity.Position.X, factoryEntity.Position.Y].FactoryEntity = factoryEntity;
		}
		
		public void AddUnit(Mecha mecha) {
			if(units == null)
				units = new List<Mecha>();
			
			units.Add(mecha);
		}
		
		public string Name {
			get {
				return name;
			}
		}
		
		public FactoryLayer FactoryLayer {
			get {
				return factoryLayer;
			}
			set {
				factoryLayer = value;
			}
		}
		
		[XmlIgnore]
		public ReadOnlyCollection<Mecha> Units {
			get {
				return units.AsReadOnly();
			}
		}
		
		[XmlElement("Units")]
		private List<Mecha> ListOfUnits {
			get {
				return units;
			}
			set {
				units = value;
			}
		}
	}
}
