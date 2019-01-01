/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 17:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using GameEngine;
using GameEngine.Factory.Component;
using GameEngine.Factory.Entities;

namespace SettlerLikeConsole.Renderer
{
	/// <summary>
	/// Description of FactoryLayers.
	/// </summary>
	public static class FactoryLayers
	{
		public const char EMPTY_CELL = ' ';
		private static readonly Dictionary<string, char> mapper = new Dictionary<string, char>
		{
		    { "Harvester", 'H' },
		    { "Conveyor", 'c' },
		    { "Builder", 'B' },
		    { "Grabber", 'G' },
		    { "Iron", 'i' },
            { "Buying Station", '$' }
		};
		public static bool displayRessources = true;
		
		public static char RenderCell(int x, int y) {
			char cell = EMPTY_CELL;
			var entity = World.Instance.Terrain.Cells[x,y].FactoryEntity;
			
			if(entity != null && mapper.ContainsKey(entity.Name)) {
				var container = entity.GetComponent<Container>();
				if(container != null && container.Ressource != null && mapper.ContainsKey(container.Ressource.Name) && 
				   displayRessources && container.Ressource.Quantity > 0) {
					cell = mapper[container.Ressource.Name];
				} else
					cell = mapper[entity.Name];
			}
			
			return cell;
		}
	}
}
