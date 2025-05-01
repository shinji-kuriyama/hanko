// License: MIT, see the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Project1 {
    public class Form1: Form {
        public Form1(IEnumerable<Hanko> items) {
            Width = 125;
            Height = 219;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            InitializeComponents(items);
        }


        public void InitializeComponents(IEnumerable<Hanko> items) {
            var cmb = new ComboBox() {
                Top = 10, Left = 10, Width = 100, Height = 20,
                DropDownStyle = ComboBoxStyle.DropDownList,
            };
            var hnk = create_hanko(35, 10);
            var btn = new Button() {
                Top = 140, Left = 10, Width = 100, Height = 20,
                Text = "Copy",
            };
            var dat = new DateTimePicker() {
                Top = 165, Left = 10, Width = 100, Height = 20,
            };
            Controls.AddRange(new Control[] {cmb, hnk, btn,
                                             dat
                              });

            foreach (var i in items) {
                cmb.Items.Add(i.title);
            }
            if (cmb.Items.Count > 0) {cmb.SelectedIndex = 0;}

            dat.ValueChanged += (s, e) => {hnk.Invalidate();};

            Func<int, string> fn = (n) => {
                if (n == 0) {
                    return dat.Value.ToString("yy/MM/dd");
                }
                return "undefined";
            };

            var n_hanko = 0;
            cmb.SelectedIndexChanged += (s, o) => {
                var n = (s as ComboBox).SelectedIndex;
                if (n >= 0) {
                    n_hanko = n;
                    hnk.Invalidate();
                }
            };
            hnk.Paint += (s, o) => {
                var h = items.ElementAt(n_hanko);
                paint_hanko(o.Graphics, h.data, fn);
            };
        }


        public Button create_hanko(int x, int y) {
            var ret = new Button() {
                Top = x, Left = y, Width = 100, Height = 100,
                Enabled = false,
            };
            return ret;
        }


        public static void paint_hanko(
                Graphics g, object[] data, Func<int, string> fn
        ) {
            g.FillRectangle(Brushes.White, 0, 0, 100, 100);

            if (data == null) {return;}
            foreach (var i in data) {
                HankoDraw.draw(g, i, fn);
            }
        }
    }
}

