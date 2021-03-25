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
           
        }
        public async Task refresh()
        {
            allClass = await "https://api.shikkhanobish.com/api/ApiMaker/getAPiMaker".GetJsonAsync<ObservableCollection<ClassGen>>();
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
            //Genarate all code here

            string path = @"D:\CodeGenerator\Auto Genareted Code/" + allClass[0].tableName+".txt";



            string c_Code = makeSql(0) + "\n" + makeClass(0);


            File.WriteAllText(path, c_Code);

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

            classCode = "--------------------- Class Code ----------------------------\n" + classCode + ");\n---------------------------------------------------------";

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
    }
}
