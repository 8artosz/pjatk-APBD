using APBD2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace APBD2
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var path = args[0];
            var fi = new FileInfo(path);
            var fileContent = new List<string>();
            try
            {
                using (var stream = new StreamReader(fi.OpenRead()))
                {
                    string line = null;
                    while ((line = stream.ReadLine()) != null)
                    {
                        fileContent.Add(line);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText("log.txt","Plik " + path + " nie istnieje\n");

                System.Console.WriteLine("Plik " + path + " nie istnieje");
            }
    



            var information = new Information();


            information.writer(fileContent);
            information.studiesName();

            var json = new Output(information);
            try
            {
                using var sw = new StreamWriter(args[1] + "\\json." + args[2]);
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true
                };
                var jsonString = JsonSerializer.Serialize(json, options);
                sw.WriteLine(jsonString);
            }
            catch (DirectoryNotFoundException)
            {
                File.WriteAllText("log.txt","Podana sciezka jest niepoprawna\n");
                Console.WriteLine("Podana ścieżka jest niepoprawna");
            }

        }
    }
}
