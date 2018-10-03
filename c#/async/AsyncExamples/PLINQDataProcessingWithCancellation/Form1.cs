using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLINQDataProcessingWithCancellation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                ProcessData();
            });
          }
        private void ProcessData()
        {
            int[] source = Enumerable.Range(1, 1000000).ToArray();

            // Split work between multiple processors using the .AsParallel () expansion method
            int[] modThreeIsZero = null;
            try { 
            modThreeIsZero = (from num in source.AsParallel().WithCancellation(cancellationTokenSource.Token) where num % 3 == 0 orderby num descending select num).ToArray();
            }
            catch (OperationCanceledException ex)
            {
                this.Invoke((Action)delegate
                {
                    this.Text = ex.Message;
                    cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                    cancellationTokenSource = new CancellationTokenSource();
                });
            }
            MessageBox.Show(string.Format("Found {0} numbers that match query!", modThreeIsZero.Length));


        }
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
        }
    }
}
