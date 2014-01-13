using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using Common;
using Core;

namespace PBS.Models
{
    public class FirstRunGuideDialogueModel
    {
        private void AnalyzeExecute(object parameter)
        {
            PBSCore core = PBSCore.Instance;
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.ShowNewFolderButton = false;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = folderBrowserDialog.SelectedPath;
                    core.AnalyzeCollection(path);
                }
            }
        }

        public ICommand Analyze
        {
            get { return new RelayCommand(AnalyzeExecute); }
        }
    }
}
