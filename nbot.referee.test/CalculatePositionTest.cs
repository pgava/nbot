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
            var screenProvider = new D3ScreenProvider(640, 480);
            var positionProvider = new PositionProvider(screenProvider, 200, 200);

            positionProvider.SetMoveAhead(1000);
            positionProvider.SetMoveRight(90);

            var plays = new List<Play>();

            for (int turn = 0; turn < 10; turn++)
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

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var jsonData = JsonConvert.SerializeObject(plays, serializerSettings);
            File.WriteAllText("json-data.txt", jsonData);
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
