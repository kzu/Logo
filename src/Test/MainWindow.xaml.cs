using System;
using System.Windows;

using Logo;
using Microsoft.SmallBasic.Library;

namespace Test
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Loaded += (s, e) => Test();
		}

		/// <summary>
		/// Modify this method at will to test out the API outside of the Workbooks client 
		/// which is significantly slower to try out the actual behavior.
		/// </summary>
		void Test()
		{

			//Pantalla.Mostrar();

			//Turtle.Show();

			//for (int i = 0; i < 4; i++)
			//{
			//	Turtle.Move(100);
			//	Turtle.TurnRight();
			//}

			Pantalla.Mostrar();
			Pintar
				.Rectangulo(100, 100, 100, 300)
				.Mover(x: 250, y: 250, duracion: 1500)
				.Rotar(angulo: 180, duracion: 1500)
				.Mover(duracion: 1500)
				.Ocultar(duracion: 1500)
				.Mostrar(duracion: 1500)
				.Mover(x: 500, y: 500, duracion: 1500)
				.Ocultar(duracion: 2000);

			Dibujar.Texto(x: 200, y: 200, texto: "Terminado!");
		}
	}
}