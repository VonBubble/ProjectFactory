/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 17:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine.Utils;
using GameEngine.Factory.Entities;

namespace GameEngine.Factory
{
	/// <summary>
	/// Description of FactoryComponent.
	/// </summary>
	public interface IFactoryComponent
	{
		Vector2Int Position { get; set;}
		string Name { get; set; }
		Faction Owner { get; set; }
		int DelayUntilNextTick { get; }
		
		void Update();
	}
}
