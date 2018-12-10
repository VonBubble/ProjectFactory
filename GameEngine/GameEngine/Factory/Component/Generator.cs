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
	public class Generator: IFactoryComponent
	{
		private FactoryEntity parent;
		private Ressource ressource;
		private Container container;
		
		public Generator(Ressource ressource, FactoryEntity parent) {
			this.ressource = ressource;
			this.parent = parent;
		}
		
		public void Update() {
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
