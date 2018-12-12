/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 14:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using GameEngine.Utils;

namespace GameEngine.Environment
{
	/// <summary>
	/// Description of Terrain.
	/// </summary>
	[Serializable]
	public class Terrain: IXmlSerializable
	{		
		private TerrainCell[,] cells;
		
		public void GenerateNewMap(int width, int height) {
			cells = new TerrainCell[width, height];
			
			for(int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					cells[x, y] = new TerrainCell(new Vector2Int(x, y), 0);
				}
			}
		}
		
		public TerrainCell GetTerrainCellAt(int x, int y) {
			if(x >= 0 && x < cells.GetLength(0) && y >= 0 && y < cells.GetLength(1)) {
				return cells[x, y];
			} else {
				return null;
			}
		}
	    
	    #region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
	    	writer.WriteAttributeString("Width", cells.GetLength(0).ToString());
	    	writer.WriteAttributeString("Height", cells.GetLength(1).ToString());
	    	foreach (var cell in cells) {
	    		writer.WriteStartElement(cell.GetType().Name);
	            cell.WriteXml(writer);
	            writer.WriteEndElement();
	    	}
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
	    	int width = Convert.ToInt32(reader["Width"]);
	    	int height = Convert.ToInt32(reader["Height"]);
	    	cells = new TerrainCell[width, height];
	    	if (reader.ReadToDescendant(typeof(TerrainCell).Name))
            {
                while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == typeof(TerrainCell).Name)
                {
                	var cell = new TerrainCell(Vector2Int.Zero, 0);
                	cell.ReadXml(reader);
                	cells[cell.Position.X, cell.Position.Y] = cell;
                }
            }
	    }
	
	    public XmlSchema GetSchema()
	    {
	        return(null);
	    }
	    #endregion
		
		public TerrainCell[,] Cells {
			get {
				return cells;
			}
		}
	}
}
