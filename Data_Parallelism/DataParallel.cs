using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Data_Parallelism
{
    public partial class DataParallel : Form
    {
        private readonly string picturesPath = @"C:\Documents and Settings\Marcus\My Documents\My Pictures";
        private readonly string newDir = @"C:\Modified";

        private CancellationTokenSource cancelToken = new CancellationTokenSource();


        public DataParallel()
        {
            InitializeComponent();
        }

        private void btnProcessImages_Click(object sender, EventArgs e)
        {
            if (checkBoxAsync.Checked)
                Task.Factory.StartNew(() =>
                    {
                        ProcessFilesASync();
                    });
            else
                ProcessFilesSync();
        }

        private void ProcessFilesSync()
        {
            string[] files = Directory.GetFiles(picturesPath, "*.jpg");
           
            Directory.CreateDirectory(newDir);

            foreach (string currentFile in files)
            {
                string fileName = Path.GetFileName(currentFile);

                using (Bitmap bmp = new Bitmap(currentFile))
                {
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipX);
                    bmp.Save(Path.Combine(newDir, fileName));
                    Thread.Sleep(500);
                    this.Text = string.Format("Processing {0} on thread {1}", fileName,
Thread.CurrentThread.ManagedThreadId);
                }

            }

        }

        private void ProcessFilesASync()
        {
            ParallelOptions parOptions = new ParallelOptions
            {
                CancellationToken = cancelToken.Token,
                MaxDegreeOfParallelism = System.Environment.ProcessorCount
            };

            string[] files = Directory.GetFiles(picturesPath, "*.jpg");
            Directory.CreateDirectory(newDir);
            try
            {

                Parallel.ForEach(files, parOptions, (filename) =>
                    {
                        parOptions.CancellationToken.ThrowIfCancellationRequested();
                        using (Bitmap bmp = new Bitmap(filename))
                        {
                            string currentFile = Path.GetFileName(filename);
                            bmp.RotateFlip(RotateFlipType.Rotate90FlipY);
                            bmp.Save(Path.Combine(newDir, currentFile));
                              Thread.Sleep(500);
                            textBox1.Invoke((Action)delegate
                            {
                                textBox1.AppendText(string.Format("Processing {0} on thread {1}",
                                    currentFile, Thread.CurrentThread.ManagedThreadId));
                            });
                        }
                    });
            }
            catch (OperationCanceledException ex)
            {
                textBox1.Invoke((Action)delegate
                {
                    textBox1.AppendText("\n");
                    textBox1.AppendText(ex.Message);
                });
             }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancelToken.Cancel();
        }
    }
}
