// License: MIT, see the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;


namespace Project1 {
    public class Hanko {
        public string title = "";
        public object[] data = new object[0];

        public bool is_valid {get => data != null && data.Length > 0;}


        public bool parse(string line) {
            line = Configs.trim_comments(line);
            if (line.Length < 1) {
                return false;
            }
            if (Configs.is_section(line)) {
                return true;
            }
            var inst = Configs.parse_statement(line);
            if (inst == null) {
                return false;
            }
            var tmp = data.ToList();
            tmp.Add(inst);
            data = tmp.ToArray();
            return false;
        }
    }


    public class Configs {
        public Hanko[] hankos = new Hanko[0];


        public static Configs parse_configs_file(FileInfo fname) {
            if (!fname.Exists) {
                return TestData.get_test_data();
            }

            var data = new List<Hanko>();
            using (var sr = fname.OpenText()) {
                var hnk = new Hanko() {title = "", data = new object[0]};
                var line = sr.ReadLine();
                while (line != null) {
                    if (hnk.parse(line)) {
                        if (hnk.title.Length > 0) {data.Add(hnk);}
                        hnk = move_to_next(line);
                    }
                    line = sr.ReadLine();
                }
                if (hnk.is_valid) {data.Add(hnk);}
            }
            var dat2 = expand_data(data);
            return new Configs() {
                hankos = dat2,
            };
        }


        public static Hanko move_to_next(string line) {
            var t = parse_section(line);
            return new Hanko() {
                title = t,
                data = new object[0] {},
            };
        }


        public static Hanko[] expand_data(IEnumerable<Hanko> src) {
            var ret = new List<Hanko>();
            foreach (var i in src) {
                if (i.title.StartsWith("_")) {continue;}

                var tmp = new List<object>();
                foreach (var j in i.data) {
                    var (n, sec) = is_include(j);
                    if (n != 99) {
                        tmp.Add(j);
                        continue;
                    }
                    foreach (var k in expand_include(src, n, sec)) {
                        tmp.Add(k);
                    }
                }
                i.data = tmp.ToArray();
                ret.Add(i);
            }
            return ret.ToArray();
        }


        public static (int, string) is_include(object src) {
            if (src is ValueTuple<int, string> inc) {
                return inc;
            }
            return (0, "");
        }


        public static IEnumerable<object> expand_include(
                IEnumerable<Hanko> src, int n, string sec
        ) {
            if (n != 99) {yield break;}

            var hnk = src.Where((x) => x.title == sec);
            if (hnk.Count() < 1) {yield break;}

            foreach (var i in hnk.First().data) {
                var (n_sub, sec_sub) = is_include(i);
                if (n_sub != 99) {
                    yield return i;
                    continue;
                }
                foreach (var j in expand_include(src, n_sub, sec_sub)) {
                    yield return j;
                }
            }
        }


        public static string trim_comments(string src) {
            src = src.Trim();
            if (src.StartsWith("#")) {return "";}
            if (!src.Contains(";") &&
                !src.Contains("#")) {return src;}
            var ret = "";
            foreach (var ch in src.Reverse()) {
                ret = ch + ret;
            }
            return ret;
        }


        public static bool is_section(string src) {
            src = src.Trim();
            if (!src.StartsWith("[")) {return false;}
            if (!src.EndsWith("]")) {return false;}
            return true;
        }


        public static string parse_section(string src) {
            src = src.Substring(1, src.Length - 2);
            src = src.Trim();
            return src;
        }


        public static object parse_statement(string src) {
            if (!src.Contains("=")) {
                return null;
            }
            var l_and_r = src.Split('=');
            var typ = l_and_r[0].Trim();
            var val = string.Join('=', l_and_r.Skip(1)).Trim();
            return parse_values(typ, val);
        }


        public static object parse_values(string typ, string values) {
            switch (typ) {
            case "text":    return parse_text(values);
            case "date":    return parse_date(values);
            case "line":    return parse_line(values);
            case "circle":  return parse_circle(values);
            case "include": return parse_include(values);
            default:
                throw new NotImplementedException(
                        "type?: " + typ + "=>" + values);
            }
        }


        public static object parse_include(string values) {
            return (99, values);
        }


        public static object parse_text(string values) {
            var tmp = values.Split(",");
            float pt = 10f, x = 0f, y = 0f;
            if (tmp.Length < 2) {
                Console.WriteLine("text: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[1].Trim(), out pt)) {
                Console.WriteLine("text: invalid data {0}", tmp[1]);
            }
            if (tmp.Length < 3) {
                Console.WriteLine("text: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[2].Trim(), out x)) {
                Console.WriteLine("text: invalid data {0}", tmp[2]);
            }
            if (tmp.Length < 4) {
                Console.WriteLine("text: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[3].Trim(), out y)) {
                Console.WriteLine("text: invalid data {0}", tmp[3]);
            }
            return (tmp[0].Trim(), pt, new Point((int)x, (int)y));
        }


        public static object parse_date(string values) {
            var tmp = values.Split(",");
            float pt = 10f, x = 0f, y = 0f;
            if (!float.TryParse(tmp[0].Trim(), out pt)) {
                Console.WriteLine("text: invalid data {0}", tmp[1]);
            }
            if (tmp.Length < 2) {
                Console.WriteLine("text: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[1].Trim(), out x)) {
                Console.WriteLine("text: invalid data {0}", tmp[1]);
            }
            if (tmp.Length < 3) {
                Console.WriteLine("text: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[2].Trim(), out y)) {
                Console.WriteLine("text: invalid data {0}", tmp[2]);
            }
            return (0, pt, new Point((int)x, (int)y));
        }


        public static object parse_line(string values) {
            var tmp = values.Split(",");
            int n = 0;
            float x1 = 0f, y1 = 0f, x2 = 0f, y2 = 0f;
            if (!int.TryParse(tmp[0].Trim(), out n)) {
                Console.WriteLine("text: invalid data {0}", tmp[0]);
            }
            if (tmp.Length < 2) {
                Console.WriteLine("text: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[1].Trim(), out x1)) {
                Console.WriteLine("text: invalid data {0}", tmp[1]);
            }
            if (tmp.Length < 3) {
                Console.WriteLine("text: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[2].Trim(), out y1)) {
                Console.WriteLine("text: invalid data {0}", tmp[2]);
            }
            if (tmp.Length < 4) {
                Console.WriteLine("text: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[3].Trim(), out x2)) {
                Console.WriteLine("text: invalid data {0}", tmp[3]);
            }
            if (tmp.Length < 4) {
                Console.WriteLine("text: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[4].Trim(), out y2)) {
                Console.WriteLine("text: invalid data {0}", tmp[4]);
            }
            return (n, new Point((int)x1, (int)y1),
                       new Point((int)x2, (int)y2));
        }


        public static object parse_circle(string values) {
            var tmp = values.Split(",");
            float x1 = 0f, y1 = 0f, phi = 10f;
            if (!float.TryParse(tmp[0].Trim(), out x1)) {
                Console.WriteLine("text: invalid data {0}", tmp[0]);
            }
            if (tmp.Length < 2) {
                Console.WriteLine("text: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[1].Trim(), out y1)) {
                Console.WriteLine("text: invalid data {0}", tmp[1]);
            }
            if (tmp.Length < 3) {
                Console.WriteLine("text: data length  {0}", tmp.Length);
            } else if (!float.TryParse(tmp[2].Trim(), out phi)) {
                Console.WriteLine("text: invalid data {0}", tmp[2]);
            }
            return (1, new Point((int)x1, (int)y1), phi);
        }
    }
}

