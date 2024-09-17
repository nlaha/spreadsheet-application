// <copyright file="TestFibonacciReader.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Homework3Tests
{
    using System.Numerics;
    using Homework3;

    public class TestFibonacciReader
    {
        [Test]
        public void FibonacciReader_ReturnsSingleFibonacci()
        {
            // Arrange
            var fibTextReader = new FibonacciTextReader(50);

            // Act
            string? result = string.Empty;
            for (int i = 0; i < 27; i++)
            {
                result = fibTextReader.ReadLine();
            }

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo("27: 121393"));
        }

        [Test]
        public void FibonacciReader_ReturnsFirst50Numbers()
        {
            // Arrange
            var fibTextReader = new FibonacciTextReader(50);

            // Act
            var result = fibTextReader.ReadToEnd();

            // Assert
            Assert.That(result, Is.SubsetOf(@"
                1: 0
                2: 1
                3: 1
                4: 2
                5: 3
                6: 5
                7: 8
                8: 13
                9: 21
                10: 34
                11: 55
                12: 89
                13: 144
                14: 233
                15: 377
                16: 610
                17: 987
                18: 1597
                19: 2584
                20: 4181
                21: 6765
                22: 10946
                23: 17711
                24: 28657
                25: 46368
                26: 75025
                27: 121393
                28: 196418
                29: 317811
                30: 514229
                31: 832040
                32: 1346269
                33: 2178309
                34: 3524578
                35: 5702887
                36: 9227465
                37: 14930352
                38: 24157817
                39: 39088169
                40: 63245986
                41: 102334155
                42: 165580141
                43: 267914296
                44: 433494437
                45: 701408733
                46: 1134903170
                47: 1836311903
                48: 2971215073
                49: 4807526976
                50: 7778742049
            "));
        }

        [Test]
        public void FibonacciReader_ReturnsFirst100Numbers()
        {
            // Arrange
            var fibTextReader = new FibonacciTextReader(50);

            // Act
            var result = fibTextReader.ReadToEnd();

            // Assert
            Assert.That(result, Is.SubsetOf(@"
                1: 0
                2: 1
                3: 1
                4: 2
                5: 3
                6: 5
                7: 8
                8: 13
                9: 21
                10: 34
                11: 55
                12: 89
                13: 144
                14: 233
                15: 377
                16: 610
                17: 987
                18: 1597
                19: 2584
                20: 4181
                21: 6765
                22: 10946
                23: 17711
                24: 28657
                25: 46368
                26: 75025
                27: 121393
                28: 196418
                29: 317811
                30: 514229
                31: 832040
                32: 1346269
                33: 2178309
                34: 3524578
                35: 5702887
                36: 9227465
                37: 14930352
                38: 24157817
                39: 39088169
                40: 63245986
                41: 102334155
                42: 165580141
                43: 267914296
                44: 433494437
                45: 701408733
                46: 1134903170
                47: 1836311903
                48: 2971215073
                49: 4807526976
                50: 7778742049
                51: 12586269025
                52: 20365011074
                53: 32951280099
                54: 53316291173
                55: 86267571272
                56: 139583862445
                57: 225851433717
                58: 365435296162
                59: 591286729879
                60: 956722026041
                61: 1548008755920
                62: 2504730781961
                63: 4052739537881
                64: 6557470319842
                65: 10610209857723
                66: 17167680177565
                67: 27777890035288
                68: 44945570212853
                69: 72723460248141
                70: 117669030460994
                71: 190392490709135
                72: 308061521170129
                73: 498454011879264
                74: 806515533049393
                75: 1304969544928657
                76: 2111485077978050
                77: 3416454622906707
                78: 5527939700884757
                79: 8944394323791464
                80: 14472334024676221
                81: 23416728348467685
                82: 37889062373143906
                83: 61305790721611591
                84: 99194853094755497
                85: 160500643816367088
                86: 259695496911122585
                87: 420196140727489673
                88: 679891637638612258
                89: 1100087778366101931
                90: 1779979416004714189
                91: 2880067194370816120
                92: 4660046610375530309
                93: 7540113804746346429
                94: 12200160415121876738
                95: 19740274219868223167
                96: 31940434634990099905
                97: 51680708854858323072
                98: 83621143489848422977
                99: 135301852344706746049
                100: 218922995834555169026
            "));
        }

        [Test]
        public void FibonacciReader_ThrowsOnNegativeInput()
        {
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new FibonacciTextReader(-22));
        }

        [Test]
        public void FibonacciReader_ReturnsEmptyOnZeroInput()
        {
            // Arrange
            var fibTextReader = new FibonacciTextReader(0);

            // Act
            var result = fibTextReader.ReadToEnd();

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }
    }
}