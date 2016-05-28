using System.Drawing;
using Microsoft.SmallBasic.Library;
using Colors = System.Drawing.Color;

namespace Logo
{
	partial class Tortuga
	{
		/// <summary>
		/// Cambiá el tipo de lápiz que utiliza la tortuga.
		/// </summary>
		public static class Lapiz
		{
			/// <summary>
			/// Apoya el lápiz para que la tortuga dibuje mientras 
			/// se mueve.
			/// </summary>
			public static void Apoyar()
			{
				Turtle.PenDown();
			}

			/// <summary>
			/// Levanta el lápiz para que la tortuga deje de dibujar mientras 
			/// se mueve.
			/// </summary>
			public static void Levantar()
			{
				Turtle.PenUp();
			}

			/// <summary>
			/// Color del lápiz que usa la tortuga.
			/// </summary>
			public static Colores Color
			{
				set
				{
					switch (value)
					{
						case Colores.Blanco:
							GraphicsWindow.PenColor = ToString(Colors.White);
							break;
						case Colores.Negro:
							GraphicsWindow.PenColor = ToString(Colors.Black);
							break;
						case Colores.Rojo:
							GraphicsWindow.PenColor = ToString(Colors.Red);
							break;
						case Colores.Naranja:
							GraphicsWindow.PenColor = ToString(Colors.Orange);
							break;
						case Colores.Amarillo:
							GraphicsWindow.PenColor = ToString(Colors.Yellow);
							break;
						case Colores.Verde:
							GraphicsWindow.PenColor = ToString(Colors.Green);
							break;
						case Colores.Azul:
							GraphicsWindow.PenColor = ToString(Colors.Blue);
							break;
						case Colores.Indigo:
							GraphicsWindow.PenColor = ToString(Colors.Indigo);
							break;
						case Colores.Violeta:
							GraphicsWindow.PenColor = ToString(Colors.Violet);
							break;
						default:
							break;
					}
				}
			}

			/// <summary>
			/// Grosor del lápiz que usa la tortuga, como un 
			/// número con decimales. El grosor del lápiz 
			/// inicial de la tortuga es de 2.0
			/// </summary>
			public static double Grosor
			{
				get { return GraphicsWindow.PenWidth; }
				set { GraphicsWindow.PenWidth = value; }
			}

			static Primitive ToString(Colors color) =>
				ColorTranslator.ToHtml(Colors.FromArgb(color.A, color.R, color.G, color.B));
		}
	}
}
