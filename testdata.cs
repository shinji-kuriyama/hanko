// License: MIT, see the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Project1 {
    public class TestData {
        public static Configs get_test_data() {
            return new Configs() {
                hankos = new List<Hanko>() {
                    new Hanko() {title = "hanko.ini does", data = test1()},
                    new Hanko() {title = "not found. this", data = test2()},
                    new Hanko() {title = "is dummy data", data = test3()},
                }.ToArray(),
            };
        }


        public static object[] test1() {
            return new object[] {
                (1, new Point(50, 50), 45f),
                (2, new Point(6, 37), new Point(94, 37)),
                (2, new Point(6, 63), new Point(94, 63)),
                ("Test office", 10f, new Point(50, 30)),
                (0, 10f, new Point(50, 50)),
                ("No Hanko", 10f, new Point(50, 80)),
            };
        }


        public static object[] test2() {
            return new object[0];
        }


        public static object[] test3() {
            return new object[0];
        }
    }
}

