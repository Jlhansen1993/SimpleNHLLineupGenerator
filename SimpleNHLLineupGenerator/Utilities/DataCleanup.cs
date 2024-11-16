using SimpleNHLLineupGenerator.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNHLLineupGenerator.Utilities
{
    public static class DataCleanup
    {
        public static string FixNames(string name)
        {
            // Fix ' issues.
            name = name.Replace("&#x27;", "'");
            name = name.Replace("''", "'");
            name = name.Replace("ü", "u");

            switch (name)
            {

                case "Mitchell Marner":
                    name = "Mitch Marner";
                    break;
                case "Alexander Kerfoot":
                    name = "Alex Kerfoot";
                    break;
                case "Matthew Grzelcyk":
                    name = "Matt Grzelcyk";
                    break;
                case "Jack Mcbain":
                    name = "Jack McBain";
                    break;
                case "Jon Audy-Marchessault":
                    name = "Jonathan Marchessault";
                    break;
                case "Jonathan Audy-Marchessault":
                    name = "Jonathan Marchessault";
                    break;
                case "Michael Matheson":
                    name = "Mike Matheson";
                    break;
                case "Thomas Novak":
                    name = "Tommy Novak";
                    break;
                case "John-Jason Peterka":
                    name = "JJ Peterka";
                    break;
                case "Evgeni Dadonov":
                    name = "Evgenii Dadonov";
                    break;
                case "Janis Moser":
                    name = "J.J. Moser";
                    break;
                case "Zachary Jones":
                    name = "Zac Jones";
                    break;
                case "Andrei Vasilevskii":
                    name = "Andrei Vasilevskiy";
                    break;
                case "Samuel Montembeault":
                    name = "Sam Montembeault";
                    break;
                case "Cameron Talbot":
                    name = "Cam Talbot";
                    break;
                case "Alexander Ovechkin":
                    name = "Alex Ovechkin";
                    break;
                case "Zachary Werenski":
                    name = "Zach Werenski";
                    break;
                case "Samuel Bennett":
                    name = "Sam Bennett";
                    break;
                case "Yegor Zamula":
                    name = "Egor Zamula";
                    break;
                case "Michael Anderson":
                    name = "Mikey Anderson";
                    break;
                case "Anthony-John Greer":
                    name = "A.J. Greer";
                    break;
                case "Theodor Blueger":
                    name = "Teddy Blueger";
                    break;
                case "Joseph Anderson":
                    name = "Joey Anderson";
                    break;
                case "William Borgen":
                    name = "Will Borgen";
                    break;
                case "Willy Borgen":
                    name = "Will Borgen";
                    break;
                case "Matthew Benning":
                    name = "Matt Benning";
                    break;
                case "Joel Daccord":
                    name = "Joey Daccord";
                    break;
                case "Jon Quick":
                    name = "Jonathan Quick";
                    break;
                case "Joshua Morrissey":
                    name = "Josh Morrissey";
                    break;
                case "William Cuylle":
                    name = "Will Cuylle";
                    break;
                case "Alexei Toropchenko":
                    name = "Alexey Toropchenko";
                    break;
                case "Tim Stutzle":
                    name = "Tim Stuetzle";
                    break;
                case "Matt Knies":
                    name = "Matthew Knies";
                    break;
                case "Charles McAvoy":
                    name = "Charlie McAvoy";
                    break;
                case "Matthew Coronato":
                    name = "Matt Coronato";
                    break;
                case "Zachary Bolduc":
                    name = "Zack Bolduc";
                    break;
                case "Patrick Maroon":
                    name = "Pat Maroon";
                    break;
                case "Daniel Vladar":
                    name = "Dan Vladar";
                    break;
                case "Matthew Stienburg":
                    name = "Matt Stienburg";
                    break;
                case "Mathew Dumba":
                    name = "Matt Dumba";
                    break;
                case "Matthew Boldy":
                    name = "Matt Boldy";
                    break;
                case "Jacob Middleton":
                    name = "Jake Middleton";
                    break;
                case "Jonathon Merrill":
                    name = "Jon Merrill";
                    break;
                case "Alexis Lafrenire":
                    name = "Alexis Lafreniere";
                    break;
                case "Joshua Mahura":
                    name = "Josh Mahura";
                    break;
            }

            return name.Trim();
        }

        public static string ConvertLongTeamNameToAbbreviation(string name)
        {
            switch (name)
            {
                case "Vegas Golden Knights":
                    return "VGK";
                case "Colorado Avalanche":
                    return "COL";
                case "Florida Panthers":
                    return "FLA";
                case "Minnesota Wild":
                    return "MIN";
                case "Pittsburgh Penguins":
                    return "PIT";
                case "New York Rangers":
                    return "NYR";
                case "Winnipeg Jets":
                    return "WPG";
                case "Tampa Bay Lightning":
                    return "TB";
                case "San Jose Sharks":
                    return "SJ";
                case "Buffalo Sabres":
                    return "BUF";
                case "Seattle Kraken":
                    return "SEA";
                case "New Jersey Devils":
                    return "NJ";
                case "Columbus Blue Jackets":
                    return "CBJ";
                case "Montreal Canadiens":
                    return "MON";
                case "Dallas Stars":
                    return "DAL";
                case "Toronto Maple Leafs":
                    return "TOR";
                case "Carolina Hurricanes":
                    return "CAR";
                case "Ottawa Senators":
                    return "OTT";
                case "Philadelphia Flyers":
                    return "PHI";
                case "Utah Hockey Club":
                    return "UTA";
                case "Calgary Flames":
                    return "CGY";
                case "Edmonton Oilers":
                    return "EDM";
                case "Chicago Blackhawks":
                    return "CHI";
                case "Vancouver Canucks":
                    return "VAN";
                case "Los Angeles Kings":
                    return "LA";
                case "St. Louis Blues":
                    return "STL";
                case "Washington Capitals":
                    return "WSH";
                case "Nashville Predators":
                    return "NSH";
                case "Boston Bruins":
                    return "BOS";
                case "Detroit Red Wings":
                    return "DET";
                case "New York Islanders":
                    return "NYI";
                case "Anaheim Ducks":
                    return "ANH";
                default:
                    return name;
            }
        }

        public static List<NHLObject> FixPositions(List<NHLObject> players)
        {
            // Created sorted list of players.
            List<NHLObject> util = new List<NHLObject>();

            // Fill positions.
            players.ForEach(p =>
            {
                // FLEX
                if (!p.Position.Contains("G"))
                    util.Add(new NHLObject() { PlayerFDId = p.PlayerFDId, Name = p.Name, Position = "UTIL", Team = p.Team, Opponent = p.Opponent, FantasyPoints = p.FantasyPoints, Salary = p.Salary });
            });

            // Build list of projections.
            var updatedProjections = new List<NHLObject>();
            updatedProjections.AddRange(players);
            updatedProjections.AddRange(util);

            // Order list.
            return updatedProjections.OrderByDescending(up => up.FantasyPoints).ToList();
        }
    }
}
