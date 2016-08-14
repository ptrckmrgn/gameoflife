/*
    GAME PAINTER
*/

function paintGame(element, height, width, cells) {
    var canvas = $(element);

    // get correct dimensions from parent elements
    var canvasWidth = parseInt(canvas.parent().css('width'), 10) - parseInt(canvas.parent().css('padding-left'), 10) - parseInt(canvas.parent().css('padding-right'), 10);
    var canvasHeight = canvasWidth * (height / width);

    // set canvas dimensions
    canvas.attr('width', canvasWidth);
    canvas.attr('height', canvasHeight);

    // set cell dimensions
    var cellWidth = canvasWidth / width;
    var cellHeight = canvasHeight / height;
    var cellGutter = 2;

    // paint canvas
    var ctx = canvas[0].getContext('2d');
    ctx.fillStyle = "#ffffff";

    for (var i = 0; i < height; i++) {
        for (var j = 0; j < width; j++) {
            // get cell state from string
            cellNum = i * width + j;
            cell = cells.substring(cellNum, cellNum + 1);

            // paint if alive
            if (cell.toLowerCase() == 'o') {
                ctx.fillRect(j * cellWidth + cellGutter, i * cellHeight + cellGutter, cellWidth - cellGutter * 2, cellHeight - cellGutter * 2);
            }
        }
    }
}