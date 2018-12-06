/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 03/12/2018
 * Time: 14:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine.Environment;
using GameEngine.Factory.Entities;

namespace GameEngine
{
	/// <summary>
	/// TODO: Fill this
	/// </summary>
	[Serializable]
	public sealed class World
	{
		private static World instance = new World();

		private Terrain terrain;
		private FactionList factionList;
		
	    // Explicit static constructor to tell C# compiler
	    // not to mark type as beforefieldinit
	    static World()
	    {
	    }
	
	    private World()
	    {
	    	terrain = new Terrain();
	    	factionList = new FactionList();
	    	factionList.AddFaction("Player");
	    }
	    
	    public void LoadSave(World save) {
	    	instance = save;
	    }
	    
	    public static World Instance
	    {
	        get
	        {
	            return instance;
	        }
	    }
	    
	    public Terrain Terrain
	    {
	        get
	        {
	            return terrain;
	        }
	    }
	    
	    public FactionList FactionList
	    {
	        get
	        {
	            return factionList;
	        }
	    }
	}
}