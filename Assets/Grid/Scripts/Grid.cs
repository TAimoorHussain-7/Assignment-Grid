using UnityEngine;

namespace ProjectCore.Grid
{
    public class Grid : MonoBehaviour
    {
        private int _with, _height;
        private int[,] _gridArray;

        public Grid(int width, int height)
        {
            _with = width;
            _height = height;
            _gridArray = new int[width,height];
        }
    }
}
