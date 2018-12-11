/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 11/12/2018
 * Time: 11:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine.Factory.Entities.Construction;

namespace GameEngine.Factory.Component
{
	/// <summary>
	/// Description of Producer.
	/// </summary>
	public class Producer: IFactoryComponent
	{
		private FactoryEntity parent;
		private int timeToProduce;
		private int timeSinceLastProduction;
		private int productionNumber;
		private Container container;
		
		public Producer(int timeToProduce, FactoryEntity parent)
		{
			this.Parent = parent;
			this.timeToProduce = timeToProduce;
			productionNumber = 2000;
		}
		
		public void Update() {
			if(container == null)
				container = parent.GetComponent<Container>();
			
			timeSinceLastProduction++;
			if(timeSinceLastProduction < timeToProduce)
				return;
			
			if(container.Ressource != null && container.Ressource.Quantity > 0) {
				container.Ressource.Quantity = 0;
				timeSinceLastProduction = 0;
				Mecha mecha = new Mecha(
					"MK" + productionNumber, parent.Position, parent.Owner);
				parent.Owner.AddUnit(mecha);
				productionNumber++;
			}
		}
		
		public FactoryEntity Parent {
			get {
				return parent;
			}
			set {
				parent = value;
				container = parent.GetComponent<Container>();
			}
		}
	}
}
