using System;
using System.Collections.Generic;

namespace ProjectCore.Grid
{
    [Serializable]
    public class GridTileIndex
    {
        public int TileType;
    }

    public class GridJsonData
    {
        public List<List<GridTileIndex>> TerrainGrid;
    }

    [Serializable]
    public class GridRow
    {
        public List<GridTile> TilesRow;
    }
}
