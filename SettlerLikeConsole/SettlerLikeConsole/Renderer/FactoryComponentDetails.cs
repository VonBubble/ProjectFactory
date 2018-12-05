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
using GameEngine.Utils;
using GameEngine.Factory;
using GameEngine.Factory.Component;

namespace SettlerLikeConsole.Renderer
{
	/// <summary>
	/// Description of FactoryComponentDetails.
	/// </summary>
	public static class FactoryComponentDetails
	{
		public static List<string> GetDetails(IFactoryComponent component) {
			var details = new List<string>();
			if(component.GetType() == typeof(Harvester)) {
				details.Add(component.Position.ToString());
				details.Add(component.Name);
				details.Add("Owner: " + component.Owner.Name);
				details.Add("Gather: " + (component as Harvester).Ressource.Name);
			} else if(component.GetType() == typeof(Conveyor)) {
				details.Add(component.Position.ToString());
				details.Add(component.Name);
				details.Add("Owner: " + component.Owner.Name);
				if((component as Conveyor).Ressource != null) {
					details.Add("Carry: " + (component as Conveyor).Ressource.Name);
				}
				else
					details.Add("Carry: Nothing");
				details.Add("Feeding " + Enum.GetName(typeof(Orientation), (component as Conveyor).Orientation));
			} else if(component.GetType() == typeof(Builder)) {
				details.Add(component.Position.ToString());
				details.Add(component.Name);
				details.Add("Owner: " + component.Owner.Name);
				if((component as Builder).Ressource != null) {
					details.Add("Hold: " + (component as Builder).Ressource.Name);
				}
				else
					details.Add("Hold: Empty");
			}
			
			return details;
		}
	}
}
