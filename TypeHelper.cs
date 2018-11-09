using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TypeHelper
{
    public partial class TypeHelper : Form
    {
        int nMaxId = 0;
        Random rnd;
        public TypeHelper()
        {
            InitializeComponent();
        }
        //procedure
        public void Init()
        {
            String sQuery = "select max(id) from typehelper";
            String sId = "0";
            DataTable dt = DataBase.executeQuery(sQuery);
            foreach (DataRow row in dt.Rows)
            {
                sId = row.ItemArray[0].ToString();
                if (sId == "")
                    sId = "0";
            }
            nMaxId = int.Parse(sId);
        }
        public void RefreshData()
        {
            if (nMaxId == 0)
                return;
            txt_typed.Text = "";
            int nId = rnd.Next(nMaxId + 1);
            if (nId == 0)
                nId = 1;
            String sQuery = "select word,explanation,example from typehelper where id=" + nId;
            DataTable dt = DataBase.executeQuery(sQuery);
            foreach (DataRow row in dt.Rows)
            {
                txt_word.Text = row.ItemArray[0].ToString();
                txt_explanation.Text = row.ItemArray[1].ToString();
                txt_example.Text = row.ItemArray[2].ToString();
            }
        }
        public void SetTyped(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                txt_typed.Text += "q";
            }
            else if (e.KeyCode == Keys.W)
            {
                txt_typed.Text += "w";
            }
            else if (e.KeyCode == Keys.E)
            {
                txt_typed.Text += "e";
            }
            else if (e.KeyCode == Keys.R)
            {
                txt_typed.Text += "r";
            }
            else if (e.KeyCode == Keys.T)
            {
                txt_typed.Text += "t";
            }
            else if (e.KeyCode == Keys.Y)
            {
                txt_typed.Text += "y";
            }
            else if (e.KeyCode == Keys.U)
            {
                txt_typed.Text += "u";
            }
            else if (e.KeyCode == Keys.I)
            {
                txt_typed.Text += "i";
            }
            else if (e.KeyCode == Keys.O)
            {
                txt_typed.Text += "o";
            }
            else if (e.KeyCode == Keys.P)
            {
                txt_typed.Text += "p";
            }
            else if (e.KeyCode == Keys.A)
            {
                txt_typed.Text += "a";
            }
            else if (e.KeyCode == Keys.S)
            {
                txt_typed.Text += "s";
            }
            else if (e.KeyCode == Keys.D)
            {
                txt_typed.Text += "d";
            }
            else if (e.KeyCode == Keys.F)
            {
                txt_typed.Text += "f";
            }
            else if (e.KeyCode == Keys.G)
            {
                txt_typed.Text += "g";
            }
            else if (e.KeyCode == Keys.H)
            {
                txt_typed.Text += "h";
            }
            else if (e.KeyCode == Keys.J)
            {
                txt_typed.Text += "j";
            }
            else if (e.KeyCode == Keys.K)
            {
                txt_typed.Text += "k";
            }
            else if (e.KeyCode == Keys.L)
            {
                txt_typed.Text += "l";
            }
            else if (e.KeyCode == Keys.Z)
            {
                txt_typed.Text += "z";
            }
            else if (e.KeyCode == Keys.X)
            {
                txt_typed.Text += "x";
            }
            else if (e.KeyCode == Keys.C)
            {
                txt_typed.Text += "c";
            }
            else if (e.KeyCode == Keys.V)
            {
                txt_typed.Text += "v";
            }
            else if (e.KeyCode == Keys.B)
            {
                txt_typed.Text += "b";
            }
            else if (e.KeyCode == Keys.N)
            {
                txt_typed.Text += "n";
            }
            else if (e.KeyCode == Keys.M)
            {
                txt_typed.Text += "m";
            }
        }
        //eventi
        private void btn_import_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;


            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            int nSize = -1;
            DialogResult result = fdlg.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = fdlg.FileName;
                try
                {
                    String[] lines = File.ReadAllLines(file);
                    nSize = lines.Length;
                    int nCurrLine = 0;
                    int nNoOfRecords = 0;
                    while (nCurrLine < nSize)
                    {
                        String sWord = lines[nCurrLine++];
                        String sExplanation = lines[nCurrLine++];
                        String sExample = lines[nCurrLine++];
                        String sQuery = "select count(*) from typehelper where word like '"+sWord+"'";
                        DataTable dt = DataBase.executeQuery(sQuery);
                        int nCount = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            nCount=int.Parse(row.ItemArray[0].ToString());
                        }
                        if (nCount == 0)
                        {
                            sQuery = "select max(id)+1 from typehelper";
                            dt = DataBase.executeQuery(sQuery);
                            String sId = "1";
                            foreach (DataRow row in dt.Rows)
                            {
                                sId = row.ItemArray[0].ToString();
                                if (sId == "")
                                    sId = "1";
                            }
                            sQuery = "insert into typehelper(id,word,explanation,example) values(" + sId + ",'" +
                                sWord.Replace("'", "''") + "','" +
                                sExplanation.Replace("'", "''") + "','" +
                                sExample.Replace("'", "''") + "')";
                            DataBase.executeNonQuery(sQuery);
                            nNoOfRecords++;
                        }
                    }
                    Init();
                    RefreshData();
                    // Set cursor as default arrow
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(nNoOfRecords + " records inserted.");
                     
                }
                catch (IOException)
                {
                }
            }
        }

        private void TypeHelper_Load(object sender, EventArgs e)
        {
            DataBase.Init();
            rnd = new Random();
            Init();
            RefreshData();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void TypeHelper_KeyDown(object sender, KeyEventArgs e)
        {
            SetTyped(e);
            String sLastTyped=txt_typed.Text.Substring(txt_typed.Text.Length - 1, 1);
            String sLastInWord=txt_word.Text.Substring(txt_word.Text.Length - 1, 1);
            if (sLastTyped == sLastInWord)//corect word
            {
                RefreshData();
            }
            else if (sLastTyped == txt_word.Text.Substring(txt_typed.Text.Length - 1, 1))//corect
            {
            }
            else
            {
                MessageBox.Show("Incorect.");
                txt_typed.Text = "";
            }
        }
    }
}
