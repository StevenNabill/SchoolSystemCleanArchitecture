using System.Globalization;

namespace SchoolProject.Data.Common
{
    public class GeneralLocalizableEntity
    {
        public string Localize(string NameAr, string NameEn)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            return culture.TwoLetterISOLanguageName.ToLower() switch
            {
                "ar" => NameAr,
                _ => NameEn
            };
        }
    }
}
