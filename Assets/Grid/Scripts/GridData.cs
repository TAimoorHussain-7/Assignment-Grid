using System;
using System.Collections.Generic;

namespace ProjectCore.Grid
{
    [Serializable]
    public class GridTileIndex
    {
        public int TileType;
    }
    
    [Serializable]
    public class GridRow
    {
        public List<int> IntList;
    }

    [Serializable]
    public class GridData
    {
        public List<List<GridTileIndex>> TerrainGrid;
    }

    //public enum TileType
    //{
    //    Dirt,
    //    Grass,
    //    Stone,
    //    Wood
    //}

}
