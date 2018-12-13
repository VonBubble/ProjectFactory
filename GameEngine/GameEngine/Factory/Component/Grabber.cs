/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 10/12/2018
 * Time: 11:18
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
	/// Description of Grabber.
	/// </summary>
	[Serializable]
	public class Grabber: IFactoryComponent
	{
		private FactoryEntity parent;
		private Ressource ressource;
		private Orientation input;
		private Orientation output;
		private int delayUntilNextMove;
		private int timeSinceLastMove;
		
		private Grabber() { }
		
		public Grabber(string name, int amountToGrab, int delayUntilNextMove, FactoryEntity parent)
		{
			this.Ressource = new Ressource(name);
			this.ressource.Quantity = amountToGrab;
			this.delayUntilNextMove = delayUntilNextMove;
			this.parent = parent;
			this.timeSinceLastMove = 0;
		}
		
		public void Update() {
			timeSinceLastMove++;
			if(timeSinceLastMove < delayUntilNextMove)
				return;
			
			timeSinceLastMove = 0;
			
			var cellInput = input.GetNeighboor(parent.Position);
			var cellOutput = output.GetNeighboor(parent.Position);
			var containerOutput = cellOutput.FactoryEntity.GetComponent<Container>();
			var containerInput = cellInput.FactoryEntity.GetComponent<Container>();
			
			if(containerInput != null && containerOutput != null) {
			   if(containerInput.Ressource != null) {
					if(containerInput.Ressource.Name == ressource.Name) {
						int grabbed = 0;
						if(containerInput.Ressource.Quantity >= ressource.Quantity) {
							grabbed = ressource.Quantity;
						} else {
							grabbed = containerInput.Ressource.Quantity;
						}
						containerInput.Gather(new Ressource(ressource.Name, grabbed));
						containerOutput.Receive(new Ressource(ressource.Name, grabbed));
					}
				}
			}
		}
		
		private void RessourceReceived(object sender, EventArgs e)
	    {
			timeSinceLastMove = 0;
	    }
		
		#region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
	    	writer.WriteAttributeString("Type", "Grabber");
	    	writer.WriteAttributeString("Input", input.ToString());
	    	writer.WriteAttributeString("Output", output.ToString());
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
	    	Enum.TryParse(reader["Input"], out input);
	    	Enum.TryParse(reader["Output"], out output);
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
				//parent.GetComponent<Container>().RessourceReceived += RessourceReceived;
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
		
		public int AmountToGrab {
			get {
				return ressource.Quantity;
			}
			set {
				ressource.Quantity = value;
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
		
		public Orientation Input {
			get {
				return input;
			}
			set {
				input = value;
			}
		}
		
		public Orientation Output {
			get {
				return output;
			}
			set {
				output = value;
			}
		}
	}
}
