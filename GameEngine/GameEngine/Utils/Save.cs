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
using System.Xml;
using System.Xml.Serialization;
using GameEngine.Environment;
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
			//var formatter = new BinaryFormatter();
			var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
			var xmlSerializer = new XmlSerializer(toSerialize.GetType(), new Type[] {
			                                      	typeof(Terrain),
			                                      	typeof(Producer),
			                                      	typeof(PutInto),
			                                      	typeof(Container),
			                                      	typeof(Grabber),
			                                      	typeof(Turnable),
			                                      	typeof(Generator)});
	        var settings = new XmlWriterSettings();
	        settings.Indent = true;
	        settings.OmitXmlDeclaration = true;
	        using (var stream = new StreamWriter(filename))
	        using (var writer = XmlWriter.Create(stream, settings))
	        {
				xmlSerializer.Serialize(writer, toSerialize, emptyNamespaces);
	        }
		}
		
		public static T DeserializeObject<T>(String filename) {
			var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
			var xmlSerializer = new XmlSerializer(typeof(T), new Type[] {
			                                      	typeof(Terrain),
			                                      	typeof(Producer),
			                                      	typeof(PutInto),
			                                      	typeof(Container),
			                                      	typeof(Grabber),
			                                      	typeof(Turnable),
			                                      	typeof(Generator)});
	        var settings = new XmlWriterSettings();
	        settings.Indent = true;
	        settings.OmitXmlDeclaration = true;
	        using (var stream = new StreamReader(filename))
        	using (var reader = XmlReader.Create(stream))
	        {
				return(T)xmlSerializer.Deserialize(reader);
			}
//			using(var fileStream = new FileStream(filename, FileMode.Open))
//	        {
//	            var formatter = new BinaryFormatter();
//	            return (T)formatter.Deserialize(fileStream);
//	        }
		}
	}
}
