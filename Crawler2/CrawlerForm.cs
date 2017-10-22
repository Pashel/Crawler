using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Crawler2.BLL.Contracts;
using Crawler2.BLL.Services;
using Crawler2.Extensions;

namespace Crawler2
{
    public partial class CrawlerForm : Form
    {
        private ICrawler _crawler;

        public CrawlerForm()
        {
            InitializeComponent();

            _crawler = new Crawler(new BLL.Services.Validator());
        }

        private async void StartClick(object sender, EventArgs e)
        {
            FormStateWorking();

            var deep = Deep.Text.ToInt();
            if(!deep.HasValue) {
                SetError("Can't convert Deep field to the number");
                return;
            }

            var timeLimitToDownload = ConfigurationManager.AppSettings["TimeLimitToDownload"].ToInt();
            var downloadingGroupSize = ConfigurationManager.AppSettings["DownloadingGroupSize"].ToInt();

            List<string> result;
            try {
                if (timeLimitToDownload.HasValue) {
                    _crawler.TimeLimitToDownload = timeLimitToDownload.Value;
                }
                if (downloadingGroupSize.HasValue) {
                    _crawler.DownloadingGroupSize = downloadingGroupSize.Value;
                }

                result = await _crawler.StartAsync(Url.Text, deep.Value, Word.Text);
            }
            catch (ValidationException ex) {
                SetError(ex.Message);
                return;
            }

            PrintResult(result);
            FormStateFree();
        }

        private void PrintResult(List<string> result)
        {
            OutputBox.Text += String.Format("Total: {0}\r\n", result.Count);
            foreach (var url in result) {
                OutputBox.Text += String.Format("{0}\r\n", url); ;
            }
        }

        private void SetError(string message)
        {
            OutputBox.Text = message;
            FormStateFree();
        }

        private void FormStateWorking()
        {
            Start.Enabled = false;
            OutputBox.Clear();
        }

        private void FormStateFree()
        {
            Start.Enabled = true;
        }
    }
}
