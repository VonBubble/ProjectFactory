﻿/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 04/12/2018
 * Time: 15:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace GameEngine.Environment.Material
{
	/// <summary>
	/// Description of Ressource.
	/// </summary>
	[Serializable]
	public class Ressource
	{
		private string name;
		private int quantity;
		
		public Ressource(string name)
		{
			this.name = name;
			quantity = 0;
		}
		
		public bool MergeStack(Ressource ressource) {
			if(ressource.Name != this.name)
				return false;
			
			this.quantity += ressource.quantity;
			return true;
		}
		
		public string Name {
			get {
				return name;
			}
		}
		
		public int Quantity {
			get {
				return quantity;
			}
			set {
				quantity = value;
			}
		}
	}
}
