// License: MIT, see the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Project1 {
    public class Form1: Form {
        public Form1(Func<IEnumerable<Hanko>> items) {
            Width = 125;
            Height = 238;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            InitializeComponents(items);
        }


        public void InitializeComponents(Func<IEnumerable<Hanko>> fn_items) {
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
            var btu = new Button() {
                Top = 187, Left = 10, Width = 100, Height = 20,
                Text = "Reload",
            };
            Controls.AddRange(new Control[] {cmb, hnk, btn,
                                             dat, btu
                              });

            Func<int, string> fn = (n) => {
                if (n == 0) {
                    return dat.Value.ToString("yy/MM/dd");
                }
                return "undefined";
            };

            var items = fn_items();

            cmb.SelectedIndexChanged += (s, o) => {
                var n = (s as ComboBox).SelectedIndex;
                if (n >= 0) {
                    var h = items.ElementAt(n);
                    update_hanko(hnk, h, fn);
                }
            };
            dat.ValueChanged += (s, e) => {
                var n = cmb.SelectedIndex;
                if (n >= 0) {
                    var h = items.ElementAt(n);
                    update_hanko(hnk, h, fn);
                }
            };

            load_items(items, cmb);

            btu.Click += (s, e) => {
                items = fn_items();
                load_items(items, cmb);
            };
        }


        public void load_items(IEnumerable<Hanko> items, ComboBox cmb) {
            cmb.Items.Clear();
            foreach (var i in items) {
                cmb.Items.Add(i.title);
            }
            if (cmb.Items.Count > 0) {
                cmb.SelectedIndex = 0;
            }
        }


        public PictureBox create_hanko(int x, int y) {
            var ret = new PictureBox() {
                Top = x, Left = y, Width = 100, Height = 100,
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


        public static void update_hanko(PictureBox hnk,
                                        Hanko h, Func<int, string> fn
        ) {
            var bmp = new Bitmap(100, 100);
            var g = Graphics.FromImage(bmp);
            paint_hanko(g, h.data, fn);
            hnk.Image = bmp;
        }
    }
}

