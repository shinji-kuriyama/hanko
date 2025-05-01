// License: MIT, see the LICENSE file for details.
using System;
using System.Windows.Forms;


namespace Project1 {
    public class Options {
        public string config {get; protected set;}


        public static Options parse_args(string[] args) {
            var ret = new Options() {
                config = "hanko.ini",
            };
            return ret;
        }
    }
}

