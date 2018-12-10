/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 17:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameEngine.Factory
{
	/// <summary>
	/// Description of FactoryLayer.
	/// </summary>
	[Serializable]
	public class FactoryLayer
	{
		private List<FactoryEntity> factoryEntities;
		
		public FactoryLayer()
		{
			factoryEntities = new List<FactoryEntity>();
		}
		
		public void Update() {
			new Debug("---UPDATING FACTORY---");
			foreach (var entity in factoryEntities) {
				entity.Update();
			}
			new Debug("---CLOSING FACTORY---");
		}
		
		public void AddFactoryEntity(FactoryEntity entity) {
			factoryEntities.Add(entity);
		}
		
		public ReadOnlyCollection<FactoryEntity> FactoryEntities {
			get {
				return factoryEntities.AsReadOnly();
			}
		}
	}
}
