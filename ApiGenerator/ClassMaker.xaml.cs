using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        List<ClassGen> allClass = new List<ClassGen>();
        List<ClassGen> allClassForCal = new List<ClassGen>();
        public ClassMaker()
        {
            InitializeComponent();
            getData();
        }
        public async Task GetAllClass()
        {          
            allClass = await "https://api.shikkhanobish.com/api/ApiMaker/getAPiMaker".GetJsonAsync<List<ClassGen>>();
            clsList.ItemsSource = allClass;
            allClassForCal = allClass;
        }
        public void getData()
        {
           GetAllClass();
        }
        private void clearnItemList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddTableItem_Click(object sender, RoutedEventArgs e)
        {
           
            string item = "";
            if (!singleTableItemTextBox.Text.Contains(" "))
            {
                item = singleTableItemTextBox.Text;
            }           

            if(intch.IsChecked == true)
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
            lfSuck();
        }
        public async Task lfSuck()
        {           
            await SetClass().ConfigureAwait(false);
            clsList.ItemsSource = null;
            getData();
        }
        public List<string> ConvertToServerList()
        {
            List<string> content = new List<string>();
            content.Add("" + tblItemList.Count);
            content.Add(clsName.Text);
            for(int i = 0; i < tblItemList.Count; i++)
            {
                content.Add(tblItemList[0]);
            }
            for(int i = tblItemList.Count; i < 20; i++)
            {
                content.Add("N/A");
            }if (allClassForCal.Count != 0)
            {
                int max = allClassForCal[0].ID;
                for (int i = 0; i < allClassForCal.Count; i++)
                {
                    if (max > allClassForCal[i].ID)
                    {
                        max = allClassForCal[i].ID;
                    }
                }
                content.Add("" + max);
            }
            content.Add(""+0);
            return content;
        }
    }
}
