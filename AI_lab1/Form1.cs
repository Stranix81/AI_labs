using AI_lab1.Library.Enums;
using AI_lab1.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AI_lab1
{
    public partial class Form1 : Form
    {
        private (int rows, int cols) fieldSize = default;
        private (int x, int y)? cubePos = null;
        private (int x, int y)? cubePosPrevious = null;
        private (int x, int y)? markPos = null;
        private (int x, int y)? markPosPrevious = null;
        private AddingStates addingState = AddingStates.None;
        private Button[,] fieldCells;
        private CellStates[,] cellStates;
        private NodeForRestore?[,] nodesCurrent;
        private NodeForRestore?[,] nodesPrevious;
        private CancellationTokenSource showPathCancellation;

        public Form1()
        {
            InitializeComponent();

            radioButtonBFS.Checked = true;
            buttonRestore.Enabled = false;
            buttonSkip.Enabled = false;
            buttonClear.Enabled = false;

            fieldSize = (tableLayoutPanelField.RowCount, tableLayoutPanelField.ColumnCount);
            fieldCells = new Button[tableLayoutPanelField.RowCount, tableLayoutPanelField.ColumnCount];
            cellStates = new CellStates[tableLayoutPanelField.RowCount, tableLayoutPanelField.ColumnCount];
            nodesCurrent = new NodeForRestore[tableLayoutPanelField.RowCount, tableLayoutPanelField.ColumnCount];
            nodesPrevious = new NodeForRestore[tableLayoutPanelField.RowCount, tableLayoutPanelField.ColumnCount];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < fieldSize.rows; i++)
            {
                for (int j = 0; j < fieldSize.cols; j++)
                {
                    //the button that will be added to the tableLayoutPanel later
                    var button = new Button
                    {
                        //docking and marging the button to remove extra paddings in the tableLayoutPanel
                        Dock = DockStyle.Fill,
                        Margin = new Padding(0),
                        BackColor = Color.White,
                        Tag = (i, j),    //the button stores its location in the Tag field
                    };

                    cellStates[i, j] = CellStates.Clean;

                    button.Click += (s, eArgs) =>
                    {
                        var (x, y) = ((int, int))button.Tag;
                        switch (addingState)
                        {
                            case AddingStates.AddingCube:
                                {
                                    if (cubePos != null) //if u wanna place the cube on another cell
                                    {
                                        var (oldx, oldy) = cubePos.Value;
                                        fieldCells[oldx, oldy].BackColor = Color.White;
                                        fieldCells[oldx, oldy].Text = default;
                                    }

                                    button.BackColor = Color.Blue;
                                    button.Text = "Cube";
                                    cubePos = (x, y);
                                    nodesCurrent[x, y] = new NodeForRestore(x, y, "Cube", Color.Blue, false);
                                    break;
                                }
                            case AddingStates.AddingMark:
                                {
                                    if (markPos != null) //if u wanna place the mark on another cell
                                    {
                                        var (oldx, oldy) = markPos.Value;
                                        fieldCells[oldx, oldy].BackColor = Color.White;
                                        fieldCells[oldx, oldy].Text = default;
                                    }

                                    button.BackColor = Color.Red;
                                    button.Text = "Mark";
                                    markPos = (x, y);
                                    nodesCurrent[x, y] = new NodeForRestore(x, y, "Mark", Color.Red, false);
                                    break;
                                }
                            case AddingStates.AddingAbyss:
                                {
                                    button.BackColor = Color.Black;
                                    button.Text = "Abyss";
                                    cellStates[x, y] = CellStates.Abyss;
                                    nodesCurrent[x, y] = new NodeForRestore(x, y, "Abyss", Color.Black, true);
                                    break;
                                }
                            default:
                                { break; }
                        }
                    };
                    fieldCells[i, j] = button;
                    tableLayoutPanelField.Controls.Add(button, j, i);   //adding the button
                }
            }
        }

        #region Clicks handlers
        private void buttonCube_Click(object sender, EventArgs e)
        {
            addingState = AddingStates.AddingCube;
        }

        private void buttonMark_Click(object sender, EventArgs e)
        {
            addingState = AddingStates.AddingMark;
        }

        private void buttonAbyss_Click(object sender, EventArgs e)
        {
            addingState = AddingStates.AddingAbyss;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        #endregion Clicks handlers

        private void Clear()
        {
            radioButtonBFS.Checked = true;
            radioButtonDFS.Checked = false;
            radioButtonIDDFS.Checked = false;
            radioButtonBiBFS.Checked = false;
            cubePos = null;
            markPos = null;

            for (int i = 0; i < fieldSize.rows; i++)
                for (int j = 0; j < fieldSize.cols; j++)
                {
                    cellStates[i, j] = CellStates.Clean;
                    fieldCells[i, j].Image = null;
                    fieldCells[i, j].Text = "";
                    fieldCells[i, j].BackColor = Color.White;
                    nodesCurrent[i, j] = null;
                }

            addingState = AddingStates.None;

            buttonStart.Enabled = true;
            buttonRestore.Enabled = true;
            buttonAbyss.Enabled = true;
            buttonCube.Enabled = true;
            buttonMark.Enabled = true;
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (cubePos == null || markPos == null)
            {
                MessageBox.Show("The cube or the red mark are not placed on the field!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cubePosPrevious = cubePos;
            markPosPrevious = markPos;
            for (int i = 0; i < fieldSize.rows; i++)
            {
                for (int j = 0; j < fieldSize.cols; j++)
                    nodesPrevious[i, j] = nodesCurrent[i, j];
            }

            var solver = new Solver(cellStates);

            List<Node>? path = null;
            Func<(int x, int y), (int x, int y), List<Node>?>? findMethod = null;

            if (radioButtonIDDFS.Checked)
            {
                //FormL formForL = new();
                //formForL.ShowDialog();
                path = solver.FindPathIDS(cubePos.Value, markPos.Value, 64);
            }
            else
            {
                if (radioButtonBFS.Checked) findMethod = solver.FindPathBFS;
                else if (radioButtonDFS.Checked) findMethod = solver.FindPathDFS;
                else if (radioButtonBiBFS.Checked) findMethod = solver.FindPathBiBFS;
                else
                {
                    MessageBox.Show("Choose a search method (BFS, DFS, IDDFS or Bi-BFS)!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                path = findMethod(cubePos.Value, markPos.Value);    //finding the path
            }
            if (path == null)
            {
                MessageBox.Show("The path has not been found!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var node in path.Skip(1))
            {
                if (cellStates[node.X, node.Y] == CellStates.Clean)
                    cellStates[node.X, node.Y] = CellStates.Visited;
            }

            int totalSteps = (path.Count - 1);

            if(radioButtonIDDFS.Checked)
                MessageBox.Show(
                    $"The path has been found!\nSteps taken: {path.Count - 1}\nIteration count: {solver.iterCount - 1}\nMax O + C length: {solver.listsLengthMax}\n" +
                    $"L at which the solution is found: {solver.LFromIDS}\n",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            else
                MessageBox.Show(
                    $"The path has been found!\nSteps taken: {path.Count - 1}\nIteration count: {solver.iterCount - 1}\nMax O + C length: {solver.listsLengthMax}\n",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information
                );

            buttonStart.Enabled = false;
            buttonRestore.Enabled = false;
            buttonAbyss.Enabled = false;
            buttonClear.Enabled = false;
            buttonCube.Enabled = false;
            buttonMark.Enabled = false;
            showPathCancellation = new CancellationTokenSource();
            buttonSkip.Enabled = true;
            await ShowPathAsync(showPathCancellation.Token, path, Color.Purple);
        }

        private async Task ShowPathAsync(CancellationToken token, List<Node> path, Color color, bool markVisited = true)
        {
            foreach (var node in path)
            {
                var btn = fieldCells[node.X, node.Y];
                var original = btn.BackColor;

                if(!node.IsMeetingPoint)
                    btn.BackColor = color;
                else 
                    btn.BackColor = Color.Green;

                    btn.Text = node.Orientation.ToString();
                if (!token.IsCancellationRequested)
                    await Task.Delay(500);

                if (markVisited)
                {
                    if(!node.IsMeetingPoint)
                        btn.BackColor = Color.Gray;
                    else btn.BackColor = Color.Green;
                }
                else
                {
                    btn.BackColor = original;
                }
            }
            buttonClear.Enabled = true;
            buttonSkip.Enabled = false;
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            if (nodesPrevious == null)
            {
                MessageBox.Show("Use this at least after one run of the function!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cubePos = cubePosPrevious;
            markPos = markPosPrevious;

            for (int i = 0; i < fieldSize.rows; i++)
            {
                for (int j = 0; j < fieldSize.cols; j++)
                {
                    if (nodesPrevious[i, j] == null) continue;
                    fieldCells[i, j].BackColor = nodesPrevious[i, j].Color;
                    fieldCells[i, j].Text = nodesPrevious[i, j].ButtonText;
                    cellStates[i, j] = nodesPrevious[i, j].Abyss ? CellStates.Abyss : CellStates.Clean;
                    nodesCurrent[i, j] = nodesPrevious[i, j];
                }
            }
            buttonRestore.Enabled = false;
        }

        private void buttonSkip_Click(object sender, EventArgs e)
        {
            if (showPathCancellation == null) return;
            showPathCancellation.Cancel();
        }
    }
}
