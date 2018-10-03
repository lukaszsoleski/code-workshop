using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
namespace DataParallelismWithForEach
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource CancelToken = new CancellationTokenSource();

        public MainForm()
        {
            InitializeComponent();
        
        }

        private void ProcessImagesBtn_Click(object sender, EventArgs e)
        {
            //   ProcessFiles_StillBlocksMainThread();

            //Creates new thread. The UI is no longer blocked !
            Task.Factory.StartNew(() => ProcessFiles());
        } 
        private void ProcessFiles_StillBlocksMainThread()
        {
            //Load all .jpg images
            string[] files = Directory.GetFiles(@"C:\Users\Public\Pictures\Sample Pictures", "*.jpg");

            string newDir = @"C:\Users\Public\Pictures\Modified Pictures";
            Directory.CreateDirectory(newDir);

            // Distribute work between the largest possible number of processors by using threads pool 
             
            Parallel.ForEach(files, currentFile =>
            {
                string filename = Path.GetFileName(currentFile);

                using (Bitmap bitmap = new Bitmap(currentFile))
                {
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDir, filename));

                    //this.Text = string.Format("Processing {0} on thread {1}", filename, Thread.CurrentThread.ManagedThreadId);
                    //Give other threads access to the control
                    // Invoke - WinForms
                    // Dispather - WPF
                    this.Invoke((Action)delegate
                    {
                        this.Text = string.Format("Processing {0} on thread {1}", filename,Thread.CurrentThread.ManagedThreadId);

                    });

                }
            });
        }

        private void ProcessFiles()
        {

            // Add cancelation token and other parallel options
            ParallelOptions options = new ParallelOptions();
            options.CancellationToken = CancelToken.Token;
            options.MaxDegreeOfParallelism = System.Environment.ProcessorCount;





            //Load all .jpg images
            string[] files = Directory.GetFiles(@"C:\Users\Public\Pictures\Sample Pictures", "*.jpg");

            string newDir = @"C:\Users\Public\Pictures\Modified Pictures";
            Directory.CreateDirectory(newDir);
            try
            {
                // Distribute work between the largest possible number of processors by using threads pool 
                Parallel.ForEach(files, options, currentFile =>
                 {
                     options.CancellationToken.ThrowIfCancellationRequested();


                     string filename = Path.GetFileName(currentFile);

                     using (Bitmap bitmap = new Bitmap(currentFile))
                     {
                         bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                         bitmap.Save(Path.Combine(newDir, filename));

                    //this.Text = string.Format("Processing {0} on thread {1}", filename, Thread.CurrentThread.ManagedThreadId);
                    //Give other threads access to the control
                    // Invoke - WinForms
                    // Dispather - WPF
                    this.Invoke((Action)delegate
                         {
                             this.Text = string.Format("Processing {0} on thread {1}", filename, Thread.CurrentThread.ManagedThreadId);

                         });

                     }

                 });
                // All UI controls in this method must be invoked by the following delegate and cannot be changed directly because this method is running in different thread
                this.Invoke((Action)delegate
                {
                    this.Text = "Done";
                });

            }catch(OperationCanceledException ex)
            {
                this.Invoke((Action)delegate
                {
                    this.Text = ex.Message;
                });
                CancelToken.Dispose();
                CancelToken = null;
                CancelToken = new CancellationTokenSource();
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            CancelToken.Cancel();
        }
    }
}
