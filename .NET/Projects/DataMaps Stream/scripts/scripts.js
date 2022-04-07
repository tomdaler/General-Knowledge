
function ValidateDropDownListBox(id) {
  var options = document.getElementById(id).options;
  for (var i = 0; i < options.length; i++) {
    if (options[i].text != '--All--') {
      if (options[i].selected == true) {
        return true;
      }
    }
  }
  return false;
}

function ValidateListBox(id) {
  var options = document.getElementById(id).options;
  for (var i = 0; i < options.length; i++) {
    if (options[i].selected == true) {
      return true;
    }
  }
  return false;
}

// IS NUMBER
function IsNumeric(sText, allowNeg) {
  if (sText == "") return false; // no number
    
  // TRIM
  sText = sText.replace(/^\s*/, '').replace(/\s*$/, '');
  sText = sText.replace(/^[\s]+/, '').replace(/[\s]+$/, '').replace(/[\s]{2,}/, ' ');
  
  if (sText.length == 0) return false;

  var ValidChars = "0123456789.";
  var Char;
  var punto = false;

  // CHECK IF NEGATIVE NUMBER
  for (i = 0; i < sText.length; i++) {
    Char = sText.charAt(i);
    if (Char == "-") {
      if (i > 0 || !allowNeg) return false;
    }
    else {

      if (ValidChars.indexOf(Char) == -1) return false;

      // check doble punto
      if (Char == "." && punto) return false;
      if (Char == ".") punto = true;
    }
  }
  return true;
}


function isDate(txtDate) {

  var objDate, mSeconds, day, month, year;
  if (txtDate.length > 10 && txtDate.length < 8) return false;

  var posi1 = txtDate.indexOf("/", 0);
  var posi2 = txtDate.indexOf("/", (posi1 + 1));
  if (posi2 == 0) return false;

  month = txtDate.substring(0, posi1);
  if (month < 1 || month > 12) return false;

  posi1++;
  day = txtDate.substring(posi1, posi2);
  if (day < 0 || day > 31) return false;

  year = txtDate.substring(posi2 + 1);
  if (year < 2012 || year > 2099) return false;

  if (month == 2 || month == 4 || month == 6 || month == 9 || month == 11) {
    if (day == 31) return false;
  }

  if (month == 2) {
    if (day > 29) return false;

    var year2 = year;
    while (year2 > 2016) {
      year2 = year2 - 4;
    }

    if (day == 29 && year != 2012 && year != 2016) return false;
  }
  return true;

  mSeconds = (new Date(year, month, day)).getTime();
  objDate = new Date();
  objDate.setTime(mSeconds);

  if (objDate.getFullYear() !== year ||
           objDate.getMonth() !== month ||
           objDate.getDate() !== day) return false;

  return true;
}


// BLINKING
//=========
function Blink(control) {
  var n = document.getElementById(control);
  var t = 4;
  var b = 0;
  var c = ['visible', 'hidden'];

  var i = window.setInterval(function () {
    n.style.visibility = n.style.visibility != c[0] ? c[0] : c[1];
    b++;

    if (b == t) {
      window.clearInterval(i);
    }
  }, 500);
  n.style.visibility = "visible";
}

/*
$('#tabs').tabs();
$('#tabs').removeAttr('style');
*/

function loadjQuery() {

  $('.datepicker').datepicker({
    showOn: 'button',
    buttonImage: '/images/calendar.gif',
    buttonImageOnly: true
  });

  $('tr.RowStyle').hover(function () {
    $(this).css('background-color', '#C1DAD7');
  }, function () {
    $(this).css('background-color', '#FFFFFF');
  });

  $('tr.AlternatingRowStyle').hover(function () {
    $(this).css('background-color', '#C1DAD7');
  }, function () {
    $(this).css('background-color', '#FFFFFF');
  });

}

$(document).ready(function () {
  loadjQuery();
});
