
$(document).ready(function() {
 $("#readFile").click(function() {


     $.get('demo_test.txt', function(data) {  $("#container").html(data); },'text');

 });
});
 


