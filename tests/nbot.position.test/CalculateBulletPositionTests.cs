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
    public class CalculateBulletPositionTests
    {
        [Fact]
        public void Can_Calculate_Bullet_Position()
        {
            var screenProvider = new PhaserScreenProperties(800, 600, 20);
            var positionProvider = new BulletPosition(300, 100, 60, screenProvider);
            var plays = new List<Play>();

            GenerateMoves(positionProvider, plays, 0, 10);

            DumpMoves(plays);
        }

        private static void DumpMoves(List<Play> plays)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var jsonData = JsonConvert.SerializeObject(plays, serializerSettings);
            var dumpJsData = $"let turns = {jsonData};";
            File.WriteAllText("../../../../nbot.ui.test/phaserjs/bulletdata.js", dumpJsData);
        }

        private static void GenerateMoves(BulletPosition positionProvider, List<Play> plays, int min, int max)
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

}
