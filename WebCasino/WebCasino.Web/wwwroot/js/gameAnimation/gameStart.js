function intSlotGameApp(images, board, totalRows, totalCols) {
    let fullBoardNumbers = [];

    board.map(function (inArrays) {

        let objArr = [];

        inArrays.map(function (objects) {

            objArr.push(objects);

        });

        let final = [];
        objArr.map(function (elements) {

            let stringValue = Object.values(elements)[0];

            if (stringValue === "low") {
                final.push(1);
            }
            else if (stringValue === "medium") {
                final.push(2);
            }
            else if (stringValue === "high") {
                final.push(3);
            }
            else if (stringValue === "wild") {
                final.push(4);
            }
            else {
                final.push(0);
            };

        });

        fullBoardNumbers.push(final);
    });


    let slots = [];

    for (var i = 0; i <= totalRows; i += 1) {
        $('#slot_div').append(`<div class="row" id="ezslots${i}"></div>`);
    };

    for (var i = 0; i < totalRows; i += 1) {

        let rowElements = fullBoardNumbers[i];

        //TODO: FOR DEVELOPMENT TEST --> REMOVE ME!!
       // console.log(rowElements);
        slots.push(new EZSlots(`ezslots${i}`,
            {
                "reelCount": totalCols,
                "winningSet": rowElements,
                "symbols": images,
                "height": 126,
                "width": 126
            }))
    };

    $("#gogogo2").click(function (e) {
        e.preventDefault();

        for (var i = 0; i < totalRows; i += 1) {
            slots[i].win();
        };
    });


};
