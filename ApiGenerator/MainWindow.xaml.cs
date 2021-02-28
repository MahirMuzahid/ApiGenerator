using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> tableItemList = new List<string>();
        public List<string> tableItemVariableList = new List<string>();
        public List<string> tableItemStringList = new List<string>();

        public List<string> tableItemListWhere = new List<string>();
        public List<string> tableItemVariableListWhere = new List<string>();
        public List<string> tableItemStringListWhere = new List<string>();
        public bool isList;
        public MainWindow()
        {
            InitializeComponent();
            SetAPIGenarate.IsEnabled = false;
            GetAPIGenarate.IsEnabled = false;

        }

        private void AddTableItem_Click(object sender, RoutedEventArgs e)
        {
            tableItemList.Add(singleTableItemTextBox.Text);
            if(isInt.IsChecked == true)
            {
                tableItemVariableList.Add("int");
                tableItemStringList.Add(singleTableItemTextBox.Text + " int");
            }
            else
            {
                tableItemVariableList.Add("string");
                tableItemStringList.Add(singleTableItemTextBox.Text + " string");
            }
            
            TableElementList.ItemsSource = null;
            TableElementList.ItemsSource = tableItemStringList;
        }
        private void AddTableItemwhere_Click(object sender, RoutedEventArgs e)
        {
            tableItemListWhere.Add(singleTableItemTextBoxWhere.Text);
            if (isIntWhere.IsChecked == true)
            {
                tableItemVariableListWhere.Add("int");
                tableItemStringListWhere.Add(singleTableItemTextBoxWhere.Text + " int");
            }
            else
            {
                tableItemVariableListWhere.Add("string");
                tableItemStringListWhere.Add(singleTableItemTextBoxWhere.Text + " string");
            }

            TableElementListWhere.ItemsSource = null;
            TableElementListWhere.ItemsSource = tableItemStringListWhere;
        }
        private void tableGenarate_Click(object sender, RoutedEventArgs e)
        {
            string thisTxbxString = "create table " + clsName.Text + "(\n";
            for(int i = 0; i < tableItemVariableList.Count; i++)
            {
                if(tableItemVariableList[i] == "int")
                {
                    if(i == tableItemVariableList.Count-1)
                    {
                        thisTxbxString += tableItemList[i] + " int" + "\n";
                    }
                    else
                    {
                        thisTxbxString += tableItemList[i] + " int" + ",\n";
                    }
                    
                }
                else
                {
                    if (i == tableItemVariableList.Count - 1)
                    {
                        thisTxbxString += tableItemList[i] + " nvarchar(50)" + "\n";
                    }
                    else
                    {
                        thisTxbxString += tableItemList[i] + " nvcarchar(50)" + ",\n";
                    }
                }
            }
            tableTextBox.Text = thisTxbxString + ");";
        }

        private void SetprocedureGenarate_Click(object sender, RoutedEventArgs e)
        {
            string thisTxbxString = "create procedure Set" + clsName.Text + "\n";
            for (int i = 0; i < tableItemVariableList.Count; i++)
            {
                if (tableItemVariableList[i] == "int")
                {
                    if (i == tableItemVariableList.Count - 1)
                    {
                        thisTxbxString += " @" + tableItemList[i] + " int" + "\n";
                    }
                    else
                    {
                        thisTxbxString += " @" + tableItemList[i] + " int" + ",\n";
                    }

                }
                else
                {
                    if (i == tableItemVariableList.Count - 1)
                    {
                        thisTxbxString += " @" + tableItemList[i] + " nvarchar(50)" + "\n";
                    }
                    else
                    {
                        thisTxbxString += " @" + tableItemList[i] + " nvcarchar(50)" + ",\n";
                    }
                }
            }
            thisTxbxString += "as begin \n insert into " + clsName.Text + "( ";

            for (int i = 0; i < tableItemVariableList.Count; i++)
            {
                if (i == tableItemVariableList.Count - 1)
                {
                    thisTxbxString += tableItemList[i];
                }
                else
                {
                    thisTxbxString += tableItemList[i] + ", ";
                }
                
            }
            thisTxbxString += ") \v values ( ";
            for (int i = 0; i < tableItemVariableList.Count; i++)
            {
                if (i == tableItemVariableList.Count - 1)
                {
                    thisTxbxString += "@"+ tableItemList[i];
                }
                else
                {
                    thisTxbxString += "@" + tableItemList[i] + ", ";
                }

            }
            thisTxbxString += ") \n end";
            procedureTextBox.Text = thisTxbxString;
        }

        private void GetprocedureGenarate_Click(object sender, RoutedEventArgs e)
        {
            string thisTxbxString;
            if(IsConditionalProcedure.IsChecked == false)
            {
                thisTxbxString = "create procedure get" + clsName.Text + "\n as begin \n Select * from " + clsName.Text + "\n end";
            }
            else
            {
                thisTxbxString = "create procedure Get" + clsName.Text + "\n";
                for (int i = 0; i < tableItemVariableList.Count; i++)
                {
                    if (tableItemVariableList[i] == "int")
                    {
                        if (i == tableItemVariableList.Count - 1)
                        {
                            thisTxbxString += " @" + tableItemList[i] + " int" + "\n";
                        }
                        else
                        {
                            thisTxbxString += " @" + tableItemList[i] + " int" + ",\n";
                        }

                    }
                    else
                    {
                        if (i == tableItemVariableList.Count - 1)
                        {
                            thisTxbxString += " @" + tableItemList[i] + " nvarchar(50)" + "\n";
                        }
                        else
                        {
                            thisTxbxString += " @" + tableItemList[i] + " nvcarchar(50)" + ",\n";
                        }
                    }
                }
            }
            
            procedureTextBox.Text = thisTxbxString;
        }

        private void classGenerate_Click(object sender, RoutedEventArgs e)
        {
            string thisTxbxString = "";
            for (int i = 0; i < tableItemVariableList.Count; i++)
            {
                thisTxbxString += "public " + tableItemVariableList[i] + " " + tableItemList[i] + " { get ; set ;} \n";
            }
            classTxbx.Text = thisTxbxString;
        }

        private void SetAPIGenarate_Click(object sender, RoutedEventArgs e)
        {
            string thisTxbxString = "[AcceptVerbs(\"GET\", \"POST\")]\n public Response set" + clsName.Text + "(" + clsName.Text + " obj) \n {\n Response response = new Response(); \n try \n { \n Connection();\n  SqlCommand cmd = new SqlCommand(\"set"+ clsName.Text + "\",conn);\n cmd.CommandType = System.Data.CommandType.StoredProcedure;\n";
            
            for (int i = 0; i < tableItemVariableList.Count; i++)
            {
                thisTxbxString += " cmd.Parameters.AddWithValue(\"@" + tableItemList[i] + "\", obj." + tableItemList[i] + ");\n";
            }
            thisTxbxString += " conn.Open();\nint i = cmd.ExecuteNonQuery();\n" +
                "if (i != 0)\n" +
                "{" +
                "\n" +
                " response.Massage = \"Succesfull!\";\n" +
                "response.Status = 0;\n" +
                "}\n" +
                "else\n" +
                "{\n " +
                "response.Massage = \"Unsuccesfull!\";\nresponse.Status = 1;\n}\n}\ncatch(Exception ex)\n{\nresponse.Massage = ex.Message;\nresponse.Status = 0;\n}\nreturn response;\n}\n";
            APITextBox.Text = thisTxbxString;
        }

        private void GetAPIGenarate_Click(object sender, RoutedEventArgs e)
        {
            string thisTxbxString = "" +
                "[AcceptVerbs(\"GET\", \"POST\")]\n" +
                "public List<" + clsName.Text + "> get" + clsName.Text + "(" + clsName.Text + " obj)\n" +
                "{\n" +
                "List<" + clsName.Text + "> objRList" + " = new List<" + clsName.Text + ">();\n" +
                "try\n" +
                "{\n" +
                " Connection();\n" +
                "SqlCommand cmd = new SqlCommand(\"Get" + clsName.Text + "\", conn);\n" +
                "conn.Open();\n" +
                "SqlDataReader reader = cmd.ExecuteReader();\n" +
                "while (reader.Read())\n" +
                "{\n" +
                clsName.Text + " objAdd" + " = new " + clsName.Text + "();\n";

            for (int i = 0; i < tableItemVariableList.Count; i++)
            {
                if(tableItemVariableList[i] == "int")
                {
                    thisTxbxString += "objAdd." + tableItemList[i] + " = Convert.ToInt32(reader[\""+ tableItemList[i]+ "\"]);\n";
                }
                else
                {
                    thisTxbxString += "objAdd." + tableItemList[i] + " reader[\"" + tableItemList[i] + "\"].ToString(); \n";
                }
                
            }
            thisTxbxString += "objRList.Add(objAdd);\n}\n" +
                "conn.Close();\n" +
                "}\n" +
                "catch (Exception ex)\n" +
                "{\n" +
                clsName.Text + " objAdd" + " = new " + clsName.Text + "();\n" +
                "objAdd.Response = ex.Message;\n" +
                "objRList.Add(objAdd);\n"+
                "}\n" +
                "retrun objRList\n" +
                "}\n";
            APITextBox.Text = thisTxbxString;
        }

        private void ListAPIGenarate_Click(object sender, RoutedEventArgs e)
        {
            isList = true;
            SetAPIGenarate.IsEnabled = true;
            GetAPIGenarate.IsEnabled = true;
        }

        private void NonListAPIGenarate_Click(object sender, RoutedEventArgs e)
        {
            isList = false;
            SetAPIGenarate.IsEnabled = true;
            GetAPIGenarate.IsEnabled = true;
        }

       
    }
}
 