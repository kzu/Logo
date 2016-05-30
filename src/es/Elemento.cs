using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SmallBasic.Library;

namespace Logo
{
	/// <summary>
	/// Representa a cualquier elemento agregado a la pantalla.
	/// </summary>
	public class Elemento : IFluentInterface
	{
		string name;

		internal Elemento(string name)
		{
			this.name = name;
		}

		/// <summary>
		/// Cambiar la transparencia del elemento, en un rango de 
		/// 0 (totalmente transparente) a 100 (totalmente visible 
		/// u opaco).
		/// </summary>
		public int Transparencia
		{
			get { return Shapes.GetOpacity(name); }
			set { Shapes.SetOpacity(name, value); }
		}

		/// <summary>
		/// Elimina el elemento de la pantalla.
		/// </summary>
		public void Borrar()
		{
			Shapes.Remove(name);
		}

		/// <summary>
		/// Mueve el elemento a una nueva posición, en forma 
		/// instantánea.
		/// </summary>
		/// <param name="x">Posicion en el eje horizontal (izquierda/derecha) donde mover el elemento.</param>
		/// <param name="y">Posicion en el eje vertical (abajo/arriba) donde mover el elemento.</param>
		public void Mover(double x, double y)
		{
			Shapes.Move(name, x, y);
		}

		/// <summary>
		/// Mueve el elemento a una nueva posición, con una animación que 
		/// dura el tiempo especificado.
		/// </summary>
		/// <param name="x">Posicion en el eje horizontal (izquierda/derecha) donde mover el elemento.</param>
		/// <param name="y">Posicion en el eje vertical (abajo/arriba) donde mover el elemento.</param>
		public void Mover(double x, double y, double duracion)
		{
			Shapes.Animate(name, x, y, duracion);
		}

		public void Mostrar()
		{
			Shapes.ShowShape(name);
		}

		public void Ocultar()
		{
			Shapes.HideShape(name);
		}

		/// <summary>
		/// Rota el elemento en el angulo especificado.
		/// </summary>
		/// <param name="angulo">Ángulo de rotación.</param>
		public void Rotar(double angulo)
		{
			Shapes.Rotate(name, angulo);
		}

		/// <summary>
		/// Agranda el elemento usando el nivel de zoom especificado. Los 
		/// valores deben estar entre 0.1 y 20.
		/// </summary>
		/// <param name="zoomX">Factor de zoom para el eje horizontal (izquierda/derecha).</param>
		/// <param name="zoomY">Factor de zoom para el eje vertical (arriba/abajo).</param>
		public void Zoom(double zoomX, double zoomY)
		{
			Shapes.Zoom(name, zoomX, zoomY);
		}
	}
}
