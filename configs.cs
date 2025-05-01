// License: MIT, see the LICENSE file for details.
using System.Collections.Generic;
using System.IO;


namespace Project1 {
    public class Hanko {
        public string title;
        public object[] data;


        public bool parse(string line) {
            return false;
        }


        public Hanko move_to_next(string line) {
            return new Hanko();
        }
    }


    public class Configs {
        public Hanko[] hankos;


        public static Configs parse_configs_file(FileInfo fname) {
            if (!fname.Exists) {
                return TestData.get_test_data();
            }

            var data = new List<Hanko>();
            using (var sr = fname.OpenText()) {
                var hnk = new Hanko();
                var line = sr.ReadLine();
                while (line != null) {
                    if (hnk.parse(line)) {
                        data.Add(hnk);
                        hnk = hnk.move_to_next(line);
                    }
                    line = sr.ReadLine();
                }
            }
            return new Configs() {
                hankos = data.ToArray(),
            };
        }
    }
}

