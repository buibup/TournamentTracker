﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

// * Load the text file
// * Convert the text to List<PrizeModel>
// Find the max ID
// Add the new record with the new ID (max +1)
// Convert the prizes to list<string>
// Save the list<string> to the text file

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName) 
        {
            return $"{ ConfigurationManager.AppSettings["filePath"] }\\{ fileName }";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            var output = new List<PrizeModel>();

            foreach(var line in lines)
            {
                string[] cols = line.Split(',');

                var p = new PrizeModel()
                {
                    Id = int.Parse(cols[0]),
                    PlaceNumber = int.Parse(cols[1]),
                    PlaceName = cols[2],
                    PrizeAmount = decimal.Parse(cols[3]),
                    PrizePercentage = double.Parse(cols[4])
                };
                output.Add(p);
            }

            return output;
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            var output = new List<PersonModel>();

            foreach(var line in lines)
            {
                string[] cols = line.Split(',');

                var p = new PersonModel()
                {
                    Id = int.Parse(cols[0]),
                    FirstName = cols[1],
                    LastName = cols[2],
                    EmailAddress = cols[3],
                    CellphoneNumber = cols[4]
                };
                output.Add(p);
            }

            return output;
        }

        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            var lines = new List<string>();

            foreach(var p in models)
            {
                lines.Add($"{ p.Id }, { p.PlaceNumber },{ p.PlaceName },{ p.PrizeAmount },{ p.PrizePercentage }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName)
        {
            var lines = new List<string>();

            foreach(var p in models)
            {
                lines.Add($"{ p.Id },{ p.FirstName },{ p.LastName },{ p.EmailAddress },{ p.CellphoneNumber }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
    }
}
