/**
 * Created by Gregor on 18-1-2017.
 */
export default
    class BarChartDrawer{
        constructor(canvas){
            this.canvasCtx = canvas.getContext("2d");
            this.canvasHeight = canvas.clientHeight;
            this.canvasWidth = canvas.clientWidth;
            this.yLabelHeight = 50;
            this.xLabelWidth = 50;
            this.yMargin = 2;
            this.barTextYMargin = 5;
            this.barTextSize = 12;

            this.backgroundColor = "white";
            this.color = "red";
            this.margin = 5;
            this.defaultBarWidth = 50;
        }

        DrawGraph(data){
            let ctx = this.canvasCtx;
            let keys = Object.keys(data);
            let maxValue = this._getMaxValue(data, keys);
            let maxBarHeight = this.canvasHeight - this.yMargin*2 - this.yLabelHeight;
            let barWidth = this._getBarWidth(keys.length);
            let barHeight = null;
            let barXMargin = null;
            let barYMargin = null;
            let textBarXMargin = null;
            let textBarYMargin = null;

            //Resetting the canvas
            ctx.clearRect(0, 0, this.canvasWidth, this.canvasHeight);

            //Background
            ctx.fillStyle = this.backgroundColor;
            ctx.fillRect(0,0,this.canvasWidth, this.canvasHeight);

            //Vertical and horizontal line
            ctx.fillStyle = "black";
            ctx.beginPath();
            ctx.moveTo(this._getBarXMargin(0, barWidth) - this.margin, this.margin);
            ctx.lineTo(this._getBarXMargin(0, barWidth) - this.margin, maxBarHeight + this.margin * 2);
            ctx.lineTo(this._getBarXMargin(keys.length, barWidth) ,maxBarHeight + this.margin * 2);
            ctx.stroke();

            //Painting the bars and associated labels
            for(let i = 0; i < keys.length; i++){
                let value = data[keys[i]];
                barHeight = this._getBarHeight(maxBarHeight, maxValue, value);
                barXMargin = this._getBarXMargin(i, barWidth);
                barYMargin = this._getBarYMargin(barHeight, maxBarHeight);
                textBarXMargin = this._getBarTextXMargin(barXMargin, barWidth);
                textBarYMargin = this._getBarTextYMargin(barYMargin);

                //Painting the bars
                ctx.fillStyle = this.color;
                ctx.imageSmoothingEnabled = false;
                ctx.fillRect(barXMargin, barYMargin, barWidth, barHeight);

                //Painting the labels
                ctx.font = this.barTextSize + 'pt Times New Roman';
                ctx.fillStyle = "black";
                ctx.textAlign = "center";
                ctx.fillText(value, textBarXMargin, textBarYMargin, barWidth);
            }
        }

        SetColor(color){
            this.color = color;
        }

        SetBackgroundColor(backgroundColor){
            this.backgroundColor = backgroundColor;
        }

        _getBarXMargin(barNumber, barWidth) {
            let totalUsedMargin = this.margin * 2 + barNumber * this.margin + this.xLabelWidth;
            let totalUsedBarSpace = barNumber * barWidth;

            return totalUsedBarSpace + totalUsedMargin;
        }

        _getBarYMargin(barHeight, maxBarHeight){
            let barMargin = maxBarHeight - barHeight;
            let totalMargin = barMargin + this.margin;
            return totalMargin;
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

        _getBarWidth(barAmount){
            let usedMarginWidth = (barAmount + 1) * this.margin + this.xLabelWidth;
            let width = (this.canvasWidth - usedMarginWidth) / barAmount;

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