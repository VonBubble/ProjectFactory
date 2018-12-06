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
		
		public override string ToString()
		{
			return string.Format("[X={0}, Y={1}]", X, Y);
		}
	}
}
