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

            this.backgroundColor = "white";
            this.color = "red";
            this.margin = "2";
        }

        DrawGraph(data){
            let ctx = this.canvasCtx;
            let keys = Object.keys(data);
            let maxValue = this._getMaxValue(data, keys);
            let maxBarHeight = this.canvasHeight - this.yMargin*2 - this.xLabelHeight;
            let maxBarWidth = this._getMaxBarWidth(keys.length);

            //Background
            ctx.fillStyle = this.backgroundColor;
            ctx.fillRect(0,0,this.canvasWidth, this.canvasHeight);

            for(let i = 0; i < keys.length; i++){
                let value = data[keys[i]];

                ctx.fillStyle = this.color;
                ctx.fillRect(this._getBarXMargin(i, maxBarWidth), this._getBarYMargin(), maxBarWidth, this._getBarHeight(maxBarHeight, maxValue, value));
            }
        }

        SetColor(color){
            this.color = color;
        }

        SetBackgroundColor(backgroundColor){
            this.backgroundColor = backgroundColor;
        }

        _getBarXMargin(barNumber, maxBarWidth) {
            let effectiveBarNumber = barNumber > 0 ? barNumber - 1 : 0;
            let totalUsedMargin = this.margin + effectiveBarNumber * this.margin;
            let totalUsedBarSpace = barNumber * maxBarWidth;

            return totalUsedBarSpace + totalUsedMargin;
        }

        _getBarYMargin(){
            let totalUsedMargin = this.margin;
            let totalUsedBarSpace = this.xLabelHeight;
            return totalUsedMargin+totalUsedBarSpace;
        }

        _getMaxValue(data, keys){
            let maxvalue = 0;
            for(let i = 0; i < keys.length; i++){
                let value = !data[keys[i]];
                if(!value.isNaN() && value > this.maxvalue){
                    maxvalue = value;
                }
            }

            return maxvalue;
        }

        _getMaxBarWidth(barAmount){
            let usedMarginWitdh = (barAmount - 1 + 2) * this.margin;
            return (this.canvasWidth - usedMarginWitdh) / barAmount;
        }

        _getBarHeight(maxBarHeight, maxValue, value){
            let ratio = value/maxValue;
            return maxBarHeight * ratio;
        }
    }