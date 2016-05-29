using Microsoft.SmallBasic.Library;

namespace Logo
{
	/// <summary>
	/// Imagina que la tortuga tiene un lápiz, que puedes levantar 
	/// o bajar para que dibuje cuando se mueve (o no).
	/// </summary>
	public static partial class Tortuga
	{
		static Tortuga()
		{
			Pantalla.Init();
			Turtle.Show();
		}

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
		public static Velocidad Velocidad
		{
			get { return (Velocidad)(int)Turtle.Speed; }
			set { Turtle.Speed = (int)value; }
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
		/// Mueve la tortuga la distancia indicada.
		/// Si el lápiz de la tortuga está apoyado, va a 
		/// dibujar una linea mientras se mueve. 
		/// Ver LevantarLapiz y ApoyarLapiz.
		/// </summary>
		/// <param name="distancia">La distancia que la tortuga debe moverse.</param>
		public static void Mover(double distancia)
		{
			Turtle.Show();
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
			Turtle.Show();
			Turtle.MoveTo(x, y);
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
			Turtle.Show();
			Turtle.Turn(angulo);
		}

		/// <summary>
		/// Gira la tortuga 90 grados a la izquierda.
		/// </summary>
		public static void GirarIzquierda()
		{
			Turtle.Show();
			Turtle.TurnLeft();
		}

		/// <summary>
		/// Gira la tortuga 90 grados a la derecha.
		/// </summary>
		public static void GirarDerecha()
		{
			Turtle.Show();
			Turtle.TurnRight();
		}

		public static class Lapiz
		{
			/// <summary>
			/// Apoya el lápiz para que la tortuga dibuje mientras 
			/// se mueve.
			/// </summary>
			public static void Apoyar()
			{
				Turtle.PenDown();
			}

			/// <summary>
			/// Levanta el lápiz para que la tortuga deje de dibujar mientras 
			/// se mueve.
			/// </summary>
			public static void Levantar()
			{
				Turtle.PenUp();
			}

			/// <summary>
			/// Color del lápiz que usa la tortuga.
			/// </summary>
			public static Color Color
			{
				set { Logo.Lapiz.Color = value; }
			}

			/// <summary>
			/// Grosor del lápiz que usa la tortuga, como un 
			/// número con decimales. El grosor del lápiz 
			/// inicial de la tortuga es de 2.0
			/// </summary>
			public static double Grosor
			{
				get { return Logo.Lapiz.Grosor; }
				set { Logo.Lapiz.Grosor = value; }
			}
		}
	}
}