using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro
{
    public partial class Form1 : Form
    {
        int size = 19;
        int playerTurn = 1; // for player 1,    2 for player 2
        int col, row; // position in the grid
        int count;
        bool winner = false; 

        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initializeBoard(size);
        }

        private void initializeBoard(int size)
        {
            clearGameBoard();
            gameBoard.BackgroundColor = Color.White;
            gameBoard.DefaultCellStyle.BackColor = Color.White;
            count = 0;
            winner = false;
            playerTurn = 1;
            if (size == 3)
            {
                gameBoard.Columns.Add(dataGridViewColumn: Column1); Column1.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column2); Column2.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column3); Column3.Visible = true;
            }
            if (size == 5)
            {
                gameBoard.Columns.Add(dataGridViewColumn: Column1); Column1.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column2); Column2.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column3); Column3.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column4); Column4.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column5); Column5.Visible = true;
            }
            if (size == 7)
            {
                gameBoard.Columns.Add(dataGridViewColumn: Column1); Column1.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column2); Column2.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column3); Column3.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column4); Column4.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column5); Column5.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column6); Column6.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column7); Column7.Visible = true;
            }
            if (size == 19)
            {
                gameBoard.Columns.Add(dataGridViewColumn: Column1); Column1.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column2); Column2.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column3); Column3.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column4); Column4.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column5); Column5.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column6); Column6.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column7); Column7.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column8); Column8.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column9); Column9.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column10); Column10.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column11); Column11.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column12); Column12.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column13); Column13.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column14); Column14.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column15); Column15.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column16); Column16.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column17); Column17.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column18); Column18.Visible = true;
                gameBoard.Columns.Add(dataGridViewColumn: Column19); Column19.Visible = true;
            }
            
            for (int row = 0; row < gameBoard.ColumnCount; row++)
            {
                gameBoard.Rows.Add();
            }
            foreach (DataGridViewColumn c in gameBoard.Columns)
            {
                c.Width = gameBoard.Width / gameBoard.Columns.Count;
            }
            foreach (DataGridViewRow r in gameBoard.Rows)
            {
                r.Height = (gameBoard.Height) / gameBoard.Rows.Count;
            }
            for (int row = 0; row < gameBoard.Rows.Count; row++)
            {
                for (int col = 0; col < gameBoard.Columns.Count; col++)
                {
                    gameBoard[col, row].ReadOnly = true;
                }
            }
        }

        private void clearGameBoard()
        {
            for (int i = 0; i < gameBoard.ColumnCount; i++)
                gameBoard.Columns.Clear();
        }
        private void x19ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 19;
            clearGameBoard();            
            initializeBoard(size);
        }

        private void x7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 7;
            initializeBoard(size);
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            size = 5;
            initializeBoard(size);
        }

        private void formatCell(int row, int col, string letter)
        {
            DataGridViewCell c = gameBoard[row, col];
            c.Style.BackColor = Color.White;
            c.ReadOnly = false;
            c.Style.SelectionBackColor = Color.Cyan;
            c.Tag = letter;
        }

        private void gameBoard_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {   
            Turn.Text = "Playing: PLAYER " + playerTurn;           

            col = e.ColumnIndex;
            row = e.RowIndex;
            gameBoard[col, row].Style.SelectionBackColor = Color.Green;
            if (gameBoard[col, row].Value == null)
            {
                if (playerTurn == 1)
                {
                    gameBoard[col, row].Value = "X";
                    playerTurn = 2;
                }                
                else
                {
                    gameBoard[col, row].Value = "O";
                    playerTurn = 1;
                }                        
            }
            checkForWinner(col,row, count);
        }

        private void checkForWinner(int col, int row, int count)
        {
            
            checkHorizontal(col, row, count);

            label1.Text = "rowIndex = " + row + " colIndex= " + col;           
            

            if (winner)
            {
                MessageBox.Show(gameBoard[col, row].Value + " Win");                
                initializeBoard(size);
            }     
            
        }       

        private void checkHorizontal(int col, int row, int count)
        {
            if (gameBoard[col, row].Value != null)
            {
                int minCol = 0;               

                if (col - 4 >= 0)
                    minCol = col - 4;
                
                //check horizontal    
                for (int i = minCol; i <= col + 4 && i >= 0 && i < size; i++)
                {
                    System.Console.WriteLine("i: {0} ", i);
                    if (gameBoard[i, row].Value == gameBoard[col, row].Value && gameBoard[i, row].Value != null)
                    {
                        count++;
                    }
                }
                if (count >= 5)
                {
                    winner = true;
                }
                else
                {
                    count = 0;
                    checkVertical(col, row, count);
                }
            }
        }
        private void checkVertical(int col, int row, int count)
        {
            //check vertical    
            int minRow = 0;
            if (row - 4 >= 0)
                minRow = row - 4;
            for (int i = minRow; i <= col + 4 && i >= 0 && i < size; i++)
            {
                System.Console.WriteLine("i: {0} ", i);
                if (gameBoard[col, i].Value == gameBoard[col, row].Value && gameBoard[col, i].Value != null)
                {
                    count++;
                }
            }
            if (count >= 5)
                winner = true;
            else
            {
                count = 0;
                checkDiagonal1(col, row, count);
            }
        }
        private void checkDiagonal1(int col, int row, int count)
        {
            int minCol = 0, minRow = 0, maxCol = 0, maxRow = 0;
            int minNumber = Math.Min(col, row);
            int maxNumber = Math.Max(col, row);
            if (minNumber - 4 < 0)
            {
                minCol = col - minNumber;
                minRow = row - minNumber;
            }
            else
            {
                minCol = col - minNumber;
                minRow = row - minNumber;
            }
            if (maxNumber + 4 > size-1)
            {
                maxCol = col + ((size-1)- maxNumber);
                maxRow = row + ((size-1) - maxNumber);
            }
            else
            {
                maxRow = row + maxNumber;
                maxCol = col + maxNumber;
            }

            label3.Text = "maxCol: " + maxCol.ToString() + " maxRow: " + maxRow.ToString();

            for (int i = minRow; i <= maxRow; i++)
            {
                Count.Text = "Count: " + count.ToString();

                if (gameBoard[minCol, i].Value == gameBoard[col, row].Value)
                {
                    count++;
                    Count.Text = "Count: " + count.ToString();

                }
                minCol += 1;
            }
            if (count >= 5)
            {
                winner = true;
            }
            else
            {
                count = 0;
                //checkDiagonal2(col, row, count);
            }

        }
        
    }
}
