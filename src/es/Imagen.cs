using System.IO;
using Microsoft.SmallBasic.Library;

/// <summary>
/// Permite mostrar imagenes.
/// </summary>
public static class Imagen
{
	/// <summary>
	/// Muestra una imagen de un archivo local, una dirección en internet o 
	/// una imagen cualquiera con las palabras especificadas, por ejemplo "gato".
	/// </summary>
	/// <param name="imagen">Imagen a mostrar.</param>
	public static Elemento Mostrar(string imagen)
	{
		if (imagen.StartsWith("http://") || Path.IsPathRooted(imagen))
			return new Elemento (Shapes.AddImage (imagen));

		var pic = Flickr.GetRandomPicture(imagen);

		return new Elemento (Shapes.AddImage (pic));
	}
}
