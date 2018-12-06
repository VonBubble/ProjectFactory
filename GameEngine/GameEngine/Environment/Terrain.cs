/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 14:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine.Utils;

namespace GameEngine.Environment
{
	/// <summary>
	/// Description of Terrain.
	/// </summary>
	[Serializable]
	public class Terrain
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
		
		public TerrainCell[,] Cells {
			get {
				return cells;
			}
		}
	}
}
