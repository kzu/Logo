using Microsoft.SmallBasic.Library;

namespace Logo
{
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
		/// Limpia la pantalla de todo lo que se haya 
		/// dibujado hasta el momento.
		/// </summary>
		public static void Limpiar()
		{
			GraphicsWindow.Clear();
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
}
