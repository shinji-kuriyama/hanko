// License: MIT, see the LICENSE file for details.
using System;
using System.Windows.Forms;


namespace Project1 {
    public static class Program {
        public static int Main(string[] args) {
            var opts = Options.parse_args(args);

            Application.Run(new Form1());
            return 0;
        }
    }
}

