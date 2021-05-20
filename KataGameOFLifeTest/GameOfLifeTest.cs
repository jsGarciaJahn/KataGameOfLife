using KataGameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KataGameOfLifeTest
{
    [TestClass]
    public class GameOfLifeTest
    {
        [TestMethod]
        public void TestAllDead()
        {
            GameOfLife.Board = "000\n000\n000".Split('\n');
            GameOfLife.NextGen();

            CollectionAssert.AreEqual("000\n000\n000".Split('\n'), GameOfLife.Board);
        }

        [TestMethod]
        public void TestOneBirth()
        {
            GameOfLife.Board = "110\n100\n000".Split('\n');
            GameOfLife.NextGen();
            CollectionAssert.AreEqual("110\n110\n000".Split('\n'), GameOfLife.Board);
        }

        [TestMethod]
        public void TestSpinner()
        {
            GameOfLife.Board = "010\n010\n010".Split('\n');
            GameOfLife.NextGen();
            CollectionAssert.AreEqual("000\n111\n000".Split('\n'), GameOfLife.Board);
            GameOfLife.NextGen();
            CollectionAssert.AreEqual("010\n010\n010".Split('\n'), GameOfLife.Board);
        }
    }
}
