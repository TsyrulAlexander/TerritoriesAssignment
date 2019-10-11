namespace TerritoriesAssignment.Core.Utilities
{
	using System.IO;
	using System.Runtime.Serialization.Formatters.Binary;

	static class ObjectUtilities
	{
		public static T DeepClone<T>(this T obj) {
			using (MemoryStream memory_stream = new MemoryStream()) {
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(memory_stream, obj);
				memory_stream.Position = 0;
				return (T)formatter.Deserialize(memory_stream);
			}
		}
	}
}
