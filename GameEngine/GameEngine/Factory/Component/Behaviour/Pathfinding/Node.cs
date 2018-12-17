/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 14/12/2018
 * Time: 10:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using GameEngine.Utils;

namespace GameEngine.Factory.Component.Behaviour.Pathfinding
{
	/// <summary>
	/// Description of Node.
	/// </summary>
	public struct Node
	{
		public Vector2Int position;
		public Vector2Int bestNeighboor;
		public int costFromOrigin;
		public int costToGoal;
	}
}
