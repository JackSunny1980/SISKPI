
var progressEnd = 30; // set to number of progress <span>'s.

var progressColor = '#6591CE'; // set to progress bar color

var progressInterval = 300; // set to time between updates (milli-seconds)

var progressAt = 0;

var progressTimer;

function progress_clear() {

for (var i = 1; i <= progressEnd; i++) document.getElementById('progress'+i).style.backgroundColor = 'transparent';

progressAt = 0;

}

function progress_update() {

progressAt++;

if (progressAt > progressEnd) progress_clear();

else 
{
  document.getElementById('progress'+progressAt).style.backgroundColor = progressColor;
}

progressTimer = setTimeout('progress_update()',progressInterval);

}

function progress_stop() {

clearTimeout(progressTimer);

progress_clear();

}

function setDivPos(v)
{
    var screenHeight = window["mainWindow"].offsetHeight;
    var screenWidth = window["mainWindow"].offsetWidth;
    var ProgressBarSide=document.getElementById(v);
    ProgressBarSide.style.width = Math.round(screenWidth / 2);
    ProgressBarSide.style.left = Math.round(screenWidth / 4);
    ProgressBarSide.style.top = Math.round(screenHeight / 2-100);   
}
//progress_update(); // start progress bar