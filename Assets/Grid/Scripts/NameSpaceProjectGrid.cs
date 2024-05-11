using System;
using UnityEngine;
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
        public List<GridBuilingBlock> GridBuildings = new List<GridBuilingBlock>();
    }

    [Serializable]
    public class GridRow
    {
        public List<GridTile> TilesRow;
    }

    [Serializable]
    public class GridBuilingBlock
    {
        public List<Vector2Int> TilesOccupied = new List<Vector2Int>();
        public int BuildingId;
    }
}
