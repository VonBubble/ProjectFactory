/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 10/12/2018
 * Time: 09:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine.Utils;
using GameEngine.Environment;
using GameEngine.Environment.Material;

namespace GameEngine.Factory.Component
{
	/// <summary>
	/// Description of Provider.
	/// </summary>
	[Serializable]
	public class Generator: IFactoryComponent
	{
		private FactoryEntity parent;
		private int timeToProduce;
		private Ressource ressource;
		private Container container;
		private int timeSinceLastProduction;
		
		public Generator(int timeToProduce, Ressource ressource, FactoryEntity parent) {
			this.timeToProduce = timeToProduce;
			this.ressource = ressource;
			this.parent = parent;
			timeSinceLastProduction = 0;
		}
		
		public void Update() {
			timeSinceLastProduction++;
			if(timeSinceLastProduction < timeToProduce)
				return;
			
			timeSinceLastProduction = 0;
			if(ressource == null)
				return;
			
			if(container == null)
				container = parent.GetComponent<Container>();
			
			container.Receive(ressource);
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
		}
		public Container Container {
			get {
				return container;
			}
			set {
				container = value;
			}
		}
		
	}
}
