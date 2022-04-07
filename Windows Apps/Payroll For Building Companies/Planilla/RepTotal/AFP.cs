﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planilla.RepTotal
{
    public partial class AFP : Form
    {
        public AFP()
        {
            InitializeComponent();
        }

        private void AFP_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select Codigo, Fecha1, Fecha2, AFP, ";
            sql = sql + "Sum(Bruto) as Bruto, Sum(Neto) as Neto, Sum(MonISSS) as ISSS, Sum(MonAFPEmpl) as AFP_E, Sum(MonAFPPatrono) as AFP_P, Sum(MonRenta) as Renta, Sum(cuota_sind) as Sindicato ";

            sql = sql + " FROM  AFP , planillas p, pla_detalle d ";
            sql = sql + " where p.cod_pla = d.cod_pla ";
            sql = sql + " and d.Cod_afp = AFP.cod_afp ";
            //  sql = sql + " and AFP.Activo = 1";

            sql = sql + Variables.DESDE + rangoCtl1.Fec1();
            sql = sql + Variables.HASTA + rangoCtl1.Fec2();

            sql = sql + " group by Codigo, Fecha1, Fecha2, AFP ";
            sql = sql + " order by AFP, Fecha1 desc ";

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");
            if (dt.Rows.Count > 0)
            {
                dgObs.DataSource = dt;
                dgObs.Visible = true;
                dgObs.Columns[1].Width = 130;

                DataGridViewCellStyle estilo = new DataGridViewCellStyle();
                estilo.Format = "C";
                estilo.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //estilo.ForeColor = Color.Green;


                for (int i = 0; i < 5; i++)
                {
                    dgObs.Columns[i].Width = 90;
                }

                for (int i = 4; i < dgObs.Columns.Count; i++)
                {
                    dgObs.Columns[i].DefaultCellStyle = estilo;
                    dgObs.Columns[i].Width = 90;
                }


            }
            else
                dgObs.Visible = false;

        }
    }
}
