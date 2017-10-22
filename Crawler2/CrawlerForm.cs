using System;
using System.Collections.Generic;
using System.Windows.Forms;
using  Crawler2.BLL.Contracts;
using Crawler2.BLL.Services;
using System.ComponentModel.DataAnnotations;

namespace Crawler2
{
    public partial class CrawlerForm : Form
    {
        private ICrawler _crawler;

        public CrawlerForm()
        {
            InitializeComponent();

            _crawler = new Crawler();
        }

        private async void StartClick(object sender, EventArgs e)
        {
            FormStateWorking();

            int deep;
            try {
                deep = int.Parse(Deep.Text);
            }
            catch (Exception ex) {
                richTextBox1.Text = "Can't convert Deep field to the number";
                FormStateFree();
                return;
            }

            // when DownloadingGroupSize >>> and TimeLimitToDownload <<< than crawler works faster but quality is worse (next example works faster)
            // crawler.DownloadingGroupSize = 250;
            // crawler.TimeLimitToDownload = 5;

            List<string> result;
            try {
                result = await _crawler.StartAsync(Url.Text, deep, Word.Text);
            }
            catch (ValidationException ex) {
                richTextBox1.Text = ex.Message;
                FormStateFree();
                return;
            }

            PrintResult(result);
            FormStateFree();
        }

        private void PrintResult(List<string> result)
        {
            richTextBox1.Text += String.Format("Total: {0}\r\n", result.Count);
            foreach (var url in result) {
                richTextBox1.Text += String.Format("{0}\r\n", url); ;
            }
        }
        private void FormStateWorking()
        {
            Start.Enabled = false;
            richTextBox1.Clear();
        }

        private void FormStateFree()
        {
            Start.Enabled = true;
        }
    }
}
