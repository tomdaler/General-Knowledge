using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UpdateDatabase
{
        public class NewEmployee
        {
            public string Location;
        }

        public class ContactDate
        {
            public object Shallowcopy()
            {
                return this.MemberwiseClone();
            }

            private string SetDate(string date)
            {
                if (date == null) return "";
                if (date.Trim() == "") return "";

                date = date.Replace("'", "");
                if (date.Length > 10) date = date.Substring(0, 10);
                date = date.Trim();
                int pos = date.IndexOf(" ");
                if (pos > 0) date = date.Substring(0, pos);
                date = date.Trim();

                return date;
            }

            string SetString(string valor, int limite)
            {
                if (valor == null) return "";
                if (valor.Length > limite) valor = valor.Substring(0, limite);
                valor = valor.Replace("'", "''");
                valor = valor.Trim();
                return valor;
            }

            public string Email;

            private string datecreated = "";
            public string DateCreated
            {
                get
                {
                    return datecreated.Trim();
                }
                set
                {
                    datecreated = value;
                    // datecreated = SetDate(datecreated);
                }
            }

            private string initialRemarks = "";
            public string InitialRemarks
            {
                get
                {

                    return initialRemarks.Trim();
                }
                set
                {
                    initialRemarks = value;
                    if (initialRemarks.Length > 195) initialRemarks = initialRemarks.Substring(0, 195);
                }
            }

            private string poc = "";
            public string POC
            {
                get { return poc.Trim(); }
                set { poc = SetString(value, 100); }
            }

            private string reportsource = "";
            public string ReportSource
            {
                get { return reportsource.Trim(); }
                set
                {
                    reportsource = SetString(value, 100);
                    if (reportsource.ToUpper() == "OPS") reportsource = "Operations";
                    reportsource = reportsource.Replace("'", "");
                }
            }

            private string reptype = "";
            public string RepType
            {
                get { return reptype.Trim(); }
                set
                {
                    reptype = value;
                    reptype = reptype.ToUpper();
                    reptype = reptype.Replace("'", "");
                    if (reptype.Length > 50) reptype = reptype.Substring(0, 50);
                    //if (RepType == "") RepType = ".";
                }
            }

            private string tracing = "";
            public string Tracing
            {
                get { return tracing.Trim(); }
                set
                {
                    tracing = SetString(value, 50);
                }
            }


            private string forcttracing = "";
            public string ForCTTracing
            {
                get { return forcttracing.Trim(); }
                set { forcttracing = SetString(value, 50); }
            }

            private string closecontact = "";
            public string CloseContact
            {
                get { return closecontact.Trim(); }
                set
                {
                    closecontact = SetString(value, 50);
                }
            }

            private string testtype = "";
            public string TestType
            {
                get { return testtype.Trim(); }
                set
                {
                    testtype = SetString(value, 100);
                    testtype = testtype.Replace("'", "");
                }
            }

            private string pcr = "";
            public string PCR
            {
                get { return pcr.Trim(); }
                set
                {
                    pcr = SetString(value, 100);
                }
            }

            private string empid = "";
            public string EmpID
            {
                get { return empid.Trim(); }
                set
                {
                    empid = value;

                    if (!(empid == "NA" || empid == "N/A"))
                    {
                        empid = Regex.Replace(empid, @"[^\d-+()]", "");
                    }

                    if (empid.Length > 15) empid = empid.Substring(0, 15);
                    empid = empid.Replace("-", "");
                }
            }

            private string firstname = "";
            public string FirstName
            {
                get { return firstname.Trim(); }
                set { firstname = SetString(value, 100); }
            }

            private string middlename = "";
            public string MiddleName
            {
                get { return middlename.Trim(); }
                set
                {
                    middlename = value;
                    if (middlename == null) middlename = "";
                    middlename = SetString(middlename, 100);
                }

            }

            private string lastname = "";
            public string LastName
            {
                get { return lastname.Trim(); }
                set { lastname = SetString(value, 100); }
            }

            private string empprogram = "";
            public string EmpProgram
            {
                get { return empprogram.Trim(); }
                set { empprogram = SetString(value, 200); }
            }

            private string fourpillars = "";
            public string FourPillars
            {
                get { return fourpillars.Trim(); }
                set { fourpillars = SetString(value, 100); }

            }

            private string location = "";
            public string Location
            {
                get { return location.ToString(); }
                set { location = SetString(value, 100); }

            }

            private string residence = "";
            public string Residence
            {
                get { return residence; }
                set { residence = SetString(value, 100); }

            }

            private string mobile = "";
            public string Mobile
            {
                get { return mobile; }
                set
                {
                    mobile = value;
                    mobile = Regex.Replace(mobile, @"[^\d-+()]", "");
                    if (mobile.Length > 25) mobile = mobile.Substring(0, 25);
                    mobile = mobile.Trim();
                }
            }

            private string landline = "";
            public string Landline
            {
                get { return landline; }
                set
                {
                    landline = value;
                    landline = Regex.Replace(landline, @"[^\d-+()]", "");
                    if (landline.Length > 25) landline = landline.Substring(0, 25);
                    landline = landline.Trim();
                }
            }

            private string besttime = "";
            public string BestTime
            {
                get { return besttime.Trim(); }
                set
                {
                    besttime = value;
                    if (besttime.Length > 20) besttime = besttime.Substring(0, 20);
                }
            }

            private string lastbadge = "";
            public string LastBadge
            {
                get { return lastbadge.Trim(); }
                set
                {
                    lastbadge = SetDate(value);
                }
            }

            private string transport = "";
            public string Transport
            {
                get { return transport.Trim(); }
                set
                {
                    transport = SetString(value, 20);

                }

            }

            private string totPCR = "";
            public string TotPCR
            {
                get { return totPCR.Trim(); }
                set { totPCR = SetString(value, 100); }

            }

            private string dttest = "";
            public string dtTest
            {
                get { return dttest.Trim(); }
                set
                {
                    dttest = SetDate(value);
                }
            }

            private string dtrelease = "";
            public string dtRelease
            {
                get { return dtrelease.Trim(); }
                set
                {
                    dtrelease = value;
                    dtrelease = SetDate(dtrelease);
                }
            }

            private string faculty = "";
            public string Faculty
            {
                get { return faculty.Trim(); }
                set { faculty = SetString(value, 100); }

            }

            private string contactTracing = ""; // remarks
            public string ContactTracing
            {
                get { return contactTracing.Trim(); }
                set { contactTracing = SetString(value, 100); }
            }

            private string dtcontactconfirm = ""; // dtContact
            public string dtContactConfirm
            {
                get { return dtcontactconfirm.Trim(); }
                set
                {
                    dtcontactconfirm = value;
                    dtcontactconfirm = SetDate(dtcontactconfirm);
                }
            }

            private string wd_ID = "";
            public string WD_ID
            {
                get { return wd_ID.Trim(); }
                set { wd_ID = SetString(value, 100); }

            }

            private string empNameCNX = "";
            public string EmpNameCNX
            {
                get { return empNameCNX.Trim(); }
                set { empNameCNX = SetString(value, 150); }

            }

            private string whereAbouts = ""; // latests
            public string WhereAbouts
            {
                get { return whereAbouts.Trim(); }
                set { whereAbouts = SetString(value, 200); }
            }

            private string currFaculty = "";
            public string CurrFaculty
            {
                get { return currFaculty.Trim(); }
                set { currFaculty = SetString(value, 100); }

            }

            private string dtconfinement = "";
            public string dtConfinement
            {
                get { return dtconfinement.Trim(); }
                set
                {
                    dtconfinement = value;
                    dtconfinement = SetDate(dtconfinement);
                }
            }

            private string icu = "";
            public string ICU
            {
                get { return icu.Trim(); }
                set
                {
                    icu = value;
                    if (icu.IndexOf("App") > 1) icu = "N/A";
                    if (icu.Length > 5) icu = icu.Substring(0, 5);
                }
            }

            private string dtfirstsymptoms = "";
            public string dtFirstSymptoms
            {
                get { return dtfirstsymptoms.Trim(); }
                set
                {
                    dtfirstsymptoms = value;
                    dtfirstsymptoms = SetDate(dtfirstsymptoms);
                }
            }


            // MoreDetails
            private string symptoms = "";
            public string Symptoms
            {
                get { return symptoms.Trim(); }
                set { symptoms = SetString(value, 3500); }

            }

            private string category = "";
            public string Category
            {
                get { return category.Trim(); }
                set { category = SetString(value, 100); }
            }

            private string doh = "";
            public string DOH
            {
                get { return doh.Trim(); }
                set { doh = SetString(value, 200); }
            }

            private string severity = "";
            public string Severity
            {
                get { return severity.Trim(); }
                set { severity = SetString(value, 20); }

            }

            private string bhert = "";
            public string BHERT
            {
                get { return bhert.Trim(); }
                set
                {
                    bhert = value;
                    if (bhert.IndexOf("App") > 1) bhert = "N/A";
                    if (bhert.Length > 20) bhert = bhert.Substring(0, 20);
                }
            }

            private string recommendation = "";
            public string Recommendation
            {
                get { return recommendation.Trim(); }
                set { recommendation = SetString(value, 200); }
            }

            private string quarantinestartdate = "";
            public string QuarantineStartDate
            {
                get { return quarantinestartdate.Trim(); }
                set
                {
                    quarantinestartdate = value;
                    quarantinestartdate = SetDate(quarantinestartdate);
                }
            }

            private string quarantineenddate = "";
            public string QuarantineEndDate
            {
                get { return quarantineenddate.Trim(); }
                set
                {
                    quarantineenddate = value;
                    quarantineenddate = SetDate(quarantineenddate);
                }
            }

            private string dtextend = "";
            public string dtExtEnd
            {
                get { return dtextend.Trim(); }
                set
                {
                    dtextend = value;
                    dtextend = SetDate(dtextend);
                }
            }

            private string dtftw = "";
            public string dtFTW
            {
                get { return dtftw.Trim(); }
                set
                {
                    dtftw = value;
                    dtftw = SetDate(dtftw);
                }
            }

            private string dtreturn = "";
            public string dtReturn
            {
                get { return dtreturn.Trim(); }
                set
                {
                    dtreturn = value;
                    dtreturn = SetDate(dtreturn);
                }
            }

            private string remarks = "";
            public string Remarks
            {
                get { return remarks.Trim(); }
                set
                {
                    remarks = SetString(value, 3000);
                    remarks = remarks.Replace("Required information", "");
                    remarks = remarks.Replace("Required Information", "");
                    remarks = remarks.Replace("Require information", "");
                    remarks = remarks.Replace("Require Information", "");
                }
            }

            private string address = "";
            public string Address
            {
                get { return address.Trim(); }
                set { address = SetString(value, 300); }
            }

            private string empName = "";
            public string EmpName
            {
                get { return empName.Trim(); }
                set
                {
                    if (value == null)
                        empName = "";

                    if (empName.Length > 100)
                        empName = FirstName + " " + LastName;

                    empName = SetString(value, 100);
                }
            }

            private string msa = "";
            public string MSA
            {
                get { return msa.Trim(); }
                set { msa = SetString(value, 100); }
            }

            private string dtCTA1Start = "";
            public string DtCTA1Start
            {
                get
                {
                    return dtCTA1Start.Trim();
                }
                set
                {
                    dtCTA1Start = value;
                    dtCTA1Start = SetDate(dtCTA1Start);
                }
            }

            private string dtCTA1End = "";
            public string DtCTA1End
            {
                get
                {
                    return dtCTA1End.Trim();
                }
                set
                {
                    dtCTA1End = value;
                    dtCTA1End = SetDate(dtCTA1End);
                }
            }

        private string dtmodified = "";
        public string dtModified
        {
            get
            {
                return dtmodified.Trim();
            }
            set
            {
                dtmodified = value;
                dtmodified = SetDate(dtmodified);
            }
        }

        public string Status = "";

            public string sender = "";


            public string A1SQNewEndDate = "";


            private string dtA1Assessment1 = "";
            public string dtA1Assessment
            {
                get
                {
                    return dtA1Assessment1;
                }
                set
                {
                    dtA1Assessment1 = value.ToString();

                }
            }

            private string age = "";
            public string Age
            {
                get
                {
                    return age;
                }
                set
                {
                    age = value.ToString();

                }
            }

            private string gender = "";
            public string Gender
            {
                get
                {
                    return gender;
                }
                set
                {
                    gender = value.ToString();

                }
            }

            private string id = "";
            public string ID
            {
                get { return id; }
                set { id = value.ToString(); }
            }

            private string wk_address1 = "";
            public string WK_Address1
            {
                get
                {
                    return wk_address1;
                }
                set
                {
                    wk_address1 = value.ToString();
                    wk_address1 = wk_address1.Replace("'", "''");
                    wk_address1 = wk_address1.Trim();
                }
            }

            private string wk_address2 = "";
            public string WK_Address2
            {
                get
                {
                    return wk_address2;
                }
                set
                {
                    wk_address2 = value.ToString();
                    wk_address2 = wk_address2.Replace("'", "''");
                    wk_address2 = wk_address2.Trim();
                }
            }

            private string siteCity = "";
            public string SiteCity
            {
                get
                {
                    return siteCity;
                }
                set
                {
                    siteCity = value.ToString();
                    siteCity = siteCity.Replace("'", "''");
                    siteCity = siteCity.Trim();
                }
            }

        }

        public class Duplicate
        {
            public string Symptom;
            public string Remarks;
            public string RepType;
            public string ext1;
            public string ext2;
            public string ext3;
            public int len1;
            public int len2;
            public int len3;
            public string Location;
            public string EmpMobile;
            public string Landline;
        }


    }