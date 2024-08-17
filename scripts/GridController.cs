using System.Linq.Expressions;

public class GridController   {

    private Grid grid;
    //private Block[] blocks;

    public void createGrid()   {
        grid = Grid.initialize(GridData.DEFAULT_GRID_WIDTH,GridData.DEFAULT_GRID_HEIGHT);
    }

}