using System;

namespace ProjectCore.Grid
{
    public class TerrainGridJson
    {
        public int[][] TerrainGrid { get; set; }
    }

    public class TerrainGridTile
    {
        public int TileType { get; set; }
    }
    //public enum TileType
    //{
    //    Dirt,
    //    Grass,
    //    Stone,
    //    Wood
    //}

}
