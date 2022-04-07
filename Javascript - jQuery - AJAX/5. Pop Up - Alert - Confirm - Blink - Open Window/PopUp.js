
var popUp;

function OpenCalendar(idname, postBack) 
{      
   try 
   {
       var fecha = document.getElementById(idname).value
       window.open('Calendar.aspx?formname=document.forms[0].name&id=' + idname + '&selected=' + fecha + '&postBack=' + postBack, 'PopUpCal', 'titlebar=no,location=no,status=no,menubar=no,width=220,height=200,left=200,top=250');
    }
    catch (E) {
       alert('Control ' + idname + ' no existe o invisitble');
       alert(E);
    }
       
}

function SetDate(formName, id, newDate, postBack) {
    eval('var theform = document.' + formName + ';');
    popUp.close();
    theform.elements[id].value = newDate;
    if (postBack)
        __doPostBack(id, '');
}


function ALERTA() {
    alert('sdfd');
}