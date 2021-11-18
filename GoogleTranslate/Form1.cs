﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace GoogleTranslate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string InputLanguage = "auto"; // tự động phát hiện ngôn ngữ
        private string OutputLanguage = "en";
        private string[] ListLanguge = { "vi", "en", "fr" };

        public string TranslateText(string input)
        {
            string url = String.Format
            ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
             InputLanguage, OutputLanguage, Uri.EscapeUriString(input));
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync(url).Result;
            var jsonData = new JavaScriptSerializer().Deserialize<List<dynamic>>(result);
            var translationItems = jsonData[0];
            string translation = "";
            foreach (object item in translationItems)
            {
                IEnumerable translationLineObject = item as IEnumerable;
                IEnumerator translationLineString = translationLineObject.GetEnumerator();
                translationLineString.MoveNext();
                translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
            }
            if (translation.Length > 1) { translation = translation.Substring(1); };
            return translation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Đầu vào không rỗng
            if(textBox1.Text != "")
            {
                textBox2.Text = TranslateText(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // chuyển đổi ngôn ngữ ra
            if (OutputLanguage.Equals("en"))
            {
                OutputLanguage = "vi";
            }
            else
            {
                OutputLanguage = "en";
            }
            //đổi ngôn ngữ dịch (Label)
            string temp = label1.Text;
            label1.Text = label2.Text;
            label2.Text = temp;
            //dịch lại
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string temp2 = textBox1.Text;
                textBox1.Text = textBox2.Text;
                textBox2.Text = temp2;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //vị trí bật form ở giữa
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }
    }
}
