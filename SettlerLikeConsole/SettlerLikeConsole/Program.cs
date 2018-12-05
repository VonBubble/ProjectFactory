/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 14:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine;
using SettlerLikeConsole.Renderer;
using SettlerLikeConsole.Controller;
using SettlerLikeConsole.Configuration;

namespace SettlerLikeConsole
{
	class Program
	{
		private static int MARGIN_BUFFER = 3;
		
		public static void Main(string[] args)
		{
			Initialize(120, 35, 5, 20);
			bool play = true;
			
			while(play) {				
				Console.Clear();
				HUD.UpdateHeader();
				Grid.Update();
				
				var input = Console.ReadKey(true).Key;
				if(input == ConsoleKey.Escape)
					play = false;
				Cursor.Handle(input);
			}
		}
		
		public static void Initialize(int screenWidth, int screenHeight, int headerHeight, int marginWidth) {
			DisableConsoleQuickEdit.Go(screenWidth, screenHeight);
			
			Grid.Initialize(screenWidth - MARGIN_BUFFER - marginWidth, screenHeight - MARGIN_BUFFER - headerHeight);
			Grid.marginWidth = marginWidth;
			//Grid.Initialize(117, 27);
			HUD.Initialize(headerHeight);
		}
	}
}