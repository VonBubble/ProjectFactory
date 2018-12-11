/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 10/12/2018
 * Time: 09:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine.Utils;

namespace GameEngine.Factory.Component
{
	/// <summary>
	/// Description of ITurnable.
	/// </summary>
	[Serializable]
	public class Turnable: IFactoryComponent
	{
		private FactoryEntity parent;
		private Orientation orientation;
		
		public Turnable(FactoryEntity parent) {
			this.parent = parent;
		}
		
		public void Update() { }
		
		public void Rotate() {
			orientation.Rotate(false);
		}
		
		public FactoryEntity Parent {
			get {
				return parent;
			}
			set {
				parent = value;
			}
		}
		
		public Orientation Orientation {
			get {
				return orientation;
			}
			set {
				orientation = value;
			}
		}
	}
}
