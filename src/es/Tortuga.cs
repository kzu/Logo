using System;
using Microsoft.SmallBasic.Library;

/// <summary>
/// Imagina que la tortuga tiene un lápiz, que puedes levantar 
/// o bajar para que dibuje cuando se mueve (o no).
/// </summary>
public static partial class Tortuga
{
	static ITortuga instance = new TortugaWrapper();
	static bool visible = false;

	static Tortuga()
	{
		Pantalla.Init();
		Mostrar();
	}

	/// <summary>
	/// Cambia en forma instantánea el ángulo en que se orienta la tortuga.
	/// </summary>
	public static ITortuga CambiarAngulo(int angulo)
	{
		Angulo = angulo;
		return instance;
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
	/// Cambia en forma instantánea la posición de la tortuga.
	/// </summary>
	/// <param name="x">
	/// Ubicación en el eje horizontal (izquierda/derecha) de la tortuga.
	/// </param>
	/// <param name="y">
	/// Ubicación en el eje vertical (abajo/arriba) de la tortuga.
	/// </param>
	public static ITortuga CambiarPosicion(double? x = null, double? y = null)
	{
		if (x != null)
			X = x.Value;
		if (y != null)
			Y = y.Value;

		return instance;
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
	/// Cambia la velocidad a la que se mueve la tortuga.
	/// </summary>	
	public static ITortuga CambiarVelocidad(Velocidad velocidad)
	{
		Velocidad = velocidad;
		return instance;
	}

	/// <summary>
	/// Velocidad a la que se mueve la tortuga.
	/// </summary>
	public static Velocidad Velocidad
	{
		get { return (Velocidad)(int)Turtle.Speed; }
		set { Turtle.Speed = (int)value; }
	}

	/// <summary>
	/// Gira la tortuga en el ángulo indicada. 
	/// El ángulo está en grados, y puede ser positivo o negativo.
	/// Si es positivo, la tortuga gira a su derecha. 
	/// Si es negativo, la tortuga gira a su izquierda.
	/// </summary>
	/// <param name="angulo">Angulo de giro de la tortuga.</param>
	public static ITortuga Girar(double angulo)
	{
		Turtle.Turn(angulo);
		return instance;
	}

	/// <summary>
	/// Gira la tortuga 90 grados a la derecha.
	/// </summary>
	public static ITortuga GirarDerecha()
	{
		Turtle.TurnRight();
		return instance;
	}

	/// <summary>
	/// Gira la tortuga 90 grados a la izquierda.
	/// </summary>
	public static ITortuga GirarIzquierda()
	{
		Turtle.TurnLeft();
		return instance;
	}

	/// <summary>
	/// Mueve la tortuga la distancia indicada.
	/// Si el lápiz de la tortuga está apoyado, va a 
	/// dibujar una linea mientras se mueve. 
	/// Ver LevantarLapiz y ApoyarLapiz.
	/// </summary>
	/// <param name="distancia">La distancia que la tortuga debe moverse.</param>
	public static ITortuga Mover(double distancia)
	{
		Turtle.Move(distancia);
		return instance;
	}

	/// <summary>
	/// Gira y mueve la tortuga hasta la ubicacion indicada. 
	/// Si el lápiz de la tortuga está apoyado, va a 
	/// dibujar una linea mientras se mueve. 
	/// Ver LevantarLapiz y ApoyarLapiz.
	/// </summary>
	/// <param name="x">Ubicación en el eje horizontal (izquierda/derecha) del destino.</param>
	/// <param name="y">Ubicación en el eje vertical (abajo/arriba) del destino.</param>
	public static ITortuga MoverHasta(double? x = null, double? y = null)
	{
		Turtle.MoveTo(x.GetValueOrDefault(Turtle.X), y.GetValueOrDefault(Turtle.Y));
		return instance;
	}

	/// <summary>
	/// Mostrar la tortuga en la pantalla.
	/// </summary>
	public static ITortuga Mostrar()
	{
		if (!visible)
			Turtle.Show();

		visible = true;
		return instance;
	}

	/// <summary>
	/// Ocultar la tortuga de la pantalla.
	/// </summary>
	public static ITortuga Ocultar()
	{
		Turtle.Hide();
		visible = false;
		return instance;
	}


	/// <summary>
	/// Restablece la tortuga a su posicion y angulo iniciales, 
	/// y limpia la pantalla de todo lo que haya hecho hasta el 
	/// momento, llamando a Pantalla.Limpiar.
	/// </summary>
	public static void Restablecer()
	{
		Turtle.X = 320;
		Turtle.Y = 240;
		Turtle.Angle = 0;
		Turtle.Speed = 5;
		Turtle.PenDown();
		Mostrar();
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
			set { Lapiz.Color = value; }
		}

		/// <summary>
		/// Grosor del lápiz que usa la tortuga, como un 
		/// número con decimales. El grosor del lápiz 
		/// inicial de la tortuga es de 2.0
		/// </summary>
		public static double Grosor
		{
			get { return Lapiz.Grosor; }
			set { Lapiz.Grosor = value; }
		}
	}

	class TortugaWrapper : ITortuga
	{
		public ITortuga CambiarAngulo(int angulo)
		{
			return Tortuga.CambiarAngulo(angulo);
		}

		public ITortuga CambiarPosition(double? x = null, double? y = null)
		{
			return Tortuga.CambiarPosicion(x, y);
		}

		public ITortuga CambiarVelocidad(Velocidad velocidad)
		{
			return Tortuga.CambiarVelocidad(velocidad);
		}

		public ITortuga Girar(double angulo)
		{
			return Tortuga.Girar(angulo);
		}

		public ITortuga GirarDerecha()
		{
			return Tortuga.GirarDerecha();
		}

		public ITortuga GirarIzquierda()
		{
			return Tortuga.GirarIzquierda();
		}

		public ITortuga Mostrar()
		{
			return Tortuga.Mostrar();
		}

		public ITortuga Mover(double distancia)
		{
			return Tortuga.Mover(distancia);
		}

		public ITortuga MoverHasta(double? x = null, double? y = null)
		{
			return Tortuga.MoverHasta(x, y);
		}

		public ITortuga Ocultar()
		{
			return Tortuga.Ocultar();
		}
	}
}