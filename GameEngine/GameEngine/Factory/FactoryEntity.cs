/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 10/12/2018
 * Time: 09:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using GameEngine.Utils;
using GameEngine.Factory;
using GameEngine.Factory.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GameEngine.Factory.Component;

namespace GameEngine.Factory
{
	/// <summary>
	/// Description of FactoryEntity.
	/// </summary>
	[Serializable]
	public class FactoryEntity: IFactoryEntity, IXmlSerializable
	{
		private Vector2Int position;
		private string name;
		private Faction owner;
		private List<IFactoryComponent> components;
		
		private FactoryEntity() {}
		
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
		
		#region IXmlSerializer Methods
	    public void WriteXml (XmlWriter writer)
	    {
	    	writer.WriteAttributeString("Name", name);
	    	writer.WriteAttributeString("Owner", Owner.Name);
	    	
	    	if(components != null) {
	    		foreach (var component in components) {
		    		writer.WriteStartElement(typeof(IFactoryComponent).Name);
		            component.WriteXml(writer);
		            writer.WriteEndElement();
	    		}
	    	}
	    }
	
	    public void ReadXml (XmlReader reader)
	    {
	    	name = reader["Name"];
	    	components = new List<IFactoryComponent>();
	    	owner = World.Instance.FactionList.GetFaction(reader["Owner"]);
	    	owner.AddFactoryEntity(this);
	    	//owner = Convert.ToInt32(reader["Z"]);
	    	reader.MoveToContent();
	    	if (reader.ReadToDescendant(typeof(IFactoryComponent).Name))
            {
                while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == typeof(IFactoryComponent).Name)
                {
                	var component = new EmptyComponent();
                	component.ReadXml(reader);
                	components.Add(component.ActualComponent);
                }
            }
	    	foreach (var component in components) {
	    		component.Parent = this;
	    	}
	    	
	    	reader.Read();
	    }
	
	    public XmlSchema GetSchema()
	    {
	        return(null);
	    }
	    #endregion
		
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
		
		public ReadOnlyCollection<IFactoryComponent> Components { 
			get {
				return components.AsReadOnly();
			}
		}
	}
}
