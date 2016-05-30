using System;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using Microsoft.SmallBasic.Library;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace Logo
{
	internal class GridLines
	{
		public const int DefaultCellSize = 10;
		public const double DefaultLineWidth = 0.5;

		Canvas canvas;
		int cellSize;
		double lineWidth;
		Grid grid;
		Label coordinates;

		public GridLines(Canvas canvas, int cellSize = DefaultCellSize, double lineWidth = DefaultLineWidth)
		{
			this.canvas = canvas;
			this.cellSize = cellSize;
			this.lineWidth = lineWidth;
		}

		public void Show()
		{
			Initialize();

			if (grid.Parent == null)
			{
				canvas.Children.Add(grid);
				canvas.Children.Add(coordinates);
				canvas.SizeChanged += OnCanvasResized;
				RefreshGrid();
			}
		}

		public void Hide()
		{
			if (grid != null && grid.Parent == canvas)
			{
				canvas.Children.Remove(grid);
				canvas.Children.Remove(coordinates);
				canvas.SizeChanged -= OnCanvasResized;
			}
		}

		void Initialize()
		{
			if (grid == null)
			{
				var height = new Binding
				{
					RelativeSource = new RelativeSource(
						RelativeSourceMode.FindAncestor,
						typeof(Canvas), 1),
					Path = new PropertyPath("ActualHeight"),
					Mode = BindingMode.OneWay,
				};
				var width = new Binding
				{
					RelativeSource = new RelativeSource(
						RelativeSourceMode.FindAncestor,
						typeof(Canvas), 1),
					Path = new PropertyPath("ActualHeight"),
					Mode = BindingMode.OneWay,
				};

				grid = new Grid();
				coordinates = new Label();

				BindingOperations.SetBinding(grid, Grid.HeightProperty, height);
				BindingOperations.SetBinding(grid, Grid.WidthProperty, width);
				Panel.SetZIndex(coordinates, int.MaxValue);
				Panel.SetZIndex(grid, int.MinValue);
			}
		}

		void OnCanvasResized(object sender, EventArgs args)
		{
			RefreshGrid();
		}

		void RefreshGrid()
		{
			var rows = (int)(canvas.ActualHeight / cellSize);
			var cols = (int)(canvas.ActualWidth / cellSize);

			grid.ColumnDefinitions.Clear();
			grid.RowDefinitions.Clear();
			for (int i = 0; i < cols; i++)
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(cellSize) });
			}
			for (int i = 0; i < rows; i++)
			{
				grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(cellSize) });
			}

			for (int col = 0; col < cols; col++)
			{
				for (int row = 0; row < rows; row++)
				{
					var border = new Border
					{
						BorderBrush = new SolidColorBrush(Colors.LightGray),
						BorderThickness = new Thickness(lineWidth),
					};

					border.MouseEnter += (sender, args) => {
						var ui = (UIElement)sender;
						var x = Grid.GetColumn(ui) * cellSize;
						var y = Grid.GetRow(ui) * cellSize;
						coordinates.Content = "x=" + x + "; y=" + y;
						Canvas.SetTop(coordinates, y + 30);
						Canvas.SetLeft(coordinates, x + 30);
					};

					Grid.SetColumn(border, col);
					Grid.SetRow(border, row);
					Panel.SetZIndex(border, int.MinValue);

					grid.Children.Add(border);
				}
			}
		}
	}
}
