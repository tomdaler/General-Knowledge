using System;
using System.Data;

namespace Planilla.Planillas
{
    class FxCalculo
    {
        public void Calcular(string Gcodi)
        {
            Funciones fx = new Funciones();

            double porc_sind = System.Convert.ToDouble(fx.GetDataValue("Select porc from sindicato where id = 2"));

            fx.UpdateData("update pla_detalle set bruto=0, neto=0 where cod_pla=" + Gcodi);

            string sql = "select * from pla_detalle where activo = True and cod_pla=" + Gcodi;
            DataTable dtDetalle = fx.GetData(sql, "");

            DataTable dtGob = fx.GetData("Select * from gobierno", "");
            double afp1 = System.Convert.ToDouble(dtGob.Rows[0][0].ToString());
            double afp2 = System.Convert.ToDouble(dtGob.Rows[0][1].ToString());

            double minimo = System.Convert.ToDouble(dtGob.Rows[0][3].ToString());
            double tasa1 = System.Convert.ToDouble(dtGob.Rows[0][2].ToString());

            DataTable dtAjuste = fx.GetData("Select * from ajuste_renta where aplicado=False ", "");

            sql = "Select cod_empl, Sum(valor*cantidad) from pla_partidas ";
            sql = sql + " where cod_pla = " + Gcodi;
            sql = sql + " group by cod_empl";

            DataTable dtPartidas = fx.GetData(sql, "");

            string periodo = fx.GetDataValue("Select periodo from proyecto a, planillas b where a.cod_proy = b.cod_proy and cod_pla=" + Gcodi);

            string PERIODO = "1";
            if (periodo == "Catorcenal") PERIODO = "2";
            if (periodo == "Quincenal") PERIODO = "3";
            if (periodo == "Mensual") PERIODO = "4";


            DataTable dtRenta1 = fx.GetData("Select * from renta where cod_renta=" + PERIODO, "");
            DataTable dtRenta_det1 = fx.GetData("Select * from renta_detalle where cod_renta = " + PERIODO + " order by minimo", "");

            double LimAFP1 = System.Convert.ToDouble(dtRenta1.Rows[0]["LimAFP"]);
            double Hasta1 = System.Convert.ToDouble(dtRenta1.Rows[0]["ISSS_Hasta"]);
            double ISSS_porc1 = System.Convert.ToDouble(dtRenta1.Rows[0]["ISSS_porc"]);
            double ISSS_fijo1 = System.Convert.ToDouble(dtRenta1.Rows[0]["ISSS_fijo"]);

            // MENSUAL PARA EVENTUAL, VAC, LIQ, AGUI
            DataTable dtRenta2 = fx.GetData("Select * from renta where cod_renta=4", "");
            DataTable dtRenta_det2 = fx.GetData("Select * from renta_detalle where cod_renta = 4 order by minimo", "");

            double LimAFP2 = System.Convert.ToDouble(dtRenta2.Rows[0]["LimAFP"]);
            double Hasta2 = System.Convert.ToDouble(dtRenta2.Rows[0]["ISSS_Hasta"]);
            double ISSS_porc2 = System.Convert.ToDouble(dtRenta2.Rows[0]["ISSS_porc"]);
            double ISSS_fijo2 = System.Convert.ToDouble(dtRenta2.Rows[0]["ISSS_fijo"]);

            //=================

            foreach (System.Data.DataRow dr in dtDetalle.Rows)
            {
                double tasa = 0;
                double sumar = 0;
                double restar = 0;

                double MonRenta = 0;
                double MonISSS = 0;
                double MonAFP1 = 0;
                double MonAFP2 = 0;
                double bruto = 0;

                string empl = dr["cod_empl"].ToString();

                string bruto2 = dr["Bruto"].ToString();
                if (bruto2 != "") bruto = System.Convert.ToDouble(bruto2);
                string tipo = dr["tipo_pago"].ToString();

                double bruto1 = 0;

                // POR TIEMPO
                if (tipo == "T")
                {
                    bruto1 = System.Convert.ToDouble(dr["Horas"]) * System.Convert.ToDouble(dr["ValorHora"]);
                    bruto = bruto1;
                }

                // POR PARTIDA
                if (tipo == "P")
                {
                    string where = "cod_empl=" + empl;
                    DataRow[] result = dtPartidas.Select(where);
                    if (result.Length > 0) bruto1 = System.Convert.ToDouble(result[0][1].ToString());
                    bruto = bruto1;
                }


                double bono = System.Convert.ToDouble(dr["bono"].ToString());
                double adel = System.Convert.ToDouble(dr["adelanto"].ToString());
                double descu = System.Convert.ToDouble(dr["Descuento"].ToString());
                int cod_sind = System.Convert.ToInt32(dr["Cod_sind"].ToString());

                //===================
                bruto = bruto + bono;
                //===================

                DataTable dtRenta_Det = null;

                double LimAFP = LimAFP1;
                double Hasta = Hasta1;
                double ISSS_porc = ISSS_porc1;
                double ISSS_fijo = ISSS_fijo1;

                if (bruto > 0)
                {
                    dtRenta_Det = dtRenta_det1;

                    // EVENTUAL, VACACIONES, LIQUIDACION, AQUINALDO   USE TABLA MENSUAL
                    if (tipo == "E" || tipo == "C" || tipo == "L" || tipo == "A")
                    {
                        LimAFP = LimAFP2;
                        Hasta = Hasta2;
                        ISSS_porc = ISSS_porc2;
                        ISSS_fijo = ISSS_fijo2;

                        dtRenta_Det = dtRenta_det2;
                    }
                }

                // AFP Y ISSS
                //===========

                // ISSS
                if (dr["Aplica_isss"].ToString() == "True")
                {
                    if (bruto > Hasta)
                        MonISSS = ISSS_fijo;
                    else
                        MonISSS = bruto * ISSS_porc / 100;
                }

                // AFP'S
                if (dr["Aplica_isss"].ToString() == "True")
                {
                    double AFPS = bruto;
                    if (AFPS > LimAFP) AFPS = LimAFP;
                    MonAFP1 = AFPS * afp1 / 100;
                    MonAFP2 = AFPS * afp2 / 100;
                }

                bruto = bruto - MonAFP1 - MonISSS;
                //================================

                // RENTA
                //=======

                if (tipo != "E" && dr["Aplica_Renta"].ToString() == "True" && bruto > 0)
                {
                    foreach (DataRow dr2 in dtRenta_Det.Rows)
                    {
                        double min = System.Convert.ToDouble(dr2["Minimo"]);
                        double max = System.Convert.ToDouble(dr2["Maximo"]);

                        if (bruto < min) break;

                        if (bruto > min && bruto <= max)
                        {
                            tasa = System.Convert.ToDouble(dr2["tasa"]);
                            restar = System.Convert.ToDouble(dr2["restar"]);
                            sumar = System.Convert.ToDouble(dr2["sumar"]);

                            break;
                        }
                    }
                }

                // RENTA

                // REVISE ajuste_renta con status aplicado=0 
                // CUANDO SE TERMINE RENTA SE ACTULIZARA STATUS EN ajuste_renta
                string ajuste = "0";

                if (dtAjuste.Rows.Count > 0)
                {
                    string where = "cod_empl=" + empl;
                    DataRow[] result = dtAjuste.Select(where);
                    if (result.Length > 0) ajuste = result[0][3].ToString();
                }

                if (tipo == "E") tasa = tasa1;
                MonRenta = (bruto - restar) * tasa / 100 + sumar + System.Convert.ToDouble(ajuste);


                // SINDICATO
                double MonSind = bruto / 100 * porc_sind * cod_sind;
                double Neto = bruto - MonSind - adel - descu;

                sql = "update pla_detalle ";
                sql = sql + " set  MonRenta =" + MonRenta.ToString("0.##");
                sql = sql + ",  Neto =" + Neto.ToString("0.##");

                if (bruto1 > 0)
                    sql = sql + ",  Bruto =" + bruto1.ToString("0.##");

                sql = sql + ",  MonISSS =" + MonRenta.ToString("0.##");
                sql = sql + ",  MonAFPEmpl =" + MonAFP1.ToString("0.##");
                sql = sql + ",  MonAFPPatrono =" + MonAFP2.ToString("0.##");
                sql = sql + ",  Cuota_sind =" + MonSind.ToString("0.##");

                sql = sql + " where cod_empl=" + empl;
                sql = sql + " and cod_pla=" + Gcodi;
                fx.UpdateData(sql);
            }
        }

        public void Reajuste()
        {
            Funciones fx = new Funciones();
            fx.UpdateData("delete from Ajuste_Renta");

            int year = DateTime.Now.Year;
            int mes = DateTime.Now.Month;

            string fec1 = fx.GetDate(1, 1, year);

            if (mes > 6) fec1 = fx.GetDate(1, 7, year);

            string sql = "select cod_empl, sum(neto) from pla_detalle ";
            sql = sql + " where cod_empl in (select cod_empl from Empleado where activo = True and tipo_pago <>'E') ";
            sql = sql + " and cod_pla in (Select cod_pla from planillas where fec2 >= " + fec1;
            sql = sql + " and tipo_pago<> 'E'";

            sql = sql + " group by cod_empl ";

            DataTable dt = fx.GetData(sql, "");

            if (mes < 7)
                sql = "select * from renta_detalle_junio where cod_periodo=4 order by minimo desc";
            else
                sql = "select * from renta_detalle_diciembre where cod_periodo=4 order by minimo desc";

            DataTable dtRenta = fx.GetData(sql, "");

            foreach (DataRow dr in dt.Rows)
            {
                double tasa = 0;
                double sumar = 0;
                double restar = 0;
                double MonRenta = 0;
                double bruto = 0;

                string empl = dr["cod_empl"].ToString();

                string bruto2 = dr[1].ToString();
                if (bruto2 != "") bruto = System.Convert.ToDouble(bruto2);

                bruto = 0;
                //===========

                foreach (DataRow dr2 in dtRenta.Rows)
                {
                    double min = Convert.ToDouble(dr2["Minimo"]);
                    double max = Convert.ToDouble(dr2["Maximo"]);

                    if (bruto < min) break;

                    if (bruto > min && bruto <= max)
                    {
                        tasa = Convert.ToDouble(dr2["tasa"]);
                        restar = Convert.ToDouble(dr2["restar"]);
                        sumar = Convert.ToDouble(dr2["sumar"]);

                        break;
                    }
                }

                MonRenta = (bruto - restar) * tasa / 100 + sumar;

                if (MonRenta > 0)
                {
                    sql = " insert into Ajuste_renta (cod_empl, valor, renta ) values ";
                    sql = sql + "(" + empl + "," + bruto.ToString() + "," + MonRenta.ToString() + ")";
                    fx.UpdateData(sql);
                }
            }
        }

    }
}
