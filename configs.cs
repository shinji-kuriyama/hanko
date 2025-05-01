// License: MIT, see the LICENSE file for details.
using System.Collections.Generic;
using System.IO;


namespace Project1 {
    public class Hanko {
        public string title;
    }


    public class Configs {
        public Hanko[] hankos;


        public static Configs parse_configs_file(FileInfo fname) {
            if (!fname.Exists) {
                return test_data();
            }

            using (var sr = fname.OpenText()) {
                var line = sr.ReadLine();
                while (line != null) {
                    line = sr.ReadLine();
                }
            }
            return new Configs() {
            };
        }


        public static Configs test_data() {
            return new Configs() {
                hankos = new List<Hanko>() {
                    new Hanko() {title = "test1", },
                }.ToArray(),
            };
        }


        public IEnumerable<string> get_items() {
            foreach (var i in hankos) {
                yield return i.title;
            }
        }
    }
}

