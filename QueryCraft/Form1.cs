using System.Windows.Forms;

namespace QueryCraft
{
    public partial class Form1 : Form
    {
        BindingSource albumBindingSource = new BindingSource();
        BindingSource tracksBindingSource = new BindingSource();
        List<Album> albums = new List<Album>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();

            //connect the list to the grid view control
            albums = albumsDAO.getAllAlbums();
            albumBindingSource.DataSource = albums;
            dataGridView1.DataSource = albumBindingSource;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();

            //connect the list to the grid view control
            albumBindingSource.DataSource = albumsDAO.searchTitles(textBox1.Text);
            dataGridView1.DataSource = albumBindingSource;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            //get the row number clicked

            int rowClicked = dataGridView.CurrentRow.Index;

            String imageURL = dataGridView.Rows[rowClicked].Cells[4].Value.ToString();

            pictureBox1.Load(imageURL);



            tracksBindingSource.DataSource = albums[rowClicked].Tracks;
            dataGridView2.DataSource = tracksBindingSource;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //add a new item to the database
            Album album = new Album
            {
                AlbumName = txt_name.Text,
                ArtistName = txt_artist.Text,
                Year = Int32.Parse(txt_year.Text),
                ImageURL = txt_imageurl.Text,
                Description = txt_description.Text,

            };
            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.addOneAlbum(album);
            MessageBox.Show(result + "new row(s) inserted");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //get the row number clicked

            int rowClicked = dataGridView2.CurrentRow.Index;
            int trackID = (int) dataGridView2.Rows[rowClicked].Cells[0].Value;

            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.deleteTrack(trackID);
            dataGridView2.DataSource = null;
            albums = albumsDAO.getAllAlbums();
        }
    }
}