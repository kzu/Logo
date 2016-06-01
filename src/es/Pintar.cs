/// <summary>
/// Permite dibujar formas rellenas (pintadas) en la pantalla.
/// </summary>
public static class Pintar
{
	static Pintar()
	{
		Pantalla.Init();
	}

	/// <summary>
	/// Pinta una elipse (por ejemplo un círculo o un óvalo) en la pantalla.
	/// </summary>
	/// <param name="x">Posicion en el eje horizontal (izquierda/derecha) donde comenzar a dibujar.</param>
	/// <param name="y">Posicion en el eje vertical (abajo/arriba) donde comenzar a dibujar.</param>
	/// <param name="ancho">Ancho de la elipse a dibujar.</param>
	/// <param name="alto">Alto de la elipse a dibujar.</param>
	/// <param name="color">Color del lápiz a utilizar para pintar.</param>
	public static Elemento Elipse(double x = 100, double y = 100, int ancho = 100, int alto = 100, Color? color = null)
	{
		return new Elemento(CoreGraphics.AddEllipse(true, x, y, ancho, alto, ColorConverter.ToString(color)));
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
	public static Elemento Rectangulo(double x = 100, double y = 100, int ancho = 200, int alto = 100, Color? color = null)
	{
		return new Elemento(CoreGraphics.AddRectangle(true, x, y, ancho, alto, ColorConverter.ToString(color)));
	}

	/// <summary>
	/// Pinta un triángulo en la pantalla con el color especificado.
	/// </summary>
	/// <param name="x1">Posición en el eje horizontal (izquierda/derecha) del primer vértice del triángulo.</param>
	/// <param name="y1">Posición en el eje vertical (abajo/arriba) del primer vértice del triángulo.</param>
	/// <param name="x2">Posición en el eje horizontal (izquierda/derecha) del segundo vértice del triángulo.</param>
	/// <param name="y2">Posición en el eje vertical (abajo/arriba) del segundo vértice del triángulo.</param>
	/// <param name="x3">Posición en el eje horizontal (izquierda/derecha) del tercer vértice del triángulo.</param>
	/// <param name="y3">Posición en el eje vertical (abajo/arriba) del tercer vértice del triángulo.</param>
	/// <param name="color">Color del lápiz a utilizar para pintar.</param>
	public static Elemento Triangulo(double x1 = 100, double y1 = 100, double x2 = 150, double y2 = 50, double x3 = 200, double y3 = 100, Color? color = null)
	{
		return new Elemento(CoreGraphics.AddTriangle(true, x1, y1, x2, y2, x3, y3, ColorConverter.ToString(color)));
	}
}