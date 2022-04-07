using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {

        string source = "Teams.xml";
        string source2 = "Credentials.xml";

        Boolean Timeout = false;

        string uri_accounts = "https://admin.instantservice.com/isadmin/csr.CreateCsr";
        string uri_team = "https://admin.instantservice.com/isadmin/csr.AgentTeams";
        string uri_login = "https://login.instantservice.com/";
        string uri_delete = "https://admin.instantservice.com/isadmin/csr.DeleteCsr?ec=ID&action=delete&tk=TOKEN";
        string uri_logout = "https://admin.instantservice.com/isadmin/csr.Logout";

        //string post_data = @"ai=8029&ci=220253534&di=-1&tk=BBE65AEE89F2769EA3D98B1813B653DC&nwfn=nom4&nwln=apell4&nwal=nom+ape&nwem=tomas.dale@steam.com&nwti=&assigndepts=1&nwun=ddddddd&nwpd=stream%231&confirmnwpd=stream%231&fp=on&nwcd1=&nwcd2=&nwcd3=&nwcd4=&nwcd5=&IMAGE.x=29&IMAGE.y=6";

        // REQUEST
        HttpWebRequest requestLogin = (HttpWebRequest)WebRequest.Create("https://login.instantservice.com/Login");
        string GCOOKIE;
        string GTOKEN;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cb.Items.Add("");

            System.Xml.Linq.XElement _empXml = System.Xml.Linq.XElement.Load(source);

            var Query1 = from emp in _empXml.Descendants("Team")
                         group emp by emp.Element("Name").Value
                             into empGroup
                             select empGroup.First().Element("Name").Value;

            cb.DataSource = Query1.ToList();
     



            XmlTextReader xtr = null; 
            string fileName = source2;
            xtr = new XmlTextReader(fileName);
            xtr.WhitespaceHandling = WhitespaceHandling.None;
               
            xtr.Read();
            string userid = "";

            while (xtr.Read())
                {
                    string ss = xtr.Value;
                    if (ss.Length > 3)
                    {
                        if (userid == "")
                        {
                            textBox4.Text = ss;
                                userid = ss;
                        }
                        else
                            textBox5.Text = ss;
                     }
                 }


                            
        }


        #region CREATE BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            Generate(1);
        }

        private void Generate(int opcion)
        {
            progressBar1.Value = 0;

            string UserID;
            string site;
            string correo;
            string Nombre;
            string Apellido;
            int i = 1;
            
            if (opcion ==1) LOGIN();

            DataTable dt = ReadFile();
            foreach (DataRow dr in dt.Rows)
            {
                UserID = dr[0].ToString();

                correo = dr[1].ToString();
                site = dr[2].ToString();
                Nombre = dr[3].ToString();
                Apellido = dr[4].ToString();

                label6.Text = UserID;
                
                Insertar(correo, site, Nombre, Apellido, UserID, opcion);
                progressBar1.Value = i++;
            }
            if (opcion==1) LOGOUT();
            MessageBox.Show("Finish");

        }
        #region INSERTAR NUEVOS
        private void Insertar(string correo, string site, string Nombre, string Apellido, string UserID, int opcion)
        {
            string URI_A_USAR = uri_accounts;
            site = site.Trim();
            if (site != "") site = site + " - ";

            if (opcion == 2) GTOKEN = textBox1.Text.Trim();
            if (opcion == 2) GCOOKIE = textBox2.Text.Trim();

            string post_data = "ai=8029&ci=220253534&di=-1&tk=" + GTOKEN;
            if (opcion == 2) post_data = "ai=8029&ci=220253534&di=-1&tk=" + textBox1.Text;


            post_data = post_data + "&nwfn=" + site + Nombre.Trim();
            post_data = post_data + "&nwln=" + Apellido.Trim();
            post_data = post_data + "&nwal=" + Nombre.Trim() + " " + Apellido.Trim();
            post_data = post_data + "&nwem=" + correo.Trim();
            post_data = post_data + "&nwti=&assigndepts=0&nwun=" + UserID.Trim();
            post_data = post_data + "&nwpd=stream%231&confirmnwpd=stream%231&fp=on&nwcd1=&nwcd2=&nwcd3=&nwcd4=&nwcd5=&IMAGE.x=29&IMAGE.y=6";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI_A_USAR);

            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version11;
            request.Method = "POST";

            Uri target = new Uri(URI_A_USAR);

            request.CookieContainer = new CookieContainer();
            //request.CookieContainer.Add(new Cookie("instantserviceb", textBox2.Text) { Domain = target.Host });
            //request.CookieContainer.Add(new Cookie("BIGipServerchat_admin", "2342051338.22528.0000") { Domain = target.Host });

            request.CookieContainer.Add(new Cookie("instantserviceb", GCOOKIE) { Domain = target.Host });
            request.CookieContainer.Add(new Cookie("BIGipServerchat_admin", "2342051338.22528.0000") { Domain = target.Host });

            byte[] postBytes = Encoding.ASCII.GetBytes(post_data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            // grab te response and print it out to the console along with the status code
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
            //Console.WriteLine(response.StatusCode);

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);

            using (StreamWriter outfile = new StreamWriter(@"C:\salida.txt"))
            {
                outfile.Write(reader.ToString());
            }

            int i = responseFromServer.IndexOf("Account Admin Session Timeout");
            if (i > 0) MessageBox.Show("Time Out");

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        #endregion
        #endregion

        #region emails
        private void Emails(string correo, string Nombre, string Apellido, string UserId)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mailedc01");
                //SendTo = textBox1.Text; // CAMBIAR
                string From = textBox3.Text;
                mail.From = new MailAddress(From);
                mail.To.Add(correo);
                mail.Subject = "Oracle Chat Access";

                mail.Body = "Hi " + Nombre +" "+Apellido + "\n\nYour credentials to access Oracle chat are\n\nUserID " + UserId + "\nPassword   stream#1 \n\nRegards";
                SmtpServer.Port = 25;
                SmtpServer.Send(mail);
            }
            catch
            {
                MessageBox.Show("Error sending the emails");
            }
        }
        #endregion

        #region Asignar Team
        private void Asignar(string UserID, int opcion)
        {
            string URL_A_USAR = uri_team;

            if (opcion == 2) GTOKEN = textBox1.Text.Trim();
            if (opcion == 2) GCOOKIE = textBox2.Text;

            string post_data = "ai=8029&ci=220253534&ec="+UserID.Trim()+"&action=saveagentteams&tk="+GTOKEN+"&savenonmembers="+Team.Text;
            
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create(URL_A_USAR);
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version11;
            request.Method = "POST";

            Uri target = new Uri(URL_A_USAR);

            // "74D64ED4B527A436453B612DAE5A6F4CC7D7ACE932CD0ABEFC7C3597085CFFB4435E19016B3D79D88962EDEAB4C90971548367F63F9EA790F791144C5D18531B2167FC5945C873BAC8A1DDFE334771F90628605B54B54AF124BD528CDAC74B3D889610DE05F523EAE3FF90B11BBD953B"
            request.CookieContainer = new CookieContainer();



            request.CookieContainer.Add(new Cookie("instantserviceb", GCOOKIE) { Domain = target.Host });
            request.CookieContainer.Add(new Cookie("BIGipServerchat_admin", "2711150090.22528.0000") { Domain = target.Host });
            request.CookieContainer.Add(new Cookie("rlpcookie_instantserviceb", "deleted") { Domain = target.Host });
   
            byte[] postBytes = Encoding.ASCII.GetBytes(post_data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            // grab te response and print it out to the console along with the status code
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
            //Console.WriteLine(response.StatusCode);


            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);

            int i = responseFromServer.IndexOf("Account Admin Session Timeout");
            if (i > 0)
            {
                MessageBox.Show("Time Out");
                Timeout = true;
            }


            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        #endregion

        #region Eliminar
        private void Eliminar(string UserID)
        {
            string URL_A_USAR = uri_delete;
            URL_A_USAR = URL_A_USAR.Replace("ID", UserID);
            URL_A_USAR = URL_A_USAR.Replace("TOKEN", textBox1.Text.Trim());
            
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create(URL_A_USAR);
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version11;
            request.Method = "POST";

            Uri target = new Uri(URL_A_USAR);

            // "74D64ED4B527A436453B612DAE5A6F4CC7D7ACE932CD0ABEFC7C3597085CFFB4435E19016B3D79D88962EDEAB4C90971548367F63F9EA790F791144C5D18531B2167FC5945C873BAC8A1DDFE334771F90628605B54B54AF124BD528CDAC74B3D889610DE05F523EAE3FF90B11BBD953B"
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("instantserviceb", textBox2.Text) { Domain = target.Host });
            request.CookieContainer.Add(new Cookie("chat_cookie", "2493046282.22528.0000") { Domain = target.Host });

            byte[] postBytes = Encoding.ASCII.GetBytes("");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            // grab te response and print it out to the console along with the status code
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
            //Console.WriteLine(response.StatusCode);


            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);

            int i = responseFromServer.IndexOf("Account Admin Session Timeout");
            if (i > 0)
            {
                MessageBox.Show("Time Out");
                Timeout = true;
            }


            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        #endregion


        #region login
        private void button2_Click(object sender, EventArgs e)
        {
            LOGIN();
        }

        private void LOGIN()
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            string usuario = textBox4.Text;
            usuario = usuario.Replace("@", "%40");

            //string postData = "brand=instantservice&ai=8029&un=90980%40stream.com&pd="+textBox5.Text;
            string postData = "brand=instantservice&ai=8029&un="+usuario+"&pd="+textBox5.Text;
            
            byte[] postDataBytes = encoding.GetBytes(postData);
            
            requestLogin.Method = "POST";
            requestLogin.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:34.0) Gecko/20100101 Firefox/34.0";
            requestLogin.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            requestLogin.Headers.Add("Accept-Language: en-US,en;q=0.5");
            requestLogin.Headers.Add("Accept-Encoding: gzip, deflate");

            requestLogin.Referer = uri_login;
            
            // KEEP ALIVE UNTIL LOGOUT
            requestLogin.KeepAlive = true;
            requestLogin.ContentType = "application/x-www-form-urlencoded";
            requestLogin.ContentLength = postDataBytes.Length;
            requestLogin.AllowAutoRedirect = false;
            requestLogin.ServicePoint.Expect100Continue = false;

            using (var stream = requestLogin.GetRequestStream())
            {
                stream.Write(postDataBytes, 0, postDataBytes.Length);
                stream.Close();
            }

            var cookieContainer = new CookieContainer();

            using (var response = (HttpWebResponse)requestLogin.GetResponse())
            {
                var code = response.StatusCode;

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var setcookie = response.Headers.GetValues("Set-Cookie").FirstOrDefault();
                    var cookie = setcookie.Split(';')[0];
                    GCOOKIE = cookie.ToString().Trim();
                    GCOOKIE = GCOOKIE.Replace("instantserviceb=","");

                    var location = response.Headers.GetValues("Location").FirstOrDefault();
                    var token = location.Split('=')[1];
                    GTOKEN = token.ToString().Trim();
                }
            }


        }
        #endregion

        #region Readfile
        private DataTable ReadFile()
        {
            string sql2 = "Select * FROM [OracleChatCreate.csv]";
            string strPath = "c:\\";
            string Cn1 = "Driver={Microsoft Text Driver (*.txt; *.csv)}; DefaultDir=" + strPath;

            System.Data.Odbc.OdbcDataAdapter da = new OdbcDataAdapter(sql2, Cn1);
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds, "Files");
            }
            catch (Exception ss)
            {
                MessageBox.Show(ss.ToString());
            }

            dataGridView1.Visible = true;

            return ds.Tables[0];
        }
        #endregion


        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;

            string UserID;
            string site;
            string correo;
            string Nombre;
            string Apellido;
            int i = 1;

            DataTable dt = ReadFile();
            foreach (DataRow dr in dt.Rows)
            {
                UserID = dr[0].ToString();

                correo = dr[1].ToString();
                site = dr[2].ToString();
                Nombre = dr[3].ToString();
                Apellido = dr[4].ToString();

               Emails(correo, Nombre, Apellido, UserID);
               progressBar1.Value = i++;
            }
            MessageBox.Show("Finish");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ReadFile();
            panel1.Visible = true;

            progressBar1.Visible = true;
            progressBar1.Maximum = System.Convert.ToInt16(dataGridView1.Rows.Count.ToString());
            progressBar1.Value = 0;
            button5.Visible = true;
            button1.Visible = true;
            cb.Visible = true;

            label5.Visible = true;
            Team.Visible = true;
            progressBar1.Visible = true;

            panel3.Visible = true;
            panel2.Visible = true;
        }

       

        private void button5_Click(object sender, EventArgs e)
        {
            GenerateRoles(1);
        }

        private void GenerateRoles(int opcion)
        {
            progressBar1.Value = 0;

            if (Team.Text.Trim() == "")
            {
                MessageBox.Show("Set a team");
                Team.Focus();
                return;
            }

            string UserID;
            int i = 1;

            if (opcion ==1) LOGIN();
            DataTable dt = ReadFile();
            foreach (DataRow dr in dt.Rows)
            {
                UserID = dr[5].ToString();
                label6.Text = UserID;
                Asignar(UserID, opcion);
                if (Timeout) return;
                
                progressBar1.Value = i++;

            }
            if (opcion ==1) LOGOUT();
            MessageBox.Show("Finish");
        }

        #region TEAMS BUTTONS
        private void button13_Click(object sender, EventArgs e)
        {
            //savenonmembers=
            Team.Text = "73605%2C73569%2C73978%2C74408%2C73862%2C73578%2C73572%2C73576%2C73573%2C74225%2C73506%2C73924%2C74259%2C73105%2C74218%2C73575%2C&savemembers=74409%2C";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Team.Text = "73569%2C73570%2C73651%2C73578%2C73572%2C73576%2C73573%2C73506%2C73105%2C73575%2C&savemembers=73605%2C";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Team.Text = "73605%2C73570%2C73651%2C73578%2C73572%2C73576%2C73573%2C73506%2C73105%2C73575%2C&savemembers=73569%2C";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Team.Text = "73605%2C73569%2C73570%2C73651%2C73578%2C73572%2C73576%2C73573%2C73506%2C73105%2C&savemembers=73575%2C";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Team.Text = "73862%2C73605%2C73569%2C73978%2C73651%2C73578%2C73572%2C73573%2C73506%2C73924%2C73105%2C73575%2C&savemembers=73576%2C";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Team.Text = "73862%2C73605%2C73569%2C73978%2C73651%2C73578%2C73572%2C73576%2C73506%2C73924%2C73105%2C73575%2C&savemembers=73573%2C";
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Team.Text = "73605%2C73569%2C73978%2C73651%2C73862%2C73578%2C73576%2C73573%2C74225%2C73506%2C73924%2C74259%2C73105%2C74218%2C73575%2C&savemembers=73572%2C";
        }
#endregion


        private void button12_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;

            string UserID;
            int i = 1;

            DataTable dt = ReadFile();
            foreach (DataRow dr in dt.Rows)
            {
                UserID = dr[5].ToString();
                label6.Text = UserID;
                Eliminar(UserID);
                if (Timeout) return;

                progressBar1.Value = i++;

            }
            MessageBox.Show("Finish");
        }

   
        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string escoge = cb.Text;
            if (escoge == "") return;

            System.Xml.Linq.XElement _empXml = XElement.Load(source);

            var Query1 = from sitios in _empXml.Descendants("Team")
                         where sitios.Element("Name").Value == escoge
                         select sitios;

            foreach (XElement el in Query1)
            {
                string opcion = el.Element("info").Value.ToString();
                opcion = opcion.Replace("-", "&");
                Team.Text = opcion;
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {

            LOGOUT();
        }
        private void LOGOUT()
        {
            string URL_A_USAR = uri_logout;
                                             
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(URL_A_USAR);
            request2.KeepAlive = false;
            request2.ProtocolVersion = HttpVersion.Version11;
            request2.Method = "POST";

            Uri target = new Uri(URL_A_USAR);
            
            request2.CookieContainer = new CookieContainer();
            request2.CookieContainer.Add(new Cookie("instantserviceb", GCOOKIE) { Domain = target.Host });
            //request.CookieContainer.Add(new Cookie("chat_cookie", "2509823498.22528.0000") { Domain = target.Host });

            string post_data = "";
            byte[] postBytes = Encoding.ASCII.GetBytes(post_data);
            request2.ContentType = "application/x-www-form-urlencoded";
            request2.ContentLength = postBytes.Length;
            Stream requestStream = request2.GetRequestStream();

            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            // grab te response and print it out to the console along with the status code
            HttpWebResponse response = (HttpWebResponse)request2.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);

            int i = responseFromServer.IndexOf("Account Admin Session Timeout");
            if (i > 0)
            {
                MessageBox.Show("Time Out");
                Timeout = true;
            }


            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            Generate(2);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
