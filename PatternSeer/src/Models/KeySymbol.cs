using System.Dynamic;
using System.Reflection;

namespace PatternSeer.Models;

public class KeySymbol {
    /* #region Fields */
    private int Image;
    private string ThreadColor = "";
    private int ThreadCount = 2;
    private int? StitchCount;
    private string Brand = "dmc";
    /* #endregion */

    /* #region Properties */
    
    /* #endregion */

    /* #region Private Methods */
    /* #endregion */

    /* #region Constructors */
    public KeySymbol(int image) {}
    /* #endregion */

    /* #region Public Methods */
    public int GetImage() {
        return Image;
    }
    public string GetColor() {
        return ThreadColor;
    }
    public int GetStrand() {
        return ThreadCount;
    }
    public int? GetCount() {
        return StitchCount;
    }
    public int? SetCount(int? stitchCount) {
        StitchCount = stitchCount;
        return stitchCount;
    }
    public string getBrand() {
        return Brand;
    }
    /* #endregion */
}