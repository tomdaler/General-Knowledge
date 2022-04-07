
          function DrawChats()
           {
               // Load preferences into ListPref array
               GetPreferencesFromDataBase();

               for (var x=1; x<4 ; x++) {
                     drawOneChart(x);
               }
            }

            google.load("visualization", "1", {packages:["corechart"]});
            google.setOnLoadCallback(drawVisualization);

            var ListPref[[]];


      function GetPreferencesFromDataBase() {

            ListPref.length  = 0;

            var orden_graph = 0;  // Graph 1, 2, 3
            var orden_metric = 0; // Metric 1, 2, 3

            var m1 = m2 = m3 = "";
            var id1 = id2 = id3 = or1 = or2 = or3 = t1= t2 = t3 = 0;           
 

            $.ajax({
                 type: "POST",
                 url: "IntradayBrief.aspx/GetUserPreferences",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 async: false,
                 success: function (response) {

                      for (var i = 0; i < response.d.length; i++) {

                          orden_metric++;
                          
                          var orden1  = response.d[i].DISPLAY_ORDER;
                          var ids     = response.d[i].METRIC_ID;
                          var metric1 = response.d[i].METRIC_NAME;
                          var orient1 = response.d[i].ORIENTATION;
                          var target1 = response.d[i].SHOW_TARGET;
                          
                          if (orden_graph != orden1 && (id1+id2-id3) > 0 ) {
                               var arr1 = [ orden_graph, id1, m1, or1, t1, id2, m2, or2, t2, id3, m3, or3, t3 ];
                               ListPref.push(arr1);
                               orden_metric = 1;

                               m1 = m2 = m3 = "";
                               id1 = id2 = id3 = or1 = or2 = or3 = t1= t2 = t3 = 0;
                          }

                          orden_graph = orden1;

                          if (orden_metric == 1) {
                              id1 = ids;
                              m1  = metric1;
                              or1 = orient1;
                              t1  = target1;
                           }

                          if (orden_metric == 2) {
                              id2 = ids;
                              m2  = metric1;
                              or2 = orient1;
                              t2  = target1;
                           }

                          if (orden_metric == 3) {
                              id3 = ids;
                              m3  = metric1;
                              or3 = orient1;
                              t3  = target1;
                           }                         
                      }

                      // Last one, usually Graph 3
                      if ( (id1+id2+id3) > 0) {
                         var arr1 = [ orden_graph, id1, m1, or1, t1, id2, m2, or2, t2, id3, m3, or3, t3 ];
                         ListPref.push(arr1);
                      }
 
                   },
                   error: function (XMLHttpRequest, textStatus, errorThrown) {
                       alert("Status: " + textStatus);
                       alert("Error: " + errorThrown);
                   }
               });
            }



      function PosTitle(title1) {
            if (title == "") return 0;
    
            var pos1 = 0;
            var t1 = document.getElementById('tblIntradaySnapshot');
            var r1 = t1.rows[1]; // row for Titles
            
            for (var x=2; x<r1.length; x++) {
                  if (m1 == r1.cells[x]) pos1 = x;
                }

           return pos1;
      }

      // load dummy data 
      function getDataFromGrid2(m1, t1, m2, t2, m3, t3) {
        
         var arreglo ="[ ['Interval', 'Actual', 'Forecast', 'Target'],";
 
         arreglo = arreglo + "['10:30',  735,      720,         500     ],";          
         arreglo = arreglo + "['11:00',  857,      767,         500     ],";
         arreglo = arreglo + "['11:30',  539,      610,         500     ],";
         arreglo = arreglo + "['12:00',  639,      810,         500     ],";
         arreglo = arreglo + "['12:30',  439,      710,         500     ],";
         arreglo = arreglo + "['13:00',  239,      510,         500     ],";
         arreglo = arreglo + "['13:30',  439,      810,         500     ],";
         arreglo = arreglo + "['14:00',  636,      691,         500     ] ]";

         return arreglo;       
      }


      function getDataFromGrid(m1, t1, m2, t2, m3, t3) {

            if (m1 == "") return "";

            var t1 = document.getElementById('tblIntradaySnapshot');

            var titulo = "[ ['Interval', '" + m1 + "' ";
         
            if (m2 !="") titulo = titulo + '"," + m2 + "' ";
            if (m3 !="") titulo = titulo + ",'" + m3 + "' ";

            if (t1 !="") titulo = titulo + ",'Target 1'";
            if (t2 !="") titulo = titulo + ",'Target 2'";
            if (t3 !="") titulo = titulo + ",'Target 3'";

            titulo = titulo + "], ";

            alert(titulo);
            var pos1 = PosTitle(m1);
            var pos2 = PosTitle(m2);
            var pos3 = PosTitle(m3);

            var targets = t1.rows[2]; // target row
            var arreglo = "";


            for (var x=3;x<t1.rows.count -4 ;x++) {

                 var r1 = t1.rows[x];     // each row
                 var d1 = r1.cells[1];    // date

                 var v1 = 0;
                 var v2 = 0;
                 var v3 = 0;
 
                 var t1 = 0;
                 var t2 = 0;
                 var t3 = 0;

                 if (pos1 > 0) { v1 = r1.cells[pos1]; };
                 if (pos2 > 0) { v2 = r1.cells[pos2]; };
                 if (pos3 > 0) { v3 = r1.cells[pos3]; };

                 if (t1 !="" and pos1>0) { t1 = targets.cells[pos1]; } ;
                 if (t2 !="" and pos2>0) { t2 = targets.cells[pos2]; };
                 if (t3 !="" and pos3>0) { t3 = targets.cells[pos3]; };

                 if (arreglo !="") { arreglo = arreglo + " , " } 
                 arreglo = arreglo + " ['" + d1 + "'" ;
           
                 if (pos1> 0) { arreglo = arreglo + "," + v1; }
                 if (pos2> 0) { arreglo = arreglo + "," + v2; }
                 if (pos3> 0) { arreglo = arreglo + "," + v3; }

                 if (pos1> 0 && t1!="") { arreglo = arreglo + "," + t1; }
                 if (pos2> 0 && t2!="") { arreglo = arreglo + "," + t2; }
                 if (pos3> 0 && t3!="") { arreglo = arreglo + "," + t3; }

                 arreglo = arreglo + " ] ";
              }

            arreglo = arreglo + " ] ";
                 
         }


      function drawOneChart(posicion) {

            //var arr1 = [ orden1, id1, m1, or1, t1, id2, m2, or2, t2, m3, or3, t3 ];

            var choice = 0;

            for (var j=0 ; j< ListPref.length; j++) {
                if (ListPref[j,0] == position ) choice = j;                
            }

            if (choice == 0 ) return;  

            var m1 = ListPref[choice,2];
            var o1 = ListPref[choice,3];
            var t1 = ListPref[choice,4];
            
            var m2 = ListPref[choice,6];
            var o2 = ListPref[choice,7];
            var t2 = ListPref[choice,8];

            var m3 = ListPref[choice,10];
            var o2 = ListPref[choice,11];
            var t3 = ListPref[choice,12];

            var myStr = getDataFromGrid(choice);
            if (myStr =="") return;

            var myArr = eval(myStr); 
 
            var data = google.visualization.arrayToDataTable( myArr );

            var tipo0 = {8: {type: 'bars'}}

            var tipo1 = {1: {type: 'bars'}}
            var tipo2 = {2: {type: 'bars'}}
            var tipo3 = {3: {type: 'bars'}}

            var tipo4 = {1: {type: 'bars'} , 2: {type: 'bars'}}
            var tipo5 = {1: {type: 'bars'} , 3: {type: 'bars'}}
            var tipo6 = {2: {type: 'bars'} , 3: {type: 'bars'}}

            var tipo7 = {1: {type: 'bars'} , 2: {type: 'bars'} , 3: {type: 'bars'}}

            var cual;

            var o1 = 0;
            var o2 = 0;
            var o3 = 0;

            if (o1 == 0 && o2 == 0 && o3 == 0 ) cual = tipo0;

            if (o1 == 1 && o2 == 0 && o3 == 0 ) cual = tipo1
            if (o1 == 0 && o2 == 1 && o3 == 0 ) cual = tipo2;
            if (o1 == 0 && o2 == 0 && o3 == 1 ) cual = tipo3;
            if (o1 == 1 && o2 == 0 && o3 == 0 ) cual = tipo4;
            if (o1 == 1 && o2 == 0 && o3 == 0 ) cual = tipo5;
            if (o1 == 1 && o2 == 0 && o3 == 0 ) cual = tipo6;
            if (o1 == 1 && o2 == 0 && o3 == 0 ) cual = tipo7;

        var options = {
          title : 'SL Comparison',
          vAxis: {title: 'Value'},
          hAxis: {title: 'Intervals'},
          seriesType: 'line',
          series: cual
        };

      var divs = 'chart_div'+choice;
      var chart = new google.visualization.ComboChart(document.getElementById(divs));
      chart.draw(data, options);
   }