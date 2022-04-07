

function sortableTable(tableIDx,intDef,sortProps){

  var tableID = tableIDx;
  var intCol = 0;
  var intDir = 1;
  var strMethod;
  var arrHead = null;
  var arrMethods = sortProps.split(",");

  this.init = function(){
    arrHead = document.getElementById(tableID).getElementsByTagName('thead')[0].getElementsByTagName('th');
    for(var i=0;i<arrHead.length;i++){
	  arrHead[i].onclick = new Function(tableIDx + ".sortTable(" + i + ",'" + arrMethods[i] + "');");
    }
    this.sortTable(intDef,arrMethods[intDef]);
  }

  this.sortTable = function(intColx,strMethodx){ 

    intCol = intColx;
	strMethod = strMethodx;

	var arrRows = document.getElementById(tableID).getElementsByTagName('tbody')[0].getElementsByTagName('tr');

    intDir = (arrHead[intCol].className=="asc")?-1:1;
    arrHead[intCol].className = (arrHead[intCol].className=="asc")?"des":"asc";
	for(var i=0;i<arrHead.length;i++){
      if(i!=intCol){arrHead[i].className="";}
	}
	  
	var arrRowsSort = new Array(); 
	for(var i=0;i<arrRows.length;i++){ 
      arrRowsSort[i]=arrRows[i].cloneNode(true); 
    }
    arrRowsSort.sort(sort2dFnc);
	      
	for(var i=0;i<arrRows.length;i++){   
	  arrRows[i].parentNode.replaceChild(arrRowsSort[i],arrRows[i]);
      arrRows[i].className = (i%2==0)?"":"alt";
	} 

  } 

  function sort2dFnc(a,b){
    var col = intCol;
    var dir = intDir;
    var aCell = a.getElementsByTagName("td")[col].innerHTML;
    var bCell = b.getElementsByTagName("td")[col].innerHTML;
	   
    switch (strMethod){
    case "int":
      aCell = parseInt(aCell);
      bCell = parseInt(bCell);			 
	  break;
	case "float":
      aCell = parseFloat(aCell);
      bCell = parseFloat(bCell);			 		   
	  break;
	case "date":
      aCell = new Date(aCell);
      bCell = new Date(bCell);
	  break;	   
	}
    return (aCell>bCell)?dir:(aCell<bCell)?-dir:0;
  }
}