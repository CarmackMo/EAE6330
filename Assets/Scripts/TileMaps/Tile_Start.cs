



public class Tile_Start : Tile_Base
{
    // Implementations
    //=================

    protected override void Init()
    {
        base.Init();

        m_tileState = TileState.Visited;
    }
}
