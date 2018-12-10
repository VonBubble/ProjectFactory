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
	public static class FactoryEntityDetails
	{
		public static List<string> GetDetails(FactoryEntity component) {
			var details = new List<string>();
			details.Add(component.Position.ToString());
			details.Add(component.Name);
			details.Add("Owner: " + component.Owner.Name);
			if(component.GetComponent<Generator>() != null) {
				var generator = component.GetComponent<Generator>();
				details.Add("Harvest: " + generator.Ressource.Name);
				if(component.GetComponent<Container>() != null) {
					var container = component.GetComponent<Container>();
					if(container.Ressource != null)
						details.Add("Amount: " + container.Ressource.Quantity);
				}
			}
			else if(component.GetComponent<Container>() != null) {
				var container = component.GetComponent<Container>();
				if(container.Ressource != null) {
					details.Add("Hold: " + container.Ressource.Name);
					details.Add("Amount: " + container.Ressource.Quantity);
				} else {
					details.Add("Hold: Nothing");
				}
			}
			if(component.GetComponent<Grabber>() != null) {
				var grabber = component.GetComponent<Grabber>();
				details.Add("Move: " + grabber.Ressource.Name);
				details.Add("By: " + grabber.Ressource.Quantity + "/tick");
				details.Add("IN: " + Enum.GetName(typeof(Orientation), grabber.Input));
				details.Add("OUT: " + Enum.GetName(typeof(Orientation), grabber.Output));
			}
//			if(component.GetType() == typeof(Harvester)) {
//				details.Add(component.Position.ToString());
//				details.Add(component.Name);
//				details.Add("Owner: " + component.Owner.Name);
//				details.Add("Gather: " + (component as Harvester).Ressource.Name);
//			} else if(component.GetType() == typeof(Conveyor)) {
//				details.Add(component.Position.ToString());
//				details.Add(component.Name);
//				details.Add("Owner: " + component.Owner.Name);
//				if((component as Conveyor).Ressource != null) {
//					details.Add("Carry: " + (component as Conveyor).Ressource.Name);
//				}
//				else
//					details.Add("Carry: Nothing");
//				details.Add("Feeding " + Enum.GetName(typeof(Orientation), (component as Conveyor).Orientation));
//			} else if(component.GetType() == typeof(Builder)) {
//				details.Add(component.Position.ToString());
//				details.Add(component.Name);
//				details.Add("Owner: " + component.Owner.Name);
//				if((component as Builder).Ressource != null) {
//					details.Add("Hold: " + (component as Builder).Ressource.Name);
//				}
//				else
//					details.Add("Hold: Empty");
//			}
			
			return details;
		}
	}
}
