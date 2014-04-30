using System;
using System.Timers;
using Common;
using Core;

namespace PBS.Models
{
    public class MainWindowModel
    {
        private readonly Timer processingTimer = new Timer(ApplicationConstants.ProcessInterval);

        /// <summary>
        /// The core
        /// </summary>
        private PBSCore core;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowModel"/> class.
        /// </summary>
        public MainWindowModel()
        {
            core = PBSCore.Instance;

            processingTimer.Elapsed += ProcessNewRecords;
            processingTimer.Start();
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            core.SaveChanges();
        }

        private void ProcessNewRecords(object sender, ElapsedEventArgs e)
        {
            processingTimer.Stop();

            try
            {
                core.ProcessNewRecords();
            }
            catch (Exception exception)
            {
                //TODO:ProcessExceptions
            }

            processingTimer.Start();
        }
    }
}
