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
                var h = selected_hanko(items, s as ComboBox);
                update_hanko(hnk, h, fn);
            };
            dat.ValueChanged += (s, e) => {
                var h = selected_hanko(items, cmb);
                update_hanko(hnk, h, fn);
            };

            load_items(items, cmb);

            btu.Click += (s, e) => {
                items = fn_items();
                load_items(items, cmb);
            };
            btn.Click += (s, e) => {
                var bmp = hnk.Image as Bitmap;
                if (copy_image(bmp)) {return;}
                var h = selected_hanko(items, cmb);
                if (h == null) {return;}
                Console.WriteLine("hanko({0}) was copied!", h.title);
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


        public static Hanko selected_hanko(IEnumerable<Hanko> items,
                                           ComboBox cmb
        ) {
            if (cmb == null) {return null;}
            var n = cmb.SelectedIndex;
            if (n < 0) {return null;}
            var h = items.ElementAt(n);
            return h;
        }


        public static void update_hanko(PictureBox hnk,
                                        Hanko h, Func<int, string> fn
        ) {
            var bmp = new Bitmap(100, 100);
            var g = Graphics.FromImage(bmp);
            paint_hanko(g, h.data, fn);
            hnk.Image = bmp;
            if (copy_image(bmp)) {return;}
            Console.WriteLine("hanko({0}) was copied!", h.title);
        }


        public static bool copy_image(Bitmap bmp) {
            if (bmp == null) {
                Console.WriteLine("failed to copy_image...");
                return true;
            }
            Clipboard.Clear();
            Clipboard.SetImage(bmp);
            return false;
        }
    }
}

