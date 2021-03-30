using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApiGenerator
{
    /// <summary>
    /// Interaction logic for ClassMaker.xaml
    /// </summary>
    public partial class ClassMaker : Page
    {
        List<string> tblItemList = new List<string>();
        ObservableCollection<ClassGen> allClass = new ObservableCollection<ClassGen>();
        ObservableCollection<ClassGen> allClassForCal = new ObservableCollection<ClassGen>();
        public ClassMaker()
        {
            InitializeComponent();
            getData();
        }
        public async Task GetAllClass()
        {          
            allClass = await "https://api.shikkhanobish.com/api/ApiMaker/getAPiMaker".GetJsonAsync<ObservableCollection<ClassGen>>();
            allClassForCal = allClass;
            clsList.ItemsSource = allClass;
        }
        public void getData()
        {
           GetAllClass();
        }
        private void clearnItemList_Click(object sender, RoutedEventArgs e)
        {
            TableElementList.ItemsSource = null;
            tblItemList.Clear();
        }
        public async Task refresh()
        {
            allClass = await "https://api.shikkhanobish.com/api/ApiMaker/getAPiMaker".GetJsonAsync<ObservableCollection<ClassGen>>();
            allClassForCal = allClass;
            clsList.ItemsSource = null;
            clsList.ItemsSource = allClass;
            rfslbl.Content = "";
        }
        private void AddTableItem_Click(object sender, RoutedEventArgs e)
        {
           if(tblItemList.Count == 20)
           {
                return;
           }
           else
           {
                string item = "";
                if (!singleTableItemTextBox.Text.Contains(" "))
                {
                    item = singleTableItemTextBox.Text;
                }

                if (intch.IsChecked == true)
                {
                    item += " 1";
                }
                else if (stringch.IsChecked == true)
                {
                    item += " 2";
                }
                else if (doublech.IsChecked == true)
                {
                    item += " 3";
                }
                else if (floatch.IsChecked == true)
                {
                    item += " 4";
                }
                tblItemList.Add(item);

                TableElementList.ItemsSource = null;
                TableElementList.ItemsSource = tblItemList;
           }
            
        }

       
        public async Task SetClass()
        {
            List<string> list = ConvertToServerList();
           
            string urlT = "https://api.shikkhanobish.com/api/ApiMaker/setAPiMaker";
            HttpClient clientT = new HttpClient();
            string jsonDataT = JsonConvert.SerializeObject(new {
                @PrNumber = list[0],
                @tableName = list[1],
                @pr1n = list[2],
                @pr2n = list[3],
                @pr3n = list[4],
                @pr4n = list[5],
                @pr5n = list[6],
                @pr6n = list[7],
                @pr7n = list[8],
                @pr8n = list[9],
                @pr9n = list[10],
                @pr10n = list[11],
                @pr11n = list[12],
                @pr12n = list[13],
                @pr13n = list[14],
                @pr14n = list[15],
                @pr15n = list[16],
                @pr16n = list[17],
                @pr17n = list[18],
                @pr18n = list[19],
                @pr19n = list[20],
                @pr20n = list[21],
                @ID = list[22]
            });
            StringContent contentT = new StringContent(jsonDataT, Encoding.UTF8, "application/json");
            HttpResponseMessage responseT = await clientT.PostAsync(urlT, contentT).ConfigureAwait(false);
            string resultT = await responseT.Content.ReadAsStringAsync().ConfigureAwait(false);

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {           
            SetClass();
            rfslbl.Content = "Refresh Now";
        }
       
        public List<string> ConvertToServerList()
        {
            List<string> content = new List<string>();
            content.Add("" + tblItemList.Count);
            content.Add(clsName.Text);
            for(int i = 0; i < tblItemList.Count; i++)
            {
                content.Add(tblItemList[i]);
            }
            for(int i = tblItemList.Count; i < 20; i++)
            {
                content.Add("N/A");
            }
            if (allClassForCal.Count != 0)
            {
                int max = allClassForCal[0].ID;
                for (int i = 0; i < allClassForCal.Count; i++)
                {
                    if (max < allClassForCal[i].ID)
                    {
                        max = allClassForCal[i].ID;
                    }
                }
                content.Add("" + (max+1));
            }
            else
            {
                content.Add("" + 0);
            }
            return content;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < allClass.Count; i++)
            {
                string path = @"D:\CodeGenerator\Auto Genareted Code/" + allClass[i].tableName + ".txt";

                string c_Code = makeSql(i) + "\n" + makeClass(i) + "\n" + makeGetProcedure(i) + "\n" + makeGetProcedureWithID(i) + "\n" + makeGetApi(i) + "\n" + makeGetApiWithID(i) + "\n" + makeSetProcedure(i);

                File.WriteAllText(path, c_Code);
            }
            //gg
            rfslbl.Content = "Code Genareted!!!";
        }
        public string makeClass(int clsIndex)
        {
            ClassGen thisclass = new ClassGen();
            thisclass = allClass[clsIndex];

            List<string> thisClassParameter = new List<string>();
            char[] charsToTrim = { ' ', '1', '2', '3', '4' };
            thisClassParameter.Add(thisclass.pr1n);
            thisClassParameter.Add(thisclass.pr2n);
            thisClassParameter.Add(thisclass.pr3n);
            thisClassParameter.Add(thisclass.pr4n);
            thisClassParameter.Add(thisclass.pr5n);
            thisClassParameter.Add(thisclass.pr6n);
            thisClassParameter.Add(thisclass.pr7n);
            thisClassParameter.Add(thisclass.pr8n);
            thisClassParameter.Add(thisclass.pr9n);
            thisClassParameter.Add(thisclass.pr10n);
            thisClassParameter.Add(thisclass.pr11n);
            thisClassParameter.Add(thisclass.pr12n);
            thisClassParameter.Add(thisclass.pr13n);
            thisClassParameter.Add(thisclass.pr14n);
            thisClassParameter.Add(thisclass.pr15n);
            thisClassParameter.Add(thisclass.pr16n);
            thisClassParameter.Add(thisclass.pr17n);
            thisClassParameter.Add(thisclass.pr18n);
            thisClassParameter.Add(thisclass.pr19n);
            thisClassParameter.Add(thisclass.pr20n);
            string classCode = "";
            for (int i = 0; i < thisclass.PrNumber; i++)
            {
                if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '1')
                {
                    classCode += thisClassParameter[i].Trim(charsToTrim) + " int { get ; set ;} \n";
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '2')
                {
                    classCode += thisClassParameter[i].Trim(charsToTrim) + " string { get ; set ;} \n";
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '3' )
                {
                    classCode += thisClassParameter[i].Trim(charsToTrim) + " double { get ; set ;} \n";
                }
                else if ( thisClassParameter[i][thisClassParameter[i].Length - 1] == '4')
                {
                    classCode += thisClassParameter[i].Trim(charsToTrim) + " float { get ; set ;} \n";
                }

            }

            classCode += "public Response { get ; set ;} \n";

            classCode = "--------------------- Class Code ----------------------------\n" + classCode + "\n---------------------------------------------------------";

            return classCode;
        }

        public string makeSql(int clsIndex)
        {
            ClassGen thisclass = new ClassGen();
            thisclass = allClass[clsIndex];

            List<string> thisClassParameter = new List<string>();
            char[] charsToTrim = { ' ', '1', '2', '3','4'};
            thisClassParameter.Add(thisclass.pr1n);
            thisClassParameter.Add(thisclass.pr2n);
            thisClassParameter.Add(thisclass.pr3n);
            thisClassParameter.Add(thisclass.pr4n);
            thisClassParameter.Add(thisclass.pr5n);
            thisClassParameter.Add(thisclass.pr6n);
            thisClassParameter.Add(thisclass.pr7n);
            thisClassParameter.Add(thisclass.pr8n);
            thisClassParameter.Add(thisclass.pr9n);
            thisClassParameter.Add(thisclass.pr10n);
            thisClassParameter.Add(thisclass.pr11n);
            thisClassParameter.Add(thisclass.pr12n);
            thisClassParameter.Add(thisclass.pr13n);
            thisClassParameter.Add(thisclass.pr14n);
            thisClassParameter.Add(thisclass.pr15n);
            thisClassParameter.Add(thisclass.pr16n);
            thisClassParameter.Add(thisclass.pr17n);
            thisClassParameter.Add(thisclass.pr18n);
            thisClassParameter.Add(thisclass.pr19n);
            thisClassParameter.Add(thisclass.pr20n);

            string classCode = "create table " + thisclass.tableName + "(\n";
            for (int i = 0; i < thisclass.PrNumber; i++)
            {
                if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '1')
                {                    
                    if(i == thisclass.PrNumber -1)
                    {
                        classCode += thisClassParameter[i].Trim(charsToTrim) + " int" + "\n";
                    }
                    else
                    {
                        classCode += thisClassParameter[i].Trim(charsToTrim) + " int" + ",\n";
                    }                   
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '2')
                {
                    if (i == thisclass.PrNumber - 1)
                    {
                        classCode += thisClassParameter[i].Trim(charsToTrim) + " nvarchar(50)" + "\n";
                    }
                    else
                    {
                        classCode += thisClassParameter[i].Trim(charsToTrim) + " nvarchar(50)" + ",\n";
                    }
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '3' || thisClassParameter[i][thisClassParameter[i].Length - 1] == '4')
                {
                    if (i == thisclass.PrNumber - 1)
                    {
                        classCode += thisClassParameter[i] + " float" + "\n";
                    }
                    else
                    {
                        classCode += thisClassParameter[i] + " float" + ",\n";
                    }
                }

            }
            classCode = "--------------------- Sql Table Code ----------------------------\n"+ classCode + ");\n---------------------------------------------------------";

            return classCode;
        }



        public string makeGetProcedure(int clsIndex)
        {
            string getProCode = "create procedure get" +allClass[clsIndex].tableName  + "\n as begin \n Select * from " + allClass[clsIndex].tableName + "\n end";

            getProCode = "--------------------- Get All Info Procedure ----------------------------\n" + getProCode + ";\n---------------------------------------------------------";

            return getProCode;


        }

        public string makeGetProcedureWithID (int clsIndex)
        {
            ClassGen thisclass = new ClassGen();
            thisclass = allClass[clsIndex];

            List<string> thisClassParameter = new List<string>();
            char[] charsToTrim = { ' ', '1', '2', '3', '4' };
            thisClassParameter.Add(thisclass.pr1n);
            thisClassParameter.Add(thisclass.pr2n);
            thisClassParameter.Add(thisclass.pr3n);
            thisClassParameter.Add(thisclass.pr4n);
            thisClassParameter.Add(thisclass.pr5n);
            thisClassParameter.Add(thisclass.pr6n);
            thisClassParameter.Add(thisclass.pr7n);
            thisClassParameter.Add(thisclass.pr8n);
            thisClassParameter.Add(thisclass.pr9n);
            thisClassParameter.Add(thisclass.pr10n);
            thisClassParameter.Add(thisclass.pr11n);
            thisClassParameter.Add(thisclass.pr12n);
            thisClassParameter.Add(thisclass.pr13n);
            thisClassParameter.Add(thisclass.pr14n);
            thisClassParameter.Add(thisclass.pr15n);
            thisClassParameter.Add(thisclass.pr16n);
            thisClassParameter.Add(thisclass.pr17n);
            thisClassParameter.Add(thisclass.pr18n);
            thisClassParameter.Add(thisclass.pr19n);
            thisClassParameter.Add(thisclass.pr20n);
            string proCode = "";
            proCode = "create procedure get" + thisclass.tableName + "WithID\n";
            proCode += " @" + thisClassParameter[0].Trim(charsToTrim) + " int" + "\n";

            proCode += "as begin \n Select ( ";

            for (int i = 0; i < thisclass.PrNumber; i++)
            {
                if (i == thisclass.PrNumber - 1)
                {
                    proCode += thisClassParameter[i].Trim(charsToTrim);
                }
                else
                {
                    proCode += thisClassParameter[i].Trim(charsToTrim) + ", ";
                }

            }
            proCode += ") from " +thisclass.tableName +" \n Where " + thisClassParameter[0].Trim(charsToTrim) + " = @" + thisClassParameter[0].Trim(charsToTrim) + "  \nend";

            proCode = "---------------------Get Info With ID Procedure ----------------------------\n" + proCode + "\n---------------------------------------------------------";

            return proCode;

        }

        public string makeGetApi(int clsIndex)
        {
            ClassGen thisclass = new ClassGen();
            thisclass = allClass[clsIndex];

            List<string> thisClassParameter = new List<string>();
            char[] charsToTrim = { ' ', '1', '2', '3', '4' };
            thisClassParameter.Add(thisclass.pr1n);
            thisClassParameter.Add(thisclass.pr2n);
            thisClassParameter.Add(thisclass.pr3n);
            thisClassParameter.Add(thisclass.pr4n);
            thisClassParameter.Add(thisclass.pr5n);
            thisClassParameter.Add(thisclass.pr6n);
            thisClassParameter.Add(thisclass.pr7n);
            thisClassParameter.Add(thisclass.pr8n);
            thisClassParameter.Add(thisclass.pr9n);
            thisClassParameter.Add(thisclass.pr10n);
            thisClassParameter.Add(thisclass.pr11n);
            thisClassParameter.Add(thisclass.pr12n);
            thisClassParameter.Add(thisclass.pr13n);
            thisClassParameter.Add(thisclass.pr14n);
            thisClassParameter.Add(thisclass.pr15n);
            thisClassParameter.Add(thisclass.pr16n);
            thisClassParameter.Add(thisclass.pr17n);
            thisClassParameter.Add(thisclass.pr18n);
            thisClassParameter.Add(thisclass.pr19n);
            thisClassParameter.Add(thisclass.pr20n);

            string apiCode = "";
            apiCode = "[AcceptVerbs(\"GET\", \"POST\")]\npublic List<" + thisclass.tableName + "> get" + thisclass.tableName + "()\n{\nList<" + thisclass.tableName + "> objRList" + " = new List<" + thisclass.tableName + ">();";

            apiCode += "\ntry \n" +
                    "{\n" +
                    "Connection();\n" +
                    "SqlCommand cmd = new SqlCommand(\"" + "get" + thisclass.tableName + "\", conn);\n" + "cmd.CommandType = System.Data.CommandType.StoredProcedure;\n";
            apiCode += "conn.Open();\n" +
                       "SqlDataReader reader = cmd.ExecuteReader();\n";
            apiCode += "while (reader.Read())\n" +
            "{\n";
            apiCode += thisclass.tableName + " objAdd" + " = new " + thisclass.tableName + "();\n";
            for (int i = 0; i < thisclass.PrNumber; i++)
            {
                if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '1')
                {
                    apiCode += "objAdd." + thisClassParameter[i].Trim(charsToTrim) + " = Convert.ToInt32(reader[\"" + thisClassParameter[i].Trim(charsToTrim) + "\"]);\n";
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '2')
                {
                    apiCode += "objAdd." + thisClassParameter[i].Trim(charsToTrim) + " = reader[\"" + thisClassParameter[i].Trim(charsToTrim) + "\"].ToString(); \n";
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '2')
                {
                    apiCode += "objAdd." + thisClassParameter[i].Trim(charsToTrim) + " = Convert.ToInt32(reader[\"" + thisClassParameter[i].Trim(charsToTrim) + "\"]);\n";
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '2')
                {
                    apiCode += "objAdd." + thisClassParameter[i].Trim(charsToTrim) + " = Convert.ToInt32(reader[\"" + thisClassParameter[i].Trim(charsToTrim) + "\"]);\n";
                }
            }
            apiCode += "objRList.Add(objAdd);\n}\n" +
                    "conn.Close();\n" +
                    "}\n" +
                    "catch (Exception ex)\n" +
                    "{\n" +
                    thisclass.tableName + " objAdd" + " = new " + thisclass.tableName + "();\n" +
                    "objAdd.Response = ex.Message;\n" +
                    "objRList.Add(objAdd);\n" +
                    "}\n" +
                    "retrun objRList;\n" +
                    "}\n";
            apiCode = "--------------------- Get All Info Api ----------------------------\n" + apiCode + "\n---------------------------------------------------------";
            return apiCode;
        }

        public string makeGetApiWithID(int clsIndex)
        {
            ClassGen thisclass = new ClassGen();
            thisclass = allClass[clsIndex];

            List<string> thisClassParameter = new List<string>();
            char[] charsToTrim = { ' ', '1', '2', '3', '4' };
            thisClassParameter.Add(thisclass.pr1n);
            thisClassParameter.Add(thisclass.pr2n);
            thisClassParameter.Add(thisclass.pr3n);
            thisClassParameter.Add(thisclass.pr4n);
            thisClassParameter.Add(thisclass.pr5n);
            thisClassParameter.Add(thisclass.pr6n);
            thisClassParameter.Add(thisclass.pr7n);
            thisClassParameter.Add(thisclass.pr8n);
            thisClassParameter.Add(thisclass.pr9n);
            thisClassParameter.Add(thisclass.pr10n);
            thisClassParameter.Add(thisclass.pr11n);
            thisClassParameter.Add(thisclass.pr12n);
            thisClassParameter.Add(thisclass.pr13n);
            thisClassParameter.Add(thisclass.pr14n);
            thisClassParameter.Add(thisclass.pr15n);
            thisClassParameter.Add(thisclass.pr16n);
            thisClassParameter.Add(thisclass.pr17n);
            thisClassParameter.Add(thisclass.pr18n);
            thisClassParameter.Add(thisclass.pr19n);
            thisClassParameter.Add(thisclass.pr20n);

            string apiCode = "";
            apiCode = "[AcceptVerbs(\"GET\", \"POST\")]\npublic " + thisclass.tableName + " get" + thisclass.tableName + "WithID("+ thisclass.tableName +" obj)\n{\n" + thisclass.tableName + " objR" + " = new " + thisclass.tableName + "();";

            apiCode += "\ntry \n" +
                    "{\n" +
                    "Connection();\n" +
                    "SqlCommand cmd = new SqlCommand(\"" + "get" + thisclass.tableName + "WithID\", conn);\n" + "cmd.CommandType = System.Data.CommandType.StoredProcedure;\n" +
                    "cmd.Parameters.AddWithValue(\"@" + thisClassParameter[0].Trim(charsToTrim) + "\", obj." + thisClassParameter[0].Trim(charsToTrim) + ");\n"; 
            apiCode += "conn.Open();\n" +
                       "SqlDataReader reader = cmd.ExecuteReader();\n";
            apiCode += "while (reader.Read())\n" +
            "{\n";
            for (int i = 0; i < thisclass.PrNumber; i++)
            {
                if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '1')
                {
                    apiCode += "objR." + thisClassParameter[i].Trim(charsToTrim) + " = Convert.ToInt32(reader[\"" + thisClassParameter[i].Trim(charsToTrim) + "\"]);\n";
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '2')
                {
                    apiCode += "objR." + thisClassParameter[i].Trim(charsToTrim) + " = reader[\"" + thisClassParameter[i].Trim(charsToTrim) + "\"].ToString(); \n";
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '2')
                {
                    apiCode += "objR." + thisClassParameter[i].Trim(charsToTrim) + " = Convert.ToInt32(reader[\"" + thisClassParameter[i].Trim(charsToTrim) + "\"]);\n";
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '2')
                {
                    apiCode += "objR." + thisClassParameter[i].Trim(charsToTrim) + " = Convert.ToInt32(reader[\"" + thisClassParameter[i].Trim(charsToTrim) + "\"]);\n";
                }
            }
            apiCode += "\n}\n" +
                    "conn.Close();\n" +
                    "}\n" +
                    "catch (Exception ex)\n" +
                    "{\n" +
                    thisclass.tableName + " objR" + " = new " + thisclass.tableName + "();\n" +
                    "objR.Response = ex.Message;\n" +
                    "}\n" +
                    "retrun objR;\n" +
                    "}\n";
            apiCode = "--------------------- Get All Info With ID Api ----------------------------\n" + apiCode + "\n---------------------------------------------------------";
            return apiCode;
        }



        public string makeSetProcedure(int clsIndex)
        {
            ClassGen thisclass = new ClassGen();
            thisclass = allClass[clsIndex];

            List<string> thisClassParameter = new List<string>();
            char[] charsToTrim = { ' ', '1', '2', '3', '4' };
            thisClassParameter.Add(thisclass.pr1n);
            thisClassParameter.Add(thisclass.pr2n);
            thisClassParameter.Add(thisclass.pr3n);
            thisClassParameter.Add(thisclass.pr4n);
            thisClassParameter.Add(thisclass.pr5n);
            thisClassParameter.Add(thisclass.pr6n);
            thisClassParameter.Add(thisclass.pr7n);
            thisClassParameter.Add(thisclass.pr8n);
            thisClassParameter.Add(thisclass.pr9n);
            thisClassParameter.Add(thisclass.pr10n);
            thisClassParameter.Add(thisclass.pr11n);
            thisClassParameter.Add(thisclass.pr12n);
            thisClassParameter.Add(thisclass.pr13n);
            thisClassParameter.Add(thisclass.pr14n);
            thisClassParameter.Add(thisclass.pr15n);
            thisClassParameter.Add(thisclass.pr16n);
            thisClassParameter.Add(thisclass.pr17n);
            thisClassParameter.Add(thisclass.pr18n);
            thisClassParameter.Add(thisclass.pr19n);
            thisClassParameter.Add(thisclass.pr20n);
            string getProCode = "create procedure set" + allClass[clsIndex].tableName + "\n";

            for (int i = 0; i < thisclass.PrNumber; i++)
            {
                if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '1')
                {
                    if (i == thisclass.PrNumber - 1)
                    {
                        getProCode += "@" + thisClassParameter[i].Trim(charsToTrim) + " int" + "\n";
                    }
                    else
                    {
                        getProCode += "@" + thisClassParameter[i].Trim(charsToTrim) + " int" + ",\n";
                    }
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '2')
                {
                    if (i == thisclass.PrNumber - 1)
                    {
                        getProCode += "@" + thisClassParameter[i].Trim(charsToTrim) + " nvarchar(50)" + "\n";
                    }
                    else
                    {
                        getProCode += "@" + thisClassParameter[i].Trim(charsToTrim) + " nvarchar(50)" + ",\n";
                    }
                }
                else if (thisClassParameter[i][thisClassParameter[i].Length - 1] == '3' || thisClassParameter[i][thisClassParameter[i].Length - 1] == '4')
                {
                    if (i == thisclass.PrNumber - 1)
                    {
                        getProCode += "@" + thisClassParameter[i] + " float" + "\n";
                    }
                    else
                    {
                        getProCode += "@" + thisClassParameter[i] + " float" + ",\n";
                    }
                }
            }
            getProCode += "as begin \n Insert Into " + thisclass.tableName + "(";
            for (int i = 0; i < thisclass.PrNumber; i++)
            {
                if (i == thisclass.PrNumber - 1)
                {
                    getProCode += thisClassParameter[i].Trim(charsToTrim);
                }
                else
                {
                    getProCode += thisClassParameter[i].Trim(charsToTrim) + ", ";
                }
            }
            getProCode += ")\n values (";
            for (int i = 0; i < thisclass.PrNumber; i++)
            {
                if (i == thisclass.PrNumber - 1)
                {
                    getProCode += "@" + thisClassParameter[i].Trim(charsToTrim);
                }
                else
                {
                    getProCode += "@" + thisClassParameter[i].Trim(charsToTrim) + ", ";
                }
            }
            getProCode += ")\nend;";
            getProCode = "---------------------Set Info Procedure ----------------------------\n" + getProCode + "\n---------------------------------------------------------";
            return getProCode;
        }

    }
}
