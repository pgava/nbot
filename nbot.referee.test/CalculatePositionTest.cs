using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using nbot.contracts;
using System;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Serialization;

namespace nbot.referee.test
{
    public class CalculatePositionTest
    {
        [Fact]
        public void Can_Calculate_Bot_Position()
        {
            var screenProvider = new D3ScreenProperties(640, 480);
            var positionProvider = new Position(screenProvider, 200, 200);
            var plays = new List<Play>();

            positionProvider.SetMoveAhead(1000);

            GenerateMoves(positionProvider, plays, 5);

            positionProvider.SetMoveRight(90);

            GenerateMoves(positionProvider, plays, 10);

            DumpMoves(plays);
        }

        private static void DumpMoves(List<Play> plays)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var jsonData = JsonConvert.SerializeObject(plays, serializerSettings);
            var dumpJsData = $"let turns = {jsonData};";
            File.WriteAllText("../../../../nbot.ui.test/phaserjs/nbotdata.js", dumpJsData);
        }

        private static void GenerateMoves(Position positionProvider, List<Play> plays, int max)
        {
            for (int turn = 0; turn < max; turn++)
            {
                positionProvider.CalculateNextPosition();
                plays.Add(new Play
                {
                    Turn = turn,
                    Actors = new List<Actor>
                    {
                        new Actor
                        {
                            Type = "bot",
                            Id = "bot1",
                            Pos = new Pos
                            {
                                X = Math.Round(positionProvider.X),
                                Y = Math.Round(positionProvider.Y)
                            }
                        }
                    }
                });
            }
        }
    }

    public class Play
    {
        public int Turn { get; set; }
        public List<Actor> Actors { get; set; }
    }

    public class Actor
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public Pos Pos { get; set; }
    }

    public class Pos
    {
        public double X { get; set; }
        public double Y { get; set; }

    }
}
