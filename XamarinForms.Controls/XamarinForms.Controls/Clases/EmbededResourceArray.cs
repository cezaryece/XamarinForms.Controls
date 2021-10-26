using System.Reflection;

namespace XamarinForms.Controls.Clases
{
	public class EmbededResourceArray
	{
		public static byte[] Get(string resourcePath, object assemblyObject)
		{
			var assembly = assemblyObject.GetType().GetTypeInfo().Assembly;
			using (var s = assembly.GetManifestResourceStream(resourcePath))
			{
				var length = s.Length;
				var buffer = new byte[length];
				s.Read(buffer, 0, (int)length);
				return buffer;
			}
		}
	}
}