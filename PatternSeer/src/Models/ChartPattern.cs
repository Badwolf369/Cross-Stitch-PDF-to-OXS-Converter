using Emgu.CV;

namespace PatternSeer.Models;

public class ChartPattern
{
    /* #region Fields */
    /// <summary>
    /// Pattern grid of symbols that do not have a thread associated with them.
    /// </summary>
    private List<List<Mat>> _unkeyedGrid;
    /// <summary>
    /// Pattern grid of symbols with their associated threads attached.
    /// </summary>
    private List<List<KeySymbol>> _keyedGrid;
    /* #endregion Fields */

    /* #region Properties */
    /// <summary>
    /// Pair of integers describing the x and y dimensions of the pattern.
    /// </summary>
    public Tuple<int, int> Size { get; private set; }
    /* #endregion Properties */

    /* #region Constructors */
    public ChartPattern(List<List<Mat>> unkeyedPattern, ChartKey key)
    {

    }
    /* #endregion Constructors */

    /* #region Private Methods */
    /* #endregion Private Methods */

    /* #region Public Methods */
    /// <summary>
    /// Gets the KeySymbol at a certain position in the pattern
    /// </summary>
    /// <param name="x">x position</param>
    /// <param name="y">y position</param>
    /// <returns>KeySymbol the position (x, y)</returns>
    public KeySymbol GetSymbolAt(int x, int y)
    {
        return _keyedGrid[y][x];
    }

    /// <summary>
    /// Links a thread to each symbol image in the pattern based on a given key
    /// </summary>
    /// <param name="Key">ChartKey that defines the pattern links</param>
    public void KeyGrid(ChartKey Key)
    {

    }
    /* #endregion Public Methods */
}