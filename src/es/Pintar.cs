using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SmallBasic.Library;

namespace Logo
{
	/// <summary>
	/// Permite dibujar formas rellenas (pintadas) en la pantalla.
	/// </summary>
	public static class Pintar
	{
		/// <summary>
		/// Pinta una elipse (por ejemplo un círculo o un óvalo) en la 
		/// pantalla utilizando el lápiz configurado en <see cref="Lapiz"/>.
		/// </summary>
		/// <param name="x">Posicion en el eje horizontal (izquierda/derecha) donde comenzar a dibujar.</param>
		/// <param name="y">Posicion en el eje vertical (abajo/arriba) donde comenzar a dibujar.</param>
		/// <param name="ancho">Ancho de la elipse a dibujar.</param>
		/// <param name="alto">Alto de la elipse a dibujar.</param>
		public static void Elipse(int x, int y, int ancho, int alto)
		{
			GraphicsWindow.FillEllipse(x, y, ancho, alto);
		}

		/// <summary>
		/// Pinta una elipse (por ejemplo un círculo o un óvalo) en la pantalla.
		/// </summary>
		/// <param name="x">Posicion en el eje horizontal (izquierda/derecha) donde comenzar a dibujar.</param>
		/// <param name="y">Posicion en el eje vertical (abajo/arriba) donde comenzar a dibujar.</param>
		/// <param name="ancho">Ancho de la elipse a dibujar.</param>
		/// <param name="alto">Alto de la elipse a dibujar.</param>
		/// <param name="color">Color del lápiz a utilizar para pintar.</param>
		public static void Elipse(int x, int y, int ancho, int alto, Color color)
		{
			var current = GraphicsWindow.PenColor;
			try
			{
				Lapiz.Color = color;
				GraphicsWindow.FillEllipse(x, y, ancho, alto);
			}
			finally
			{
				GraphicsWindow.PenColor = current;
			}
		}

		/// <summary>
		/// Pinta un rectángulo en la pantalla.
		/// </summary>
		/// <param name="x">Posición en el eje horizontal (izquierda/derecha) donde comenzar a dibujar.</param>
		/// <param name="y">Posición en el eje vertical (abajo/arriba) donde comenzar a dibujar.</param>
		/// <param name="ancho">Ancho del rectángulo a dibujar.</param>
		/// <param name="alto">Alto del rectángulo a dibujar.</param>
		public static void Rectangulo(int x, int y, int ancho, int alto)
		{
			GraphicsWindow.FillRectangle(x, y, ancho, alto);
		}


		/// <summary>
		/// Pinta un rectángulo en la pantalla utilizando el 
		/// lápiz configurado en <see cref="Lapiz"/>.
		/// </summary>
		/// <param name="x">Posición en el eje horizontal (izquierda/derecha) donde comenzar a dibujar.</param>
		/// <param name="y">Posición en el eje vertical (abajo/arriba) donde comenzar a dibujar.</param>
		/// <param name="ancho">Ancho del rectángulo a dibujar.</param>
		/// <param name="alto">Alto del rectángulo a dibujar.</param>
		/// <param name="color">Color del lápiz a utilizar para pintar.</param>
		public static void Rectangulo(int x, int y, int ancho, int alto, Color color)
		{
			var current = GraphicsWindow.PenColor;
			try
			{
				Lapiz.Color = color;
				GraphicsWindow.FillRectangle(x, y, ancho, alto);
			}
			finally
			{
				GraphicsWindow.PenColor = current;
			}
		}

		/// <summary>
		/// Pinta un triángulo en la pantalla
		/// </summary>
		/// <param name="x1">Posición en el eje horizontal (izquierda/derecha) del primer vértice del triángulo.</param>
		/// <param name="y1">Posición en el eje vertical (abajo/arriba) del primer vértice del triángulo.</param>
		/// <param name="x2">Posición en el eje horizontal (izquierda/derecha) del segundo vértice del triángulo.</param>
		/// <param name="y2">Posición en el eje vertical (abajo/arriba) del segundo vértice del triángulo.</param>
		/// <param name="x3">Posición en el eje horizontal (izquierda/derecha) del tercer vértice del triángulo.</param>
		/// <param name="y3">Posición en el eje vertical (abajo/arriba) del tercer vértice del triángulo.</param>
		/// <param name="color">Color del lápiz a utilizar para pintar.</param>
		public static void Triangulo(int x1, int y1, int x2, int y2, int x3, int y3)
		{
			GraphicsWindow.FillTriangle(x1, y1, x2, y2, x3, y3);
		}

		/// <summary>
		/// Pinta un triángulo en la pantalla.
		/// </summary>
		/// <param name="x1">Posición en el eje horizontal (izquierda/derecha) del primer vértice del triángulo.</param>
		/// <param name="y1">Posición en el eje vertical (abajo/arriba) del primer vértice del triángulo.</param>
		/// <param name="x2">Posición en el eje horizontal (izquierda/derecha) del segundo vértice del triángulo.</param>
		/// <param name="y2">Posición en el eje vertical (abajo/arriba) del segundo vértice del triángulo.</param>
		/// <param name="x3">Posición en el eje horizontal (izquierda/derecha) del tercer vértice del triángulo.</param>
		/// <param name="y3">Posición en el eje vertical (abajo/arriba) del tercer vértice del triángulo.</param>
		/// <param name="color">Color del lápiz a utilizar para pintar.</param>
		public static void Triangulo(int x1, int y1, int x2, int y2, int x3, int y3, Color color)
		{
			var current = GraphicsWindow.PenColor;
			try
			{
				Lapiz.Color = color;
				GraphicsWindow.FillTriangle(x1, y1, x2, y2, x3, y3);
			}
			finally
			{
				GraphicsWindow.PenColor = current;
			}
		}

	}
}
