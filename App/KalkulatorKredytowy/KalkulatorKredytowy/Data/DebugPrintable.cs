using System.Collections;
using System.Reflection;

namespace KalkulatorKredytowy.Data
{
	public class DebugPrintable
	{
		public override string ToString()
		{
			string fields = $"-- ToString: {GetType()} --\n";
			foreach (FieldInfo fi in GetType().GetFields())
			{
				if (IsList(fi.FieldType))
				{
					fields += $"{fi.FieldType.FullName} {fi.Name} (.Count: {(fi.GetValue(this) as IList).Count}) {{\n";
					IEnumerable e = fi.GetValue(this) as IEnumerable;
					foreach (object obj in e)
					{
						fields += $"{obj},\n";
					}
					fields += "}\n";
				}
				else
					fields += $"{fi.FieldType.Name} {fi.Name} = {fi.GetValue(this) ?? "null"}\n";
			}
			return fields;
		}

		private static bool IsList(Type fieldType) => fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>);

		public Dictionary<FieldInfo, (object fi1, object fi2)> Diff(object obj2)
		{
			if (GetType() != obj2.GetType())
				return null;

			Dictionary<FieldInfo, (object fi1, object fi2)> toret = new Dictionary<FieldInfo, (object fi1, object fi2)>();

			foreach (FieldInfo fi in GetType().GetFields())
			{
				object v1 = fi.GetValue(this);
				object v2 = fi.GetValue(obj2);
				if (IsList(fi.FieldType))
				{
					if (ReferenceEquals(v1, v2))
						continue;

					if ((v1 == null && v2 != null) || (v1 != null && v2 == null))
						toret.Add(fi, (v1, v2));

					if (v1 is IEnumerable a && v2 is IEnumerable b)
						if (!a.Cast<object>().SequenceEqual(b.Cast<object>()))
							toret.Add(fi, (v1, v2));
				}
				else if (v1?.ToString() != v2?.ToString())
					toret.Add(fi, (v1, v2));
			}

			return toret;
		}
	}
}