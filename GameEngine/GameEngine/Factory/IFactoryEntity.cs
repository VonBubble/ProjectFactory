/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 10/12/2018
 * Time: 09:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using GameEngine.Factory.Component;
using System;
using GameEngine.Utils;
using GameEngine.Factory.Entities;
using System.Collections.ObjectModel;

namespace GameEngine.Factory
{
	/// <summary>
	/// Description of IFactoryEntity.
	/// </summary>
	public interface IFactoryEntity: IIdentity
	{
		Vector2Int Position { get; set;}
		Faction Owner { get; set; }
		int DelayUntilNextTick { get; }
		ReadOnlyCollection<IFactoryComponent> Components { get; }
		
		void Update();
		T GetComponent<T>();
	}
}
