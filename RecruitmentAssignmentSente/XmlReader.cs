using System.IO;
using System.Xml.Serialization;

namespace RecruitmentAssignmentSente
{
    class XmlReader<T> where T : class
    {
        private string _filePath;

        public XmlReader(string filePath)
            => _filePath = filePath;

        public T Read()
        {
            if (!_filePath.ToLower().EndsWith(".xml"))
            {
                throw new InvalidDataException("Expected XML file.");
            }

            if (!File.Exists(_filePath))
            {
                throw new InvalidDataException("Provided file does not exist.");
            }

            try
            {
                using (var fileStream = new FileStream(_filePath, FileMode.Open))
                {
                    fileStream.Position = 0;
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    var deserializedObject = xmlSerializer.Deserialize(fileStream);

                    if (deserializedObject != null)
                    {
                        return (T)deserializedObject;
                    }
                }
            }
            catch (System.InvalidOperationException ex)
            {
                throw new InvalidDataException("Provided file is in a different format than expected.", ex);
            }

            return null;
        }
    }
}
