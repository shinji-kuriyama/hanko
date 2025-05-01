// License: MIT, see the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Project1 {
    public static class Program {
        [STAThread]
        public static int Main(string[] args) {
            var opts = Options.parse_args(args);

            Func<IEnumerable<Hanko>> fn = () => {
                var cfgs = Configs.parse_configs_file(opts.config);
                return cfgs.hankos;
            };

            Application.Run(new Form1(fn));
            return 0;
        }
    }
}

