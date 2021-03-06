﻿using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SmallBasic.Library;

/// <summary>
/// Representa a cualquier elemento agregado a la pantalla.
/// </summary>
public class Elemento
{
	string shapeName;

	internal Elemento(string shapeName)
	{
		this.shapeName = shapeName;
	}

	/// <summary>
	/// Cambiar la transparencia del elemento, en un rango de 
	/// 0 (totalmente transparente) a 100 (totalmente visible 
	/// u opaco).
	/// </summary>
	public int Transparencia
	{
		get { return Shapes.GetOpacity(shapeName); }
		set { Shapes.SetOpacity(shapeName, value); }
	}

	/// <summary>
	/// Elimina el elemento de la pantalla.
	/// </summary>
	public void Borrar()
	{
		Shapes.Remove(shapeName);
	}

	/// <summary>
	/// Mueve el elemento a una nueva posición, opcionalmente con una animación que 
	/// dura el tiempo especificado en milisegundos.
	/// </summary>
	/// <param name="x">Posicion en el eje horizontal (izquierda/derecha) donde mover el elemento.</param>
	/// <param name="y">Posicion en el eje vertical (abajo/arriba) donde mover el elemento.</param>
	/// <param name="duracion">
	/// Tiempo en que la acción debe terminar (opcional). 
	/// Si no se provee un valor, la operación es instantánea.
	/// </param>
	public Elemento Mover(double? x = null, double? y = null, int? duracion = null)
	{
		if (x == null && y == null)
		{
			// If no args were provided, do something anyway by default
			x = Shapes.GetLeft(shapeName) + 100;
			y = Shapes.GetTop(shapeName) + 100;
		}

		CoreGraphics.MoveShape(shapeName, x, y, duracion);
		return this;
	}

	/// <summary>
	/// Muestra el elemento, opcionalmente utilizado una animacion desde 
	/// el valor actual de <see cref="Transparencia"/> al maximo de 100.
	/// </summary>
	/// <param name="duracion">
	/// Tiempo en que la acción debe terminar (opcional). 
	/// Si no se provee un valor, la operación es instantánea.
	/// </param>
	public Elemento Mostrar(int? duracion = null)
	{
		CoreGraphics.ShowShape(shapeName, duracion);
		return this;
	}

	/// <summary>
	/// Oculta el elemento, opcionalmente utilizado una animacion desde 
	/// el valor actual de <see cref="Transparencia"/> al minimo de 0.
	/// </summary>
	/// <param name="duracion">
	/// Tiempo en que la acción debe terminar (opcional). 
	/// Si no se provee un valor, la operación es instantánea.
	/// </param>
	public Elemento Ocultar(int? duracion = null)
	{
		CoreGraphics.HideShape(shapeName, duracion);
		return this;
	}

	/// <summary>
	/// Rota el elemento en el angulo especificado, opcionalmente con una animación que 
	/// dura el tiempo especificado en milisegundos.
	/// </summary>
	/// <param name="angulo">Ángulo de rotación.</param>
	/// <param name="duracion">
	/// Tiempo en que la acción debe terminar (opcional). 
	/// Si no se provee un valor, la operación es instantánea.
	/// </param>
	public Elemento Rotar(double angulo = 90, int? duracion = null)
	{
		CoreGraphics.RotateShape(shapeName, angulo, duracion);
		return this;
	}

	/// <summary>
	/// Agranda el elemento usando el nivel de zoom especificado. Los 
	/// valores deben estar entre 0.1 y 20.
	/// </summary>
	/// <param name="zoomX">Factor de zoom para el eje horizontal (izquierda/derecha).</param>
	/// <param name="zoomY">Factor de zoom para el eje vertical (arriba/abajo).</param>
	/// <param name="duracion">
	/// Tiempo en que la acción debe terminar (opcional). 
	/// Si no se provee un valor, la operación es instantánea.
	/// </param>
	public Elemento Zoom(double? zoomX = null, double? zoomY = null, int? duracion = null)
	{
		if (zoomX == null && zoomY == null)
		{
			// Do something even if no values were provided at all.
			zoomX = 2;
			zoomY = 2;
		}

		CoreGraphics.ZoomShape(shapeName, zoomX, zoomY, duracion);
		return this;
	}

	/// <summary>
	/// Convierte al elemento en su nombre simple.
	/// </summary>
	/// <param name="elemento">Elemento a convertir.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static implicit operator string(Elemento elemento)
	{
		return elemento.shapeName;
	}
}