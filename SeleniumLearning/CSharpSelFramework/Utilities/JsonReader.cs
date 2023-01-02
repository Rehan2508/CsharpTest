using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelFramework.Utilities
{
    public class JsonReader
    {
        public JsonReader()
        {

        }

        public string ExtractData(string tokenName)
        {
            var myJsonString = File.ReadAllText("Utilities/TestData.json");
            var jsonObject = JToken.Parse(myJsonString);
            //Console.WriteLine(jsonObject.SelectToken("username").Value<string>()); //console.write cause it is a poc : needs a main to run
            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public string[] ExtractDataArray(string tokenName)
        {
            var myJsonString = File.ReadAllText("Utilities/TestData.json");
            var jsonObject = JToken.Parse(myJsonString);
            //Console.WriteLine(jsonObject.SelectToken("username").Value<string>()); //console.write cause it is a poc : needs a main to run
            List<string> productList = jsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return productList.ToArray();
        }
    }
}
