// License: MIT, see the LICENSE file for details.
using System;
using System.IO;
using System.Windows.Forms;


namespace Project1 {
    public class Options {
        public FileInfo config {get; protected set;}


        public Options() {
            config = new FileInfo("hanko.ini");
        }


        public static Options parse_args(string[] args) {
            var ret = new Options();
            foreach (var arg in args) {
                var tmp = new FileInfo(arg);
                if (tmp.Exists) {
                    Console.WriteLine("the config file specified: {0}", arg);
                    ret.config = tmp;
                }
            }
            return ret;
        }
    }
}

