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
    class Product
    {
       public string Name;
       public int Price;
       public string AboutItem;
    }

    class MyForm : Form
    {   // Lägger till instans så ovasett om man är i main/myforom/metod kan nå
        PictureBox box1;
        String curItem;
        ListBox itemsList;
        ListBox shoppingCart;

        // För att slippa skapa ny label varjegång kan vi anropa denna metod och ha texten som parameter
        private void Label(string text)
        {
            Label label = new Label
            {
                Text = text,
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            };
        }

        // Metod för att få fram en bild. anropa CreatPicture(sökvägen till bilden)
        private void CreatePicture(string path)
        {
            PictureBox box1 = new PictureBox
            {
                Image = Image.FromFile(path),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 150,
                Height = 150
            };
            // flow.Controls.Add(box1); fick error på flow "do not exist", lägga till som instans? men med panel ist då vår sån heter panel
        }

       
        void ClickedEventHandler(object sender, EventArgs e)
        {
            
            // Här kan man sätta in kvittot? dvs shoppingCart
            MessageBox.Show("Här kommer ditt kvitto på ditt köp:" + curItem);
        }

        void ClickedDiscountButton(object sender, EventArgs e)
        {
            // Summan av (shoppingCart - (det som rabattkoden referar till)) så ska de läggas till i som Pris vilket är [2] då priset är nr
            MessageBox.Show("Braaa nu spara du massa para bror!");
            // Här kan man göra om för att kunna anv discount. Hejsan!
        }

        void ClickedAddToCart(object sender, EventArgs e)
        {
            MessageBox.Show("Din vara är lagd till i korgen.");
            // Här kan man göra om för att kunna lägga till den valda item i itemList till SHoppingCart
            // Om selected index är en siffra elika m eller midre antal index i itemsLists
            // if (itemsList.SelectedIndex <= itemsList)// om detta är true 
            {
                itemsList.Items.Add(shoppingCart.SelectedItem);
                //itemsList[0].Items.AddRange(shoppingCartBox); så ska selected index gå till shoppingcartList

                // Lägg till den i shoppingCartBox [1] men för att visa namnet på den.  itemsList.Items.AddRange(shoppingCartBox)
                // itemsList.Items.AddRange(shoppingCartBox);
            }
        }

        public MyForm()
        {
            string[] filenames = Directory.GetFiles("images");
           
            foreach (string name in filenames)
            {
                CreatePicture(name);
            }

            ListBox itemsList = new ListBox();
            {
                itemsList.Height = 635;
                itemsList.Width = 635;
                itemsList.HorizontalScrollbar = true;
                itemsList.DoubleClick += ClickedEventHandler;
            };

            string[] lines = File.ReadAllLines("Shop.txt");
            List<string> items = new List<string> { };
            
            foreach (string line in lines)
            {
                string[] separatedItems = line.Split(';');

                //// Allt i filen delas upp i detta objekt rad för rad.
                Product p = new Product
                {
                    Name = separatedItems[0],
                    Price = int.Parse(separatedItems[1]),
                    AboutItem = separatedItems[2],
                };
                // items= datatyp list, där enbart Namnet på artikeln sparas från shop.txt.
                items.Add(p.Name);                                            
            }
            itemsList.Items.AddRange(items.ToArray());
                                 
            List<string> shoppingCartList = new List<string> { };
            ListBox shoppingCartBox = new ListBox();
            {
                shoppingCartBox.Height = 635;
                shoppingCartBox.Width = 635;
                shoppingCartBox.HorizontalScrollbar = true;
            }

            foreach (string line in shoppingCartList)
            {
                string[] separatedItems = line.Split(',');
                // För varje char i speareradeitemslistan(strängarna) så ska de add to shoppingcartbox 
                for (int i = 0; i <= separatedItems.Length; i++)
                {
                    // Lägg till de från separatedItems tills shoppingcartbox   
                    shoppingCartBox.Items.AddRange(separatedItems);
                }                
            }

            //Det finns en metod för att index från textfilen ska över till den andra via clickeventet, skapa klickevent som säger de index som är valt ska över till shoppingCart
            // gör detta som en klickmetod
            // För att visa shoppingCartList för anv.

            // To clear all selections in the ListBox.
            //shoppingCartBox.ClearSelected();

            TableLayoutPanel panel = new TableLayoutPanel
            {
                RowCount = 6,
                ColumnCount = 3,
                BackColor = Color.Black,
                Dock = DockStyle.Fill
            };
            
            // Detta blir rubriken över listboxen med artiklar
            Label item = new Label
            {
                Text = "Items",
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.White,
                Dock = DockStyle.Top          
            };

            // Detta är rutan för bilden
            PictureBox pic = new PictureBox();
            {
                pic.Size = new Size(210, 110);
                Controls.Add(pic);
            }

            // Detta blir typ rubriken över listboxen med valda artiklar själva listboxen finns högre upp
            Label shoppingCart = new Label 
            {
                Text = "Shopping Cart",
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.White,
                Dock = DockStyle.Top
            };

         
            // Label("About your item;");

            Label aboutItem = new Label
            {
                Text = "About your item;",
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.White,
                Dock = DockStyle.Top
            };

            // Box för att printa ut info o bild om det man väljer
            ListBox aboutItemBox = new ListBox();
            {
                aboutItemBox.Height = 635;
                aboutItemBox.Width = 635;
                aboutItemBox.HorizontalScrollbar = true;
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

            Label sum = new Label
            {
                Text = "Total sum of pruchase:",
                BackColor = Color.Orange,
                Dock = DockStyle.Fill
            };

            Button addToCart = new Button
            {
                Text = "Add to Cart",
                BackColor = Color.Pink,
                Dock = DockStyle.Fill
            };
            addToCart.Click += ClickedAddToCart;

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));

            panel.Controls.Add(discButton);
            panel.Controls.Add(addToCart);
            panel.Controls.Add(buyButton);
            panel.Controls.Add(itemsList);
            panel.Controls.Add(shoppingCartBox);
            panel.Controls.Add(aboutItemBox);
            panel.Controls.Add(sum);
            panel.Controls.Add(pic);
            Controls.Add(panel);
            itemsList.Controls.Add(item);
            shoppingCartBox.Controls.Add(shoppingCart);
            aboutItemBox.Controls.Add(aboutItem);
            panel.Controls.Add(box1);
            Controls.Add(box1);
        }         
    }
} 