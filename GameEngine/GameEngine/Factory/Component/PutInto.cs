/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 11/12/2018
 * Time: 09:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using GameEngine.Utils;
using GameEngine.Factory;
using GameEngine.Environment.Material;

namespace GameEngine.Factory.Component
{
	/// <summary>
	/// Description of PutInto.
	/// </summary>
	[Serializable]
	public class PutInto: IFactoryComponent
	{
		private FactoryEntity parent;
		private Ressource ressource;
		private Orientation target;
		private int delayUntilNextMove;
		private int timeSinceLastMove;
		private bool alreadyReset = false;
		
		private PutInto() {}
		
		public PutInto(string name, int amountToMove, int delayUntilNextMove, FactoryEntity parent)
		{
			this.Ressource = new Ressource(name);
			this.ressource.Quantity = amountToMove;
			this.delayUntilNextMove = delayUntilNextMove;
			this.parent = parent;
			this.timeSinceLastMove = 0;
		}
		
		public void Update() {
			timeSinceLastMove++;
			if(timeSinceLastMove < delayUntilNextMove)
				return;
			
			var container = parent.GetComponent<Container>();
			if(container == null ||  container.Ressource == null || container.Ressource.Quantity == 0)
				return;
			
			timeSinceLastMove = 0;
			alreadyReset = false;
			
			var cellTarget = target.GetNeighboor(parent.Position);
			var containerTarget = cellTarget.FactoryEntity.GetComponent<Container>();
			
			if(containerTarget != null) {
				containerTarget.Receive(container.Ressource);
				container.Gather(container.Ressource);
			}
		}
		
		private void RessourceReceived(object sender, EventArgs e)
	    {
			if(alreadyReset == false) {
				timeSinceLastMove = 0;
				alreadyReset = true;
			}
	    }
		
		#region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
	    	writer.WriteAttributeString("Type", "PutInto");
	    	writer.WriteAttributeString("Orientation", target.ToString());
	    	writer.WriteAttributeString("Delay", delayUntilNextMove.ToString());
	    	writer.WriteAttributeString("LastMove", timeSinceLastMove.ToString());
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
	    	Enum.TryParse(reader["Orientation"], out target);
	    	delayUntilNextMove = Convert.ToInt32(reader["Delay"]);
	    	timeSinceLastMove = Convert.ToInt32(reader["LastMove"]);
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
				parent.GetComponent<Container>().RessourceReceived += RessourceReceived;
			}
		}
		
		public Ressource Ressource {
			get {
				return ressource;
			}
			set {
				ressource = value;
				timeSinceLastMove = 0;
			}
		}
		
		public int DelayUntilNextMove {
			get {
				return delayUntilNextMove;
			}
			set {
				delayUntilNextMove = value;
			}
		}
		
		public Orientation Target {
			get {
				return target;
			}
			set {
				target = value;
			}
		}
	}
}
