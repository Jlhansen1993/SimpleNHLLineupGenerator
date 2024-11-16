﻿using SimpleNHLLineupGenerator.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNHLLineupGenerator.Utilities
{
    public class DFSLineups
    {
        public static List<List<NHLObject>> BuildLineups(List<NHLObject> players)
        {
            // Define built lineups.
            List<(double, List<NHLObject>)> builtLineups = new List<(double, List<NHLObject>)>();

            // Define max salary.
            int maxSalary = 55000;

            // Loop through each projection.
            foreach (var player in players)
            {
                // Create temp lineup.
                List<NHLObject> tempLineup = new List<NHLObject>();

                // Add player to teamp lineup.
                tempLineup.Add(player);

                // Do the following until lineup has 9 positions.
                while (tempLineup.Count < 9)
                {
                    // Define list to filter.
                    var projectionListToFilter = new List<NHLObject>(players);

                    // Filter projections.
                    projectionListToFilter = FilterProjectionsByName(projectionListToFilter, tempLineup);
                    projectionListToFilter = FilterProjectionsByPosition(projectionListToFilter, tempLineup);
                    projectionListToFilter = FilterProjectionsByTeam(projectionListToFilter, tempLineup);

                    // Select top available position.
                    var topAvailableProjection = projectionListToFilter
                                                    .Where(p => p.Salary <= ((maxSalary - tempLineup.Sum(p => p.Salary)) / (9 - tempLineup.Count)))
                                                    .OrderByDescending(p => p.FantasyPoints)
                                                    .FirstOrDefault();

                    // Check if we did found a projection.
                    if (topAvailableProjection != null)
                        tempLineup.Add(topAvailableProjection);
                    else
                        break;
                }

                // Check if we have a full lineup.
                if (tempLineup.Count == 9)
                    //Check if salary is compliant.
                    if (tempLineup.Sum(tl => tl.Salary) <= maxSalary)
                        // Check if lineup is unique.
                        if(IsLineupUnique(tempLineup))
                            // Add the lineup to built 
                            builtLineups.Add((tempLineup.Sum(tl => tl.FantasyPoints), new List<NHLObject>(tempLineup)));
            }

            // Order lineups.
            var orderedLineups = builtLineups.OrderByDescending(bl => bl.Item1).ToList();

            // Create returnable item.
            List<List<NHLObject>> lineups = new List<List<NHLObject>>();

            // Loop through each lineup.
            foreach (var lineup in orderedLineups)
            {
                // Add lineup to lineups.
                lineups.Add(new List<NHLObject>(lineup.Item2));
            }

            List<NHLObject> FilterProjectionsByName(List<NHLObject> projections, List<NHLObject> tempLineup)
            {
                // Loop through temp lineup.
                foreach (var projection in tempLineup)
                    // Remove matching names from projections.
                    projections.RemoveAll(p => p.Name == projection.Name);

                // Return filtered projections.
                return projections;
            }

            List<NHLObject> FilterProjectionsByPosition(List<NHLObject> projections, List<NHLObject> tempLineup)
            {
                // Define team list.
                Dictionary<string, int> positionList = new Dictionary<string, int>() {
                    { "C", 2 },
                    { "W", 2 },
                    { "D", 2 },
                    { "UTIL", 2 },
                    { "G", 1 },
                };

                // Build team list.
                foreach (var projection in tempLineup)
                    // Add decrement position.
                    positionList[projection.Position]--;

                // Filter projections.
                foreach (var position in positionList)
                    if (position.Value == 0)
                        projections.RemoveAll(p => p.Position == position.Key);

                // Return projections.
                return projections;
            }

            List<NHLObject> FilterProjectionsByTeam(List<NHLObject> projections, List<NHLObject> tempLineup)
            {
                // Define team list.
                Dictionary<string, int> teamList = new Dictionary<string, int>();

                // Build team list.
                foreach (var projection in tempLineup)
                    // Add team to counter.
                    if (teamList.ContainsKey(projection.Team))
                        teamList[projection.Team]++;
                    else
                        teamList.Add(projection.Team, 1);

                // Filter projections.
                foreach (var team in teamList)
                    if (team.Value > 3)
                        projections.RemoveAll(p => p.Team == team.Key);

                // Return projections.
                return projections;
            }

            bool IsLineupUnique(List<NHLObject> tempLineup)
            {
                // Loop through each built lineup.
                foreach (var lineup in builtLineups)
                {
                    // Order built lineup.
                    var orderedBuiltLineup = lineup.Item2.OrderBy(p => p.Name).Select(p => p.Name).ToList();

                    // Order temp lineup.
                    var orderedTempLineup = tempLineup.OrderBy(p => p.Name).Select(p => p.Name).ToList();

                    // Make sure names arent the same.
                    if (orderedBuiltLineup == orderedTempLineup)
                        return false;
                }

                return true;
            }

            // Return lineups.
            return lineups;
        }
    }
}
