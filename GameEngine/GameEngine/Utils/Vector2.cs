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
	/// Description of Vector2.
	/// </summary>
	[Serializable]
	public class Vector2
	{
		private float x;
		private float y;
		
		public Vector2()
		{
			x = 0;
			y = 0;
		}
		
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		
		public float X {
			get {
				return x;
			}
			set {
				x = value;
			}
		}
		
		public float Y {
			get {
				return Y;
			}
			set {
				y = value;
			}
		}
	}
}
