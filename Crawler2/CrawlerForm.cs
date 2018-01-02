using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Windows.Forms;
using Crawler2.BLL.Contracts;
using Crawler2.BLL.Services;
using Crawler2.Extensions;
using Validator = Crawler2.BLL.Services.Validator;
using Unity;

namespace Crawler2
{
    public partial class CrawlerForm : Form
    {
        private ICrawler _crawler;
        private UnityContainer _container;

        public CrawlerForm(UnityContainer container)
        {
            InitializeComponent();
            _container = container;
            //_crawler = _container.Resolve<ICrawler>();
            _crawler = new Crawler(new HttpClientWrapper(), new Validator(), new PageParser());
        }

        private async void StartClick(object sender, EventArgs e)
        {
            FormStateWorking();

            var deep = Deep.Text.ToInt();
            if (!deep.HasValue) {
                SetError("Can't convert Deep field to the number");
                return;
            }

            var timeLimitToDownload = ConfigurationManager.AppSettings["TimeLimitToDownload"].ToInt();
            var downloadingGroupSize = ConfigurationManager.AppSettings["DownloadingGroupSize"].ToInt();

            List<string> result;
            try {
                if (timeLimitToDownload.HasValue) _crawler.TimeLimitToDownload = timeLimitToDownload.Value;
                if (downloadingGroupSize.HasValue) _crawler.DownloadingGroupSize = downloadingGroupSize.Value;

                result = await _crawler.StartAsync(Url.Text, deep.Value, Word.Text);
            }
            catch (ValidationException ex) {
                SetError(ex.Message);
                return;
            }

            ShowResult(result);
        }

        private void ShowResult(List<string> result)
        {
            OutputBox.Text += $"Total: {result.Count}\r\n";
            foreach (var url in result) {
                OutputBox.Text += $"{url}\r\n";
            }
            FormStateFree();
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