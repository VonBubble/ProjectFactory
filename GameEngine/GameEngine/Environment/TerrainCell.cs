/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 04/12/2018
 * Time: 11:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine.Utils;
using GameEngine.Factory;

namespace GameEngine.Environment
{
	/// <summary>
	/// Description of TerrainCell.
	/// </summary>
	[Serializable]
	public class TerrainCell
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
	}
}
