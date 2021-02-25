﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            
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
            string thisTxbxString = "create procedure get"+ clsName.Text +"\n as begin \n Select * from " + clsName.Text + "\n end";
            procedureTextBox.Text = thisTxbxString;
        }
    }
}
