using Microsoft.SmallBasic.Library;

namespace Logo
{
	/// <summary>
	/// Imagina que la tortuga tiene un lápiz, que puedes levantar 
	/// o bajar para que dibuje cuando se mueve (o no).
	/// </summary>
	public static class Tortuga
	{
		/// <summary>
		/// El ángulo en que se orienta la tortuga. Al asignar 
		/// un valor, la tortuga rota instantáneamente al nuevo 
		/// ángulo.
		/// </summary>
		public static int Angulo
		{
			get { return Turtle.Angle; }
			set { Turtle.Angle = value; }
		}

		/// <summary>
		/// Velocidad a la que se mueve la tortuga, de 1 a 10.
		/// Si la velocidad de la tortuga es 10, se mueve y 
		/// rota en forma instantánea.
		/// </summary>
		public static int Velocidad
		{
			get { return Turtle.Speed; }
			set { Turtle.Speed = value; }
		}

		/// <summary>
		/// Ubicación en el eje horizontal (izquierda/derecha) de la tortuga.
		/// Al cambiar este valor, la tortuga se mueve instantáneamente a la 
		/// nueva posición.
		/// </summary>
		public static double X
		{
			get { return Turtle.X; }
			set { Turtle.X = value; }
		}

		/// <summary>
		/// Ubicación en el eje vertical (abajo/arriba) de la tortuga.
		/// Al cambiar este valor, la tortuga se mueve instantáneamente a la 
		/// nueva posición.
		/// </summary>
		public static double Y
		{
			get { return Turtle.Y; }
			set { Turtle.Y = value; }
		}

		/// <summary>
		/// Mostrar la tortuga en la pantalla.
		/// </summary>
		public static void Mostrar ()
		{
			Turtle.Show ();
		}

		/// <summary>
		/// Ocultar la tortuga de la pantalla.
		/// </summary>
		public static void Ocultar()
		{
			Turtle.Hide();
		}

		/// <summary>
		/// Mueve la tortuga la distancia indicada.
		/// Si el lápiz de la tortuga está apoyado, va a 
		/// dibujar una linea mientras se mueve. 
		/// Ver LevantarLapiz y ApoyarLapiz.
		/// </summary>
		/// <param name="distancia">La distancia que la tortuga debe moverse.</param>
		public static void Mover(double distancia)
		{
			Turtle.Move(distancia);
		}

		/// <summary>
		/// Gira y mueve la tortuga hasta la ubicacion indicada. 
		/// Si el lápiz de la tortuga está apoyado, va a 
		/// dibujar una linea mientras se mueve. 
		/// Ver LevantarLapiz y ApoyarLapiz.
		/// </summary>
		/// <param name="x">Ubicación en el eje horizontal (izquierda/derecha) del destino.</param>
		/// <param name="y">Ubicación en el eje vertical (abajo/arriba) del destino.</param>
		public static void MoverHasta(double x, double y)
		{
			Turtle.MoveTo(x, y);
		}
		
		/// <summary>
		/// Apoya el lápiz para que la tortuga dibuje mientras 
		/// se mueve.
		/// </summary>
		public static void ApoyarLapiz()
		{
			Turtle.PenDown();
		}

		/// <summary>
		/// Levanta el lápiz para que la tortuga deje de dibujar mientras 
		/// se mueve.
		/// </summary>
		public static void LevantarLapiz()
		{
			Turtle.PenUp();
		}

		/// <summary>
		/// Gira la tortuga en el ángulo indicada. 
		/// El ángulo está en grados, y puede ser positivo o negativo.
		/// Si es positivo, la tortuga gira a su derecha. 
		/// Si es negativo, la tortuga gira a su izquierda.
		/// </summary>
		/// <param name="angulo">Angulo de giro de la tortuga.</param>
		public static void Girar(double angulo)
		{
			Turtle.Turn(angulo);
		}

		/// <summary>
		/// Gira la tortuga 90 grados a la izquierda.
		/// </summary>
		public static void GirarIzquierda()
		{
			Turtle.TurnLeft();
		}

		/// <summary>
		/// Gira la tortuga 90 grados a la derecha.
		/// </summary>
		public static void GirarDerecha()
		{
			Turtle.TurnRight();
		}

		/// <summary>
		/// Cambiá el tipo de lápiz que utiliza la tortuga.
		/// </summary>
		public static class Lapiz
		{
			/// <summary>
			/// Color del lápiz que usa la tortuga. Podés usar 
			/// colores conocidos de la lista Colores.
			/// </summary>
			public static string Color
			{
				get { return GraphicsWindow.PenColor; }
				set { GraphicsWindow.PenColor = value; }
			}

			/// <summary>
			/// Grosor del lápiz que usa la tortuga, como un 
			/// número con decimales. El grosor del lápiz 
			/// inicial de la tortuga es de 2.0
			/// </summary>
			public static double Grosor
			{
				get { return GraphicsWindow.PenWidth; }
				set { GraphicsWindow.PenWidth = value; }
			}
		}
	}
}
