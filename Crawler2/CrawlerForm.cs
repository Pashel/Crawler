using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Crawler2.BLL.Services;

namespace Crawler2
{
    public partial class CrawlerForm : Form
    {
        public CrawlerForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Start();

            Crawler crawler = new Crawler(Word.Text, int.Parse(Deep.Text));

            // when DownloadingGroupSize >>> and TimeLimitToDownload <<< than crawler works faster but quality is worse (next example works faster)
            // crawler.DownloadingGroupSize = 250;
            // crawler.TimeLimitToDownload = 5;

            var result = await crawler.StartAsync(Url.Text);

            Finish(result);
        }

        private void Start()
        {
            button1.Enabled = false;
            richTextBox1.Text = "";
        }

        private void Finish(List<string> result)
        {
            richTextBox1.Text += String.Format("Total: {0}\r\n", result.Count);
            foreach (var url in result) {
                richTextBox1.Text += String.Format("{0}\r\n", url); ;
            }
            button1.Enabled = true;
        }
    }
}
