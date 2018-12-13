/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 13/12/2018
 * Time: 14:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace GameEngine.Factory.Component
{
	/// <summary>
	/// Description of Destructible.
	/// </summary>
	public class Destructible: IFactoryComponent
	{
		private FactoryEntity parent;
		private int maxHealth;
		private int currentHealth;
		
		public Destructible(int maxHealth) {
			this.maxHealth = maxHealth;
			currentHealth = maxHealth;
		}
		
		public void Update()
		{
			if(currentHealth <= 0) {
				if(parent != null) {
					parent.Owner.RemoveFactoryEntity(parent);
				}
			}
		}
		
		#region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
	    	writer.WriteAttributeString("Type", "Destructible");
	    	writer.WriteAttributeString("CurrentHealth", currentHealth.ToString());
	    	writer.WriteAttributeString("MaxHealth", maxHealth.ToString());
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
	    	currentHealth = Convert.ToInt32(reader["CurrentHealth"]);
	    	maxHealth = Convert.ToInt32(reader["MaxHealth"]);
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
		
		public int MaxHealth {
			get {
				return maxHealth;
			}
			set {
				maxHealth = value;
			}
		}
		
		public int CurrentHealth {
			get {
				return currentHealth;
			}
			set {
				currentHealth = value;
			}
		}
	}
}
