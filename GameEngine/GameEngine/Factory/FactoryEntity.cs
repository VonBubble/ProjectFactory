/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 10/12/2018
 * Time: 09:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameEngine.Utils;
using GameEngine.Factory;
using GameEngine.Factory.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameEngine.Factory
{
	/// <summary>
	/// Description of FactoryEntity.
	/// </summary>
	public class FactoryEntity: IFactoryEntity
	{
		private Vector2Int position;
		private string name;
		private Faction owner;
		private int delayUntilNextTick;
		private List<IFactoryComponent> components;
		
		public FactoryEntity(string name, Vector2Int pos, Faction owner) {
			components = new List<IFactoryComponent>();
			this.name = name;
			this.position = pos;
			this.owner = owner;
		}
		
		public void Update() {
			foreach (var component in components) {
				component.Update();
			}
		}
		
		public IFactoryComponent AddComponent(IFactoryComponent component) {
			components.Add(component);
			return component;
		}
		
		public void RemoveComponent(IFactoryComponent component) {
			components.Remove(component);
		}
		
		public T GetComponent<T>() {
			foreach (var component in components) {
				if(component.GetType() == typeof(T))
					return (T)component;
			}
			return default(T);
		}
		
		public Vector2Int Position { 
			get {
				return position;
			}
			set {
				position = value;
			}
		}
		public string Name { 
			get {
				return name;
			}
		}
		public Faction Owner { 
			get{
				return owner;
			} set{
				owner = value;
			}
		}
		public int DelayUntilNextTick { 
			get {
				return delayUntilNextTick; 
			}
		}
		public ReadOnlyCollection<IFactoryComponent> Components { 
			get {
				return components.AsReadOnly();
			}
		}
	}
}
