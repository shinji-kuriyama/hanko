// License: MIT, see the LICENSE file for details.
using System.Drawing.Drawing2D;
using System.Globalization;


namespace Project1 {
    public class DrawDateFormat {
        public static readonly string typename = "date";


        /// <summary> see `Hanko.get_parsers` </summary>
        public static object parse(string value) {
            var tmp = value.Split(",");
            float pt = 10f, x = 0f, y = 0f;
            if (!float.TryParse(tmp[0].Trim(), out pt)) {
                Console.WriteLine("date: invalid data {0}", tmp[1]);
            }
            if (tmp.Length < 2) {
                Console.WriteLine("date: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[1].Trim(), out x)) {
                Console.WriteLine("date: invalid data {0}", tmp[1]);
            }
            if (tmp.Length < 3) {
                Console.WriteLine("date: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[2].Trim(), out y)) {
                Console.WriteLine("date: invalid data {0}", tmp[2]);
            }
            var fmt = tmp.Length < 4 ? format_default()
                                     : parse_format(tmp[3].Trim());
            return (pt, new Point((int)x, (int)y), fmt);
        }


        /// <summary> the default date-time format
        /// `yy/m/d-` ... ex) `25/9/9-`
        /// </summary>
        public static (int, string)[] format_default() {
            return new[] {
                (1, "yy"), (0, "/"),
                (1, "%M"), (0, "/"),
                (4, "d-")
            };
        }


        /// <summary> parse the format of date-time
        /// 
        /// - yyyy ... a year by 4 digit
        /// - yy   ... a year by 2 digit
        /// - mmm  ... a month by US format
        /// - mm   ... a month by 2 digit 01-12
        /// - m    ... a month by 1 or 2 digit 1-12
        /// - dd   ... a day by 2 digit 01-31
        /// - d    ... a day by 1 or 2 digit 1-31
        /// - d-   ... a day by 2 digit `1-`-31
        /// - -d   ... a day by 2 digit `-1`-31
        /// </summary>
        public static (int, string)[] parse_format(string fmt) {
            var ret = new List<(int, string)>();
            while (fmt.Length > 0) {
                var n = 0;
                if (fmt.StartsWith("yyyy")) {
                    n = 4; ret.Add((1, "yyyy"));
                } else if (fmt.StartsWith("yy")) {
                    n = 2; ret.Add((1, "yy"));
                } else if (fmt.StartsWith("mmm")) {
                    n = 3; ret.Add((2, "MMM"));
                } else if (fmt.StartsWith("mm")) {
                    n = 2; ret.Add((1, "MM"));
                } else if (fmt.StartsWith("m")) {
                    n = 1; ret.Add((1, "%M"));
                } else if (fmt.StartsWith("dd")) {
                    n = 2; ret.Add((1, "dd"));
                } else if (fmt.StartsWith("d-")) {
                    n = 2; ret.Add((4, "d-"));
                } else if (fmt.StartsWith("d")) {
                    n = 1; ret.Add((1, "%d"));
                } else if (fmt.StartsWith("-d")) {
                    n = 2; ret.Add((3, "-d"));
                } else {
                    n = 1; ret.Add((0, fmt.Substring(0, 1)));
                }
                if (n > 0) { fmt = fmt.Substring(n); }
            }
            return ret.ToArray();
        }


        /// <summary> see `HankoDraw.collect_draws` in use this function.
        /// - see parse_format() for inputs.
        /// </summary>
        public static bool draw(Graphics g, object src, Func<DateTime> fn) {
            if (!(src is ValueTuple<float, Point, (int, string)[]> src_fmt)) {
                return false;
            }
            var (pt, xy, format) = src_fmt;
            var txt = "";
            var dt = fn();
            foreach (var (typ, fmt) in format) {
                switch (typ) {
                case 0: txt += fmt; break;
                case 1: txt += dt.ToString(fmt); break;
                case 2: txt += dt.ToString(fmt, CultureInfo.CreateSpecificCulture("en-US")); break;
                case 3: {
                    var tmp = dt.ToString("%d");
                    txt += (tmp.Length < 2) ? ("-" + tmp): tmp;
                    break;}
                case 4: {
                    var tmp = dt.ToString("%d");
                    txt += (tmp.Length < 2) ? tmp + "-": tmp;
                    break;}
                default: throw new NotImplementedException();
                }
            }
            HankoDraw.draw_text(g, txt, pt, xy);
            return true;
        }
    }
}
