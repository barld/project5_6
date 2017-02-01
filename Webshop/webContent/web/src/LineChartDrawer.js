/**
 * Created by Gregor on 24-1-2017.
 */
class PieChartDrawer{
    _construct(canvas_element){

    }

    DrawChart(data, keys){

    }

    _getValueFromArray(data, keys, index){
        console.log(data + "-"+ index + "-"+keys);
        return data[index];
    }

    _getValueFromObject(data, keys, index){
        return data[keys[index]];
    }
}