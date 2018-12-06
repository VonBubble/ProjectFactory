/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 06/12/2018
 * Time: 09:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using GameEngine.Factory;
using GameEngine.Factory.Component;

namespace GameEngine.Utils
{
	/// <summary>
	/// Description of Save.
	/// </summary>
	public static class Save
	{
		public static void SerializeObject<T>(this T toSerialize, String filename)
		{
			var formatter = new BinaryFormatter();
//			var xmlSerializer = new XmlSerializer(toSerialize.GetType(), new Type[] {
//			                                      	typeof(IFactoryComponent),
//			                                      	typeof(Harvester),
//			                                      	typeof(Conveyor),
//			                                      	typeof(Builder)});
			
			using(var fileStream = new FileStream(filename, FileMode.Create)) {
            	formatter.Serialize(fileStream, toSerialize);
//				xmlSerializer.Serialize(textWriter, toSerialize);
			}
		}
		
		public static T DeserializeObject<T>(String filename) {
			using(var fileStream = new FileStream(filename, FileMode.Open))
	        {
	            var formatter = new BinaryFormatter();
	            return (T)formatter.Deserialize(fileStream);
	        }
		}
	}
}
