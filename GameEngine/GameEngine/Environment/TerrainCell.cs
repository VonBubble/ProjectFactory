/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 04/12/2018
 * Time: 11:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using GameEngine.Utils;
using GameEngine.Factory;

namespace GameEngine.Environment
{
	/// <summary>
	/// Description of TerrainCell.
	/// </summary>
	[Serializable]
	public class TerrainCell: IXmlSerializable
	{
		private Vector2Int position;
		private int elevation;
		private FactoryEntity factoryEntity;
		
		public TerrainCell(Vector2Int position, int elevation)
		{
			this.position = position;
			this.elevation = elevation;
			factoryEntity = null;
		}
	    
	    #region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
	    	writer.WriteAttributeString("X", position.X.ToString());
	    	writer.WriteAttributeString("Y", position.Y.ToString());
	    	writer.WriteAttributeString("Z", elevation.ToString());
	    	
	    	if(factoryEntity != null) {
	    		writer.WriteStartElement(factoryEntity.GetType().Name);
	            factoryEntity.WriteXml(writer);
	            writer.WriteEndElement();
	    	}
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
	    	position = new Vector2Int(Convert.ToInt32(reader["X"]), Convert.ToInt32(reader["Y"]));
	    	if(position.X == 0 && position.Y == 4)
	    	{
	    		int i = 0;
	    		i++;
	    	}
	    	elevation = Convert.ToInt32(reader["Z"]);
	    	
	    	if (reader.ReadToDescendant(typeof(FactoryEntity).Name))
            {
                while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == typeof(FactoryEntity).Name)
                {
                	factoryEntity = new FactoryEntity("", Vector2Int.Zero, null);
            		factoryEntity.Position = position;
            		factoryEntity.ReadXml(reader);
                }
	    	}
	    	reader.Read();
	    }
	
	    public XmlSchema GetSchema()
	    {
	        return(null);
	    }
	    #endregion
		
		public Vector2Int Position {
			get {
				return position;
			}
		}
		
		public int Elevation {
			get {
				return elevation;
			}
		}
		
		public FactoryEntity FactoryEntity {
			get {
				return factoryEntity;
			}
			set {
				factoryEntity = value;
			}
		}
	    
	    public bool IsTraversable {
	    	get {
	    		return (factoryEntity == null);
	    	}
	    }
	}
}
