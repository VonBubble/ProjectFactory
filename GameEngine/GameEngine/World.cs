/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 14:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using GameEngine.Environment;
using GameEngine.Factory.Entities;

namespace GameEngine
{
	/// <summary>
	/// TODO: Fill this
	/// </summary>
	[Serializable]
	public sealed class World: IXmlSerializable
	{
		private static World instance = new World();

		private Terrain terrain;
		private FactionList factionList;
		
	    // Explicit static constructor to tell C# compiler
	    // not to mark type as beforefieldinit
	    static World() { }
	
	    private World() { InitializeNewWorld(); }
	    
	    public void InitializeNewWorld() {
	    	terrain = new Terrain();
	    	factionList = new FactionList();
	    	var player = factionList.AddFaction("Player");
            player.Wallet.Balance = 150000;
	    }
	    
	    public void LoadSave(World save) {
	    	instance = save;
	    }
	    
	    #region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
    		writer.WriteStartElement(terrain.GetType().Name);
            terrain.WriteXml(writer);
            writer.WriteEndElement();
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
	    	Instance = this;
	    	factionList = new FactionList();
	    	if(reader.ReadToDescendant(typeof(Terrain).Name)) 
	    	{
	    		if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == typeof(Terrain).Name)
			    {
		    		terrain = new Terrain();
		    		terrain.ReadXml(reader);
			        reader.Read();
			    }
	    	}
	        //personName = reader.ReadString();
	    }
	
	    public XmlSchema GetSchema()
	    {
	        return(null);
	    }
	    #endregion
	    
	    public static World Instance
	    {
	        get
	        {
	            return instance;
	        }
	        private set {
	        	instance = value;
	        }
	    }
	    
	    public Terrain Terrain
	    {
	        get
	        {
	            return terrain;
	        }
	    }
	    
	    public FactionList FactionList
	    {
	        get
	        {
	            return factionList;
	        }
	        private set {
	        	factionList = value;
	        }
	    }
	}
}