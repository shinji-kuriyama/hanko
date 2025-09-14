// License: MIT, see the LICENSE file for details.
using System.Drawing.Drawing2D;


namespace Project1 {
    public class DrawMode {
        public static readonly string typename = "mode";


        /// <summary> see `Hanko.get_parsers` </summary>
        public static object parse(string value) {
            switch (value.Trim().ToLower()) {
            case "0": return (98, SmoothingMode.Default);
            case "1": return (98, SmoothingMode.HighSpeed);
            case "2": return (98, SmoothingMode.HighQuality);
            case "3": return (98, SmoothingMode.None);
            case "4": return (98, SmoothingMode.AntiAlias);

            case "default": return (98, SmoothingMode.Default);
            case "highspeed": return (98, SmoothingMode.HighSpeed);
            case "highquality": return (98, SmoothingMode.HighQuality);
            case "none": return (98, SmoothingMode.None);
            case "antialias": return (98, SmoothingMode.AntiAlias);
            }
            return null;
        }


        /// <summary> see `HankoDraw.collect_draws` </summary>
        public static bool draw(Graphics g, object src, Func<int, string> fn) {
            if (!(src is ValueTuple<int, SmoothingMode> src_mode)) {
                return false;
            }
            var (n, mode) = src_mode;
            g.SmoothingMode = mode;
            return true;
        }
    }
}
