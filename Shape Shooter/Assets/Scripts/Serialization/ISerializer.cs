using System.Collections.Generic;

namespace Wokarol.SerializationSystem.Serializers
{
	public interface ISerializer
	{
		bool FileExist (string fileName, string folderName);

		void Delete (string fileName, string folderName, System.Action beforeDelete = null, System.Action afterDelete = null);

		void SerializeData (Dictionary<string, object> dataToSerialize, string fileName, string folderName);
		Dictionary<string, object> DeserializeData (string fileName, string folderName);
	}
}