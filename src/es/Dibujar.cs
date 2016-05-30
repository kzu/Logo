using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.SmallBasic.Library;

namespace Logo
{
	/// <summary>
	/// Permite dibujar formas en la pantalla.
	/// </summary>
	public static class Dibujar
	{
		static Dibujar()
		{
			Pantalla.Init();
		}

		/// <summary>
		/// Dibuja una elipse (por ejemplo un círculo o un óvalo) en la 
		/// pantalla utilizando el lápiz configurado en <see cref="Lapiz"/>.
		/// </summary>
		/// <param name="x">Posicion en el eje horizontal (izquierda/derecha) donde comenzar a dibujar.</param>
		/// <param name="y">Posicion en el eje vertical (abajo/arriba) donde comenzar a dibujar.</param>
		/// <param name="ancho">Ancho de la elipse a dibujar.</param>
		/// <param name="alto">Alto de la elipse a dibujar.</param>
		public static Elemento Elipse(double x, double y, int ancho, int alto)
		{
			var name = Shapes.AddEllipse(ancho, alto);
			Shapes.Move(name, x, y);
			return new Elemento(name);
		}

		/// <summary>
		/// Dibuja una línea de un punto a otro de la pantalla, utilizando el 
		/// lápiz configurado en <see cref="Lapiz"/>.
		/// </summary>
		/// <param name="x1">Posicion en el eje horizontal (izquierda/derecha) donde comienza la línea.</param>
		/// <param name="y1">Posicion en el eje vertical (abajo/arriba) donde donde comienza la línea.</param>
		/// <param name="x2">Posicion en el eje horizontal (izquierda/derecha) donde termina la línea.</param>
		/// <param name="y2">Posicion en el eje vertical (abajo/arriba) donde termina la línea.</param>
		public static Elemento Linea(double x1, double y1, double x2, double y2)
		{
			return new Elemento(Shapes.AddLine(x1, y1, x2, y2));
		}

		/// <summary>
		/// Dibuja un rectángulo en la pantalla utilizando el 
		/// lápiz configurado en <see cref="Lapiz"/>.
		/// </summary>
		/// <param name="x">Posición en el eje horizontal (izquierda/derecha) donde comenzar a dibujar.</param>
		/// <param name="y">Posición en el eje vertical (abajo/arriba) donde comenzar a dibujar.</param>
		/// <param name="ancho">Ancho del rectángulo a dibujar.</param>
		/// <param name="alto">Alto del rectángulo a dibujar.</param>
		public static Elemento Rectangulo(double x, double y, int ancho, int alto)
		{
			var name = Shapes.AddRectangle(ancho, alto);
			Shapes.Move(name, x, y);
			return new Elemento(name);
		}

		/// <summary>
		/// Dibuja un triángulo en la pantalla utilizando el 
		/// lápiz configurado en <see cref="Lapiz"/>.
		/// </summary>
		/// <param name="x1">Posición en el eje horizontal (izquierda/derecha) del primer vértice del triángulo.</param>
		/// <param name="y1">Posición en el eje vertical (abajo/arriba) del primer vértice del triángulo.</param>
		/// <param name="x2">Posición en el eje horizontal (izquierda/derecha) del segundo vértice del triángulo.</param>
		/// <param name="y2">Posición en el eje vertical (abajo/arriba) del segundo vértice del triángulo.</param>
		/// <param name="x3">Posición en el eje horizontal (izquierda/derecha) del tercer vértice del triángulo.</param>
		/// <param name="y3">Posición en el eje vertical (abajo/arriba) del tercer vértice del triángulo.</param>
		public static Elemento Triangulo(double x1, double y1, double x2, double y2, double x3, double y3)
		{
			return new Elemento(Shapes.AddTriangle(x1, y1, x2, y2, x3, y3));
		}

		/// <summary>
		/// Dibuja una línea de texto en la posición especificada.
		/// </summary>
		/// <param name="x">Posicion en el eje horizontal (izquierda/derecha) donde comenzar a escribir el texto.</param>
		/// <param name="y">Posicion en el eje vertical (abajo/arriba) donde comenzar a escribir el texto.</param>
		/// <param name="texto">Texto a escribir.</param>
		public static Elemento Texto(double x, double y, string texto, double? tamañoDeLetra = null, bool? letraGruesa = null, bool? letraCursiva = null, string tipoDeLetra = "")
		{
			double size = GraphicsWindow.FontSize;
			bool bold = GraphicsWindow.FontBold;
			bool italic = GraphicsWindow.FontItalic;
			string font = GraphicsWindow.FontName;
			try
			{
				if (tamañoDeLetra != null)
					GraphicsWindow.FontSize = tamañoDeLetra.Value;
				if (letraGruesa != null)
					GraphicsWindow.FontBold = letraGruesa.Value;
				if (letraCursiva != null)
					GraphicsWindow.FontItalic = letraCursiva.Value;
				if (tipoDeLetra != null)
					GraphicsWindow.FontName = tipoDeLetra;

				var name = Shapes.AddText(texto);
				Shapes.Move(name, x, y);
				return new Elemento(name);
			}
			finally
			{
				if (tamañoDeLetra != null)
					GraphicsWindow.FontSize = size;
				if (letraGruesa != null)
					GraphicsWindow.FontBold = bold;
				if (letraCursiva != null)
					GraphicsWindow.FontItalic = italic;
				if (tipoDeLetra != null)
					GraphicsWindow.FontName = font;
			}
		}
	}
}