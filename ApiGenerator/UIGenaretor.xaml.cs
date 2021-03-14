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
    /// Interaction logic for UIGenaretor.xaml
    /// </summary>
    public partial class UIGenaretor : Page
    {
        string code = "";
        public UIGenaretor()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string codetxt = "<Grid";
            if(subRow.Text != "")
            {
                codetxt += " Grid.Row=\"" + subRow.Text + "\"";
            }
            if (subColumn.Text != "")
            {
                codetxt += " Grid.Column=\"" + subColumn.Text + "\"";
            }
            codetxt += ">\n";

            for(int i =0; i < int.Parse(mRow.Text); i++)
            {
                if( i == 0)
                {
                    codetxt += "<Grid.RowDefinitions>\n";
                }
                codetxt += "<RowDefinition Height=\"1 * \"/>\n";
                if(i == int.Parse(mRow.Text)-1)
                {
                    codetxt += "</Grid.RowDefinitions>\n";
                }
            }
            for (int i = 0; i < int.Parse(mColumn.Text); i++)
            {
                if (i == 0)
                {
                    codetxt += "<Grid.ColumnDefinitions>\n";
                }
                codetxt += "<ColumnDefinition Width=\"1 * \"/>\n";
                if (i == int.Parse(mColumn.Text) - 1)
                {
                    codetxt += "</Grid.ColumnDefinitions>\n";
                }
            }
            code = codetxt;
            codeTxbx.Text = code;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string codet = "";
            string r = "", c = "", rs = "", cs = "";  
            if(eRow.Text != "")
            {
                r = " Grid.Row = \"" + eRow.Text + "\"";
            }
            if (eColumn.Text != "")
            {
                c = " Grid.Column = \"" + eColumn.Text + "\"";
            }
            if (rowSpan.Text != "")
            {
                rs = " Grid.rowSpan = \"" + rowSpan.Text + "\"";
            }
            if (columSpan.Text != "")
            {
                cs = " Grid.columSpan = \"" + columSpan.Text + "\"";
            }
            codet = "<" + eName.Text + r + c + rs + cs + " "+addtxxt.Text + "/>\n";
            code += codet + "\n";
            codeTxbx.Text = code;
        }
    }
}
