/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 04/12/2018
 * Time: 16:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using GameEngine.Factory.Entities.Construction;
using GameEngine.Utils;
using GameEngine.Factory;
using GameEngine.Factory.Component;

namespace SettlerLikeConsole.Renderer
{
	/// <summary>
	/// Description of FactoryComponentDetails.
	/// </summary>
	public static class UnitDetails
	{
		public static List<string> GetDetails(List<Mecha> mechas) {
			var details = new List<string>();
			
			foreach (var mecha in mechas) {
				if(details.Count == 0)
					details.Add(mecha.Position.ToString());
				
				details.Add(mecha.Name);
			}
			return details;
		}
	}
}
