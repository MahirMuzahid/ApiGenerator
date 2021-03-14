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
    /// Interaction logic for APIGenaretor.xaml
    /// </summary>
    public partial class APIGenaretor : Page
    {
        public List<string> tableItemList = new List<string>();
        public List<string> tableItemVariableList = new List<string>();
        public List<string> tableItemStringList = new List<string>();

        public List<string> tableItemListWhere = new List<string>();
        public List<string> tableItemVariableListWhere = new List<string>();
        public List<string> tableItemStringListWhere = new List<string>();
        public bool isList;
        public APIGenaretor()
        {
            InitializeComponent();
            ListAPIGenarate.IsEnabled = false;

        }

        private void AddTableItem_Click(object sender, RoutedEventArgs e)
        {
            tableItemList.Add(singleTableItemTextBox.Text);
            if (isInt.IsChecked == true)
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
            ListAPIGenarate.IsEnabled = true;
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
            for (int i = 0; i < tableItemVariableList.Count; i++)
            {
                if (tableItemVariableList[i] == "int")
                {
                    if (i == tableItemVariableList.Count - 1)
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
                    thisTxbxString += "@" + tableItemList[i];
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
            if (IsConditionalProcedure.IsChecked == false)
            {
                thisTxbxString = "create procedure " + ProcedureNameTxbx.Text + "\n as begin \n Select * from " + clsName.Text + "\n end";
            }
            else
            {
                thisTxbxString = "create procedure " + ProcedureNameTxbx.Text + "\n";
                for (int i = 0; i < tableItemVariableListWhere.Count; i++)
                {
                    if (tableItemVariableListWhere[i] == "int")
                    {
                        if (i == tableItemVariableListWhere.Count - 1)
                        {
                            thisTxbxString += " @" + tableItemListWhere[i] + " int" + "\n";
                        }
                        else
                        {
                            thisTxbxString += " @" + tableItemListWhere[i] + " int" + ",\n";
                        }

                    }
                    else
                    {
                        if (i == tableItemVariableListWhere.Count - 1)
                        {
                            thisTxbxString += " @" + tableItemListWhere[i] + " nvarchar(50)" + "\n";
                        }
                        else
                        {
                            thisTxbxString += " @" + tableItemListWhere[i] + " nvcarchar(50)" + ",\n";
                        }
                    }
                }
                thisTxbxString += "as begin \n Select ( ";

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
                thisTxbxString += ") \v Where ";
                for (int i = 0; i < tableItemVariableListWhere.Count; i++)
                {
                    if (tableItemVariableListWhere.Count - 1 == 0)
                    {
                        thisTxbxString += tableItemListWhere[i] + " = @" + tableItemListWhere[i] + " ";
                    }
                    else if (i == tableItemVariableListWhere.Count - 1)
                    {
                        thisTxbxString += tableItemListWhere[i] + " = @" + tableItemListWhere[i] + ", ";
                    }
                    else
                    {
                        thisTxbxString += tableItemListWhere[i] + " = @" + tableItemListWhere[i] + " AND ";
                    }

                }
                thisTxbxString += " \n end";

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
            thisTxbxString += "public Response { get ; set ;} \n";
            classTxbx.Text = thisTxbxString;
        }

        private void ListAPIGenarate_Click(object sender, RoutedEventArgs e)
        {
            isList = true;
            APITextBox.Text = MaikeGetApi1stSegment() + addParapeter() + ReturnInfo();
        }

        private void NonListAPIGenarate_Click(object sender, RoutedEventArgs e)
        {
            isList = false;
            APITextBox.Text = MaikeGetApi1stSegment() + addParapeter() + ReturnInfo();
        }
        public string MaikeGetApi1stSegment()
        {
            string returnString = "";

            string returnType = "";

            if (TableElementList.Items.Count != 0)
            {
                returnType = clsName.Text;
            }
            else
            {
                returnType = "Response";
            }
            if (!isList)
            {
                if (TableElementListWhere.Items.Count != 0)
                {
                    returnString = "[AcceptVerbs(\"GET\", \"POST\")]\npublic " + returnType + " " + ApiNameTxbx.Text + "(" + returnType + " obj)\n" + clsName.Text + " objR" + " = new " + clsName.Text + "();";
                }
                else
                {
                    returnString = "[AcceptVerbs(\"GET\", \"POST\")]\npublic " + returnType + " " + ApiNameTxbx.Text + "()\n" + returnType + " objR" + " = new " + clsName.Text + "();";
                }

            }
            else
            {
                if (TableElementListWhere.Items.Count != 0)
                {
                    returnString = "[AcceptVerbs(\"GET\", \"POST\")]\npublic List<" + returnType + "> " + ApiNameTxbx.Text + "(" + returnType + " obj)\nList<" + clsName.Text + "> objRList" + " = new List<" + clsName.Text + ">();";
                }
                else
                {
                    returnString = "[AcceptVerbs(\"GET\", \"POST\")]\npublic List<" + returnType + "> " + ApiNameTxbx.Text + "()\nList<" + returnType + "> objRList" + " = new List<" + clsName.Text + ">();";
                }

            }
            return returnString;
        }

        public string addParapeter()
        {
            string returnString = "";
            returnString = "\ntry \n" +
                    "{\n" +
                    "Connection();\n" +
                    "SqlCommand cmd = new SqlCommand(\"" + ApiNameTxbx.Text + "\", conn);\n";

            if (tableItemStringListWhere.Count != 0)
            {
                returnString += "cmd.CommandType = System.Data.CommandType.StoredProcedure;\n";
                for (int i = 0; i < tableItemStringListWhere.Count; i++)
                {
                    returnString += "cmd.Parameters.AddWithValue(\"@" + tableItemListWhere[i] + "\", obj." + tableItemListWhere[i] + ");\n";
                }
            }

            return returnString;
        }

        public string ReturnInfo()
        {
            string returnString = "";

            if (TableElementList.Items.Count != 0)
            {
                returnString += "conn.Open();\n" +
                           "SqlDataReader reader = cmd.ExecuteReader();\n";
                returnString = "while (reader.Read())\n" +
                "{\n";
                returnString += clsName.Text + " objAdd" + " = new " + clsName.Text + "();\n";
                for (int i = 0; i < tableItemVariableList.Count; i++)
                {
                    if (isList)
                    {

                        if (tableItemVariableList[i] == "int")
                        {
                            returnString += "objAdd." + tableItemList[i] + " = Convert.ToInt32(reader[\"" + tableItemList[i] + "\"]);\n";
                        }
                        else
                        {
                            returnString += "objAdd." + tableItemList[i] + " = reader[\"" + tableItemList[i] + "\"].ToString(); \n";
                        }

                    }
                    else
                    {
                        if (tableItemVariableList[i] == "int")
                        {
                            returnString += "objR." + tableItemList[i] + " = Convert.ToInt32(reader[\"" + tableItemList[i] + "\"]);\n";
                        }
                        else
                        {
                            returnString += "objR." + tableItemList[i] + " = reader[\"" + tableItemList[i] + "\"].ToString(); \n";
                        }

                    }
                }
                if (isList)
                {
                    returnString += "objRList.Add(objAdd);\n}\n" +
                    "conn.Close();\n" +
                    "}\n" +
                    "catch (Exception ex)\n" +
                    "{\n" +
                    clsName.Text + " objAdd" + " = new " + clsName.Text + "();\n" +
                    "objAdd.Response = ex.Message;\n" +
                    "objRList.Add(objAdd);\n" +
                    "}\n" +
                    "retrun objRList\n" +
                    "}\n";
                }
                else
                {
                    returnString +=
                   "conn.Close();\n" +
                   "}\n" +
                   "catch (Exception ex)\n" +
                   "{\n" +
                   "objR.Response = ex.Message;\n" +
                   "}\n" +
                   "retrun objR\n" +
                   "}\n";
                }
            }
            else
            {
                returnString += " conn.Open();\nint i = cmd.ExecuteNonQuery();\n" +
                "if (i != 0)\n" +
                "{" +
                "\n" +
                " response.Massage = \"Succesfull!\";\n" +
                "response.Status = 0;\n" +
                "}\n" +
                "else\n" +
                "{\n " +
                "response.Massage = \"Unsuccesfull!\";\nresponse.Status = 1;\n}\n}\ncatch(Exception ex)\n{\nresponse.Massage = ex.Message;\nresponse.Status = 0;\n}\nreturn response;\n}\n";
            }


            return returnString;
        }

        private void clearnItemList_Click(object sender, RoutedEventArgs e)
        {
            TableElementList.ItemsSource = "";

            tableItemList.Clear();
            tableItemVariableList.Clear();
            tableItemStringList.Clear();
            ListAPIGenarate.IsEnabled = false;
        }

        private void clearnItemListWhere_Click(object sender, RoutedEventArgs e)
        {
            TableElementListWhere.ItemsSource = "";

            tableItemListWhere.Clear();
            tableItemVariableListWhere.Clear();
            tableItemStringListWhere.Clear();
        }
        UIGenaretor ui = new UIGenaretor();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UIGenaretor());
        }

        private void copyItemList_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < tableItemList.Count; i++)
            {
                tableItemListWhere.Add(tableItemList[i]);
                tableItemVariableListWhere.Add(tableItemVariableList[i]);
                tableItemStringListWhere.Add(tableItemStringList[i]);
            }
           
        }

        private void copyItemListWhere_Click(object sender, RoutedEventArgs e)
        {
            TableElementListWhere.ItemsSource = null;
            TableElementListWhere.ItemsSource = tableItemStringListWhere;


        }
    }

}
