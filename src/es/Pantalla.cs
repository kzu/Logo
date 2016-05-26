using Microsoft.SmallBasic.Library;

namespace Logo
{
	/// <summary>
	/// Controla la pantalla donde se mueve la tortuga.
	/// </summary>
	public static class Pantalla
	{
		/// <summary>
		/// Limpia la pantalla de todo lo que se haya 
		/// dibujado hasta el momento.
		/// </summary>
		public static void Limpiar()
		{
			GraphicsWindow.Clear();
		}
	}
}
