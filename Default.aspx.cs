using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using RestSharp;


namespace CloudAugurTest
{
    public partial class _Default : Page
    {
        int warehouseNum;
       

        //      File uploader
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                warehouseNum = System.Convert.ToInt32(LBWHNumSign.Text);
                string filename = Path.GetFileName(FileUpload.FileName);
                FileUpload.SaveAs(Server.MapPath("~/") + filename);
                LBUploadedFile.Text = filename;
                LBWarehouseNum.Text = warehouseNum.ToString();
                LBAssignedTo.Visible = true;
                LBWarehouseNum.Visible = true;
                
                // Parses csv file and collects data to program        
                StreamReader fileReader = new StreamReader(File.OpenRead(Server.MapPath("~/") + filename));
                List<string> dates = new List<String>();
                List<string> sales = new List<String>();
                List<string> mvm = new List<String>();
                List<string> nfl = new List<String>();
                List<string> dayAfterHoliday = new List<String>();
                List<string> inclimateWeather = new List<String>();

                while (!fileReader.EndOfStream)
                {
                    string line = fileReader.ReadLine();
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(';');
                        if (values.Length >= 6)
                        {
                            dates.Add(values[0]);
                            sales.Add(values[1]);
                            mvm.Add(values[2]);
                            nfl.Add(values[3]);
                            dayAfterHoliday.Add(values[4]);
                            inclimateWeather.Add(values[5]);
                        }
                    }
                }
                string[] firstlistA = dates.ToArray();
                string[] firstlistB = sales.ToArray();
                string[] firstlistC = mvm.ToArray();
                string[] firstlistD = nfl.ToArray();
                string[] firstlistE = dayAfterHoliday.ToArray();
                string[] firstlistF = inclimateWeather.ToArray();

                //         Example of alert popup window code. A debugging tool
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + warehouseNum + "');", true);


                //      This creates a connection to the Azure database and INSERTS it

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "cloudaugurtestserver.database.windows.net";
                builder.UserID = "cloudaugurtestadmin";
                builder.Password = "Password1";
                builder.InitialCatalog = "CloudAugurTestDB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("DELETE FROM dbo.Sales WHERE WH_NUM = ");
                    sb.Append(warehouseNum);
                    sb.Append(" ");
                    sb.Append("INSERT INTO dbo.Sales (WH_NUM, SALES_DATE, SALES, MVM, NFL, DAYAFTERHOLIDAY, INCLIMATEWEATHER) ");
                    sb.Append("VALUES");
                    for (int i = 0; i < firstlistA.Length; i++)
                    {
                        sb.Append("( ");
                        sb.Append(warehouseNum);
                        sb.Append(", '");
                        sb.Append(firstlistA[i]);
                        sb.Append("', ");
                        sb.Append(firstlistB[i]);
                        sb.Append(", '");
                        sb.Append(firstlistC[i]);
                        sb.Append("', '");
                        sb.Append(firstlistD[i]);
                        sb.Append("', '");
                        sb.Append(firstlistE[i]);
                        sb.Append("', '");
                        sb.Append(firstlistF[i]);
                        sb.Append("'),");

                    }

                    String sql = sb.ToString().Substring(0, sb.Length - 1);

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2} {3} {4} {5}", reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                            }
                        }
                    }

                    connection.Close();
                }
            }
            TBWarehouseNum.Text = "";
            Session["salesList"] = GetSalesInfo(warehouseNum.ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime prevYear = new DateTime();
            prevYear = Calendar1.SelectedDate;
            prevYear = prevYear.AddYears(-1);
            Sales[] list = (Sales[])Session["salesList"];    // Assigns the Session data to a new list, as an efficient alternative to making a database call every time a new date is selected.
            TBLastYearSales.Text = "";
            TBPlannedSales.Text = "";

            if (list != null)
                {
                for (int i = 0; i < list.Length; i++)       // Reformat loop to make all dates compatible with .ToShortDateString() format
                {
                    // Reformat 1: Remove the first character of the date in the month column if it's a 0
                    if (list[i].Col1[0] == '0')
                    {
                        list[i].Col1 = list[i].Col1.Remove(0, 1);
                    }
                    // Reformat 2: Remove any leading 0's in the day column
                    list[i].Col1 = Regex.Replace(list[i].Col1, @"/0", "/");
                }

                // Loop through list of dates to find matching date from previous year and save it's coresponding sales figure to previous year sales textbox 
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i].Col1 == prevYear.ToShortDateString())
                    {
                        TBLastYearSales.Text = list[i].Col2;
                    }
                    if (list[i].Col1 == Calendar1.SelectedDate.ToShortDateString())
                    {
                        // CODE FOR REST CALL TO API
                        var client = new RestClient("https://uswestcentral.services.azureml.net/workspaces/a2be39e638f24f73a8985d53c1c0c3ea/services/b609514c0e5e4c6da55f67d91777442f/execute?api-version=2.0&format=swagger");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Connection", "keep-alive");
                        request.AddHeader("content-length", "498");
                        request.AddHeader("accept-encoding", "gzip, deflate");
                        request.AddHeader("Host", "uswestcentral.services.azureml.net");
                        request.AddHeader("Postman-Token", "86f1f14b-b5e9-4008-a28b-58676009ddd1,ba7a9458-db5f-4491-a4b3-ce0f64f4f7b9");
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Accept", "*/*");
                        request.AddHeader("User-Agent", "PostmanRuntime/7.13.0");
                        request.AddHeader("Authorization", "Bearer OCj6HGJJcPrM38Zxy2wO0zcDW//33jujDe+XvlgmAAjvHFLiw5c0ziNs947PtB0Nt4ZmZhhtjkiS1V3vZzthWg==");
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("undefined", "{\r\n        \"Inputs\": {\r\n                \"input1\":\r\n                [\r\n                    {\r\n                            'SALES_DATE': \"" + list[i].Col1 + "\",   \r\n                            'SALES': \"" + list[i].Col2 + "\",   \r\n                            'MVM': \"" + list[i].Col3 + "\",   \r\n                            'NFL': \"" + list[i].Col4 + "\",   \r\n                            'DAYAFTERHOLIDAY': \"" + list[i].Col5 + "\",   \r\n                            'INCLIMATEWEATHER': \"" + list[i].Col6 + "\",   \r\n                    }\r\n                ],\r\n        },\r\n    \"GlobalParameters\":  {\r\n    }\r\n}\r\n", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        string salesPrediction = response.Content.ToString();

                        // Trim every character past the decimal in salesPrediction
                        int index = salesPrediction.LastIndexOf(".");
                        if (index > 0)
                        {
                            salesPrediction = salesPrediction.Substring(0, index);
                        }

                        // Trim first 45 characters in salesPrediction from elongated REST response output to leave only the sales prediction amount
                        salesPrediction = salesPrediction.Substring(45);

                        // Output trimmed sales prediction to Planned Sales Textbox
                        TBPlannedSales.Text = salesPrediction;

                        // Set MVM icon visibility
                        if (list[i].Col3 == "y")
                        {
                            imgMVM.Visible = true;
                        }
                        else
                        {
                            imgMVM.Visible = false;
                        }
                        // Set NFL game icon visibility
                        if (list[i].Col4 == "y")
                        {
                            imgNFL.Visible = true;
                        }
                        else
                        {
                            imgNFL.Visible = false;
                        }
                        // Set day after holiday icon visibility
                        if (list[i].Col5 == "y")
                        {
                            imgDAHoliday.Visible = true;
                        }
                        else
                        {
                            imgDAHoliday.Visible = false;
                        }
                        // Set inclimate weather icon visibility
                        if (list[i].Col6 == "y")
                        {
                            imgInclimateWeather.Visible = true;
                        }
                        else
                        {
                            imgInclimateWeather.Visible = false;
                        }
                    }
                }
            }
            else
            {
                TBLastYearSales.Text = "";
                TBPlannedSales.Text = "";
            }
        }

        protected void TBWarehouseNum_TextChanged(object sender, EventArgs e)
        {

        }

        protected void BtnEnterWarehouseNum_Click(object sender, EventArgs e)
        {
            
            if (TBWarehouseNum.Text.Length > 0)
            {
                warehouseNum = System.Convert.ToInt32(TBWarehouseNum.Text);
                if (warehouseNum > 0)
                {
                    FileUpload.Visible = true;
                    UploadButton.Visible = true;
                    LB1.Visible = true;
                    LBUploadedFile.Visible = true;
                    LBWHNumSign.Text = warehouseNum.ToString();
                    LBWHNumSign.Visible = true;
                    LBWarehouseNum.Visible = true;
                    Calendar1.Visible = true;
                    // Stores the Sales[] list into Session data, so it may remain during the entire instance of the application and not be deleted when a server call occurs. 
                    // This Session data can then be accessed across all functions. (mrm)
                    Session["salesList"] = GetSalesInfo(warehouseNum.ToString());
                    LB2.Visible = true;
                    LB3.Visible = true;
                    TBLastYearSales.Visible = true;
                    TBPlannedSales.Visible = true;
                    LBAssignedTo.Visible = true;
                    pnlCalendar.Visible = true;
                }
                else
                {
                    FileUpload.Visible = false;
                    UploadButton.Visible = false;
                    LB1.Visible = false;
                    LBUploadedFile.Visible = false;
                    LBWHNumSign.Visible = false;
                    LB2.Visible = false;
                    LB3.Visible = false;
                    TBLastYearSales.Visible = false;
                    TBPlannedSales.Visible = false;
                    Calendar1.Visible = false;
                    LBAssignedTo.Visible = false;
                    LBWarehouseNum.Visible = false;
                    pnlCalendar.Visible = false;
                }
            }
            TBWarehouseNum.Text = "";
        }

        private Sales[] GetSalesInfo(string wNum)//"wNum" is your search paramater inside database. This function retrieves info from database and returns it as an array of Sales object
        {

            //   this reads data for that warehouse from DB and saves to array. 
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "cloudaugurtestserver.database.windows.net";
            builder.UserID = "cloudaugurtestadmin";
            builder.Password = "Password1";
            builder.InitialCatalog = "CloudAugurTestDB";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT CONVERT(VARCHAR(10), SALES_DATE, 101), SALES, MVM, NFL, DAYAFTERHOLIDAY, INCLIMATEWEATHER ");
                sb.Append("FROM dbo.Sales ");
                sb.Append("WHERE WH_NUM = ");
                sb.Append(wNum);
                String sql = sb.ToString();
                
                Sales[] allRecords = null;

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader sReader = command.ExecuteReader())
                    {
                        var list = new List<Sales>();
                        while (sReader.Read())
                        {
                            list.Add(new Sales
                            {
                                Col1 = sReader.GetString(0),
                                Col2 = sReader.GetSqlInt32(1).ToString(),
                                Col3 = sReader.GetString(2),
                                Col4 = sReader.GetString(3),
                                Col5 = sReader.GetString(4),
                                Col6 = sReader.GetString(5)
                            });
                            allRecords = list.ToArray();
                        }
                    }
                }
                connection.Close();
                return allRecords;
            }
        }

        public class Sales
        {
            public string Col1 { get; set; }
            public string Col2 { get; set; }
            public string Col3 { get; set; }
            public string Col4 { get; set; }
            public string Col5 { get; set; }
            public string Col6 { get; set; }
        }
        
    }

}




