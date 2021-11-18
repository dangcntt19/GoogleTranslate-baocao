using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleTranslate
{
    class Languages
    {
        private string[] NameLanguage = { "Auto Languages", "Afrikaans", "Albanian", "Amharic", "Arabic", "Armenian", "Azerbaijani", "Basque", "Belarusian", "Bengali", "Bosnian", "Bulgarian", "Catalan", "Cebuano", "Chichewa", "Chinese Simplified", "Chinese Traditional", "Corsican", "Croatian", "Czech", "Danish", "Dutch", "English", "Esperanto", "Estonian", "Filipino", "Finnish", "French", "Frisian", "Galician", "Georgian", "German", "Greek", "Vietnamese", "Japanese", "Javanese" };
        private string[] KeyLanguage = { "auto", "af", "sq", "am", "ar", "hy", "az", "eu", "be", "bn", "bs", "bg", "ca", "ceb", "ny", "zh-cn", "zh-tw", "co", "hr", "cs", "da", "nl", "en", "eo", "et", "tl", "fi", "fr", "fy", "gl", "ka", "de", "el", "vi","ja","jw" };

        private void GetLanguages(string[] languages,string[] keyLanguage)
        {
            languages = NameLanguage;
            keyLanguage = KeyLanguage;
        }

        public string[] GetNameLanguages()
        {
            //  danh sách tên các ngôn ngữ
            return NameLanguage;
        }

        public string GetKeyLanguages(string NameLanguages)
        {
            //trả về key ngôn ngữ truyền vào để dịch
            int index = Array.IndexOf(NameLanguage, NameLanguages);

            return index >= 0 ? KeyLanguage[index] : null;
        }

    }
}
