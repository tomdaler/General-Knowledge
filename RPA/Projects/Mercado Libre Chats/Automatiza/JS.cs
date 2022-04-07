namespace Automatiza
{
    class JS
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string LoadLOB(string LOB)
        {
            string asignar = "$('#selectSkills').val('"+ LOB+"'); populateAgentsTab();";
            return asignar;

//            string cargar = "$.ajax({ url: '/supervisor/getAgentsData', ";
////            cargar = cargar + "data: { skillsId: '[\"6\"]'}, ";
//            cargar = cargar + "data: { skillsId: '[\""+LOB+"\"]'}, ";

//            cargar = cargar + "method: 'POST' ";
//            cargar = cargar + "}).done(function(html) { ";
//            cargar = cargar + " $('#agentsContent').html(html);  }); return true; ";
//            return cargar;
        }

        public string ClickBtn(string btnPag)
        {

            string js = "$('ul.pagination li:nth-child("+btnPag+")').trigger('click')";


            //string js = "var ul=document.getElementsByClassName('pagination'); ";
            //js = js + "var li = ul[0].getElementsByTagName('li'); ";
            //js = js + "li[" + btnPag + "].click; ";

            //$("li#f1").trigger("click");
            //$('ul.pagination li:first').trigger('click');

            //$('ul.pagination li':nth-child(3)).trigger('click');


            return js;

            //string ss = "$(document).ready(function() {";
            //ss = ss+ "  ul =document.getElementsByClassName('pagination');
            //li=ul[0].getElementsByTagName('li')
            //;li[3].click;return true;";
            //ss = ss + " }); ";
        }

        public void VerChat(string chatId)
        {
            string js = "$.ajax({  url: '/supervisor/loadTranscript/' + chatId, ";
            js = js + "}).done(function(data) {";

            js = js + "    ";

            js = js + " }); ";

        }


        public string Paginacion(string LOB, string pagina)
        {
            //string pagina = "2";
            //string LOB = "3";

            string js = " $.ajax({ ";
            js = js + "url: '/supervisor/getAgentChats',  ";
            js = js + " data: ";
            js = js + " { userId: " + pagina + ",";
            js = js + "   skillId: " + LOB + "}, ";
            js = js + "   method: 'POST'}).done( ";
            js = js + "      function(html) { ";
            js = js + "         this.className = 'glyphicon glyphicon-minus sign'; ";
            js = js + "         row.child( html ).show(); ";
            js = js + "   });  ";
            return js;
        }

        public void ss()
        {

            string js2 = "var btn = document.getElementById('pagination');";
            js2 = js2 + "$('li#f1').trigger('click');";
        }

        string JS2(string texto)
        {
            string js = "";
            //string valor = "<a href='#' class='select2-search-choice-close' tabindex='-1'></a>";
            //valor = "href='#' class='select2-search-choice-close' tabindex='-1'";

            js = "var li = document.createElement('li');";
            //js = js + "var children = ul.children.length + 1;";

            js = js + "var a = document.createElement('a');";
            js = js + " a=textContent = '<div>" + texto + "</div>'";
            js = js + " a.setAttribute('href', 'href='#' class='select2-search-choice-close' tabindex='-1'); ";
            js = js + " new_li.appendChild(a);";
            js = js + "ul.appendChild(new_li);";

            //js = js + "a.href='href='#' class='select2-search-choice-close' tabindex='-1';";
            //js = js + "a.target='_blank';";
            //js = js + "a.innerText='<div>" + texto + "</div>';";

            //js = js + "li.appendChild(a);";

            //js = js + "li.setAttribute('id', " + valor + ");";
            //js = js + "li.appendChild(document.createTextNode(<div>" + texto + "</div>));";

            //js = js + "ul.appendChild(li);";
            return js;
        }

        void aaa()
        {
            //string js = "ul = document.getElementsByClassName('pagination'); ";
            //js = js + " li = ul[0].getElementsByTagName('li');    ";
            //js = js + " var rect = li[3].getBoundingClientRect(); ";

            //js = js + " var x = rect.left; ";
            //js = js + " var y = rect.top;  ";
            //js = js + " x = 1051; y = 399;";

            //js = js + " var ev = document.createEvent('MouseEvents');";
            //js = js + " ev.initMouseEvent('click', true, true, window, 0, 0, 0, x, y, false, false, false, false, 0, null); ";
            //js = js + " document.body.dispatchEvent(ev); ";

        }
    }
}
