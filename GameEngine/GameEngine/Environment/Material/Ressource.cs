/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 04/12/2018
 * Time: 15:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using GameEngine.Factory.Component;

namespace GameEngine.Environment.Material
{
	/// <summary>
	/// Description of Ressource.
	/// </summary>
	[Serializable]
	public class Ressource: IIdentity, IXmlSerializable
	{
		private string name;
		private int quantity;
		
		public Ressource() {
			this.name = "";
			quantity = 0;
		}
		
		public Ressource(string name)
		{
			this.name = name;
			quantity = 0;
		}
		
		public Ressource(string name, int quantity)
		{
			this.name = name;
			this.quantity = quantity;
		}
		
		public bool MergeStack(Ressource ressource) {
			if(ressource.Name != this.name)
				return false;
			
			this.quantity += ressource.quantity;
			return true;
		}
		
		#region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
	    	writer.WriteAttributeString("Name", name);
	    	writer.WriteAttributeString("Quantity", quantity.ToString());
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
	    	name = reader["Name"];
	    	quantity = Convert.ToInt32(reader["Quantity"]);
	    	reader.Read();
	    }
	
	    public XmlSchema GetSchema()
	    {
	        return(null);
	    }
	    #endregion
		
		public string Name {
			get {
				return name;
			}
		}
		
		public int Quantity {
			get {
				return quantity;
			}
			set {
				quantity = value;
			}
		}
	}
}
