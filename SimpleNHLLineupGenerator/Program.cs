using System.Net;
using System;
using HtmlAgilityPack;
using SimpleNHLLineupGenerator.BOL;
using System.Reflection.Metadata.Ecma335;
using SimpleNHLLineupGenerator.Utilities;

// Get events.
List<NHLEvent> events = CSVTools.GetEvents();

// Get players.
List<NHLObject> projections = CSVTools.GetPlayers();

// Get NumberFire projections.
projections = NumberFire.GetProjections(projections);

// Update projections to have only one position.
projections = DataCleanup.FixPositions(projections);

// Build lineups.
var lineups = DFSLineups.BuildLineups(projections);

// Write lineups csv.
CSVTools.BuildLineupCSV(lineups, events);