using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testJsonPars
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            do
            {
                string fileName = null;
                do
                {
                    fileName = getFilePath();
                }
                while (fileName == null);

                if (fileName == "exit")
                    exit = true;
                else
                    printJson(fileName);
            }
            while (!exit);
            Environment.Exit(0);
        }

        static void printJson(string fileName)
        {
            StreamReader streamReader = new StreamReader(fileName);

            string json = streamReader.ReadToEnd();
            dynamic parsedObj = JsonConvert.DeserializeObject(json);
            var jObj = (JObject)parsedObj;

            foreach (JToken token in jObj.Children())
            {
                foreach (KeyValuePair<string, string> keyValue in MyJsonPars.JsonPars.getJsonValid(token))
                    Console.WriteLine(keyValue.Key + " = " + keyValue.Value);

                Console.WriteLine("");
            }
        }

        static string getFilePath()
        {
            string filePathReturn = null;
            string errorFile = "It's not .json file. ";
            string errorJson = null;

            Console.Write("/Exit-enter 'c'/...Enter path to json file: ");
            string pathFile = Console.ReadLine();

            if (pathFile != "c")
            {
                if (isValidFile(pathFile))
                {
                    if (MyJsonPars.JsonPars.Validate(pathFile, out errorJson))
                        return pathFile;
                    else
                        Console.Write(errorFile + errorJson);
                }
                else
                    Console.Write(errorFile + errorJson);
            }
            else
                filePathReturn = "exit";
            return filePathReturn;
        }

        static bool isValidFile(string pathFile)
        {
            bool isValid = false;
            string validFileType = "json";
            string ext = Path.GetExtension(pathFile);

            if (ext == "." + validFileType)
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
