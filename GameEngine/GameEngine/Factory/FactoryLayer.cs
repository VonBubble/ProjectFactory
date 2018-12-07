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
		private List<IFactoryComponent> components;
		
		public FactoryLayer()
		{
			components = new List<IFactoryComponent>();
		}
		
		public void Update() {
			new Debug("---UPDATING FACTORY---");
			foreach (var component in components) {
				component.Update();
			}
			new Debug("---CLOSING FACTORY---");
		}
		
		public void AddFactoryComponent(IFactoryComponent component) {
			components.Add(component);
		}
		
		public ReadOnlyCollection<IFactoryComponent> Components {
			get {
				return components.AsReadOnly();
			}
		}
	}
}
