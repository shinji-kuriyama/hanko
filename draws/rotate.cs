// License: MIT, see the LICENSE file for details.
using System.Drawing.Drawing2D;


namespace Project1 {
    public class DrawRotate {
        public static readonly string typename = "rotate";


        /// <summary> see `Hanko.get_parsers` </summary>
        public static object parse(string value) {
            var tmp = value.Split(',').Select(x => x.Trim()).ToArray();
            if (!decimal.TryParse(tmp[0], out decimal degree)) {
                return null;
            }
            if (tmp.Length < 2) {
                return (degree, -1.0, -1.0);
            }
            if (tmp.Length != 3) {
                return null;
            }
            if (!decimal.TryParse(tmp[1], out decimal x)) {
                return null;
            }
            if (!decimal.TryParse(tmp[2], out decimal y)) {
                return null;
            }
            return (degree, (double)x, (double)y);
        }


        /// <summary> see `HankoDraw.collect_draws` in use this function.
        /// - x == y == -1 ... auto calculate the center point.</summary>
        public static bool draw(Graphics g, object src, Func<int, string> fn) {
            if (!(src is ValueTuple<decimal, double, double> src_rot)) {
                return false;
            }
            var (deg, x, y) = src_rot;
            if (x == -1 && y == -1) {
                var (xp, yp) = (50.0, 50.0);
                var r = (double)-deg * Math.PI / 180;
                x = xp * Math.Cos(r) - yp * Math.Sin(r);
                y = xp * Math.Sin(r) + yp * Math.Cos(r);
            }
            g.TranslateTransform((float)x, (float)y, MatrixOrder.Append);
            g.RotateTransform((float)deg);
            return true;
        }
    }
}
