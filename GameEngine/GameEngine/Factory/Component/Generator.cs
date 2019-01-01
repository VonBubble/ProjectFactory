/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 10/12/2018
 * Time: 09:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using GameEngine.Utils;
using GameEngine.Environment;
using GameEngine.Environment.Material;

namespace GameEngine.Factory.Component
{
	/// <summary>
	/// Description of Provider.
	/// </summary>
	[Serializable]
	public class Generator: IFactoryComponent, IProgressableComponent
    {
        public event EventHandler OnProgressMade;

        private FactoryEntity parent;
		private int timeToProduce;
		private Ressource ressource;
		private Container container;
		private int timeSinceLastProduction;
		
		private Generator() { }
		
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

        protected virtual void OnGenerationProgress()
        {
            OnProgressMade?.Invoke(this, new EventArgs());
        }
		
		#region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
	    	writer.WriteAttributeString("Type", "Generator");
	    	writer.WriteAttributeString("Speed", timeToProduce.ToString());
	    	writer.WriteAttributeString("LastProd", timeSinceLastProduction.ToString());
    		writer.WriteStartElement("GeneratedRessource");
            ressource.WriteXml(writer);
            writer.WriteEndElement();
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
	    	timeToProduce = Convert.ToInt32(reader["Speed"]);
	    	timeSinceLastProduction = Convert.ToInt32(reader["LastProd"]);
	    	ressource = new Ressource();
	    	if (reader.ReadToDescendant("GeneratedRessource"))
            {
                while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "GeneratedRessource")
                {
	    		ressource.ReadXml(reader);
                }
	    	}
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

        public int TimeSinceLastProduction { get => timeSinceLastProduction; set => timeSinceLastProduction = value; }
        public int TimeToProduce { get => timeToProduce; set => timeToProduce = value; }

        public int ProgressPercent
        {
            get
            {
                return (int)((timeToProduce - timeSinceLastProduction) / ((timeToProduce + timeSinceLastProduction) / 2f) * 100);
            }
        }
    }
}
