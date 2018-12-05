/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 14:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using GameEngine;
using GameEngine.Utils;
using GameEngine.Factory.Entities.Construction;
using SettlerLikeConsole.Controller;

namespace SettlerLikeConsole.Renderer
{
	/// <summary>
	/// Description of Grid.
	/// </summary>
	public static class Grid
	{
		public static int marginWidth;
		
		private const char BORDER_CORNER = '+';
		private const char BORDER_HORIZONTAL = '-';
		private const char BORDER_VERTICAL = '|';
		
		private const char EMPTY_CELL = '.';
		
		private const char MECHA_UNIT = 'm';
		private const char MECHA_UNITS = 'M';
		
		public static void Initialize(int width, int height) {
			World.Instance.Terrain.GenerateNewMap(width, height);
			Cursor.Set(width/2, height/2);
		}
		
		public static void Update()
		{
			int width = World.Instance.Terrain.Cells.GetLength(0);
			int height = World.Instance.Terrain.Cells.GetLength(1);
			
			for (int y = -1; y <= height; y++) {
				var line = new StringBuilder();
				for (int x = -1; x <= width; x++) {
					if(y == -1 || y == height) {
						if(x == -1 || x == width)
							line.Append(BORDER_CORNER);
						else
							line.Append(BORDER_HORIZONTAL);
					}
					else if(x == -1 || x == width)
						line.Append(BORDER_VERTICAL);
					else {
						if(Cursor.Position.X == x && Cursor.Position.Y == y)
							line.Append(Cursor.CHARACTER);
						else {
							char factory = FactoryLayers.RenderCell(x, y);
							if(factory == FactoryLayers.EMPTY_CELL) {
								bool unitFound = false;
								foreach (var faction in World.Instance.FactionList.Factions) {
									foreach (Mecha unit in faction.Units) {
										if(unit.Position.X == x && unit.Position.Y == y) {
											line.Append(MECHA_UNIT);
											unitFound = true;
											break;
										}
									}
								}
								if(unitFound == false)
									line.Append(EMPTY_CELL);
							} else {
								line.Append(factory);
							}
						}
					}
				}
				line.Append(HUD.GetLineMargin(y));
				Console.WriteLine(line);
			}
		}
	}
}
