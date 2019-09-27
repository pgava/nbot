
let d3Helper = (function () {
    let turns2 = [
        {
            "turn": 1,
            "actors": [
                {
                    "type": "bot",
                    "id": "bot1",
                    "pos": {
                        x: 200,
                        y: 200
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
                        x: 250,
                        y: 200
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
                        x: 200,
                        y: 150
                    }
                }
            ]
        },
        {
            "turn": 4,
            "actors": [
                {
                    "type": "bot",
                    "id": "bot1",
                    "pos": {
                        x: 150,
                        y: 200
                    }
                }
            ]
        },
        {
            "turn": 5,
            "actors": [
                {
                    "type": "bot",
                    "id": "bot1",
                    "pos": {
                        x: 200,
                        y: 250
                    }
                }
            ]
        },
        {
            "turn": 6,
            "actors": [
                {
                    "type": "bot",
                    "id": "bot1",
                    "pos": {
                        x: 250,
                        y: 200
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
            .style("border", "2px solid white");
    }

    function draw(turns) {
        let time = 100;
        for (let index = 0; index < turns.length; index++) {

            currentActors.forEach((item) => {
                item.status = "off"
            });

            setTimeout(function(){ drawActors(turns[index].actors); }, time);

            time += 100;

            //drawActors(turns[index].actors);
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
                const query = `circle[data-id='${currentActors[index].id}']`;

                let deleteBot = container.selectAll(query)
                    .transition()
                    .style("fill", "purple")
                    .remove();

                currentActors.splice(index, 1);
            }
        }

        console.log(container.selectAll("circle").data());
    }

    function drawActor(actor) {
        currentActors.push({
            "id": actor.id,
            "status": "active"
        });

        actors = container.selectAll("bots")
            .data([actor])
            .enter()
            .append("circle")
            .attr("data-type", function (d) {
                console.log("type: " + d.type);
                return d.type;
            })
            .attr("data-id", function (d) {
                return d.id;
            })
            .attr("cx", function (d) {
                return d.pos.x;
            })
            .attr("cy", function (d) {
                return d.pos.y;
            })
            .style("opacity", .2)
            .style("stroke", "red")
            .style("fill", "white")
            .attr("r", 10);            
    }

    function moveActor(actor) {
        currentActors.forEach((item) => {
            if (item.id === actor.id) {
                item.status = "active"
            }
        });
        const query = `circle[data-id='${actor.id}']`;

        let moveBots = container.selectAll(query)
            .transition()            
            .attr("cx", function (d, i) {
                return actor.pos.x;
            })
            .attr("cy", function (d) {
                return actor.pos.y;
            });
    }

    function hasActor(id) {
        const query = `circle[data-id='${id}']`;
        return container.selectAll(query).data().length > 0;
    }

    return {
        init: init,
        draw: function (turns) {
            draw(turns);
        }
    }
})();
