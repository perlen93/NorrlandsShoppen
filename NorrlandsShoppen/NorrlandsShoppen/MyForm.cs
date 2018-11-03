using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NorrlandsShoppen;
using System.Data.OleDb;
using System.Data;

namespace NorrlandsShoppen
{
    class Product
    {
        public string Name;
        public int Price;
        public string AboutItem;
        public string Picture;
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
                RowCount = 6,
                ColumnCount = 3,
                BackColor = Color.Black,
                Dock = DockStyle.Fill,
                AutoSize = true,
            };
            Controls.Add(panel);     

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));         
            
            ListBox itemsList = new ListBox();
            {
                Text = "NorrlandsShoppen";
                itemsList.Height = 230;
                itemsList.Width = 498;
                Dock = DockStyle.Top;

                Label item = new Label
                {
                    Text = "Items",
                    TextAlign = ContentAlignment.TopCenter,
                    BackColor = Color.White,
                    Dock = DockStyle.Top
                };
                itemsList.Controls.Add(item);
            };
            panel.Controls.Add(itemsList);

            ListBox shoppingCartBox = new ListBox(); 
            {
                shoppingCartBox.Height = 230;
                shoppingCartBox.Width = 407;

                Label shoppingCart = new Label
                {
                    Text = "Shopping Cart",
                    TextAlign = ContentAlignment.TopCenter,
                    BackColor = Color.White,
                    Dock = DockStyle.Top

                };
                shoppingCartBox.Controls.Add(shoppingCart);               
            }
            panel.Controls.Add(shoppingCartBox);
            
            ListBox aboutItemBox = new ListBox(); // där informationen om varorna hamnar när man klickar på dom 
            {
                aboutItemBox.Height = 635;
                aboutItemBox.Width = 635;
                aboutItemBox.HorizontalScrollbar = true;

                Label aboutItem = new Label
                {
                    Text = "About your item;",
                    TextAlign = ContentAlignment.TopCenter,
                    BackColor = Color.White,
                    Dock = DockStyle.Top
                };
                aboutItemBox.Controls.Add(aboutItem);
            }
            panel.Controls.Add(aboutItemBox);

            ListBox totalSum = new ListBox(); // där informationen om varorna hamnar när man klickar på dom 
            {
                totalSum.Height = 635;
                totalSum.Width = 635;
                totalSum.HorizontalScrollbar = true;

                Label totalSumL = new Label
                {
                    Text = "Total sum of purchase",
                    TextAlign = ContentAlignment.TopCenter,
                    BackColor = Color.White,
                    Dock = DockStyle.Top,                    
                };
                totalSum.Controls.Add(totalSumL);
                panel.Controls.Add(totalSum);
                // anropa metoden för totalsum här
            }            

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
            List<string> items = new List<string> { };
            List<string> description = new List<string> { };
            List<int> money = new List<int> { };
            List<string> picPath = new List<string> { };

            foreach (string line in lines)
            {
                string[] separatedItems = line.Split(';');
                
                Product p = new Product
                {
                    Name = separatedItems[0],
                    Price = int.Parse(separatedItems[1]),
                    AboutItem = separatedItems[2],
                    Picture = separatedItems[3]
                };
                items.Add(p.Name);
                description.Add(p.AboutItem);
                money.Add(p.Price);
                picPath.Add(p.Picture);
            }
            itemsList.Items.AddRange(items.ToArray());

            List<string> shoppingCartList = new List<string> { };
            foreach (string line in shoppingCartList)
            {
                string[] separatedItems = line.Split(',');

                for (int i = 0; i <= separatedItems.Length; i++)
                {
                    shoppingCartBox.Items.AddRange(separatedItems);
                }
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
            panel.Controls.Add(discountText);
            Button("USE DISCOUNT", ClickedDiscountButton);
            Button("Add to cart", ClickedAddToCart);
            Button("Remove from Cart", ClickedRemoveFromCart);
            Button("Clear Shoppingcart", RemoveAllItemsHandler);         
            Button("BUY", ClickedEventHandler);

            void CreatePicture(string path)
            {
                PictureBox box1 = new PictureBox
                {
                    Image = Image.FromFile(path),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = 150,
                    Height = 150
                };
                panel.Controls.Add(box1);
            }

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
                    BackColor = Color.White,
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
                aboutItemBox.Items.Add(itemsList.SelectedItem);


                int index = itemsList.SelectedIndex;
                aboutItemBox.Items.Add(description[index]);
                string pathToPic = picPath[index];

                CreatePicture(pathToPic);
            }
            itemsList.Click += ClickedAboutItem;

            void ClickedAddToCart(object sender, EventArgs e)
            {
                if (itemsList.SelectedIndex <= 0)
                {
                    MessageBox.Show("You need to choose an item ");
                }
                else
                {
                    shoppingCartBox.Items.Add(itemsList.SelectedItem);
                }              
            }

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
        }
    }
}

