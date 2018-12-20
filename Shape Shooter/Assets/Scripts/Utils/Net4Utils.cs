using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
	public static string ToString(this Color color, string format, System.Globalization.CultureInfo cultureInfo)
	{
		return string.Format("RGBA({0}, {1}, {2}, {3})"
			, color.r.ToString(format, cultureInfo)
			, color.g.ToString(format, cultureInfo)
			, color.b.ToString(format, cultureInfo)
			, color.a.ToString(format, cultureInfo)
			);
	}
}
