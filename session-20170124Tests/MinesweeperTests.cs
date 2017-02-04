using NUnit.Framework;

namespace session_20170124.Tests
{
    [TestFixture]
    public class MinesweeperTests
    {
        [Test]
        public void ThereIsNoBombInEmptyField()
        {
            var minesweeper = new Minesweeper(new bool[0, 0]);
            Assert.IsFalse(minesweeper.IsBomb(0, 0));
        }

        [Test]
        public void ThereIsABombAtFieldWithBomb()
        {
            var minesweeper = new Minesweeper(new[,] { { true } });
            Assert.IsTrue(minesweeper.IsBomb(0, 0));
        }

        [Test]
        public void CellWithoutBombsInNeighborhoodHasScoreZero()
        {
            var minesweeper = new Minesweeper(new[,] { { false, false }, { false, false } });
            Assert.AreEqual(0, minesweeper.GetCellScore(0, 0));
        }

        [Test]
        public void CellWithOneBombInNeighborhoodHasScoreOne()
        {
            var minesweeper = new Minesweeper(new[,] { { false, true }, { false, false } });
            Assert.AreEqual(1, minesweeper.GetCellScore(0, 0));
        }

        [Test]
        public void CellWithThreeBombsInNeighborhoodHasScoreThree()
        {
            var minesweeper = new Minesweeper(new[,] { { true, true }, { false, true } });
            Assert.AreEqual(3, minesweeper.GetCellScore(1, 0));
        }

        [Test]
        public void CellWithEightBombsInNeighborhoodHasScoreEight()
        {
            var minesweeper = new Minesweeper(new[,] { { true, true, true }, { true, false, true }, { true, true, true } });
            Assert.AreEqual(8, minesweeper.GetCellScore(1, 1));
        }

        [Test]
        public void CellWithTwoBombsInNeighborhoodHasScoreTwo()
        {
            var minesweeper = new Minesweeper(new[,] { { false, true, true }, { false, false, false }, { false, false, false } });
            Assert.AreEqual(2, minesweeper.GetCellScore(1, 2));
        }

        [Test]
        public void CellWithTwoBombsAtFieldAndZeroInNeighborhoodHasScoreZero()
        {
            var minesweeper = new Minesweeper(new[,] { { false, true, true }, { false, false, false }, { false, false, false } });
            Assert.AreEqual(0, minesweeper.GetCellScore(2, 0));
        }

        [Test]
        public void CellWithThreeBombsAtFieldAndZeroInNeighborhoodHasScoreZero()
        {
            var minesweeper = new Minesweeper(new[,] { { false, false, false }, { false, false, false }, { true, true, true } });
            Assert.AreEqual(0, minesweeper.GetCellScore(0, 0));
        }

        [Test]
        public void CellWithThreeBombsInNeighborhoodHasAtLeastOneBombInNeighborhood()
        {
            var minesweeper = new Minesweeper(new[,] { { false, false, false }, { false, false, false }, { true, true, true } });
            Assert.IsTrue(minesweeper.CellHasAtLeastOneBombInNeighborhood(1, 1));
        }

        [Test]
        public void CellWithoutAnyBombInNeighborhoodHasntAnyBombInNeighborhood()
        {
            var minesweeper = new Minesweeper(new[,] { { false, false, false }, { false, false, false }, { true, true, true } });
            Assert.IsFalse(minesweeper.CellHasAtLeastOneBombInNeighborhood(0, 0));
        }

        [Test]
        public void CellWithBoombInNeighborhoodIsShownAsCellScore()
        {
            var minesweeper = new Minesweeper(new[,] { { false, false, false }, { false, false, false }, { true, true, true } });
            Assert.AreEqual('-', minesweeper.ShowCell(0, 1));
        }

        [Test]
        public void ChoosenCellWithThreeBombsInNeighborhoodWillBeShown()
        {
            var minesweeper = new Minesweeper(new[,] { { false, false, false }, { false, false, false }, { true, true, true } });
            minesweeper.CellChoosen(1, 1);
            Assert.AreEqual(" 0 1 2\n0- - -\n1- 3 -\n2- - -\n", minesweeper.GetActualViewOfField());
        }

        [Test]
        public void AllNeighborsOfCellWithoutAnyBombInNeighborhoodWillBeShown()
        {
            var minesweeper = new Minesweeper(new bool[3, 3]);
            minesweeper.CellChoosen(1, 1);
            Assert.AreEqual(" 0 1 2\n0     \n1     \n2     \n", minesweeper.GetActualViewOfField());
        }

        [Test]
        public void AllCellsExceptTheBombsOnTheEdgesWillBeDisplayedOnMiddleBombChoosen()
        {
            var minesweeper = new Minesweeper(new[,] { { true, true, true, true, true }, { true, false, false, false, true }, { true, false, false, false, true }, { true, false, false, false, true }, { true, true, true, true, true }});
            minesweeper.CellChoosen(2, 2);
            Assert.AreEqual(" 0 1 2 3 4\n0- - - - -\n1- 5 3 5 -\n2- 3   3 -\n3- 5 3 5 -\n4- - - - -\n", minesweeper.GetActualViewOfField());
        }

        [Test]
        public void CreatesViewRepresentaionOfField()
        {
            var minesweeper = new Minesweeper(new[,] { { false, false, false }, { false, false, false }, { false, false, true} });
            minesweeper.CellChoosen(0, 0);
            Assert.AreEqual(" 0 1 2\n0     \n1  1 1\n2  1 -\n", minesweeper.GetActualViewOfField());
        }

        [Test]
        public void ReturnsNextRoundViewRepresentationOfField()
        {
            var minesweeper = new Minesweeper(new[,] { { false, false, false }, { false, false, false }, { false, false, true } });
            Assert.AreEqual(" 0 1 2\n0- - -\n1- 1 -\n2- - -\n", minesweeper.NextRound(1, 1));
        }

        [Test]
        public void ReturnsGameOverOnBombChoosen()
        {
            var minesweeper = new Minesweeper(new[,] { { false, false, false }, { false, false, false }, { false, false, true } });
            Assert.AreEqual("Game over!", minesweeper.NextRound(2, 2));
        }

        [Test]
        public void PlayerWinsWhenAllCellsWithoutBombAreShown()
        {
            var minesweeper = new Minesweeper(new[,] { { false, false, false }, { false, false, false }, { false, false, true } });
            minesweeper.CellChoosen(0, 0);
            Assert.IsTrue(minesweeper.PlayerWin());
        }

        [Test]
        public void ReturnsYouWinWhenAllCellsWihoutBomsAreShown()
        {
            var minesweeper = new Minesweeper(new[,] { { false, false, false }, { false, false, false }, { false, false, true } });
            Assert.AreEqual("You win!", minesweeper.NextRound(0, 0));
        }
    }
}