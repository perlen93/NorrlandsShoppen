using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NorrlandsShoppen;

namespace NorrlandsShoppen
{
    class Product
    {
        public string Name;
        public int Price;
        public string AboutItem;
    }

    // anv. objekt för att på samma sätt som med artikellistan få ut objekten fr rabattkoderna
    class DiscountCode
    {
        public string Name;
        public int Procent;
    }

    public class MyForm : Form
    {   
        public MyForm()
        {
            TableLayoutPanel panel = new TableLayoutPanel
            {
                RowCount = 5,
                ColumnCount = 10,
                BackColor = Color.Black,
                Dock = DockStyle.Fill,
                AutoSize = true,
            };
            Controls.Add(panel);
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            ListBox itemsList = new ListBox();
            {
                Title("Items");
                itemsList.Height = 230;
                itemsList.Width = 498;
                itemsList.HorizontalScrollbar = true;
            };

            //List<int> discountProcent = new List<int> { }; //hår ligger procentsatsen sparad på rabattkoderna
            //string[] rows = File.ReadAllLines("TextFile2.txt"); // här är hela databasen m koder o namn
            //foreach(string line in rows)
            //{ 
            //    string[] codes = line.Split(';');
            //    DiscountCode D = new DiscountCode
            //    {
            //        Name = codes[0],
            //        Procent = int.Parse(codes[1]),
            //    };
            //    discountProcent.Add(D.Procent);
            //}

            string[] lines = File.ReadAllLines("Shop.txt");
            List<string> items = new List<string> {};
            List<string> description = new List<string> {};
            List<int> money = new List<int> {};

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
            panel.Controls.Add(shoppingCartBox);
            

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
                panel.Controls.Add(pic);
            }  

            Title("Total sum of purchase");

            ListBox aboutItemBox = new ListBox();
            {
                Title("About your item;");
                aboutItemBox.Height = 635;
                aboutItemBox.Width = 635;
                aboutItemBox.HorizontalScrollbar = true;
                itemsList.Click += ClickedAboutItem;
            }
            panel.Controls.Add(aboutItemBox);

            TextBox discountText = new TextBox
            {
                Text = "Please enter discount code here",
                BackColor = Color.White,
                Height = 20,
                Width = 230,
                Dock = DockStyle.Fill,
                TextAlign = HorizontalAlignment.Center,
            };
            panel.Controls.Add(discountText);
            
            Button("Add to cart", ClickedAddToCart);            
            Button("Remove from Cart", ClickedRemoveFromCart);         
            Button("Clear Shoppingcart", ClickedEventHandler);
            Button("UsE DISCOUNT", ClickedDiscountButton);                  
            Button("BUY", ClickedEventHandler);
            Button("Remove all", RemoveAllItemsHandler);
            
            void CreatePicture(string path)
            {
                PictureBox box1 = new PictureBox
                {
                    Image = Image.FromFile(path),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = 150,
                    Height = 150
                };
            }

            //// amount är procenten ec. o,2 som ska in, kan hämtas från en loop i filen där 
            //int TotalSum(int separatedItems) // shopingcart för att kunna plussa allt därl
            //{
            //    int sum = shoppingCartList(separatedItems[1]);
            //    return sum;
            //};

            //double CalculateSumAfterDiscount(double amount)
            //{
            //    double sum = +SelectedIndex(separatedItems[1]);
            //    amount =- 1.0;
            //    return sum * amount;
            //};

            void Button(string text, EventHandler balle)
            {
                Button button = new Button
                {
                    Text = text,
                    BackColor = Color.Yellow,

                    Height = 44,
                    Width = 95,
                };
                button.Click += balle;
                panel.Controls.Add(button);
            };
            
            void Title(string text)
            {
                Label name = new Label
                {
                    Text = text,
                    TextAlign = ContentAlignment.TopCenter,
                    BackColor = Color.LightPink,
                    Dock = DockStyle.Fill
                };
                //kod för att få den att lägga sig i rätt ruta i layouten ?
                panel.Controls.Add(name);
            }            

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

            //List<string> bajs = new List<string> { };
            void ClickedAddToCart(object sender, EventArgs e)
            {
              MessageBox.Show("Din vara är lagd till i korgen.");
            //   shoppingCartBox.Items.Add(itemsList.SelectedItem); // sätt objeket framför metoden      
            //    foreach (string line in shoppingCartBox)
            //    {
            //        shoppingCartBox.Items.Add(bajs);
           }
            //}

            void ClickedRemoveFromCart(object sender, System.EventArgs e)
            {
                if (shoppingCartBox.SelectedIndex <= 0)
                {
                    MessageBox.Show("Choose an item to remove! ");
                }
                else
                {
                    shoppingCartBox.Items.RemoveAt(shoppingCartBox.SelectedIndex);
                }

            }

            void RemoveAllItemsHandler(object sender, EventArgs e)
            {
                MessageBox.Show("All items has been removed ");
                shoppingCartBox.Items.Clear();
            }


            string[] filenames = Directory.GetFiles("images");
            foreach (string name in filenames)
            {
                CreatePicture(name);
            }            
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
                //shoppingCartBox.Controls.Add(shoppingCart);
        }
    }
}

