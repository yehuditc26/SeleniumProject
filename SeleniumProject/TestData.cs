using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SeleniumProject
{
    public class TestData
    {
        public string SearchTerm { get; set; }
    }

    public class XmlDataReader
    {
        public static IEnumerable<TestData> ReadTestData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} does not exist.");
            }

            var serializer = new XmlSerializer(typeof(List<TestData>), new XmlRootAttribute("TestCases"));
            using (var reader = new StreamReader(filePath))
            {
                return (List<TestData>)serializer.Deserialize(reader);
            }
        }
    }
}