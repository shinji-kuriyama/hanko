// License: MIT, see the LICENSE file for details.
using System;
using System.Windows.Forms;


namespace Project1 {
    public static class Program {
        public static int Main(string[] args) {
            var opts = Options.parse_args(args);
            var cfgs = Configs.parse_configs_file(opts.config);

            Application.Run(new Form1(cfgs.get_items()));
            return 0;
        }
    }
}

