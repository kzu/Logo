using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SmallBasic.Library;
using NativeColor = System.Drawing.Color;

namespace Logo
{
	static class ColorConverter
	{
		const Color DefaultColor = Color.Azul;

		public static string ToString(Color? color)
		{
			if (color == null)
				return null;

			switch (color.Value)
			{
				case Color.Blanco:
					return ToString(NativeColor.White);
				case Color.Rojo:
					return ToString(NativeColor.Red);
				case Color.Naranja:
					return ToString(NativeColor.Orange);
				case Color.Amarillo:
					return ToString(NativeColor.Yellow);
				case Color.Verde:
					return ToString(NativeColor.Green);
				case Color.Azul:
					return ToString(NativeColor.Blue);
				case Color.Indigo:
					return ToString(NativeColor.Indigo);
				case Color.Violeta:
					return ToString(NativeColor.Violet);
				case Color.Negro:
				default:
					return ToString(NativeColor.Black);
			}
		}

		static string ToString(NativeColor color) =>
			ColorTranslator.ToHtml(NativeColor.FromArgb(color.A, color.R, color.G, color.B));
	}
}
