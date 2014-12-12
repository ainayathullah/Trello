using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;

namespace Inayathullah.Trello_ContextMenu
{
    public class Helper
    {
        public string GetCodeElementAtCursor()
        {
            var result = string.Empty;
            EnvDTE.CodeElement objCodeElement = default(EnvDTE.CodeElement);
            EnvDTE.TextPoint objCursorTextPoint = default(EnvDTE.TextPoint);
            var app = (EnvDTE.DTE)Trello_ContextMenuPackage.GetGlobalService(typeof(SDTE));
            try
            {
                objCursorTextPoint = GetCursorTextPoint();

                if ((objCursorTextPoint != null))
                {
                    // Get the class at the cursor
                    objCodeElement = GetCodeElementAtTextPoint(vsCMElement.vsCMElementFunction, app.ActiveDocument.ProjectItem.FileCodeModel.CodeElements, objCursorTextPoint);
                }

                if (objCodeElement == null)
                {
                    result = "No class found at the cursor!";
                }
                else
                {
                    result = objCodeElement.FullName;
                }

            }
            catch (System.Exception ex)
            {
                
            }
            return result;
        }

        private EnvDTE.TextPoint GetCursorTextPoint()
        {
            var app = (EnvDTE.DTE)Trello_ContextMenuPackage.GetGlobalService(typeof(SDTE));
            EnvDTE.TextDocument objTextDocument = default(EnvDTE.TextDocument);
            EnvDTE.TextPoint objCursorTextPoint = default(EnvDTE.TextPoint);

            try
            {
                objTextDocument = (EnvDTE.TextDocument) app.ActiveDocument.Object(string.Empty);
                objCursorTextPoint = objTextDocument.Selection.ActivePoint;
            }
            catch (System.Exception ex)
            {
            }

            return objCursorTextPoint;

        }

        private EnvDTE.CodeElement GetCodeElementAtTextPoint(EnvDTE.vsCMElement eRequestedCodeElementKind,
            EnvDTE.CodeElements colCodeElements, EnvDTE.TextPoint objTextPoint)
        {

            //EnvDTE.CodeElement objCodeElement = default(EnvDTE.CodeElement);
            EnvDTE.CodeElement objResultCodeElement = default(EnvDTE.CodeElement);
            EnvDTE.CodeElements colCodeElementMembers = default(EnvDTE.CodeElements);
            EnvDTE.CodeElement objMemberCodeElement = default(EnvDTE.CodeElement);


            if ((colCodeElements != null))
            {

                foreach (EnvDTE.CodeElement objCodeElement in colCodeElements)
                {

                    if (objCodeElement.StartPoint.GreaterThan(objTextPoint))
                    {
                        // The code element starts beyond the point


                    }
                    else if (objCodeElement.EndPoint.LessThan(objTextPoint))
                    {
                        // The code element ends before the point

                        // The code element contains the point
                    }
                    else
                    {

                        if (objCodeElement.Kind == eRequestedCodeElementKind)
                        {
                            // Found
                            objResultCodeElement = objCodeElement;
                        }

                        // We enter in recursion, just in case there is an inner code element that also 
                        // satisfies the conditions, for example, if we are searching a namespace or a class
                        colCodeElementMembers = GetCodeElementMembers(objCodeElement);

                        objMemberCodeElement = GetCodeElementAtTextPoint(eRequestedCodeElementKind,
                            colCodeElementMembers, objTextPoint);

                        if ((objMemberCodeElement != null))
                        {
                            // A nested code element also satisfies the conditions
                            objResultCodeElement = objMemberCodeElement;
                        }

                        break; // TODO: might not be correct. Was : Exit For

                    }

                }

            }

            return objResultCodeElement;

        }

        private EnvDTE.CodeElements GetCodeElementMembers(CodeElement objCodeElement)
        {

            EnvDTE.CodeElements colCodeElements = default(EnvDTE.CodeElements);


            if (objCodeElement is EnvDTE.CodeNamespace)
            {
                colCodeElements = ((EnvDTE.CodeNamespace) objCodeElement).Members;


            }
            else if (objCodeElement is EnvDTE.CodeType)
            {
                colCodeElements = ((EnvDTE.CodeType) objCodeElement).Members;


            }
            else if (objCodeElement is EnvDTE.CodeFunction)
            {
                colCodeElements = ((EnvDTE.CodeFunction) objCodeElement).Parameters;

            }

            return colCodeElements;

        }
    }
}
