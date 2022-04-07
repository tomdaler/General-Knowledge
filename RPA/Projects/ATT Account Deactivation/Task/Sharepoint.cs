using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Task
{
    class Sharepoint
    {
        public void Update()
        {
            ClientContext context = null;

                    string SiteUrl = ConfigurationManager.AppSettings["SiteUrl"];
                    string ListTitle = ConfigurationManager.AppSettings["ListTitle"];
                    //string ListView = ConfigurationManager.AppSettings["ListView"];

                    System.Net.ServicePointManager.Expect100Continue = true;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                    context = new ClientContext(SiteUrl);
                    List list = context.Web.Lists.GetByTitle(ListTitle);

                    context.Load(list);
                    context.ExecuteQuery();
              
                    CamlQuery query = new CamlQuery();
            query.ViewXml = "<View>" +
              "<Query>" +
                   "<Where>" +
                       "<And>" +
                           "<Gt>" +
                                 "<FieldRef Name='Created' />" +
                                 "<Value Type='DateTime'><Today OffsetDays=\"-40\" /></Value>" +
                           "</Gt>" +
                           "<Eq>" +
                                "<FieldRef Name='Status' />" +
                                "<Value Type='Choice'>Error</Value>" +

                            "</Eq>" +
                        "</And>" +
                   "</Where>" +
                   
              "</Query>" +
         "</View>";

            ListItemCollection items = list.GetItems(query);
                    context.Load(items);
                    context.ExecuteQuery();

                    List<ListItem> itemList = items.ToList();
             
                    for (int i=0; i<itemList.Count;i++)
                    {
                        ListItem item = itemList[i];
                        if (item != null)
                        {
                            //"_x0020"
                            // TITLE is for EmpID
                            string empid = item["Title"].ToString();

                            //item["ATTUID"] = ATTUID;
                                                     
                            item.Update();
                            context.ExecuteQuery();
                        }
                    }
                
                if (context != null)
                    context.Dispose();            
        }
    }
}
