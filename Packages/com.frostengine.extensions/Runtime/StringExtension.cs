using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Frost
{
	public static class StringExtension
	{
		public static IEnumerable<string> CustomSort(this IEnumerable<string> _list)
		{
			int maxLen = _list.Select(s => s.Length).Max();

			return _list.Select(s => new
				{
					OrgStr = s,
					SortStr = Regex.Replace(s, @"(\d+)|(\D+)", m => m.Value.PadLeft(maxLen, char.IsDigit(m.Value[0]) ? ' ' : '\xffff'))
				})
				.OrderBy(x => x.SortStr)
				.Select(x => x.OrgStr);
		}
	}
}
