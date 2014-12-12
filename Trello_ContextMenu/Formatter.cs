using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Navigation;


namespace Inayathullah.Trello_ContextMenu
{
    public class Formatter
    {
        private Helper helper = null;
        public Formatter()
        {
                helper=new Helper();
        }
        public string GetMessage(EnvDTE.TextDocument document,string fileName)
        {
            var lineText = new StringBuilder();
            var methodName = helper.GetCodeElementAtCursor();
            var lineno = document.Selection.TopPoint.Line;
            if (!string.IsNullOrEmpty(document.Selection.Text))
            {
                lineText.Append(string.Format("File Name {0}   :" , fileName)).AppendLine();
                lineText.Append(string.Format("Method Name {0} :", methodName)).AppendLine();
                lineText.Append(string.Format("Line Number {0} :", lineno)).AppendLine();
                lineText.Append(string.Format("************************************************")).AppendLine();
                lineText.Append(document.Selection.Text).AppendLine();
                lineText.Append(string.Format("************************************************")).AppendLine();
            }
            return lineText.ToString();
        }
    }
}
