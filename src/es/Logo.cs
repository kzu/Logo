using System;

/// <summary>
/// Simplifica algunas operaciones, como repetir una acción.
/// </summary>
public static class Logo
{
	/// <summary>
	/// Repite la acción especificada el número de veces indicado.
	/// </summary>
	/// <param name="veces">Cantidad de veces a repetir la acción.</param>
	/// <param name="accion">Acción a ejecutar repetidamente.</param>
	public static void Repetir(int veces, Action accion)
	{
		for (int i = 0; i < veces; i++)
		{
			accion();
		}
	}

	/// <summary>
	/// Ejecuta una acción si la condición es verdadera.
	/// </summary>
	/// <param name="condicion">Condición para ejecutar la acción.</param>
	/// <param name="accion">Acción a ejecutar.</param>
	public static void Si (bool condicion, Action accion)
	{
		if (condicion)
			accion ();
	}
}
