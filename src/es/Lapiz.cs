using Microsoft.SmallBasic.Library;

/// <summary>
/// Cambia las características del lápiz que se utiliza para 
/// todas las operaciones de dibujo, incluyendo la tortuga.
/// </summary>
public static class Lapiz
{
	/// <summary>
	/// Color del lápiz que se utiliza para dibujar.
	/// </summary>
	public static Color Color
	{
		set
		{
			GraphicsWindow.BrushColor = ColorConverter.ToString(value);
			GraphicsWindow.PenColor = ColorConverter.ToString(value);
		}
	}

	/// <summary>
	/// Grosor del lápiz que se utiliza para dibujar, como un 
	/// número con decimales. El grosor del lápiz 
	/// inicial es de 2.0
	/// </summary>
	public static double Grosor
	{
		get { return GraphicsWindow.PenWidth; }
		set { GraphicsWindow.PenWidth = value; }
	}
}