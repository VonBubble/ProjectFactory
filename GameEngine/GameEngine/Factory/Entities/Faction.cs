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
using GameEngine.Economy;

namespace GameEngine.Factory.Entities
{
	/// <summary>
	/// Description of Faction.
	/// </summary>
	[Serializable]
	public class Faction
	{
        public delegate void EntityBuiltHandler(object sender, PositionEventArgs args);
        public event EntityBuiltHandler EntityBuilt;

		private string name;
		private FactoryLayer factoryLayer;
		private List<Mecha> units;
        private Wallet wallet;
		
		public Faction() {
			units = new List<Mecha>();
		}
		
		public Faction(string name)
		{
			this.name = name;
			factoryLayer = new FactoryLayer();
			units = new List<Mecha>();
            wallet = new Wallet();
		}

        public Faction(string name, int startingBalance)
        {
            this.name = name;
            factoryLayer = new FactoryLayer();
            units = new List<Mecha>();
            wallet = new Wallet(startingBalance);
        }

        public void Update(){
			new Debugger("Faction[" + name + "].Update() called");
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
            if (World.Instance.Terrain.Cells[factoryEntity.Position.X, factoryEntity.Position.Y] != null)
            {
                World.Instance.Terrain.Cells[factoryEntity.Position.X, factoryEntity.Position.Y].FactoryEntity = factoryEntity;
                RaiseEntityBuilt(factoryEntity.Position.X, factoryEntity.Position.Y, factoryEntity);
            }
		}
		
		public void RemoveFactoryEntity(FactoryEntity factoryEntity) {
			factoryLayer.RemoveFactoryEntity(factoryEntity);
		}
		
		public void AddUnit(Mecha mecha) {
			if(units == null)
				units = new List<Mecha>();
			
			units.Add(mecha);
		}

        public void RemoveUnit(Mecha mecha)
        {
            units.Remove(mecha);
        }
		
        protected virtual void RaiseEntityBuilt(int x, int y, FactoryEntity factoryEntity)
        {
            EntityBuilt?.Invoke(factoryEntity, new PositionEventArgs(x, y));
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
		
		public ReadOnlyCollection<Mecha> Units {
			get {
				return units.AsReadOnly();
			}
		}

        public Wallet Wallet { get => wallet; set => wallet = value; }
    }
}
