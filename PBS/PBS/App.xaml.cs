using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Core;

namespace PBS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            PBSCore core = PBSCore.Instance;

            if (core.IsCollectionEmpty)
            {
                ShutdownMode = ShutdownMode.OnExplicitShutdown;
                FirstRunGuideDialogue dialogue = new FirstRunGuideDialogue();
                dialogue.ShowDialog();
                ShutdownMode = ShutdownMode.OnMainWindowClose;
            }
        }
    }
}
