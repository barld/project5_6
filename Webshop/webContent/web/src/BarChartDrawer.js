/**
 * Created by Gregor on 18-1-2017.
 */
export default
    class BarChartDrawer{
        constructor(canvas){
            this.canvasCtx = canvas.getContext("2d");
            this.canvasHeight = canvas.clientHeight;
            this.canvasWidth = canvas.clientWidth;
            this.xLabelHeight = 40;
            this.yMargin = 2;
            this.barTextYMargin = 5;
            this.barTextSize = 20;

            this.backgroundColor = "white";
            this.color = "red";
            this.margin = 5;
            this.defaultBarWidth = 50;
        }

        DrawGraph(data){
            let ctx = this.canvasCtx;
            let keys = Object.keys(data);
            let maxValue = this._getMaxValue(data, keys);
            let maxBarHeight = this.canvasHeight - this.yMargin*2 - this.xLabelHeight;
            let barWidth = this._getBarWidth(keys.length);
            let barHeight = null;
            let barXMargin = null;
            let barYMargin = null;
            let textBarXMargin = null;
            let textBarYMargin = null;

            //Vertical and horizontal line
            ctx.fillStyle = "black";
            ctx.moveTo(this.margin, this.margin);
            ctx.lineTo(this.margin, maxBarHeight + this.margin * 2);
            ctx.lineTo(this._getBarXMargin(keys.length, barWidth) ,maxBarHeight + this.margin * 2);
            ctx.stroke();

            for(let i = 0; i < keys.length; i++){
                let value = data[keys[i]];
                barHeight = this._getBarHeight(maxBarHeight, maxValue, value);
                barXMargin = this._getBarXMargin(i, barWidth);
                barYMargin = this._getBarYMargin(barHeight, maxBarHeight);
                textBarXMargin = this._getBarTextXMargin(barXMargin, barWidth);
                textBarYMargin = this._getBarTextYMargin(barYMargin);

                ctx.fillStyle = this.color;
                ctx.imageSmoothingEnabled = false;
                ctx.fillRect(barXMargin, barYMargin, barWidth, barHeight);

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
            let totalUsedMargin = this.margin * 2 + barNumber * this.margin;
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
            let usedMarginWidth = (barAmount + 1) * this.margin;
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