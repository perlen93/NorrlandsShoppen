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
       PictureBox box1;

        public MyForm()
        {
            string[] filenames = Directory.GetFiles("images");
            foreach (string name in filenames)
            {

                CreatePicture(name);
            }
            string path = "Shop.txt";
            List<string> items = new List<string> { };
            items.Add(File.ReadAllText(path));

            ListBox itemsList = new ListBox();
            {
                itemsList.Height = 635;
                itemsList.Width = 635;
                itemsList.HorizontalScrollbar = true;
                var firstItem = items[0];
                //   itemsList.Click += itemsList.SelectedIndex;
                itemsList.DisplayMember = "Name";
                itemsList.Click += ClickedEventHandler;
               
            };
            
            foreach (string line in items)
            {
                string[] separatedItems = line.Split(';');

                itemsList.Items.AddRange(separatedItems);
            }

            List<string> shoppingCartList = new List<string> { };
            ListBox shoppingCartBox = new ListBox ();
             
            {
                shoppingCartBox.Height = 635;
                shoppingCartBox.Width = 635;
                shoppingCartBox.HorizontalScrollbar = true;
            }
            foreach (string line in shoppingCartList)
            {
               string[] separatedItems = line.Split(';');
                
            }
            int numbersOfItems = 0;
            Console.WriteLine("You have " + numbersOfItems + "in your shoppingcart");

            TableLayoutPanel panel = new TableLayoutPanel
            {
                RowCount = 6,
                ColumnCount = 3,
                BackColor = Color.Black,
                Dock = DockStyle.Fill
            };

            // Detta blir rubriken över listboxen med artiklar
            panel.Controls.Add(new Label
            {
                Text = "Items",
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill,
                

              });

            // Detta är rutan för bilden
            PictureBox pic = new PictureBox();
            {
                pic.Size = new Size(210, 110);
                Controls.Add(pic);
            }
            
            // Detta blir typ rubriken över listboxen med valda artiklar
            ListBox ShoppingCart = new ListBox
            {
                Text = "ShoppingCart",
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            };

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
            {


                aboutArticle.Height = 635;
                aboutArticle.Width = 635;
                aboutArticle.HorizontalScrollbar = true;

            }

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
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));

            panel.Controls.Add(discButton);
            panel.Controls.Add(buyButton);
            panel.Controls.Add(itemsList);
            panel.Controls.Add(shoppingCartBox);
            panel.Controls.Add(aboutArticle);
            panel.Controls.Add(summa);
            panel.Controls.Add(pic);
            panel.SetColumnSpan(itemsList, 3);
            Controls.Add(panel);
            panel.Controls.Add(box1);
            Controls.Add(box1);
            itemsList.Items.Add(ShoppingCart);
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


        void Form1_Load(object sender, EventArgs e)
        {
           //  Construct an image object from a file in the local directory.
           //  ... This file must exist in the solution.
           //Image image = Image.FromFile("BeigeMonitor1.png");
           //  Set the PictureBox image property to this image.
           //  ... Then, adjust its height and width properties.
           // pictureBox1.Image = image;
           // pictureBox1.Height = image.Height;
           // pictureBox1.Width = image.Width;  
        }

        // Få in en clickeventhandle med mousedubleclick på något vänster. Denna ska göra så att när man klickar på en rubrik så kommer info om detta upp. *DONE*
        //behöver fixa så att enbart artiklarna visas i listan.
    }
}




