/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 11/12/2018
 * Time: 11:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using GameEngine.Utils;
using GameEngine.Factory.Component.Behaviour.Pathfinding;
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
		private AStar IA;
		
		private Producer() {}
		
		public Producer(int timeToProduce, FactoryEntity parent)
		{
			this.parent = parent;
			this.timeToProduce = timeToProduce;
			productionNumber = 2000;
			this.IA = new AStar();
			IA.Origin = parent.Position;
			IA.Destination = new Vector2Int { X = World.Instance.Terrain.Cells.GetLength(0) - 1,
				Y = World.Instance.Terrain.Cells.GetLength(1) - 1 };
			IA.Start();
		}
		
		public void Update() {
			if(container == null)
				container = parent.GetComponent<Container>();
			
			timeSinceLastProduction++;
			if(timeSinceLastProduction < timeToProduce)
				return;
			
			if(container.Ressource != null && container.Ressource.Quantity > 0) {
				IA.Thread.Join();
				container.Ressource.Quantity = 0;
				timeSinceLastProduction = 0;
				Mecha mecha = new Mecha(
					"MK" + productionNumber, parent.Position, parent.Owner);
				mecha.IA = IA;
                mecha.Value = 150;
				parent.Owner.AddUnit(mecha);
				productionNumber++;
			}
		}
		
		#region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
	    	writer.WriteAttributeString("Type", "Producer");
	    	writer.WriteAttributeString("Speed", timeToProduce.ToString());
	    	writer.WriteAttributeString("LastProd", timeSinceLastProduction.ToString());
	    	writer.WriteAttributeString("ProdNb", productionNumber.ToString());
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
	    	timeToProduce = Convert.ToInt32(reader["Speed"]);
	    	timeSinceLastProduction = Convert.ToInt32(reader["LastProd"]);
	    	productionNumber = Convert.ToInt32(reader["ProdNb"]);
	    	reader.Read();
	    }
	
	    public XmlSchema GetSchema()
	    {
	        return(null);
	    }
	    #endregion
		
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
