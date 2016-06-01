using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SmallBasic.Library;

namespace Logo
{
	/// <summary>
	/// Representa a cualquier elemento agregado a la pantalla.
	/// </summary>
	public class Elemento
	{
		Task animation = Task.FromResult(true);
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

		public Task<Elemento> Terminar()
		{
			return Task.Run(() =>
			{
				SpinWait.SpinUntil(() => animation.IsCompleted);
				return this;
			});
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
		public Elemento Mover(double? x = null, double? y = null, int? duracion = null)
		{
			if (x == null && y == null)
			{
				// If no args were provided, do something anyway by default
				x = Shapes.GetLeft(shapeName) + 100;
				y = Shapes.GetTop(shapeName) + 100;
			}

			return PushAnimation(CoreGraphics.Invoke(async () => await CoreGraphics.MoveShape(shapeName, x, y, duracion)));
		}

		/// <summary>
		/// Muestra el elemento, opcionalmente utilizado una animacion desde 
		/// el valor actual de <see cref="Transparencia"/> al maximo de 100.
		/// </summary>
		public Elemento Mostrar(int? duracion = null)
		{
			return PushAnimation(CoreGraphics.Invoke(async () => await CoreGraphics.ShowShape(shapeName, duracion)));
		}

		/// <summary>
		/// Oculta el elemento, opcionalmente utilizado una animacion desde 
		/// el valor actual de <see cref="Transparencia"/> al minimo de 0.
		/// </summary>
		public Elemento Ocultar(int? duracion = null)
		{
			return PushAnimation(CoreGraphics.Invoke(async () => await CoreGraphics.HideShape(shapeName, duracion)));
		}

		/// <summary>
		/// Rota el elemento en el angulo especificado, opcionalmente con una animación que 
		/// dura el tiempo especificado en milisegundos.
		/// </summary>
		/// <param name="angulo">Ángulo de rotación.</param>
		public Elemento Rotar(double angulo = 90, int? duracion = null)
		{
			return PushAnimation(CoreGraphics.Invoke(async () => await CoreGraphics.RotateShape(shapeName, angulo, duracion)));
		}

		/// <summary>
		/// Agranda el elemento usando el nivel de zoom especificado. Los 
		/// valores deben estar entre 0.1 y 20.
		/// </summary>
		/// <param name="zoomX">Factor de zoom para el eje horizontal (izquierda/derecha).</param>
		/// <param name="zoomY">Factor de zoom para el eje vertical (arriba/abajo).</param>
		public Elemento Zoom(double? zoomX, double? zoomY, int? duracion = null)
		{
			if (zoomX == null && zoomY == null)
			{
				// Do somthing if no values were provided at all.
				zoomX = 2;
				zoomY = 2;
			}

			return PushAnimation(CoreGraphics.Invoke(async () => await CoreGraphics.ZoomShape(shapeName, zoomX, zoomY, duracion)));
		}

		Elemento PushAnimation(Task animation)
		{
			animation = animation.ContinueWith(async t => await animation);
			return this;
		}

		public static implicit operator string(Elemento elemento)
		{
			return elemento.shapeName;
		}
	}
}
