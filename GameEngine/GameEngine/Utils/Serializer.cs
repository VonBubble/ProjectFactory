/*
 * Created by SharpDevelop.
 * User: lcourtal
 * Date: 11/12/2018
 * Time: 14:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace GameEngine.Utils
{
	/// <summary>
	/// Description of XmlSerializer.
	/// </summary>
	public static class Serializer
	{
		public static readonly string PathToSaveDir = @"C:\Users\lcourtal\Documents\Games\Saves\";
		private static readonly string TemporaryExtension = ".tmp";
		private static readonly string FileExtension = ".sav";
		private static readonly char indentation = ' ';
		
		private static readonly string SEPARATOR = " = ";
		
		private static Dictionary<string, int> alreadyFetched;
		
		public static void SaveOnFile<T> (T objectToSave, string fileName)
	    {
			alreadyFetched = new Dictionary<string, int>();
			string path = PathToSaveDir + fileName + FileExtension + TemporaryExtension;
			using (var writer = new StreamWriter(File.Open(path, FileMode.Append)))
	        {
				var save = CacheSave(objectToSave.GetType(), 0);
				writer.Write(save);
	        }
			alreadyFetched.Clear();
			alreadyFetched = null;
	    }
		
		private static StringBuilder IndentByLevel(int level) {
			var str = new StringBuilder("");
			for (int i = 0; i < level; i++) {
				str.Append(indentation);
			}
			return str;
		}
		
		private static string CacheSave(Type type, int level) {			
			var str = new StringBuilder("");
			var indent = IndentByLevel(level);
			str.AppendLine(indent + "Type" + SEPARATOR + type);
			alreadyFetched.Add(type.GetMethod(
			
//			str.AppendLine("---- Fields ----");
//			foreach (var field in type.GetFields()) {
//				str.AppendLine(indent + field.Name + SEPARATOR + field.FieldType);
//			}
			str.AppendLine(indent + "---- Properties ----");
			foreach (var property in type.GetProperties()) {
				
				str.AppendLine(indent + property.Name + SEPARATOR + property.PropertyType);
				if(property.PropertyType.IsPrimitive == false && property.PropertyType.IsEnum == false)
					str.Append(CacheSave(property.PropertyType, level+1));
			}
//			str.AppendLine("---- Members ----");
//			foreach (var member in type.GetMembers()) {
//				str.AppendLine(indent + member.Name + SEPARATOR + member.MemberType);
//			}
			
			return str.ToString();
		}
		
//		public static void ReplaceTemporarySave() {
//			var d = new DirectoryInfo(PathToSaveDir);
//			FileInfo[] Files = d.GetFiles("*" + TemporaryExtension);
//			
//			foreach(FileInfo fileInfo in Files )
//			{
//				var save = File.Open(fileInfo.FullName);
//				
//			}
//		}
		
//	    public static T LoadFromFile<T> (string fileName)
//	    {
//	    	using (var reader = new StreamWriter(PathToSaveDir + fileName + FileExtension))
//	        {
//				return 
//	        }
//	    }
	}
}
