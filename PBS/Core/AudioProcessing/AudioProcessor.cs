using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Models;

namespace Core.AudioProcessing
{
    /// <summary>
    /// Decodes audio sources, calculates characteristics of signal.
    /// </summary>
    public class AudioProcessor
    {
        private readonly Queue<AudioRecord> recordsQueue = new Queue<AudioRecord>();

        /// <summary>
        /// Processes the specified records.
        /// </summary>
        /// <param name="records">The records.</param>
        public void Process(ICollection<AudioRecord> records)
        {
            lock (recordsQueue)
            {
                foreach (AudioRecord record in records)
                {
                    recordsQueue.Enqueue(record);
                }
            }

            AudioDecoder decoder = new AudioDecoder();
            ProcessMultiThreading(decoder);

        }

        private void ProcessMultiThreading(IAudioDecoder decoder)
        {
            var threads = new List<Thread>();

            var threadCount = Environment.ProcessorCount;
            for (int i = 0; i < threadCount; i++)
            {
                var t = new Thread(() => Process(decoder)) { IsBackground = true };
                t.Start();
                threads.Add(t);
            }

            while (recordsQueue.Count > 0)
            {
                Thread.Sleep(100);
            }

            foreach (var t in threads)
            {
                t.Join();
            }
        }

        private void Process(IAudioDecoder decoder)
        {
        }
    }
}
