// License: MIT, see the LICENSE file for details.
using System.Collections.Generic;
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
            var hnk = new Button() {
                Top = 35, Left = 10, Width = 100, Height = 100,
            };
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
        }
    }
}

