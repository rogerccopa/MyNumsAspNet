using System;
using System.IO;
using System.Net;

namespace MyNumsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\Temp\NewNums.txt";
            var newNums = string.Join(",", File.ReadAllLines(filePath));

            var apiUrl = "https://localhost:44399/api/MyNums";
            var request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            string payload = $"{{\"Nums\":\"{newNums}\"}}";

            using (var streamWriter = new  StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(payload);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        if (stream == null) return;

                        using (StreamReader streamReader = new StreamReader(stream))
                        {
                            string requestBody = streamReader.ReadToEnd();
                            Console.WriteLine(requestBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // ToDo
            }

            Console.WriteLine("Client done posting to server");
        }
    }
}
