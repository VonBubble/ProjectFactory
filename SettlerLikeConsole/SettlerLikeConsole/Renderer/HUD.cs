/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 15:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using GameEngine;
using GameEngine.Factory;
using GameEngine.Factory.Entities.Construction;
using GameEngine.Environment;
using SettlerLikeConsole.Controller;

namespace SettlerLikeConsole.Renderer
{
	/// <summary>
	/// Description of HUD.
	/// </summary>
	public static class HUD
	{
		private const char BORDER_CORNER = '+';
		private const char BORDER_HORIZONTAL = '-';
		private const char BORDER_VERTICAL = '|';
		
		private const char EMPTY_CELL = ' ';
		
		private static int height;
		
		public static void Initialize(int height) {
			HUD.height = height;
		}
		
		public static void UpdateHeader()
		{
			int width = World.Instance.Terrain.Cells.GetLength(0) + Grid.marginWidth;
			for (int y = 0; y < height; y++) {
				var line = new StringBuilder();
				for (int x = -1; x <= width; x++) {
					if(y == 0 || y == height - 1) {
						if(x == -1 || x == width)
							line.Append(BORDER_CORNER);
						else
							line.Append(BORDER_HORIZONTAL);
					}
					else if(x == -1 || x == width)
						line.Append(BORDER_VERTICAL);
					else {
						line.Append(EMPTY_CELL);
					}
				}
				Console.WriteLine(line);
			}
		}
		
		public static StringBuilder GetLineMargin(int i) {
			var line = new StringBuilder();
			var units = new List<Mecha>();
			var cell = Cursor.GetFocusedCell();
			foreach (var factions in World.Instance.FactionList.Factions) {
				foreach (var unit in factions.Units) {
					if(unit.Position.X == cell.Position.X && unit.Position.Y == cell.Position.Y) {
						units.Add(unit);
					}
				}
			}
			
			for (int x = 0; x < Grid.marginWidth; x++) {
				if(i == -1 || i == World.Instance.Terrain.Cells.GetLength(1)) {
					if(x == 0 || x == Grid.marginWidth-1) {
						line.Append(BORDER_CORNER);
					} else {
						line.Append(BORDER_HORIZONTAL);
					}
				} else {if(x == 0 || x == Grid.marginWidth-1) {
						line.Append(BORDER_VERTICAL);
					} else {
						if(cell != null && i == 0) {
							line.Append(DisplayBasicDetails(cell, Grid.marginWidth));
							break;
						} else if(cell != null && cell.FactoryComponent != null) {
							line.Append(DisplayFactoryComponentDetails(cell.FactoryComponent, i, Grid.marginWidth));
							break;
						} else {
							if(units.Count > 0) {
								line.Append(DisplayUnitDetails(units, i, Grid.marginWidth));
								break;
							} else
								line.Append(EMPTY_CELL);
						}
					}
				}
			}
			
			return line;
		}
		
		private static StringBuilder DisplayBasicDetails(TerrainCell cell, int width) {
			var line = new StringBuilder();
			if(cell == null)
				return line;
			line.Append(cell.Position);
			
			return PadRight(line.ToString(), EMPTY_CELL, width, BORDER_VERTICAL);
		}
		
		private static StringBuilder DisplayFactoryComponentDetails(IFactoryComponent component, int i, int width) {
			var line = new StringBuilder();
			if(component == null)
				return line;
			
			var details = FactoryComponentDetails.GetDetails(component);
			if(details.Count > i)
				line.Append(details[i]);
			
			return PadRight(line.ToString(), EMPTY_CELL, width, BORDER_VERTICAL);
		}
		
		private static StringBuilder DisplayUnitDetails(List<Mecha> units, int i, int width) {
			var line = new StringBuilder();
			if(units == null)
				return line;
			
			var details = UnitDetails.GetDetails(units);
			if(details.Count > i)
				line.Append(details[i]);
			
			return PadRight(line.ToString(), EMPTY_CELL, width, BORDER_VERTICAL);
		}
		
		private static StringBuilder PadRight(string str, char c, int width, char lastChar) {
			var line = new StringBuilder(str);
			line.Append(c, width - line.Length - 2);
			line.Append(lastChar);
			return line;
		}
	}
}

