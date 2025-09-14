// License: MIT, see the LICENSE file for details.
using System;
using System.Drawing;
using System.Reflection;

using fn_text = System.Func<int, string>;
using fn_draw = System.Func<System.Drawing.Graphics,
                            object, System.Func<int, string>, bool>;
using Draws = System.Collections.Generic.List<
                    System.Func<System.Drawing.Graphics,
                                object, System.Func<int, string>, bool>>;


namespace Project1 {
    public static class HankoDraw {
        /// <summary> functions to draw the hanko part </summary>
        public static Draws draws = null;


        public static void draw(Graphics g, object src, fn_text fn) {
            foreach (var i in collect_draws()) {
                if (i(g, src, fn)) {return;}
            }
            if (src is ValueTuple<string, float, Point> txt) {
                var (t, p, pt) = txt;
                draw_text(g, t, p, pt);
            } else if (src is ValueTuple<int, float, Point> txv) {
                var t = fn(txv.Item1);
                draw_text(g, t, txv.Item2, txv.Item3);
            } else if (src is ValueTuple<int, Point, float> cle) {
                draw_circle(g, cle.Item2, cle.Item3);
            } else if (src is ValueTuple<int, Point, Point> lin) {
                draw_line(g, lin.Item2, lin.Item3);
            } else {
                new NotImplementedException();
            }
        }


        /// <summary> collect drawing functions </summary>
        public static Draws collect_draws(Assembly assm = null) {
            if (draws != null) {return draws;}

            assm = assm ?? Assembly.GetExecutingAssembly();
            var d = new Draws();
            foreach (var cls in assm.GetTypes()) {
                if (!cls.IsClass) {continue;}
                if (!cls.GetFields().Any(x => x.Name == "typename")) {continue;}
                var typ = cls.GetField("typename")?.GetValue(null) as string;
                if (!cls.Name.StartsWith("Draw")) {continue;}
                foreach (var i in cls.GetMethods()) {
                    if (!i.IsStatic) {continue;}
                    if (i.ReturnType != typeof(bool)) {continue;}
                    var prms = i.GetParameters();
                    if (prms.Length != 3) {continue;}
                    if (prms[0].ParameterType != typeof(Graphics)) {continue;}
                    if (prms[1].ParameterType != typeof(object)) {continue;}
                    if (prms[2].ParameterType != typeof(fn_text)) {continue;}
                    d.Add((fn_draw)i.CreateDelegate(typeof(fn_draw), null));
                }
            }
            draws = d;
            return d;
        }


        public static void draw_text(
            Graphics g, string txt, float pt, Point p
        ) {
            var ft = new Font("Arial", pt);
            var siz = g.MeasureString(txt, ft);
            var ptf = new PointF(p.X - siz.Width / 2,
                                 p.Y - siz.Height / 2);
            g.DrawString(txt, ft, Brushes.Black, ptf);
        }


        public static void draw_circle(Graphics g, Point pt, float phi) {
            var p = new Pen(Brushes.Black) {
                Width = 2.0f,
            };
            var (x1, y1) = ((int)(pt.X - phi - 0.5), (int)(pt.Y - phi - 0.5));
            var (x2, y2) = ((int)(pt.X + phi + 0.5), (int)(pt.Y + phi + 0.5));
            g.DrawEllipse(p, x1, y1, x2, y2);
        }


        public static void draw_line(Graphics g, Point p1, Point p2) {
            var p = new Pen(Brushes.Black) {
                Width = 2.0f,
            };
            g.DrawLine(p, p1.X, p1.Y, p2.X, p2.Y);
        }
    }
}

