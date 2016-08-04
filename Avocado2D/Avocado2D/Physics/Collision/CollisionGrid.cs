using Avocado2D.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Avocado2D.Physics.Collision
{
    public class CollisionGrid
    {
        private readonly Cell[,] cells;
        private int width;
        private int height;
        private readonly int cellSize;
        private readonly int cols;
        private readonly int rows;
        private readonly Dictionary<int, HashSet<Cell>> colliderMap;

        public CollisionGrid(int cols, int rows, int cellSize)
        {
            this.cellSize = cellSize;
            this.rows = rows;
            this.cols = cols;

            width = cols * cellSize;
            height = rows * cellSize;
            cells = new Cell[cols, rows];
            colliderMap = new Dictionary<int, HashSet<Cell>>();

            for (var iX = 0; iX < cols; iX++)
            {
                for (var iY = 0; iY < rows; iY++)
                {
                    var left = iX * cellSize;
                    var right = cellSize + left;
                    var top = iY * cellSize;
                    var bottom = cellSize + top;

                    cells[iX, iY] = new Cell(new Rectangle(top, left, right, bottom));
                }
            }
        }

        /// <summary>
        /// Adds a collider to the grid.
        /// </summary>
        /// <param name="collider">The collider.</param>
        public void AddCollider(Collider collider)
        {
            var cells = CalculateCells(collider);
            colliderMap.Add(collider.Entity.Id, cells);
            foreach (var cell in cells)
            {
                cell.Colliders.Add(collider);
            }
        }

        /// <summary>
        /// Removes a collider from the grid.
        /// </summary>
        /// <param name="collider">The collider.</param>
        public void RemoveCollider(Collider collider)
        {
            var cells = colliderMap[collider.Entity.Id];
            foreach (var cell in cells)
            {
                cell.Colliders.Remove(collider);
            }
        }

        public void Move(Collider collider)
        {
            var cells = colliderMap[collider.Entity.Id];
            foreach (var cell in cells)
            {
                if (!collider.Bounds.IntersectsWith(cell.Bounds))
                {
                    RemoveCollider(collider);
                }
            }

            AddCollider(collider);
        }

        public HashSet<Collider> Query(Collider collider)
        {
            var id = collider.Entity.Id;
            var colliders = new HashSet<Collider>();

            foreach (var entry in colliderMap[id])
            {
                foreach (var _collider in entry.Colliders)
                {
                    colliders.Add(_collider);
                }
            }
            return colliders;
        }

        private HashSet<Cell> CalculateCells(Collider collider)
        {
            var bounds = collider.Bounds;

            var startX = (int)(bounds.X - bounds.Center().X) / cellSize;
            var startY = (int)(bounds.Y - bounds.Center().Y) / cellSize;
            var endX = (int)(bounds.X + bounds.Center().X) / cellSize;
            var endY = (int)(bounds.Y + bounds.Center().Y) / cellSize;

            var result = new HashSet<Cell>();

            if (startX < 0 || endX >= cols || startY < 0 || endY >= rows)
            {
                // TODO: Resize the grid when the collider is out of the grid...
                Console.WriteLine("OUT OF BOUNDS");
                return result;
            }

            for (var iY = startY; iY <= endY; iY++)
                for (var iX = startX; iX <= endX; iX++)
                    result.Add(cells[iX, iY]);

            return result;
        }
    }

    public class Cell
    {
        public List<Collider> Colliders { get; }
        public Rectangle Bounds { get; }

        public Cell(Rectangle bounds)
        {
            Colliders = new List<Collider>();
            Bounds = bounds;
        }
    }
}