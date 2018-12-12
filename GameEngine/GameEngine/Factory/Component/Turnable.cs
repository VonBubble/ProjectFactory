/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 10/12/2018
 * Time: 09:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using GameEngine.Utils;

namespace GameEngine.Factory.Component
{
	/// <summary>
	/// Description of ITurnable.
	/// </summary>
	[Serializable]
	public class Turnable: IFactoryComponent
	{
		private FactoryEntity parent;
		private Orientation orientation;
		
		private Turnable() { }
		
		public Turnable(FactoryEntity parent) {
			this.parent = parent;
		}
		
		public void Update() { }
		
		public void Rotate() {
			orientation.Rotate(false);
		}
		
		#region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
	    	writer.WriteAttributeString("Type", "Turnable");
	    	writer.WriteAttributeString("Orientation", orientation.ToString());
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
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
		
		public Orientation Orientation {
			get {
				return orientation;
			}
			set {
				orientation = value;
			}
		}
	}
}
