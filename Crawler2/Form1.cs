using System;
using System.Linq;
using System.Windows.Forms;

namespace Crawler2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            richTextBox1.Text = "";

            Crawler crawler = new Crawler(Word.Text, int.Parse(Deep.Text));

            // when DownloadingGroupSize >>> and TimeLimitToDownload <<< than crawler works faster but quality is worse (next example works faster)
            // crawler.DownloadingGroupSize = 250;
            // crawler.TimeLimitToDownload = 5;

            var result = await crawler.StartAsync(Url.Text);

            richTextBox1.Text += "Total: " + result.Count() + "\r\n";
            foreach (var url in result) {
                richTextBox1.Text += url + "\r\n";
            }

            button1.Enabled = true;
        }
    }
}
