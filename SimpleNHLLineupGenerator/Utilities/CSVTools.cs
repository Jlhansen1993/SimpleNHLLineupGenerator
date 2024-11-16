using SimpleNHLLineupGenerator.BOL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNHLLineupGenerator.Utilities
{
    public class CSVTools
    {
        public static List<NHLEvent> GetEvents()
        {
            // Define empty object list.
            List<NHLEvent> events = new List<NHLEvent>();

            // Define path.
            string path = @"C:\Users\Justin\Desktop\DFSData\NHL";

            // Get all files in the directory
            string[] files = Directory.GetFiles(path);

            // Find matching file.
            string fileToParse = files.Where(file => file.Contains("entries-upload")).LastOrDefault();

            // Check to see that we have a matching file.
            if (fileToParse != null)
            {
                // Read the file.
                string[] lineData = File.ReadAllLines(fileToParse);

                // Loop through each line.
                for (int i = 1; i < lineData.Length; i++)
                {
                    // Get line columns.
                    string[] lineColumns = lineData[i].Split(",");

                    // Check if the event id is missing.
                    if (lineColumns[0].Trim('\"') == "")
                    {
                        // Break out of loop
                        break;
                    }

                    // Add events.
                    events.Add(new NHLEvent()
                    {
                        EntryId = lineColumns[0].Trim('\"'),
                        ContestId = lineColumns[1].Trim('\"'),
                        ContestName = lineColumns[2].Trim('\"'),
                    });
                }
            }

            // Return events.
            return events;
        }

        public static List<NHLObject> GetPlayers()
        {
            // Define empty object list.
            List<NHLObject> players = new List<NHLObject>();

            // Define path.
            string path = @"C:\Users\Justin\Desktop\DFSData\NHL";

            // Get all files in the directory
            string[] files = Directory.GetFiles(path);

            // Find matching file.
            string fileToParse = files.Where(file => file.Contains("players-list")).LastOrDefault();

            // Check to see that we have a matching file.
            if (fileToParse != null)
            {
                // Read the file.
                string[] lineData = File.ReadAllLines(fileToParse);

                // Loop through each line.
                for (int i = 1; i < lineData.Length; i++)
                {
                    // Get line columns.
                    string[] lineColumns = lineData[i].Split(",");

                    // Add player to projections.
                    players.Add(new NHLObject()
                    {
                        PlayerFDId = lineColumns[0],
                        Position = lineColumns[1],
                        Name = lineColumns[3],
                        Salary = Convert.ToInt32(lineColumns[7]),
                        Team = lineColumns[9],
                        Opponent = lineColumns[10]
                    });
                }
            }

            // Return players.
            return players;
        }

        public static void BuildLineupCSV(List<List<NHLObject>> lineups, List<NHLEvent> events)
        {
            // Define file path.
            string csvFilePath = @$"C:\Users\Justin\Desktop\DFSData\NHL\event_lineups_{DateTime.Now.ToString("MMddyyy")}.csv";

            // Check if file exists, delete it.
            if (File.Exists(csvFilePath))
            {
                File.Delete(csvFilePath);
            }

            // Create CSV with data.
            using (var csvWriter = new StreamWriter(csvFilePath))
            {
                // Define headers.
                csvWriter.WriteLine("entry_id,contest_id,contest_name,C,C,W,W,D,D,UTIL,UTIL,G");

                // Loop through each contest.
                for(int i = 0; i < events.Count; i++)
                {
                    // Define the custom position order
                    var positionOrder = new Dictionary<string, int>
                    {
                        { "C", 1 },
                        { "W", 2 },
                        { "D", 3 },
                        { "UTIL", 4 },
                        { "G", 5 }
                    };

                    // Sort the list using OrderBy with the custom order
                    var orderedPlayers = lineups[i]
                        .OrderBy(p => positionOrder.ContainsKey(p.Position) ? positionOrder[p.Position] : int.MaxValue)
                        .ToList();

                    // Build lineup.
                    events[i].C1 = orderedPlayers[0].PlayerFDId;
                    events[i].C2 = orderedPlayers[1].PlayerFDId;
                    events[i].W1 = orderedPlayers[2].PlayerFDId;
                    events[i].W2 = orderedPlayers[3].PlayerFDId;
                    events[i].D1 = orderedPlayers[4].PlayerFDId;
                    events[i].D2 = orderedPlayers[5].PlayerFDId;
                    events[i].UTIL1 = orderedPlayers[6].PlayerFDId;
                    events[i].UTIL2 = orderedPlayers[7].PlayerFDId;
                    events[i].G = orderedPlayers[8].PlayerFDId;                
                }

                // Loop trhough each player.
                foreach (var item in events)
                {
                    csvWriter.WriteLine($"{item.EntryId},{item.ContestId},{item.ContestName},{item.C1},{item.C2},{item.W1},{item.W2},{item.D1},{item.D2},{item.UTIL1},{item.UTIL2},{item.G}");
                }
            }
        }
    }
}
