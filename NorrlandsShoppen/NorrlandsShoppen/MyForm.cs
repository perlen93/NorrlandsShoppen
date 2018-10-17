using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NorrlandsShoppen
{ class Product
    {
        string Name;
        string Description;
        int Number;
        int Price;
    };

    class MyForm : Form
    {
        public MyForm()
        //två olika sätt att göra det på antingen list, eller listview
        {   string path = @"C:/Users/perle/source/repos/NorrlandsShoppen/NorrlandsShoppen/TextFile1.txt"; // C:\Users\Viktor\source\repos\Projektarbete\Projektarbete\Shop.txt
            
            List<string> items = new List<string>();
            items.Add(File.ReadAllText(path)); //lägger till artikellistan(sökvägen) i listan som heter articles
            
            foreach (string line in items) // för att ta varje rad i items(artikellistan) o splittar så de blir en separat sträng där vi lagt ett ,
            {
                string[] values = line.Split(','); //Vad innebär/är valuies?
                items.Add(line);
            }

                // List<string> shoppingcart = new List<string>(); //för att ha en varukorg som de kan fara i
                // ListBox shoppingCart= new ListBox();

            

                TableLayoutPanel panel = new TableLayoutPanel
            {
                RowCount = 6,
                ColumnCount = 3,
                BackColor = Color.Black,
                Dock = DockStyle.Fill
            };

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));


            panel.Controls.Add(new Label
            {
                Text = "Items", //Detta blir typ rubriken över listboxen med artiklar
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill


            });
            ListBox itemsList = new ListBox(); //Lägga alla listbox i en innerpanel för att de ska ändra strlk när man ändrar strkl på fönstret. inner panel måste ha docksytle.fill
            foreach(string a in items)
            {
                a.Add(itemsList); // lägg till den i BoxView SKRIVA UT DEN? 
            };

            PictureBox pic = new PictureBox();
            {
                pic.Size = new Size(210, 110);
                Controls.Add(pic);
            }

            


            panel.Controls.Add(new Label
            {
                Text = "Shopping Cart", //Detta blir typ rubriken över listboxen med valda artiklar
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            });
            int numbersOfItems = 0;
            ListBox shoppingCart = new ListBox(); //lista för valen som man valt från articles1
            Console.WriteLine("You have " + numbersOfItems + "in your shoppingcart");
            // foreach (string i in shoppingCart)
            // {
            numbersOfItems = +1; //för att få koll på antal artiklar(till kvittot/eller)
                                 //a.Add(articles1); // lägg till den i BoxView SKRIVA UT DEN? 
                                 //};

            panel.Controls.Add(new Label
            {
                Text = "About your item;", //Detta blir typ rubriken över listboxen med information om varan
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            });
            ListBox aboutArticle = new ListBox(); //box för att printa ut info o bild om det man väljer



            panel.Controls.Add(new TextBox
            {
                Text = "Please enter discount code here",
                BackColor = Color.Red,
                Dock = DockStyle.Fill
            });

            Button discButton = new Button
            {
                Text = "USE DISCOUNT",
                BackColor = Color.Pink,
                Dock = DockStyle.Fill
            };
            discButton.Click += ClickedDiscountButton;

            Button buyButton = new Button
            {
                Text = "BUY",
                BackColor = Color.Yellow,
                Dock = DockStyle.Fill
            };
            buyButton.Click += ClickedEventHandler;

            Label summa = new Label
            {
                Text = "Total sum of pruchase:",
                BackColor = Color.Orange,
                Dock = DockStyle.Fill
            };

            panel.Controls.Add(discButton);
            panel.Controls.Add(buyButton);
            panel.Controls.Add(itemsList);
            panel.Controls.Add(shoppingCart);
            panel.Controls.Add(aboutArticle);
            panel.Controls.Add(summa);
            Controls.Add(panel);

        }
        private void ClickedEventHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Sådääär då har du köpt en massa"); //här kan man sätta in kvittot? dvs shoppingCart
        }
        private void ClickedDiscountButton(object sender, EventArgs e)
        {
            //typ summan av (shoppingCart - (det som rabattkoden referar till)) så ska de läggas till i som Pris
            MessageBox.Show("Braaa nu spara du massa para bror!"); //här kan man göra om för att kunna anv discount. Hejsan!
        }


        
        public static System.Drawing.Image FromFile(string filename)
        {
           
            Image bastuflotte = Image.FromFile("c:\\bastuflotte.jpg");
            Image hembrant = Image.FromFile("c:\\hembrant.jpg");
            Image kyla = Image.FromFile("c:\\Kyla.jpg");
            Image norrlandsTröja = Image.FromFile("c:\\norrlandströja.jpg");
            Image dialekt = Image.FromFile("c:\\Norrländska.jpg");
            Image norrsken = Image.FromFile("c:\\norrsken.jpg");
            Image ren = Image.FromFile("c:\\ren.jpg");
            Image skoter = Image.FromFile("c:\\skoter.jpg");
            Image snö = Image.FromFile("c:\\snö.jpg");
            Image värme = Image.FromFile("c:\\värme.jpg");
            Image älg = Image.FromFile("c:\\älg.jpg");
           

            return bastuflotte;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Construct an image object from a file in the local directory.
            // ... This file must exist in the solution.
            Image image = Image.FromFile("BeigeMonitor1.png");
            // Set the PictureBox image property to this image.
            // ... Then, adjust its height and width properties.
            pictureBox1.Image = image;
            pictureBox1.Height = image.Height;
            pictureBox1.Width = image.Width;
        }



    }
}




