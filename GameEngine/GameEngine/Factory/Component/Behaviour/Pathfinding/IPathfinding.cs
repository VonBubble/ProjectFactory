/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 17/12/2018
 * Time: 09:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Collections.Generic;
using GameEngine.Utils;

namespace GameEngine.Factory.Component.Behaviour.Pathfinding
{
	/// <summary>
	/// Description of IPathfinding.
	/// </summary>
	public interface IPathfinding: IBehaviourComponent
	{
		List<Vector2Int> Path { get; }
	}
}
