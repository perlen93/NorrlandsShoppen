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
    class Events
    {
        void ClickedEventHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Här kommer ditt kvitto på ditt köp:");
        }

        void ClickedDiscountButton(object sender, EventArgs e)
        {
            MessageBox.Show("Braaa nu spara du massa para bror!");
        }

        void ClickedAboutItem(object sender, EventArgs e) 
        {
            // Här kan man sätta in kvittot? dvs shoppingCart
            MessageBox.Show("hej");
            //  aboutItemView.Items.Add(itemsList.SelectedItem
        }

        void ClickedAddToCart(object sender, EventArgs e)
        {
            MessageBox.Show("Din vara är lagd till i korgen.");
            shoppingCartBox.Items.Add(itemsList.SelectedItem); // sätt objeket framför metoden
            itemsList.Items.Remove(shoppingCart);
        }
        public void ClickedRemoveFromCart(object sender, EventArgs e, Layout Titel)
        {
            ListBox shoppingCartBox = new ListBox();
            {
                shoppingCartBox.Height = 230;
                shoppingCartBox.Width = 407;
                Titel("Shopping Cart");
            }
            shoppingCartBox.Items.RemoveAt(shoppingCartBox.SelectedIndex);
            MessageBox.Show("Item have been removed! ");
        }

        void RemoveAllItemsHandler(object sender, EventArgs e)
        {
            MessageBox.Show("All items has been removed ");
            shoppingCartBox.Items.Clear();
        }
    }

    class Product
    {
        public string Name;
        public int Price;
        public string AboutItem;

        // amount är procenten ec. o,2 som ska in, kan hämtas från en loop i filen där 
        public int CalculateSumAfterDiscount(double amount)
        {            
            double sum = + SelectedIndex(separatedItems[1]);
            amount = -1.0;
            return sum * amount;
        }
    }

    class Layout
    {
        TableLayoutPanel panel = new TableLayoutPanel
        {           
                RowCount = 5,
                ColumnCount = 4,
                BackColor = Color.Black,
                Dock = DockStyle.Fill,
                AutoSize = true,
        };

        public void Titel(string text)
        {
            Label title = new Label
            {
                Text = text,
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            };
            panel.Controls.Add(title);
        }

        private void CreatePicture(string path)
        {
            PictureBox box1 = new PictureBox
            {
                Image = Image.FromFile(path),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 150,
                Height = 150
            };
        }
    }

}


    public class MyForm : Form
    {   // Lägger till instans så ovasett om man är i main/myforom/metod kan nå 
        PictureBox box1;
        String curItem;
        ListBox shoppingCart;
        // För att slippa skapa ny label varjegång kan vi anropa denna metod och ha texten som parameter 
                      
        public MyForm()
        {
            string[] filenames = Directory.GetFiles("images");
            foreach (string name in filenames)
            {
                CreatePicture(name);
            }

            ListBox itemsList = new ListBox();
            {
                Title("Items");
                itemsList.Height = 230;
                itemsList.Width = 498;
                itemsList.HorizontalScrollbar = true;
            };

            string[] lines = File.ReadAllLines("Shop.txt");
            List<string> items = new List<string> { };
            List<string> description = new List<string> { };
            List<int> money = new List<int> { };

            foreach (string line in lines)
            {
                string[] separatedItems = line.Split(';');

                Product p = new Product
                {
                    Name = separatedItems[0],
                    Price = int.Parse(separatedItems[1]),
                    AboutItem = separatedItems[2],
                };
                items.Add(p.Name);
                description.Add(p.AboutItem);
                money.Add(p.Price);
            }
            itemsList.Items.AddRange(items.ToArray());

            List<string> shoppingCartList = new List<string> { };
            ListBox shoppingCartBox = new ListBox();
            {
                shoppingCartBox.Height = 230;
                shoppingCartBox.Width = 407;
                Title("Shopping Cart");
            }

            ListBox aboutItemView = new ListBox();
            {
                Text = " "; // sätta in variabel för selcted index här?
                aboutItemView.Height = 239;
                aboutItemView.Width = 316;
                itemsList.Click += ClickedAboutItem;
            }
            
            foreach (string line in shoppingCartList)
            {
                string[] separatedItems = line.Split(',');

                for (int i = 0; i <= separatedItems.Length; i++)
                {
                    shoppingCartBox.Items.AddRange(separatedItems);
                }
            }
            
            PictureBox pic = new PictureBox();
            {

                pic.Size = new Size(239, 182);
                Controls.Add(pic);
            }

        Title("Total sum of purchase");

        ListBox aboutItemBox = new ListBox();
            {
                aboutItemBox.Height = 635;
                aboutItemBox.Width = 635;
                aboutItemBox.HorizontalScrollbar = true;
                Title("About your item;");
            }
            TextBox discountText = new TextBox
            {
                Text = "Please enter discount code here",
                BackColor = Color.White,
                Height = 20,
                Width = 230,
                Dock = DockStyle.Fill,
                TextAlign = HorizontalAlignment.Center,
            };          
           
            Button addToCart = new Button
            {
                Text = "Add to Cart",
                BackColor = Color.Pink,

                Height = 44,
                Width = 95,
                Location = new Point(639, 150)
            };
            Button RemoveFromCart = new Button
            {
                Text = "Remove From Cart ",
                BackColor = Color.Pink,

                Height = 44,
                Width = 95,
            };
            addToCart.Click += ClickedAddToCart;
            RemoveFromCart.Click += ClickedRemoveFromCart;

            Button RemoveAll = new Button
            {
                Text = "Clear Shoppingcart ",
                BackColor = Color.Pink,

                Height = 44,
                Width = 95,
            };
            RemoveAll.Click += RemoveAllItemsHandler;

            Button discButton = new Button
            {
                Text = "USE DISCOUNT",
                BackColor = Color.Pink,

                Height = 44,
                Width = 95,

            };
            discButton.Click += ClickedDiscountButton;

            Button buyButton = new Button
            {
                Text = "BUY",
                BackColor = Color.Yellow,

                Height = 44,
                Width = 95,
            };
            buyButton.Click += ClickedEventHandler;

            //panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            //panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            //panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            //panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            //panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            //panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            //panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            //panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            //panel.Controls.Add(discountText);
            //panel.Controls.Add(discButton);
            //panel.Controls.Add(addToCart);
            //panel.Controls.Add(buyButton);
            //panel.Controls.Add(itemsList);
            //panel.Controls.Add(shoppingCartBox);
            //panel.Controls.Add(aboutItemBox);            
            //panel.Controls.Add(pic);
            //panel.Controls.Add(RemoveFromCart);
            //panel.Controls.Add(box1);
            //panel.Controls.Add(RemoveAll);           
            //Controls.Add(panel);           
            shoppingCartBox.Controls.Add(shoppingCart);
        }
    }
