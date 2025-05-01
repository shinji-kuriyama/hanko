// License: MIT, see the LICENSE file for details.
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Project1 {
    public class Form1: Form {
        public Form1(IEnumerable<string> items) {
            InitializeComponents(items);
        }


        public void InitializeComponents(IEnumerable<string> items) {
            var cmb = new ComboBox() {
                Top = 10, Left = 10, Width = 100, Height = 20,
                DropDownStyle = ComboBoxStyle.DropDownList,
            };
            var hnk = create_hanko(35, 10);
            var btn = new Button() {
                Top = 140, Left = 10, Width = 100, Height = 20,
                Text = "Copy",
            };
            Controls.AddRange(new Control[] {cmb, hnk, btn});

            Width = 125;
            Height = 200;

            foreach (var i in items) {
                cmb.Items.Add(i);
            }
            if (cmb.Items.Count > 0) {cmb.SelectedIndex = 0;}
        }


        public Button create_hanko(int x, int y) {
            var ret = new Button() {
                Top = x, Left = y, Width = 100, Height = 100,
                Enabled = false,
            };
            ret.Paint += (s, o) => {
                paint_hanko(o.Graphics, TestData.test1());
            };
            return ret;
        }


        public static void paint_hanko(Graphics g, object[] data) {
            g.FillRectangle(Brushes.White, 0, 0, 100, 100);

            if (data == null) {return;}
            foreach (var i in data) {
                HankoDraw.draw(g, i);
            }
        }
    }
}

