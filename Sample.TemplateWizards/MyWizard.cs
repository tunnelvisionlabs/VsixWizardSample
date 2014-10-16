namespace Sample.TemplateWizards
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using System.Xml;
    using System.Xml.Linq;
    using EnvDTE;
    using Microsoft.VisualStudio.TemplateWizard;

    public class MyWizard : IWizard
    {
        #region IWizard Members

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            string runSilent;
            if (replacementsDictionary.TryGetValue("$runsilent$", out runSilent) && bool.TrueString.Equals(runSilent, StringComparison.OrdinalIgnoreCase))
                return;

            string wizardData;
            if (!replacementsDictionary.TryGetValue("$wizarddata$", out wizardData))
                return;

            if (string.IsNullOrWhiteSpace(wizardData))
                return;

            string message;
            try
            {
                XDocument document = XDocument.Parse(wizardData);
                message = document.Root.Value;
            }
            catch (XmlException ex)
            {
                StringBuilder error = new StringBuilder();
                error.AppendLine("Could not parse WizardData element.");
                error.AppendLine();
                error.Append(ex);
                message = error.ToString();
            }

            MessageBox.Show(message);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        #endregion
    }
}
