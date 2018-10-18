using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NorrlandsShoppen
{ 
    class MyForm : Form
    {
          // Lägger till denna som instans så att alla ovasett om man är i main/myform eller i en metod kan använda den.
        TableLayoutPanel table;
        
        public MyForm()
        {
            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Fill
            };
            Controls.Add(flow);

            // Samlar bilderna i en array som heter filenames från mappen images
            string[] filenames = Directory.GetFiles("images");
            // För varje namn i arrayen filenames
            foreach (string name in filenames)
            {
                // Skapa bilden
                CreatePicture(name);
            }

            // C:\Users\Viktor\source\repos\Projektarbete\Projektarbete\Shop.txt
            string path = @"C:/Users/perle/source/repos/NorrlandsShoppen/NorrlandsShoppen/TextFile1.txt";
            List<string> items = new List<string> { };
            // Lägger till artikellistan(sökvägen) i listan som heter articles
            items.Add(File.ReadAllText(path)); 

            // Listbox för att visa artiklarna, hur få in listan i listboxen?
            ListBox itemsList = new ListBox();
            foreach (string line in items)
            {
                string[] separatedItems = line.Split(',');

                for (int i = 0; i <= separatedItems.Length; i++)
                {
                    separatedItems[i] = "Item" + i;
                }
                itemsList.Items.AddRange(separatedItems);

            }
            // För att få koll på antal artiklar(till kvittot/eller)
            int numbersOfItems = 0;

            // För att ha en varukorg som de kan fara i
            List<string> shoppingCartList = new List<string> { };

            // För att visa de man lagt till shoppingcart i en box för anv.
            ListBox shoppingCartBox = new ListBox{};
            foreach (string line in shoppingCartList)
            {
                string[] separatedItems = line.Split(',');
                // För varje char i speareradeitemslistan(strängarna) så ska de add to shoppingcartbox 
                for (int i = 0; i <= separatedItems.Length; i++)  
                {
                    separatedItems[i] = "Item" + i;
                }
                shoppingCartBox.Items.AddRange(separatedItems);
                // För att få koll på antal artiklar(till kvittot/eller)
                numbersOfItems = +1;
            }
            Console.WriteLine("You have " + numbersOfItems + "in your shoppingcart");

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

            // Detta blir rubriken över listboxen med artiklar
            panel.Controls.Add(new Label
            {
                Text = "Items",
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            });

            // Detta är rutan för bilden
            PictureBox pic = new PictureBox();
            {
                pic.Size = new Size(210, 110);
                Controls.Add(pic);
            }

            // Detta blir typ rubriken över listboxen med valda artiklar
            panel.Controls.Add(new Label
            {
                Text = "Shopping Cart",
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            });

            // Detta blir rubriken över listboxen med information om varan
            panel.Controls.Add(new Label
            { 
                Text = "About your item;",
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            });

            // Box för att printa ut info o bild om det man väljer
            ListBox aboutArticle = new ListBox();

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
            panel.Controls.Add(shoppingCartBox);
            panel.Controls.Add(aboutArticle);
            panel.Controls.Add(summa);
            panel.Controls.Add(pic);
            Controls.Add(panel);
        }

        // Metod för att få fram en bild. anropa bilden:CreatPicture(sökvägen till bilden) !! Omg
        private void CreatePicture(string path)
        {
            PictureBox box1 = new PictureBox
            {
                Image = Image.FromFile(path),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 150,
                Height = 150
            };
            // flow.Controls.Add(box1); fick error på flow "do not exist"
        }

        void ClickedEventHandler(object sender, EventArgs e)
        {
            // Här kan man sätta in kvittot? dvs shoppingCart
            MessageBox.Show("Sådääär då har du köpt en massa"); 
        }

        void ClickedDiscountButton(object sender, EventArgs e)
        {
            // Summan av (shoppingCart - (det som rabattkoden referar till)) så ska de läggas till i som Pris
            MessageBox.Show("Braaa nu spara du massa para bror!");
            // Här kan man göra om för att kunna anv discount. Hejsan!
        }


        // Oklart om detta ens behöver vara med? 
        static System.Drawing.Image FromFile(string filename) 
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

        void Form1_Load(object sender, EventArgs e)
        {
            // Construct an image object from a file in the local directory.
            // ... This file must exist in the solution.
            Image image = Image.FromFile("BeigeMonitor1.png");
            // Set the PictureBox image property to this image.
            // ... Then, adjust its height and width properties.
            //pictureBox1.Image = image;
            //pictureBox1.Height = image.Height;
            //pictureBox1.Width = image.Width;
        }                
    }
} 




