using DNSLab.Web.DTOs.Repositories.Record;
using MD.PersianDateTime.Standard;
using System.Text;
using System.Text.Json;

namespace DNSLab.Web.Extensions
{
    public static class GeneralExtensions
    {
        public static string ToReadableSize(this long? bytes)
        {
            if (bytes is null)
                return String.Empty;

            const int scale = 1024;

            string[] suffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

            if (bytes <= 0) return "0B";

            int i = 0;
            double dblBytes = bytes.Value;
            while (Math.Round(dblBytes) >= scale && i < suffixes.Length - 1)
            {
                dblBytes /= scale;
                i++;
            }

            return $"{dblBytes:N2}{suffixes[i]}";
        }

        public static string Separate3Digits(this long value) => value.ToString("N0");

        public static string Separate3Digits(this int value) => Separate3Digits((long)value);

        public static string ToPersianDateTime(this DateTime value) => new PersianDateTime(value).ToString();
        public static string ToPersianDate(this DateTime value) => new PersianDateTime(value).ToShortDateString();

        public static T? CastTo<T>(this BaseRecordDTO record) where T : BaseRecordDataDTO
        {
            return JsonSerializer.Deserialize<T>((JsonElement)record.RData, options: new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public static T Clone<T>(this T obj)
        {
            var inst = obj.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            return (T)inst?.Invoke(obj, null)!;
        }

        public static string PrettyJson(this string? unPrettyJson)
        {
            if (String.IsNullOrEmpty(unPrettyJson))
            {
                return String.Empty;
            }

            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

            return JsonSerializer.Serialize(jsonElement, options);
        }

        static Dictionary<char, char> LettersDictionaryFaToEn = new Dictionary<char, char>() {
            {'۰', '0'},
            {'۱', '1'},
            {'۲', '2'},
            {'۳', '3'},
            {'۴', '4'},
            {'۵', '5'},
            {'۶', '6'},
            {'۷', '7'},
            {'۸', '8'},
            {'۹', '9'}
        };

        static Dictionary<char, char> LettersDictionaryEnToFa = new Dictionary<char, char>() {
            {'0', '۰'},
            {'1', '۱'},
            {'2', '۲'},
            {'3', '۳'},
            {'4', '۴'},
            {'5', '۵'},
            {'6', '۶'},
            {'7', '۷'},
            {'8', '۸'},
            {'9', '۹'}
        };

        public static string PersianToEnglishNumbers(this string input)
        {
            StringBuilder sb = new StringBuilder(input);
            foreach (var item in input.Where(x => LettersDictionaryFaToEn.Keys.Contains(x)))
                sb.Replace(item, LettersDictionaryFaToEn[item]);
            return sb.ToString();
        }

        public static string EnglishToPersianNumbers(this string input)
        {
            StringBuilder sb = new StringBuilder(input);
            foreach (var item in input.Where(x => LettersDictionaryEnToFa.Keys.Contains(x)))
                sb.Replace(item, LettersDictionaryEnToFa[item]);
            return sb.ToString();
        }

        public static string EnglishToPersianNumbers(this int? input)
        {
            if (input == null) return String.Empty;
            return EnglishToPersianNumbers(input.Value.ToString());
        }

        public static string EnglishToPersianNumbers(this long input)
        {
            if (input == null) return String.Empty;
            return EnglishToPersianNumbers(input.ToString());
        }

    }
}
