using System.Drawing;
using System.Windows.Media;
using Microsoft.SmallBasic.Library;

namespace Logo
{
	/// <summary>
	/// Paleta de colores conocidos para utilizar con 
	/// Tortuga.Lapiz.Color.
	/// </summary>
	public static class Colores
	{
		public static string Blanco
		{
			get { return ToString(Colors.White); }
		}
		public static string Negro
		{
			get { return ToString(Colors.Black); }
		}
		public static string Rojo
		{
			get { return ToString(Colors.Red); }
		}
		public static string Naranja
		{
			get { return ToString(Colors.Orange); }
		}
		public static string Amarillo
		{
			get { return ToString(Colors.Yellow); }
		}
		public static string Verde
		{
			get { return ToString(Colors.Green); }
		}
		public static string Azul
		{
			get { return ToString(Colors.Blue); }
		}
		public static string Indigo
		{
			get { return ToString(Colors.Indigo); }
		}
		public static string Violeta
		{
			get { return ToString(Colors.Violet); }
		}

		static Primitive ToString(System.Windows.Media.Color color) =>
			ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));
	}
}
