// License: MIT, see the LICENSE file for details.
using System;
using System.IO;
using System.Windows.Forms;


namespace Project1 {
    public class Options {
        public FileInfo config {get; protected set;}


        public static Options parse_args(string[] args) {
            var ret = new Options() {
                config = new FileInfo("hanko.ini"),
            };
            return ret;
        }
    }
}

