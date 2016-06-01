using Microsoft.SmallBasic.Library;

/// <summary>
/// Controla la pantalla donde se mueve la tortuga.
/// </summary>
public static class Pantalla
{
	static readonly CoreGraphics screen;

	static Pantalla()
	{
		screen = new CoreGraphics();
	}

	internal static void Init() { }

	/// <summary>
	/// Borra de la pantalla todo lo que se haya 
	/// dibujado hasta el momento y reestablece 
	/// la tortuga a su estado inicial, sin 
	/// mostrarla.
	/// </summary>
	public static void Borrar()
	{
		GraphicsWindow.Clear();
		Tortuga.Restablecer();
		Tortuga.Ocultar();
	}

	public static void Mostrar()
	{
		screen.ShowWindow();
	}

	public static void MostrarGrilla()
	{
		screen.GridLines.Show();
	}

	public static void OcultarGrilla()
	{
		screen.GridLines.Hide();
	}
}