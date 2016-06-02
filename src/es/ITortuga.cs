using System.ComponentModel;
/// <summary>
/// Representa a la <see cref="Tortuga"/> para operaciones 
/// encadenadas.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ITortuga 
{
	/// <summary>
	/// Cambia en forma instantánea el ángulo en que se orienta la tortuga.
	/// </summary>
	ITortuga CambiarAngulo(int angulo);

	/// <summary>
	/// Cambia en forma instantánea la posición de la tortuga.
	/// </summary>
	/// <param name="x">
	/// Ubicación en el eje horizontal (izquierda/derecha) de la tortuga.
	/// </param>
	/// <param name="y">
	/// Ubicación en el eje vertical (abajo/arriba) de la tortuga.
	/// </param>
	ITortuga CambiarPosition(double? x = null, double? y = null);

	/// <summary>
	/// Cambia la velocidad a la que se mueve la tortuga.
	/// </summary>	
	ITortuga CambiarVelocidad(Velocidad velocidad);

	/// <summary>
	/// Gira la tortuga en el ángulo indicada. 
	/// El ángulo está en grados, y puede ser positivo o negativo.
	/// Si es positivo, la tortuga gira a su derecha. 
	/// Si es negativo, la tortuga gira a su izquierda.
	/// </summary>
	/// <param name="angulo">Angulo de giro de la tortuga.</param>
	ITortuga Girar(double angulo);

	/// <summary>
	/// Gira la tortuga 90 grados a la derecha.
	/// </summary>
	ITortuga GirarDerecha();

	/// <summary>
	/// Gira la tortuga 90 grados a la izquierda.
	/// </summary>
	ITortuga GirarIzquierda();

	/// <summary>
	/// Mueve la tortuga la distancia indicada.
	/// Si el lápiz de la tortuga está apoyado, va a 
	/// dibujar una linea mientras se mueve. 
	/// Ver LevantarLapiz y ApoyarLapiz.
	/// </summary>
	/// <param name="distancia">La distancia que la tortuga debe moverse.</param>
	ITortuga Mover(double distancia);

	/// <summary>
	/// Gira y mueve la tortuga hasta la ubicacion indicada. 
	/// Si el lápiz de la tortuga está apoyado, va a 
	/// dibujar una linea mientras se mueve. 
	/// Ver LevantarLapiz y ApoyarLapiz.
	/// </summary>
	/// <param name="x">Ubicación en el eje horizontal (izquierda/derecha) del destino.</param>
	/// <param name="y">Ubicación en el eje vertical (abajo/arriba) del destino.</param>
	ITortuga MoverHasta(double? x = null, double? y = null);

	/// <summary>
	/// Mostrar la tortuga en la pantalla.
	/// </summary>
	ITortuga Mostrar();

	/// <summary>
	/// Ocultar la tortuga de la pantalla.
	/// </summary>
	ITortuga Ocultar();
}