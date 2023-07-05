using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
namespace Utils.NewLocalization
{
    public class LocalizationTranslate
    {
        private Languages languages;
        //private readonly LocalizationString localizationString;
        //private EntryLocalization entry;

        private CultureInfo cultureInfo;
        public enum StringFormat { None, AllUpcase, AllDownCase, FirstLetterUp, AllFirstLetterUp }

        public LocalizationTranslate(Languages languages)
        {
            this.languages = languages;
            this.cultureInfo = new CultureInfo(this.languages.language);

        }
        public string Translate(LocalizationString localizationString, StringFormat format = StringFormat.None, int qnt = 1)
        {
            return Translate(localizationString, qnt, format);
        }
        public string Translate(LocalizationString localizationString, int qnt = 1, StringFormat format = StringFormat.None)
        {
            return GetStringFormat(GetText(localizationString, qnt), format);
        }
        private string GetText(LocalizationString localizationString, int qnt = 1)
        {
            EntryLocalization entry = GetEntry(localizationString);
            if (entry != null && entry.entrys.Length > 0)
            {
                int plural = PluralForm(entry.language.pluralForm, qnt);
                if (string.IsNullOrEmpty(entry.entrys[plural]))
                    return GetDefaultEntry(localizationString.listEntry).entrys[0];
                return entry.entrys[plural];
            }
            else
                return GetDefaultEntry(localizationString.listEntry).entrys[0];
        }

        private EntryLocalization GetEntry(LocalizationString localizationString)
        {
            if (localizationString != null)
            {
                EntryLocalization entry = localizationString.listEntry.Find(x => x.language.Equals(this.languages));
                if (entry == null)
                    return GetDefaultEntry(localizationString.listEntry);
                else
                    return entry;
            }
            else
                return GetDefaultEntry(localizationString.listEntry);
        }

        private EntryLocalization GetDefaultEntry(List<EntryLocalization> list)
        {
            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        #region Internal Methods
        public string GetStringFormat(string entry, StringFormat stringFormat)
        {
            switch (stringFormat)
            {
                case StringFormat.None:
                    return entry;
                case StringFormat.AllUpcase:
                    return entry.ToUpper();
                case StringFormat.AllDownCase:
                    return entry.ToLower();
                case StringFormat.FirstLetterUp:
                    return Utils.Tools.FirstLetterToUpper(entry);
                case StringFormat.AllFirstLetterUp:
                    return cultureInfo.TextInfo.ToTitleCase(entry);
                default:
                    return entry;
            }
        }
        public int PluralForm(string pluralForm, int n)
        {
            int plural = 0;
            switch (pluralForm)
            {
                case "0":
                    return 0;
                case "(n > 1)":
                    return plural = (n > 1) ? 1 : 0;
                case "(n != 1)":
                    return plural = (n != 1) ? 1 : 0;
                case "(n==0 ? 0 : n==1 ? 1 : n==2 ? 2 : n%100>=3 && n%100<=10 ? 3 : n%100>=11 ? 4 : 5)":
                    return plural = (n == 0) ? 0 : (n == 1) ? 1 : (n == 2) ? 2 : (n % 100 >= 3 && n % 100 <= 10) ? 3 : (n % 100 >= 11) ? 4 : 5;
                case "(n%10==1 && n%100!=11 ? 0 : n%10>=2 && n%10<=4 && (n%100<10 || n%100>=20) ? 1 : 2)":
                    return plural = (n % 10 == 1 && n % 100 != 11) ? 0 : (n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20)) ? 1 : 2;
                case "(n==1) ? 0 : (n>=2 && n<=4) ? 1 : 2":
                    return plural = (n == 1) ? 0 : (n >= 2 && n <= 4) ? 1 : 2;
                case "(n==1) ? 0 : n%10>=2 && n%10<=4 && (n%100<10 || n%100>=20) ? 1 : 2":
                    return plural = (n == 1) ? 0 : (n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20)) ? 1 : 2;
                case "(n==1) ? 0 : (n==2) ? 1 : (n != 8 && n != 11) ? 2 : 3":
                    return plural = (n == 1) ? 0 : (n == 2) ? 1 : (n != 8 && n != 11) ? 2 : 3;
                case "n==1 ? 0 : n==2 ? 1 : (n>2 && n<7) ? 2 :(n>6 && n<11) ? 3 : 4":
                    return plural = (n == 1) ? 0 : (n == 2) ? 1 : (n > 2 && n < 7) ? 2 : (n > 6 && n < 11) ? 3 : 4;
                case "(n==1 || n==11) ? 0 : (n==2 || n==12) ? 1 : (n > 2 && n < 20) ? 2 : 3":
                    return plural = (n == 1 || n == 11) ? 0 : (n == 2 || n == 12) ? 1 : (n > 2 && n < 20) ? 2 : 3;
                case "(n%10!=1 || n%100==11)":
                    return plural = (n % 10 != 1 || n % 100 == 11) ? 1 : 0;
                case "(n==1) ? 0 : (n==2) ? 1 : (n == 3) ? 2 : 3":
                    return plural = (n == 1) ? 0 : (n == 2) ? 1 : (n == 3) ? 2 : 3;
                case "(n%10==1 && n%100!=11 ? 0 : n%10>=2 && (n%100<10 || n%100>=20) ? 1 : 2)":
                    return plural = (n % 10 == 1 && n % 100 != 11) ? 0 : (n % 10 >= 2 && (n % 100 < 10 || n % 100 >= 20)) ? 1 : 2;
                case "(n%10==1 && n%100!=11 ? 0 : n != 0 ? 1 : 2)":
                    return plural = (n % 10 == 1 && n % 100 != 11) ? 0 : (n != 0) ? 1 : 2;
                case "n%10==1 && n%100!=11 ? 0 : n%10>=2 && n%10<=4 && (n%100<10 || n%100>=20) ? 1 : 2":
                    return plural = (n % 10 == 1 && n % 100 != 11) ? 0 : (n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20)) ? 1 : 2;
                case "n==1 || n%10==1 ? 0 : 1":
                    return plural = (n == 1 || n % 10 == 1) ? 0 : 1;
                case "(n==0 ? 0 : n==1 ? 1 : 2)":
                    return plural = (n == 0) ? 0 : (n == 1) ? 1 : 2;
                case "(n==1 ? 0 : n==0 || ( n%100>1 && n%100<11) ? 1 : (n%100>10 && n%100<20 ) ? 2 : 3)":
                    return plural = (n == 1) ? 0 : (n == 0 || (n % 100 > 1 && n % 100 < 11)) ? 1 : (n % 100 > 10 && n % 100 < 20) ? 2 : 3;
                case "(n==1 ? 0 : n%10>=2 && n%10<=4 && (n%100<10 || n%100>=20) ? 1 : 2)":
                    return plural = (n == 1) ? 0 : (n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20)) ? 1 : 2;
                case "(n==1 ? 0 : (n==0 || (n%100 > 0 && n%100 < 20)) ? 1 : 2)":
                    return plural = (n == 1) ? 0 : (n == 0 || (n % 100 > 0 && n % 100 < 20)) ? 1 : 2;
                case "(n%100==1 ? 0 : n%100==2 ? 1 : n%100==3 || n%100==4 ? 2 : 3)":
                    return plural = (n % 100 == 1) ? 0 : (n % 100 == 2) ? 1 : (n % 100 == 3 || n % 100 == 4) ? 2 : 3;
                default:
                    return plural;
            }
        }
        #endregion
    }
}
