
//OPM
//wuguanhui
//专用于GridView的鼠标移动效果。
//
var _oldColor;
function SetNewColor(source) {
    _oldColor = source.style.backgroundColor;
    source.style.backgroundColor = '#87CEFA';   //亮天蓝色

}

function SetOldColor(source) {
    source.style.backgroundColor = _oldColor;
}

//需要在GridView的 DataBound事件中添加
//鼠标移到效果
//e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
//e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");   