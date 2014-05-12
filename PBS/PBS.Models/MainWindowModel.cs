using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using Common;
using Core;
using Models;

namespace PBS.Models
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Timer processingTimer = new Timer(ApplicationConstants.ProcessInterval);

        /// <summary>
        /// The core
        /// </summary>
        private PBSCore core;

        private List<AudioRecord> records;

        private int processingProgress = 0;

        /// <summary>
        /// Gets or sets the processing progress.
        /// </summary>
        public int ProcessingProgress 
        {
            get { return processingProgress; }
            set
            {
                processingProgress = value;
                NotifyPropertyChange("ProcessingProgress");
            } 
        }

        public List<AudioRecord> Records
        {
            get { return records; }
            set
            {
                records = value;
                NotifyPropertyChange("Records");
            }
        }

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

        protected void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ProcessNewRecords(object sender, ElapsedEventArgs e)
        {
            processingTimer.Stop();

            try
            {
                core.ProcessNewRecords(UpdateProcessingProgress);
            }
            catch (Exception exception)
            {
                //TODO:ProcessExceptions
            }

            processingTimer.Start();
        }

        private void UpdateProcessingProgress(object sender, ProgressChangedEventArgs e)
        {
            ProcessingProgress = e.ProgressPercentage;
        }
    }
}
