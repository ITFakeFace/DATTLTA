using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeManagementApplication.Model.VisualModel
{
	internal class CoordinateCalculator
	{
		private Coordinate MapSize { get; set; }
		public List<List<Coordinate>> GridCoordinateMap { get; set; }
		private Coordinate GridSize { get; set; }
		private int row { get; set; }
		private int column { get; set; }
		private double paddingX { get; set; }
		private double paddingY { get; set; }

		public CoordinateCalculator(Coordinate mapSize, Coordinate gridSize)
		{
			this.MapSize = mapSize;
			this.GridSize = gridSize;
			paddingX = CalculatePaddingX();
			paddingY = CalculatePaddingY();
			GridCoordinateMap = GenerateCoordinateMap();
		}

		public CoordinateCalculator(Coordinate mapSize, int gridSizeX, int gridSizeY)
		{
			this.MapSize = mapSize;
			GridSize = new Coordinate(gridSizeX, gridSizeY);
			paddingX = CalculatePaddingX();
			paddingY = CalculatePaddingY();
			GridCoordinateMap = GenerateCoordinateMap();
		}

		public CoordinateCalculator(Coordinate mapSize, int gridSize)
		{
			this.MapSize = mapSize;
			this.GridSize = new Coordinate(gridSize, gridSize);
			paddingX = CalculatePaddingX();
			paddingY = CalculatePaddingY();
			GridCoordinateMap = GenerateCoordinateMap();
		}

		public Coordinate GetNodeCoordinate(int X, int Y)
		{
			return new Coordinate(paddingX + X * GridSize.X, paddingY + Y * GridSize.Y);
		}

		public Coordinate GetNodeCoordinate(GridCoordinate node)
		{
			return new Coordinate(paddingX + node.X * GridSize.X, paddingY + node.Y * GridSize.Y);
		}

		public double CalculatePaddingX()
		{
			return MapSize.X % GridSize.X / 2;
		}

		public double CalculatePaddingY()
		{
			return MapSize.Y % GridSize.Y / 2;
		}

		public List<List<Coordinate>> GenerateCoordinateMap()
		{
			List<List<Coordinate>> map = new List<List<Coordinate>>();
			column = (int)((MapSize.X - MapSize.X % GridSize.X) / GridSize.X);
			row = (int)((MapSize.Y - MapSize.Y % GridSize.Y) / GridSize.Y);
			for (int i = 0; i < row; i++)
			{
				map.Add(new List<Coordinate>());
				for (int j = 0; j < column; j++)
				{
					map[i].Add(GetNodeCoordinate(j, i));
				}
			}
			Console.WriteLine("Map Created");
			this.GridCoordinateMap = map;
			return map;
		}
		public GridCoordinate GetGridCoordinate(Coordinate coordinate)
		{
			return new GridCoordinate((int)((coordinate.X - paddingX) / GridSize.X), (int)((coordinate.Y - paddingY) / GridSize.Y));
		}

		public GridCoordinate GetGridCoordinate(double X, double Y)
		{
			return new GridCoordinate((int)((X - paddingX) / GridSize.X), (int)((Y - paddingY) / GridSize.Y));
		}
	}
}
