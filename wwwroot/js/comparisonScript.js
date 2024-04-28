const numbers = [];

$(document).ready(function() {
    // Add class '5g' on every input
    document.querySelectorAll('td').forEach((td) => {
        td.querySelectorAll('input').forEach((input) => {
            input.classList.add('5g');
        });
    });document.querySelectorAll('td').forEach((td) => {
        td.querySelectorAll('input').forEach((input) => {
            input.classList.add('5g');
        });
    });
    let count = document.querySelectorAll("td.ram").length;
    if (count > 1) {
        setColor5G();
        setColor("ram");
        setColor("battery-capacity");
        setColor("resolution");
        setColor("charging-speed");
    }
    //scrolling section
    const phonesDiv = document.getElementById('phones');
    const leftBttn = document.getElementById('left-bttn');
    const rightBttn = document.getElementById('right-bttn');

    //scroll to beginning
    leftBttn.onclick = function(e) {
        if(leftBttn.classList.contains('disabled')) {
            e.preventDefault();  // Prevent the default action
        } else {
            phonesDiv.scrollTo({ left: 0, behavior: 'smooth' });
        }
    }

    //scroll to end
    rightBttn.onclick = function(e) {
        if(rightBttn.classList.contains('disabled')) {
            e.preventDefault();  // Prevent the default action
        } else {
            phonesDiv.scrollTo({ left: phonesDiv.scrollWidth - phonesDiv.clientWidth, behavior: 'smooth' });
        }
    }

    //scroll behavior
    phonesDiv.onscroll = function() {
        // start-scroll check
        if (phonesDiv.scrollLeft === 0) {
            leftBttn.classList.add('disabled');
            phonesDiv.classList.remove('boxShadowLeft');
        } else {
            leftBttn.classList.remove('disabled');
            phonesDiv.classList.add('boxShadowLeft');
        }

        // end-scroll check
        if (phonesDiv.scrollLeft + phonesDiv.clientWidth >= phonesDiv.scrollWidth) {
            rightBttn.classList.add('disabled');
            phonesDiv.classList.remove('boxShadowRight');
        } else {
            rightBttn.classList.remove('disabled');
            phonesDiv.classList.add('boxShadowRight');
        }
    }

    phonesDiv.onscroll(undefined);
});

function getNumbers(str) {
    return str.match(/-?\d+/g).map(Number);
}

function setColor(className) {
    let numbers = [];
    $("td." + className).each(function() {
        let nums = getNumbers($(this).text());
        numbers.push(nums[0]);
    });
    let maxNum = Math.max.apply(null, numbers);
    let minNum = Math.min.apply(null, numbers);
    let avgNum = numbers.reduce((a, b) => a + b, 0) / numbers.length;
    let maxAvgAvg = (maxNum + avgNum) / 2;
    let minAvgAvg = (minNum + avgNum) / 2;
    console.log(className + ':', numbers);
    console.log(className + ' Avg:' + avgNum);
    console.log(className + ' Max:' + maxNum);
    console.log(className + ' Min:' + minNum);
    console.log(className + ' MinAvg:' + minAvgAvg);
    console.log(className + ' MaxAvg:' + maxAvgAvg);

    let backgroundColor;
    $(`td.${className}`).each(function() {
        const num = getNumbers($(this).text())[0];
        if(num === minNum){
            //red
            backgroundColor = '#FC8080';
        }
        if(num > minNum && num <= minAvgAvg){
            //lightred
            backgroundColor = '#F7ACAC';
        }
        if(num >= maxAvgAvg && num < maxNum){
            //lightgreen
            backgroundColor = '#B8ECB0';
        }
        if(num === maxNum){
            //green
            backgroundColor = '#9DEA90';
        }
        if(num > minAvgAvg && num < maxAvgAvg){
            //yellow
            backgroundColor = '#F4E09F';
        }
        $(this).css('background-color', backgroundColor);
        $(this).css('color', 'black');
        $(this).css('box-shadow', 'inset 0px 4px 3px rgba(50, 50, 50, 0.75)')
    });
}
function setColor5G(){
    let allChecked = true;
    let allUnchecked = true;

    const checks = document.getElementsByClassName("5g");
    [].forEach.call(checks, function(el) {
        if (el.checked) {
            allUnchecked = false;
        }
        else{
            allChecked = false;
        }
    });
    console.log('5g allchecked:' + allChecked);
    console.log('5g allUNchecked:' + allUnchecked);
    if(!allChecked && !allUnchecked){
        [].forEach.call(checks, function(el) {
            const td = el.parentNode;
            //green
            if(el.checked){
                td.style.backgroundColor = "#9DEA90";
                td.style.color = "black";
                td.style.boxShadow = "inset 0px 4px 3px rgba(50, 50, 50, 0.75)";
            }
            //red
            if(!el.checked){
                td.style.backgroundColor = "#FC8080";
                td.style.color = "black";
                td.style.boxShadow = "inset 0px 4px 3px rgba(50, 50, 50, 0.75)";
            }
        });
    }
}