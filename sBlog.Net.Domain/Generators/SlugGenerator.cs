#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.
// See the License.txt file for more information.

/* *********************************************** */

#endregion Disclaimer/License Info

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace sBlog.Net.Domain.Generators
{
    public static class SlugGenerator
    {
        public static string GetUniqueSlug(this string srcString, List<string> allItems)
        {
            srcString = StringUtil.RemoveVietnameseSigns(srcString);

            var regex = new Regex(@"[^a-zA-Z 0-9\.\-]+");

            var slug = regex.Replace(srcString.ToLower(), string.Empty);

            slug = ReplaceMatches(slug, @"[ ]{2,}").Replace(" ", "-");
            slug = ReplaceMatches(slug, @"[\.]{2,}").Replace(".", "-");
            slug = ReplaceMatches(slug, @"[\-]{2,}");

            if (slug.StartsWith("-") && !slug.EndsWith("-"))
                slug = string.Format("0{0}", slug);

            if (!slug.StartsWith("-") && slug.EndsWith("-"))
                slug = string.Format("{0}0", slug);

            return allItems.Any(s => s == slug) ? GetUniqueSlugInternal(slug, allItems)
                                                : slug;
        }

        private static string ReplaceMatches(string srcString, string pattern)
        {
            var removalPattern = new Regex(pattern);
            return removalPattern.Replace(srcString, "-");
        }

        private static string GetUniqueSlugInternal(string srcString, List<string> srcList)
        {
            var slugRegex = new Regex(string.Format(@"^{0}-([0-9]+)$", srcString));
            var matchingSlugs = new List<int>();
            srcList.ForEach(s =>
            {
                var match = slugRegex.Match(s);
                if (match.Success)
                {
                    var number = int.Parse(match.Groups[1].Captures[0].Value);
                    matchingSlugs.Add(number);
                }
            });
            if (matchingSlugs.Any())
            {
                var max = matchingSlugs.Max();
                return string.Format("{0}-{1}", srcString, max + 1);
            }
            return string.Format("{0}-2", srcString);
        }
    }

    public static class StringUtil
    {
        private static readonly string[] VietnameseSigns = new string[]
         {
          "aAeEoOuUiIdDyY",
          "áàạảãâấầậẩẫăắằặẳẵ",
          "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
          "éèẹẻẽêếềệểễ",
          "ÉÈẸẺẼÊẾỀỆỂỄ",
          "óòọỏõôốồộổỗơớờợởỡ",
          "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
          "úùụủũưứừựửữ",
          "ÚÙỤỦŨƯỨỪỰỬỮ",
          "íìịỉĩ",
          "ÍÌỊỈĨ",
          "đ",
          "Đ",
          "ýỳỵỷỹ",
          "ÝỲỴỶỸ"
         };

        /// <summary>
        /// Bỏ dấu tiếng Việt
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveVietnameseSigns(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        public static string RemoveVietnameseSign(string vietnamString)
        {
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = vietnamString.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}