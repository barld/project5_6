/**
 * Created by Gregor on 18-1-2017.
 */
export default
    class BarChartDrawer{
        constructor(canvas){
            this.canvasCtx = canvas.getContext("2d");
            this.canvasHeight = canvas.clientHeight;
            this.canvasWidth = canvas.clientWidth;
            this.yLabelHeight = 100;
            this.xLabelWidth = 100;
            this.yMargin = 2;
            this.barTextYMargin = 5;
            this.barTextSize = 12;

            this.backgroundColor = "white";
            this.color = "red";
            this.margin = 5;
            this.defaultBarWidth = 50;
            this.remainingBarSpace = 0;
            this.remainder = 0;
        }

        DrawGraph(data, _keys){
            let ctx = this.canvasCtx;
            let keys = typeof _keys == "undefined" ? Object.keys(data) : _keys;
            let length = keys.length;
            let barMargin = length > 50 ? Math.trunc(this.margin / 2) : length > 100 ? Math.trunc(this.margin / 4) : this.margin;
            let barTextSize = length > 50 ? Math.trunc(this.barTextSize / 2) : length > 100 ? Math.trunc(this.barTextSize / 4) : this.barTextSize;
            let maxValue = this._getMaxValue(data, keys);
            let maxBarHeight = this.canvasHeight - this.yMargin*2 - this.yLabelHeight;
            let barWidth = this._getBarWidth(length, barMargin);
            let barHeight = null;
            let barXMargin = null;
            let barYMargin = null;
            let textBarXMargin = null;
            let textBarYMargin = null;

            //console.log(keys);

            //Resetting the canvas
            ctx.clearRect(0, 0, this.canvasWidth, this.canvasHeight);

            //Background
            ctx.fillStyle = this.backgroundColor;
            ctx.fillRect(0,0,this.canvasWidth, this.canvasHeight);

            //Vertical and horizontal line
            ctx.fillStyle = "black";
            ctx.beginPath();
            ctx.moveTo(this._getBarXMargin(0, barWidth, barMargin) - this.margin, this.margin);
            ctx.lineTo(this._getBarXMargin(0, barWidth, barMargin) - this.margin, maxBarHeight + this.margin + 1);
            ctx.lineTo(this._getBarXMargin(keys.length, barWidth, barMargin) ,maxBarHeight + this.margin + 1);
            ctx.stroke();

            //Painting the bars and associated labels
            for(let i = 0; i < length; i++){
                let value = data[keys[i]];

                barHeight = this._getBarHeight(maxBarHeight, maxValue, value);
                barXMargin = this._getBarXMargin(i, barWidth, barMargin);
                barYMargin = this._getBarYMargin(barHeight, maxBarHeight);
                textBarXMargin = this._getBarTextXMargin(barXMargin, barWidth);
                textBarYMargin = this._getBarTextYMargin(barYMargin);

                //Painting the bars
                ctx.fillStyle = this.color;
                ctx.imageSmoothingEnabled = false;
                ctx.fillRect(barXMargin, barYMargin, barWidth, barHeight);

                //Painting the labels
                ctx.font = barTextSize + 'pt Times New Roman';
                ctx.fillStyle = "black";
                ctx.textAlign = "center";
                ctx.fillText(value, textBarXMargin, textBarYMargin, barWidth);

                //console.log("Current item = key: " + keys[i] + " value: " + value);
            }

            //Painting the labels

        }

        SetColor(color){
            this.color = color;
        }

        SetBackgroundColor(backgroundColor){
            this.backgroundColor = backgroundColor;
        }

        _getBarXMargin(barNumber, barWidth, barMargin) {
            let beginMargin = this.xLabelWidth + this.margin * 2;
            let usedBarMargin = barNumber * barMargin;
            let usedBarSpace = barNumber * barWidth;

            //console.log("Current margin: " + (beginMargin + usedBarMargin + usedBarSpace) + " Barwidth: " + barWidth + " BarMargin: " + barMargin);
            return beginMargin + usedBarMargin + usedBarSpace;
        }

        _getBarYMargin(barHeight, maxBarHeight){
            let barMargin = maxBarHeight - barHeight;
            return barMargin + this.margin;
        }

        _getMaxValue(data, keys){
            let maxvalue = 0;
            for(let i = 0; i < keys.length; i++){
                let value = data[keys[i]];

                if(!isNaN(value) && value > maxvalue){
                    maxvalue = value;
                }
            }

            return maxvalue;
        }

        _getBarWidth(barAmount, barMargin){
            let effectiveSpace = this.canvasWidth - this.margin * 2 - this.xLabelWidth;
            let totalBarMargin = (barAmount - 1) * barMargin;
            let width = Math.trunc((effectiveSpace - totalBarMargin) / barAmount);
            let remainingSpace = effectiveSpace - (width * barAmount + totalBarMargin);

            //console.log("Effective space: " + effectiveSpace);
            //console.log("Total margin width: " + totalBarMargin);
            //console.log("Bar width: " + width);
            console.log("Remaining space: " + remainingSpace);

            return width > this.defaultBarWidth ? this.defaultBarWidth : width;
        }

        _getBarHeight(maxBarHeight, maxValue, value){
            let ratio = value/maxValue;
            return maxBarHeight * ratio;
        }

        _getBarTextXMargin(barXMargin, barWidth){
            return barXMargin + (barWidth/2);
        }

        _getBarTextYMargin(barYMargin){
            return barYMargin + this.barTextYMargin + this.barTextSize;
        }

        //console.log("usedMarginWidth = Type: " + typeof usedMarginWidth + " Value: " + usedMarginWidth);
    }