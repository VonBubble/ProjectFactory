/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 14:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace GameEngine.Utils
{
	/// <summary>
	/// Description of Vector2Int.
	/// </summary>
	[Serializable]
	public struct Vector2Int
	{
		public int X;
		public int Y;
		
		public Vector2Int(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
		
		public static bool operator ==(Vector2Int position1, Vector2Int position2)
		{
			if(position1.X == position2.X && position1.Y == position2.Y)
				return true;
			else
				return false;
		}
		
		public static bool operator !=(Vector2Int position1, Vector2Int position2)
		{
			if(position1.X != position2.X || position1.Y != position2.Y)
				return true;
			else
				return false;
		}
		
		public static Vector2Int operator +(Vector2Int position1, Vector2Int position2)
		{
			return new Vector2Int { X = position1.X + position2.X, Y = position1.Y + position2.Y };
		}
		
		public static Vector2Int operator -(Vector2Int position1, Vector2Int position2)
		{
			return new Vector2Int { X = position1.X - position2.X, Y = position1.Y - position2.Y };
		}
		
		public static int Distance(Vector2Int start, Vector2Int finish) {
			int diffX = (start.X - finish.X);
			int diffY = (start.Y - finish.Y);
			
			return (diffX * diffX) + (diffY * diffY);
		}
		
		public override string ToString()
		{
			return string.Format("[X={0}, Y={1}]", X, Y);
		}
		
		public static Vector2Int Zero {
			get {
				return new Vector2Int(0, 0);
			}
		}
		
		public static Vector2Int Null {
			get {
				return new Vector2Int(-1, -1);
			}
		}
	}
}
