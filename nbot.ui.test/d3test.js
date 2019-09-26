
let d3Helper = (function () {
    //let turns = [{ "turn": 0, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 200.0, "y": 200.0 } }] }, { "turn": 1, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 202.0, "y": 200.0 } }] }, { "turn": 2, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 204.0, "y": 200.0 } }] }, { "turn": 3, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 206.0, "y": 200.0 } }] }, { "turn": 4, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 210.0, "y": 199.0 } }] }, { "turn": 5, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 214.0, "y": 198.0 } }] }, { "turn": 6, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 219.0, "y": 197.0 } }] }, { "turn": 7, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 225.0, "y": 194.0 } }] }, { "turn": 8, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 231.0, "y": 191.0 } }] }, { "turn": 9, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 237.0, "y": 187.0 } }] }, { "turn": 10, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 243.0, "y": 181.0 } }] }, { "turn": 11, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 248.0, "y": 174.0 } }] }, { "turn": 12, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 253.0, "y": 165.0 } }] }, { "turn": 13, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 256.0, "y": 154.0 } }] }, { "turn": 14, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 258.0, "y": 143.0 } }] }, { "turn": 15, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 256.0, "y": 155.0 } }] }, { "turn": 16, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 252.0, "y": 168.0 } }] }, { "turn": 17, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 244.0, "y": 179.0 } }] }, { "turn": 18, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 234.0, "y": 190.0 } }] }, { "turn": 19, "actors": [{ "type": "bot", "id": "bot1", "pos": { "x": 220.0, "y": 197.0 } }] }];
    let turns2 = [
        {
            "turn": 1,
            "actors": [
                {
                    "type": "bot",
                    "id": "bot1",
                    "pos": {
                        x: 10,
                        y: 20
                    }
                },
                {
                    "type": "bot",
                    "id": "bot2",
                    "pos": {
                        x: 40,
                        y: 50
                    }
                }
            ]
        },
        {
            "turn": 2,
            "actors": [
                {
                    "type": "bot",
                    "id": "bot1",
                    "pos": {
                        x: 20,
                        y: 30
                    }
                },
                {
                    "type": "bot",
                    "id": "bot2",
                    "pos": {
                        x: 60,
                        y: 80
                    }
                },
                {
                    "type": "bot",
                    "id": "bot3",
                    "pos": {
                        x: 200,
                        y: 150
                    }
                }

            ]
        },
        {
            "turn": 3,
            "actors": [
                {
                    "type": "bot",
                    "id": "bot1",
                    "pos": {
                        x: 30,
                        y: 40
                    }
                },
                {
                    "type": "bot",
                    "id": "bot3",
                    "pos": {
                        x: 220,
                        y: 140
                    }
                }
            ]
        }
    ];

    let container = {};
    let currentActors = [];

    function init() {
        container = d3.select("body").append("svg")
            .attr("width", 640)
            .attr("height", 480)
            .style("border", "2px solid black");
    }

    function draw(turns) {
        for (let index = 0; index < turns.length; index++) {

            currentActors.forEach((item) => {
                item.status = "off"
            });

            drawActors(turns[index].actors);
        }
    }

    function drawActors(actors) {

        for (let index = 0; index < actors.length; index++) {
            if (!hasActor(actors[index].id)) {
                drawActor(actors[index]);
            }
            else {
                moveActor(actors[index]);
            }
        }

        //Remove 
        for (let index = 0; index < currentActors.length; index++) {
            if (currentActors[index].status === "off") {
                const query = `rect[data-id='${currentActors[index].id}']`;

                let deleteBot = container.selectAll(query)
                    .transition()
                    .style("fill", "purple")
                    .remove();

                currentActors.splice(index, 1);
            }
        }

        console.log(container.selectAll("rect").data());
    }

    function drawActor(actor) {
        currentActors.push({
            "id": actor.id,
            "status": "active"
        });

        actors = container.selectAll("bots")
            .data([actor])
            .enter()
            .append("rect")
            .attr("data-type", function (d) {
                console.log("type: " + d.type);
                return d.type;
            })
            .attr("data-id", function (d) {
                return d.id;
            })
            .attr("x", function (d) {
                return d.pos.x;
            })
            .attr("y", function (d) {
                return d.pos.y;
            })
            .style("opacity", .2)
            .style("stroke", "red")
            .style("fill", "green")
            .attr("width", 20)
            .attr("height", 20);
    }

    function moveActor(actor) {
        currentActors.forEach((item) => {
            if (item.id === actor.id) {
                item.status = "active"
            }
        });
        const query = `rect[data-id='${actor.id}']`;

        let moveBots = container.selectAll(query)
            .transition()
            .duration(1000)
            .attr("x", function (d, i) {
                return actor.pos.x;
            })
            .attr("y", function (d) {
                return actor.pos.y;
            });
    }

    function hasActor(id) {
        const query = `rect[data-id='${id}']`;
        return container.selectAll(query).data().length > 0;
    }

    return {
        init: init,
        draw: function (turns) {
            draw(turns);
        }
    }
})();
