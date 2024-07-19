using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using Assignment3.Utility;

namespace Assignment3.Tests
{
    public static class SerializationHelper
    {
        /// <summary>
        /// Serializes (encodes) users
        /// </summary>
        /// <param name="users">List of users</param>
        /// <param name="fileName"></param>
        public static void SerializeUsers(ILinkedListADT users, string fileName)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(SLL));
            using (FileStream stream = File.Create(fileName))
            {
                serializer.WriteObject(stream, users);
            }
        }

        /// <summary>
        /// Deserializes (decodes) users
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>List of users</returns>
        public static SLL DeserializeUsers(string fileName)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(SLL));

            using (FileStream stream = File.OpenRead(fileName))
            {
                SLL deserialized = serializer.ReadObject(stream) as SLL;

                // Check if deserialized is null before using it
                if (deserialized != null)
                {
                   return deserialized;
                }
                else
                {
                    Console.WriteLine("Deserialized object is null.");
                    throw new InvalidOperationException("Deserialization resulted in null.");
                }
            }
        }
    }
}
