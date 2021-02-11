using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using nbot.actions;
using nbot.actions.screens;
using System;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Serialization;

namespace nbot.engine.test
{
    public class CalculateBotPositionTests
    {
        [Fact]
        public void Can_Calculate_Bot_Position()
        {
            var screenProvider = new PhaserScreen(800, 600, 20);
            var positionProvider = new PositionProvider(screenProvider);
            var speedometer = new Speedometer();
            var moveAction = new MoveActionsController(positionProvider, speedometer, 300, 100);
            var plays = new List<Play>();

            moveAction.MoveAhead(1000);

            GenerateMoves(moveAction, plays, 0, 5);

            moveAction.TurnRight(360);

            GenerateMoves(moveAction, plays, 5, 15);

            moveAction.TurnRight(360);

            GenerateMoves(moveAction, plays, 15, 25);

            moveAction.TurnRight(90);

            GenerateMoves(moveAction, plays, 25, 30);

            moveAction.TurnRight(45);

            GenerateMoves(moveAction, plays, 30, 35);

            // GenerateMoves(positionProvider, plays, 10, 15);

            // positionProvider.SetMoveRight(90);

            // GenerateMoves(positionProvider, plays, 15, 20);

            DumpMoves(plays);
        }

        private static void DumpMoves(List<Play> plays)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var jsonData = JsonConvert.SerializeObject(plays, serializerSettings);
            var dumpJsData = $"let turns = {jsonData};";
            File.WriteAllText("../../../../nbot.ui.tests/phaserjs/nbotdata.js", dumpJsData);
        }

        private static void GenerateMoves(MoveActionsController positionProvider, List<Play> plays, int min, int max)
        {
            for (int turn = min; turn < max; turn++)
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
                                X = Math.Round(positionProvider.Position.X),
                                Y = Math.Round(positionProvider.Position.Y)
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
