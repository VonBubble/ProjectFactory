/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 17:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace GameEngine.Factory.Entities
{
	/// <summary>
	/// Description of FactionList.
	/// </summary>
	public class FactionList
	{
		private List<Faction> factions;
		
		public FactionList()
		{
			factions = new List<Faction>();
		}
		
		public void Update() {
			new Debug("FactionList.update() called");
			foreach (var faction in factions) {
				faction.Update();
			}
		}
		
		public void AddFaction(string name) {
			factions.Add(new Faction(name));
		}
		
		public Faction GetFaction(string name) {
			foreach (var faction in factions) {
				if(faction.Name == name)
					return faction;
			}
			
			return null;
		}
		
		public ReadOnlyCollection<Faction> Factions {
			get {
				return factions.AsReadOnly();
			}
		}
	}
}
