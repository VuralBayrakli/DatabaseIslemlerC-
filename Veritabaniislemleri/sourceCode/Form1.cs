using MySqlConnector;

namespace WinTableGrid
{
    public partial class Form1 : Form
    {

        private List<String[]> changedRows = new List<string[]>();

        public Form1()
        {
            InitializeComponent();
        }

        public void DGV()
        {
            
            //string aa = dataGridView1.Rows[0].Cells[0];
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                
                
              
                mySqlConnection1.Open();
                string query = "select * from urunler";
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection1);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int urunID = reader.GetInt32(0);
                        string urunAdi = reader.GetString(1);
                        string urunMiktari = reader.GetString(2);
                        string urunBirimi = reader.GetString(3);
                        string urunKategorisi = reader.GetString(4);
                        
                        dataGridView1.Rows.Add(new string[5] { urunID.ToString(), urunAdi, urunMiktari, urunBirimi, urunKategorisi });
                    }
                }
                
                //label1.Text = rowIndex.ToString();
                reader.Close();
                mySqlConnection1.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mySqlConnection1.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataGridViewRow row1 = new DataGridViewRow();
            row1 = dataGridView1.Rows[0];

            if (changedRows.Count > 0)
            {
                foreach (string[] rows in changedRows)
                {

                    string query = "UPDATE urunler set urunadi=@UrunAdi, urunbirimi=@UrunMiktari, urunfiyati=@urunfiyati, urunkategori=@UrunKategori WHERE urunid=" + int.Parse(rows[0].ToString());
                    cmd = new MySqlCommand(query, mySqlConnection1);
                    adapter.UpdateCommand = new MySqlCommand(query, mySqlConnection1);
                    cmd.Parameters.AddWithValue("@UrunAdi", rows[1]);
                    cmd.Parameters.AddWithValue("@UrunMiktari", rows[2].ToString());
                    cmd.Parameters.AddWithValue("@urunfiyati", rows[3].ToString());
                    cmd.Parameters.AddWithValue("@UrunKategori", rows[4].ToString());

                    textBox1.Text = rows[4].ToString();
                    
                    cmd.ExecuteNonQuery();
                    

                }
                

            }
            changedRows.Clear();
            mySqlConnection1.Close();

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = "";
            DataGridViewRow row;
            DataGridViewCellCollection rowCells;
            int rowIndex = e.RowIndex;
            if (rowIndex != -1 && rowIndex != dataGridView1.Rows.Count)
            {
                textBox1.Text = rowIndex.ToString() + dataGridView1.Rows.Count.ToString(); ;

  


                rowCells = dataGridView1.Rows[rowIndex].Cells;
               



            }
            if (rowIndex == dataGridView1.Rows.Count - 2 && rowIndex != -1)
            {
                label1.Text = dataGridView1.Rows.Count.ToString();
                rowCells = dataGridView1.Rows[rowIndex].Cells;
                label1.Text = (rowCells[0].Value.ToString());
                if (rowCells[0].Value != null && rowCells[1].Value != null && rowCells[2].Value != null && rowCells[3].Value != null && rowCells[4].Value != null)
                {
                    changedRows.Add(new string[] { rowCells[0].Value.ToString(), rowCells[1].Value.ToString(), rowCells[2].Value.ToString(), rowCells[3].Value.ToString(), rowCells[4].Value.ToString() });

                }
                

                //changedRows.Add(new string[] { rowCells[0].Value.ToString(), rowCells[1].Value.ToString(), rowCells[2].Value.ToString(), rowCells[3].Value.ToString(), rowCells[4].Value.ToString() });
            }

        }   

      
        private void button3_Click(object sender, EventArgs e)
        {
            mySqlConnection1.Open();
            string query;
            int veriEklenecekSatir = dataGridView1.Rows.Count - 2;
            DataGridViewCellCollection rowCells;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            rowCells = dataGridView1.Rows[veriEklenecekSatir].Cells;
           
            if (changedRows.Count > 0)
            {
                foreach (string[] rows in changedRows)
                {
                    if(!rows[0].Equals("") && !rows[1].Equals("") && !rows[2].Equals("") && !rows[3].Equals("") && !rows[4].Equals(""))
                    {
                        query = "insert into urunler(urunid, urunadi, urunbirimi, urunfiyati, urunkategori) values(@urunid, @urunadi, @urunbirimi, @urunfiyati, @urunkategori)";
                        cmd = new MySqlCommand(query, mySqlConnection1);
                        adapter.InsertCommand = new MySqlCommand(query, mySqlConnection1);

                        cmd.Parameters.AddWithValue("@urunid", int.Parse(rows[0]));
                        cmd.Parameters.AddWithValue("@urunadi", rows[1]);
                        cmd.Parameters.AddWithValue("@urunbirimi", rows[2]);
                        cmd.Parameters.AddWithValue("@urunfiyati", rows[3]);
                        cmd.Parameters.AddWithValue("@urunkategori", rows[4]);

                        cmd.ExecuteNonQuery();
                        
                    }
                    else
                        MessageBox.Show("Boþ Veri girdiniz!!!");


                }


            }
            changedRows.Clear();
            mySqlConnection1.Close();


        }


    }
}