/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 12/12/2018
 * Time: 11:58
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
	/// Description of EmptyComponent.
	/// </summary>
	public class EmptyComponent: IFactoryComponent
	{
		public IFactoryComponent ActualComponent { get; private set; }
		public FactoryEntity Parent { get; set; }
		
		public void Update() {}
		
		private void ReadActualComponent() {
			// How the heck am I supposed to get the name of the real class in order to instanciate to object ?
		}
		
		#region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
	    	var str = reader["Type"];
	    	switch(reader["Type"]) {
	    		case "Container":
	    			ActualComponent = new Container(null);
	    			ActualComponent.ReadXml(reader);
	    			break;
	    		case "Generator":
	    			ActualComponent = new Generator(0, null, null);
	    			ActualComponent.ReadXml(reader);
	    			break;
	    		case "Grabber":
	    			ActualComponent = new Grabber("", 0, 0, null);
	    			ActualComponent.ReadXml(reader);
	    			break;
	    		case "Producer":
	    			ActualComponent = new Producer(0, null);
	    			ActualComponent.ReadXml(reader);
	    			break;
	    		case "PutInto":
	    			ActualComponent = new PutInto("", 0, 0, null);
	    			ActualComponent.ReadXml(reader);
	    			break;
	    		case "Turnable":
	    			ActualComponent = new Turnable(null);
	    			ActualComponent.ReadXml(reader);
	    			break;
	    		default:
	    			reader.Read();
	    			break;
	    	}
	    }
	
	    public XmlSchema GetSchema()
	    {
	        return(null);
	    }
	    #endregion
	}
}
