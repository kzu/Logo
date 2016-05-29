using Microsoft.SmallBasic.Library;

namespace Logo
{
	/// <summary>
	/// Controla la pantalla donde se mueve la tortuga.
	/// </summary>
	public static class Pantalla
	{
		static readonly Screen screen;

		static Pantalla()
		{
			screen = new Screen();
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
			screen.Show();
		}

		public static void MostrarGrilla()
		{
			screen.Grid.Show();
		}

		public static void OcultarGrilla()
		{
			screen.Grid.Hide();
		}
	}
}
