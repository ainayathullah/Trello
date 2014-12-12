using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Inayathullah.Trello_ContextMenu
{
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class MyControl : UserControl
    {
        public ObservableCollection<BoolStringClass> TheList { get; set; }
        public MyControl()
        {
            InitializeComponent();
            
        }

        private void LoadTextBox()
        {
            var app = (EnvDTE.DTE) Trello_ContextMenuPackage.GetGlobalService(typeof (SDTE));
            if (app.ActiveDocument != null && app.ActiveDocument.Type == "Text")
            {
                //EnvDTE.Document doc = (EnvDTE.Document) app.ActiveDocument.Object(string.Empty);
                EnvDTE.TextDocument text = (EnvDTE.TextDocument) app.ActiveDocument.Object(string.Empty);
            
                Helper helper=new Helper();
                
                var fileName = app.ActiveDocument.Name;
                var lineno = text.Selection.TopPoint.Line;
                var methodName = helper.GetCodeElementAtCursor();
                var lineText = string.Empty;

                var formatter = new Formatter();
                TxtTask.Text = formatter.GetMessage(text,fileName);


                //var selection = helper.GetCodeElementAtCursor();
                //if (!text.Selection.IsEmpty)
                //{
                //    lineText = string.Format("File Name : {0}", fileName);
                //    this.TxtTask.Text = lineText;
                //    lineText = string.Format("Method Name: {0}", methodName);
                //    this.TxtTask.AppendText(lineText);
                //    lineText = string.Format("Line Number : {0}", lineno);
                //    this.TxtTask.AppendText(Environment.NewLine);
                //    this.TxtTask.AppendText(lineText);
                //    this.TxtTask.AppendText(Environment.NewLine);
                //    lineText = string.Format("************************************************");
                //    this.TxtTask.AppendText(lineText);
                //    this.TxtTask.AppendText(Environment.NewLine);
                //    this.TxtTask.AppendText(text.Selection.Text);
                //    this.TxtTask.AppendText(Environment.NewLine);
                //    lineText = string.Format("************************************************");
                //    this.TxtTask.AppendText(lineText);
                //}
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "We are inside {0}.button1_Click()", this.ToString()),
                            "TrelloWindow");

        }
        public void CreateCheckBoxList()
        {
            TheList = new ObservableCollection<BoolStringClass>
            {
                new BoolStringClass {TheText = "EAST", TheValue = 1},
                new BoolStringClass {TheText = "WEST", TheValue = 2},
                new BoolStringClass {TheText = "NORTH", TheValue = 3},
                new BoolStringClass {TheText = "SOUTH", TheValue = 4}
            };
            this.DataContext = this;
        }
        private void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkZone = (CheckBox)sender;
            //ZoneText.Text = "Selected Zone Name= " + chkZone.Content.ToString();
            //ZoneValue.Text = "Selected Zone Value= " + chkZone.Tag.ToString();
        }

        private void MyControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            CreateCheckBoxList();
            LoadTextBox();
        }
    }
}