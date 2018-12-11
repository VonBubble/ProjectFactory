/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 05/12/2018
 * Time: 09:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine.Environment;

namespace GameEngine.Utils
{
	/// <summary>
	/// Description of Orientation.
	/// </summary>
	[Serializable]
	public enum Orientation
	{
		CENTER,
		NORTH,
		NORTH_EAST,
		EAST,
		SOUTH_EAST,
		SOUTH,
		SOUTH_WEST,
		WEST,
		NORTH_WEST
	}
	
	public static class OrientationMethods
	{
		public static Orientation SetOrientationFromPosition(this Orientation orientation, Vector2Int origin, Vector2Int target) {
			if(origin.X < target.X) {
				if(origin.Y < target.Y) {
					return Orientation.NORTH_EAST;
				} else if(origin.Y == target.Y) {
					return Orientation.EAST;
				} else {
					return Orientation.SOUTH_EAST;
				}
			} else if(origin.X == target.X) {
				if(origin.Y < target.Y) {
					return Orientation.NORTH;
				} else if(origin.Y == target.Y) {
					return Orientation.CENTER;
				} else {
					return Orientation.SOUTH;
				}
			} else {
				if(origin.Y < target.Y) {
					return Orientation.NORTH_WEST;
				} else if(origin.Y == target.Y) {
					return Orientation.WEST;
				} else {
					return Orientation.SOUTH_WEST;
				}
			}
		}
		
		public static Vector2Int GetPositionOfTarget(this Orientation orientation, Vector2Int origin) {
	        switch (orientation)
	        {
	        	case Orientation.NORTH:
	        		return new Vector2Int(origin.X, origin.Y - 1);
	        	case Orientation.NORTH_EAST:
	        		return new Vector2Int(origin.X + 1, origin.Y + 1);
	        	case Orientation.EAST:
	        		return new Vector2Int(origin.X + 1, origin.Y);
	        	case Orientation.SOUTH_EAST:
	        		return new Vector2Int(origin.X + 1, origin.Y + 1);
	        	case Orientation.SOUTH:
	        		return new Vector2Int(origin.X, origin.Y + 1);
	        	case Orientation.SOUTH_WEST:
	        		return new Vector2Int(origin.X - 1, origin.Y + 1);
	        	case Orientation.WEST:
	        		return new Vector2Int(origin.X - 1, origin.Y);
	        	case Orientation.NORTH_WEST:
	        		return new Vector2Int(origin.X - 1, origin.Y - 1);
	        	default:
	        		return new Vector2Int(origin.X, origin.Y);
	        }
		}
		
	    public static TerrainCell GetNeighboor(this Orientation orientation, Vector2Int origin)
	    {
	        switch (orientation)
	        {
	        	case Orientation.NORTH:
	        		return World.Instance.Terrain.GetTerrainCellAt(origin.X, origin.Y - 1);
	        	case Orientation.NORTH_EAST:
	        		return World.Instance.Terrain.GetTerrainCellAt(origin.X + 1, origin.Y - 1);
	        	case Orientation.EAST:
	        		return World.Instance.Terrain.GetTerrainCellAt(origin.X + 1, origin.Y);
	        	case Orientation.SOUTH_EAST:
	        		return World.Instance.Terrain.GetTerrainCellAt(origin.X + 1, origin.Y + 1);
	        	case Orientation.SOUTH:
	        		return World.Instance.Terrain.GetTerrainCellAt(origin.X, origin.Y + 1);
	        	case Orientation.SOUTH_WEST:
	        		return World.Instance.Terrain.GetTerrainCellAt(origin.X - 1, origin.Y + 1);
	        	case Orientation.WEST:
	        		return World.Instance.Terrain.GetTerrainCellAt(origin.X - 1, origin.Y);
	        	case Orientation.NORTH_WEST:
	        		return World.Instance.Terrain.GetTerrainCellAt(origin.X - 1, origin.Y - 1);
	        	default:
	        		return World.Instance.Terrain.GetTerrainCellAt(origin.X, origin.Y);
	        }
	    }
	    
	    public static Orientation Rotate(this Orientation orientation, bool includeCenter) {
	    	if(includeCenter) {
		    	if(orientation == Orientation.NORTH_WEST)
	    			return Orientation.CENTER;
	    	} else {
	    		if(orientation == Orientation.NORTH_WEST || orientation == Orientation.CENTER)
	    			return Orientation.NORTH;
	    	}
	    	return (orientation + 1);
	    }
	}
}
