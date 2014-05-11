using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        protected DSPFactory factory = new DSPFactory();
        protected int itemsCount;
        private readonly Queue<AudioRecord> recordsQueue = new Queue<AudioRecord>();

        /// <summary>
        /// Requested bitrate of signal after decoding
        /// </summary>
        public float targetBitRate = 24000;

        /// <summary>
        /// Processes the specified records.
        /// </summary>
        /// <param name="records">The records.</param>
        public void Process(ICollection<AudioRecord> records)
        {
            if (records.Count == 0)
                return;

            lock (recordsQueue)
                foreach (var item in records)
                    recordsQueue.Enqueue(item);

            itemsCount += records.Count;

            using (var decoder = factory.CreateAudioDecoder())
            {
                if (decoder.AllowsMultiThreading)
                {
                    ProcessMultiThreading(decoder);
                }
                else
                {
                    Process(decoder);
                }
            }

        }

        private void ProcessMultiThreading(IAudioDecoder decoder)
        {
            var threads = new List<Thread>();

            var threadCount = 1;
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
            int counter = 0;
            AudioRecord item;
            while ((item = GetItemFromQueue()) != null)
                try
                {
                    counter++;
                    //decode audio source to samples and mp3 tags extracting
                    AudioInfo info = null;
                    using (var stream = item.GetSourceStream())
                        info = decoder.Decode(stream, targetBitRate, item.GetSourceExtension());

                    //normalize volume level
                    info.Samples.Normalize();

                    //launch sample processors
                    foreach (var processor in factory.CreateSampleProcessors())
                        try
                        {
                            processor.Process(item, info);
                        }
                        catch (Exception E)
                        {

                        }

                    //OnProgress(new ProgressChangedEventArgs(100 * (itemsCount - recordsQueue.Count) / itemsCount, null));
                    item.State = RecordState.Processed;
                }
                catch (Exception E)
                {
                    item.State = RecordState.Bad;
                }
        }
        protected virtual AudioRecord GetItemFromQueue()
        {
            lock (recordsQueue)
            {
                if (recordsQueue.Count > 0)
                {
                    return recordsQueue.Dequeue();
                }
            }

            return null;
        }
    }
}
