using System;
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
            if (!OutputLanguage.Equals(InputLanguage))
            {
                // hoán đổi key
                string tmp = InputLanguage;
                InputLanguage = OutputLanguage;
                OutputLanguage = tmp;
                //hoán đổi vị trí select trong combo box
                int tmpIndex = comboBox1.SelectedIndex;
                comboBox1.SelectedIndex = ((comboBox2.SelectedIndex - 1 ) < 0 ? 32 : comboBox2.SelectedIndex - 1 );
                comboBox2.SelectedIndex = tmpIndex + 1;
                //dịch lại ngôn ngữ mới
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    string TempTranslate = textBox1.Text;
                    textBox1.Text = textBox2.Text;
                    textBox2.Text = TempTranslate;
                }
            }           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //vị trí bật form ở giữa
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

            //add ngôn ngữ vào combobox
            string[] NameLanguages = (new Languages()).GetNameLanguages();
            for(int i = 0; i < NameLanguages.Length; i++)
            {
                if(i != 0)
                    comboBox1.Items.Add(NameLanguages[i]);
                comboBox2.Items.Add(NameLanguages[i]);
            }

            comboBox1.SelectedIndex = 21 ;
            comboBox2.SelectedIndex = 0;
            //không cho sửa chữa 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox2.Text = TranslateText(textBox1.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OutputLanguage = (new Languages()).GetKeyLanguages(comboBox1.SelectedItem.ToString());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputLanguage = (new Languages()).GetKeyLanguages(comboBox2.SelectedItem.ToString());
        }
    }
}
