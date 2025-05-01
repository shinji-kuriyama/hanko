// License: MIT, see the LICENSE file for details.
using System;
using System.Drawing;


namespace Project1 {
    public static class HankoDraw {
        public static void draw(Graphics g, object src) {
            if (src is ValueTuple<string, float, Point> txt) {
                var (t, p, pt) = txt;
                draw_text(g, t, p, pt);
            } else if (src is ValueTuple<int, Point, float> cle) {
                draw_circle(g, cle.Item2, cle.Item3);
            } else if (src is ValueTuple<int, Point, Point> lin) {
                draw_line(g, lin.Item2, lin.Item3);
            } else {
                new NotImplementedException();
            }
        }


        public static void draw_text(
            Graphics g, string txt, float pt, Point p
        ) {
            var ft = new Font("", 10f);
            var siz = g.MeasureString(txt, ft, (int)pt);
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

