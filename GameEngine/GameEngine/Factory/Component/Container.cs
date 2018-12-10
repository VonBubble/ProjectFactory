/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 10/12/2018
 * Time: 09:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine.Environment.Material;

namespace GameEngine.Factory.Component
{
	/// <summary>
	/// Description of Container.
	/// </summary>
	public class Container: IFactoryComponent
	{
		private FactoryEntity parent;
		private Ressource ressource;
		
		public Container(FactoryEntity parent) {
			this.parent = parent;
			ressource = new Ressource();
		}
		
		public void Update() { }
		
		public void Gather(Ressource ressource) {
			if(this.ressource != null && this.ressource.Name == ressource.Name)
				this.ressource.Quantity -= ressource.Quantity;
		}
		
		public void Receive(Ressource ressource)
		{
			if(this.ressource != null && this.ressource.Name == ressource.Name)
				this.ressource.Quantity += ressource.Quantity;
			else {
				this.ressource = new Ressource(ressource.Name, ressource.Quantity);
			}
		}
		
		public FactoryEntity Parent {
			get {
				return parent;
			}
			set {
				parent = value;
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
	}
}
