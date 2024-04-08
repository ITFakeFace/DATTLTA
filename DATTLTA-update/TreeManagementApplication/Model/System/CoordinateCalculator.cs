using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeManagementApplication.Model.System
{
	internal class CoordinateCalculator
	{
		private Coordinate mapSize { get; set; }
		public List<List<Coordinate>> gridCoordinateMap { get; set; }
		private Coordinate gridSize { get; set; }
		private int row { get; set; }
		private int column { get; set; }
		private double paddingX { get; set; }
		private double paddingY { get; set; }

		public CoordinateCalculator(Coordinate mapSize, Coordinate gridSize)
		{
			this.mapSize = mapSize;
			this.gridSize = gridSize;
			paddingX = calculatePaddingX();
			paddingY = calculatePaddingY();
			this.gridCoordinateMap = generateCoordinateMap();
		}

		public CoordinateCalculator(Coordinate mapSize, int gridSizeX, int gridSizeY)
		{
			this.mapSize = mapSize;
			this.gridSize = new Coordinate(gridSizeX, gridSizeY);
			paddingX = calculatePaddingX();
			paddingY = calculatePaddingY();
			this.gridCoordinateMap = generateCoordinateMap();
		}

		public CoordinateCalculator(Coordinate mapSize, int gridSize)
		{
			this.mapSize = mapSize;
			this.gridSize = new Coordinate(gridSize, gridSize);
			paddingX = calculatePaddingX();
			paddingY = calculatePaddingY();
			this.gridCoordinateMap = generateCoordinateMap();
		}

		public Coordinate getNodeCoordinate(int row, int collum)
		{
			return new Coordinate(paddingX + collum * gridSize.X, paddingY + row * gridSize.Y);
		}

		public Coordinate getNodeCoordinate(GridCoordinate node)
		{
			return new Coordinate(paddingX + node.X * gridSize.X, paddingY + node.Y * gridSize.Y);
		}

		public double calculatePaddingX()
		{
			return (mapSize.X % gridSize.X) / 2;
		}

		public double calculatePaddingY()
		{
			return (mapSize.Y % gridSize.Y) / 2;
		}

		public List<List<Coordinate>> generateCoordinateMap()
		{
			List<List<Coordinate>> map = new List<List<Coordinate>>();
			column = (int)((mapSize.X - mapSize.X % gridSize.X) / gridSize.X);
			row = (int)((mapSize.Y - mapSize.Y % gridSize.Y) / gridSize.Y);
			for (int i = 0; i < column; i++)
			{
				map.Add(new List<Coordinate>());
				for (int j = 0; j < row; j++)
				{
					map[i].Add(getNodeCoordinate(j, i));
				}
			}
			Console.WriteLine("Map Created");
			return map;
		}
	}
}
