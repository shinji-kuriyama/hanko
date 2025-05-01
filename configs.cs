// License: MIT, see the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;


namespace Project1 {
    public class Hanko {
        public string title;
        public object[] data;

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
        public Hanko[] hankos;


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
            return new Configs() {
                hankos = data.ToArray(),
            };
        }


        public static Hanko move_to_next(string line) {
            var t = parse_section(line);
            return new Hanko() {
                title = t,
                data = new object[0] {},
            };
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
            if (!float.TryParse(tmp[1].Trim(), out float pt)) {
                throw new FormatException();
            }
            if (!float.TryParse(tmp[2].Trim(), out float x)) {
                throw new FormatException();
            }
            if (!float.TryParse(tmp[3].Trim(), out float y)) {
                throw new FormatException();
            }
            return (tmp[0].Trim(), pt, new Point((int)x, (int)y));
        }


        public static object parse_date(string values) {
            var tmp = values.Split(",");
            if (!float.TryParse(tmp[0].Trim(), out float pt)) {
                throw new FormatException();
            }
            if (!float.TryParse(tmp[1].Trim(), out float x)) {
                throw new FormatException();
            }
            if (!float.TryParse(tmp[2].Trim(), out float y)) {
                throw new FormatException();
            }
            return (0, pt, new Point((int)x, (int)y));
        }


        public static object parse_line(string values) {
            return null;
        }


        public static object parse_circle(string values) {
            return null;
        }
    }
}

