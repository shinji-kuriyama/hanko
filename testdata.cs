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
                (1, new Point(48, 48), 43f),
                (2, new Point(6, 37), new Point(94, 37)),
                (2, new Point(6, 63), new Point(94, 63)),
                ("Test office", 11f, new Point(50, 23)),
                (0, 14f, new Point(50, 50)),
                ("No Hanko", 10f, new Point(50, 75)),
            };
        }


        public static object[] test2() {
            return new object[] {
                (1, new Point(48, 48), 43f),
                (2, new Point(6, 37), new Point(94, 37)),
                (2, new Point(6, 63), new Point(94, 63)),
                (0, 14f, new Point(50, 50)),
                ("Dev2", 16f, new Point(50, 25)),
                ("Hanko1", 10f, new Point(50, 80)),
                ("Ale", 8f, new Point(25, 70)),
            };
        }


        public static object[] test3() {
            return new object[] {
                (1, new Point(48, 48), 43f),
                (2, new Point(6, 37), new Point(94, 37)),
                (2, new Point(6, 63), new Point(94, 63)),
                (0, 14f, new Point(50, 50)),
                ("Dep1", 11f, new Point(50, 13)),
                ("Dev2", 11f, new Point(50, 27)),
                ("Hanko2", 10f, new Point(50, 80)),
                ("Ale", 8f, new Point(25, 70)),
            };
        }
    }
}

