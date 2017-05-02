/* GAME PAINTER */

! function ($) {
    var PaintGame = function (e, options) {
        this.canvas = $(e);
        this.ctx = this.canvas[0].getContext('2d');

        this.options = $.extend({}, $.fn.paintgame.defaults, options);

        this.mode = this.options.mode;

        // Get game inputs
        if (this.mode == 'write') {
            this.heightInput = $(this.options.height);
            this.widthInput = $(this.options.width);
            this.cellsInput = $(this.options.cells);

            this.height = this.heightInput.val();
            this.width = this.widthInput.val();
            this.cellsString = this.cellsInput.val();
        }
        else if (this.mode == 'read') {
            this.height = this.options.height;
            this.width = this.options.width;
            this.cellsString = this.options.cells;
        }

        this.fit = this.options.fit;
        this.background = this.options.background;
        this.deadCell = this.options.deadCell;
        this.liveCell = this.options.liveCell;
        this.cellGutter = this.options.cellGutter;

        this.start();
    }

    function cell(state, x1, y1) {
        this.state = state;
        this.x1 = x1;
        this.y1 = y1;
    }

    PaintGame.prototype = {
        constructor: PaintGame,

        start: function () {
            this.init();
            this.onresize();

            if (this.mode == 'write') {
                this.onmousemove();
                this.onmouseout();
                this.onclick();
            }
        },

        init: function () {
            var pos, state, x1, y1;

            this.canvas.css('background', this.background);

            // Get parent dimensions
            var parentHeight = parseInt(this.canvas.parent().css('height'), 10) - parseInt(this.canvas.parent().css('padding-top'), 10) - parseInt(this.canvas.parent().css('padding-bottom'), 10);
            var parentWidth = parseInt(this.canvas.parent().css('width'), 10) - parseInt(this.canvas.parent().css('padding-left'), 10) - parseInt(this.canvas.parent().css('padding-right'), 10);
            var parentRatio = parentHeight / parentWidth;

            var gameRatio = this.height / this.width;

            if (this.fit == 'width') {
                this.canvasWidth = parentWidth;
                this.canvasHeight = parentWidth * gameRatio;
            }
            else if (this.fit == 'height') {
                this.canvasHeight = parentHeight;
                this.canvasWidth = parentHeight / gameRatio;
            }
            else if (this.fit == 'both') {
                // Fit game to parent while keeping ratio
                if (gameRatio > parentRatio) {
                    this.canvasHeight = parentHeight;
                    this.canvasWidth = parentHeight / gameRatio;
                }
                else {
                    this.canvasWidth = parentWidth;
                    this.canvasHeight = parentWidth * gameRatio;
                }
            }

            // Set canvas dimensions
            this.canvas.attr('width', this.canvasWidth);
            this.canvas.attr('height', this.canvasHeight);

            // Set cell dimensions
            this.cellWidth = (this.canvasWidth / this.width) - (2 * this.cellGutter);
            this.cellHeight = (this.canvasHeight / this.height) - (2 * this.cellGutter);

            // Initialize array
            this.cells = new Array(this.height * this.width);

            for (var row = 0; row < this.height; row++) {
                for (var col = 0; col < this.width; col++) {
                    // Get linear position
                    pos = row * this.width + col;

                    // Get cell state from cells string
                    state = this.cellsString.substring(pos, pos + 1);

                    // Calc cell coordinates
                    x1 = col * (this.cellWidth + 2 * this.cellGutter) + this.cellGutter;
                    y1 = row * (this.cellHeight + 2 * this.cellGutter) + this.cellGutter;

                    this.cells[pos] = new cell(state, x1, y1);
                }
            }

            this.paint();
        },

        paint: function () {
            this.ctx.clearRect(0, 0, this.canvasWidth, this.canvasHeight);

            // Paint canvas
            var pos, state, x1, y1, x2, y2, rnd;
            for (var row = 0; row < this.height; row++) {
                for (var col = 0; col < this.width; col++) {
                    // Get linear position
                    pos = row * this.width + col;

                    // Paint if alive, clear if dead
                    if (this.cells[pos].state.toUpperCase() == 'O') {
                        // Pick live cell color and fill cell
                        rnd = Math.floor((Math.random() * this.liveCell.length));
                        this.ctx.fillStyle = this.liveCell[rnd];
                        this.ctx.fillRect(this.cells[pos].x1, this.cells[pos].y1, this.cellWidth, this.cellHeight);
                    }
                    else {
                        // Pick dead cell color and fill cell
                        rnd = Math.floor((Math.random() * this.deadCell.length));
                        this.ctx.fillStyle = this.deadCell[rnd];
                        this.ctx.fillRect(this.cells[pos].x1, this.cells[pos].y1, this.cellWidth, this.cellHeight);
                    }
                }
            }
        },

        onmousemove: function () {
            var self = this;

            this.canvas.mousemove(function (e) {
                var x = e.offsetX,
                    y = e.offsetY,
                    pos;

                for (var row = 0; row < self.height; row++) {
                    for (var col = 0; col < self.width; col++) {
                        // Get linear position
                        pos = row * self.width + col;

                        // Check if mouse position is within the current cell
                        if (x > self.cells[pos].x1
                                && y > self.cells[pos].y1
                            && x < self.cells[pos].x1 + self.cellWidth
                            && y < self.cells[pos].y1 + self.cellHeight) {
                            if (self.cells[pos].state == 'X') {
                                // Return all other cells to uppercase
                                self.cellsToUppercase();

                                // Make cell lowercase to prevent re-triggering
                                self.cells[pos].state = 'x';

                                // Reset canvas and fill in highlighted cell
                                self.paint();

                                // Pick live cell color
                                rnd = Math.floor((Math.random() * self.liveCell.length));
                                self.ctx.fillStyle = self.liveCell[rnd];

                                self.ctx.fillRect(self.cells[pos].x1, self.cells[pos].y1, self.cellWidth, self.cellHeight);
                            }
                            else if (self.cells[pos].state == 'O') {
                                // Return all other cells to uppercase
                                self.cellsToUppercase();

                                // Make cell lowercase to prevent re-triggering
                                self.cells[pos].state = 'o';

                                // Reset canvas
                                self.paint();
                            }
                        }
                    }
                }
            });
        },

        onmouseout: function () {
            var self = this;
            this.canvas.mouseout(function () {
                for (var row = 0; row < self.height; row++) {
                    for (var col = 0; col < self.width; col++) {
                        // Get linear position
                        pos = row * self.width + col;

                        // Reset cells and canvas
                        self.cellsToUppercase();
                        self.paint();
                    }
                }
            });
        },

        onclick: function () {
            var self = this;
            this.canvas.click(function (e) {
                var pos,
                        x = e.offsetX,
                    y = e.offsetY;

                for (var row = 0; row < self.height; row++) {
                    for (var col = 0; col < self.width; col++) {
                        // Get linear position
                        pos = row * self.width + col;

                        // Check if mouse position is within the current cell
                        if (x > self.cells[pos].x1
                            && y > self.cells[pos].y1
                            && x < self.cells[pos].x1 + self.cellWidth
                            && y < self.cells[pos].y1 + self.cellHeight) {
                            // Invert state
                            if (self.cells[pos].state.toUpperCase() == 'O') {
                                self.cells[pos].state = 'x';
                            }
                            else {
                                self.cells[pos].state = 'O';
                            }

                            // Reset canvas
                            self.paint();

                            // Rebuild cell string
                            self.cellsToString();

                            // Change input box value
                            self.cellsInput.val(self.cellsString);
                        }
                    }
                }
            });
        },

        onresize: function () {
            var self = this;
            $(window).resize(function () {
                self.resize();
            });
            if (this.mode == 'write') {
                self.heightInput.on('input', function () {
                    self.resize();
                });
                self.widthInput.on('input', function () {
                    self.resize();
                });
            }
        },

        resize: function () {
            var len, diff;

            // Refresh game inputs
            if (this.mode == 'write') {
                this.height = this.heightInput.val();
                this.width = this.widthInput.val();
                this.cellsString = this.cellsInput.val();
            }

            // Determine difference between previous state and new
            if (this.height > 0 && this.width > 0) {
                len = this.cellsString.length;
                diff = (this.height * this.width) - len;

                if (diff < 0) {
                    this.cellsString = this.cellsString.substring(0, len + diff);
                }
                else if (diff > 0) {
                    for (i = 0; i < diff; i++) {
                        this.cellsString = this.cellsString + "X";
                    }
                }
            }

            // Replace cell string in input box
            if (this.mode == 'write') {
                this.cellsInput.val(this.cellsString);
            }

            // Reinitialise
            this.init();
        },

        cellsToUppercase: function () {
            var self = this;

            // Return all other cells to uppercase
            $.each(this.cells, function (index, item) {
                self.cells[index].state = item.state.toUpperCase();
            });
        },

        cellsToString: function () {
            var self = this;

            // Rebuild string
            this.cellsString = "";
            $.each(this.cells, function (index, item) {
                self.cellsString += item.state.toUpperCase();
            });
        }
    }

    $.fn.paintgame = function (option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data('paintgame');
            var options = typeof option == 'object' && option;

            if (!data) $this.data('paintgame', (data = new PaintGame(this, options)));
            if (typeof option == 'string') data[option]();
        });
    };

    $.fn.paintgame.defaults = {
        mode: 'read',
        fit: 'width',
        cellGutter: 2,
        deadCell: ['#000000'],
        liveCell: ['#FFFFFF'],
        background: 'none',
    };
}(window.jQuery);