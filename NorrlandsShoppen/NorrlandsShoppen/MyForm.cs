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
        public double Procent;
    }

    public class MyForm : Form
    {
        public MyForm()
        {
            TableLayoutPanel panel = new TableLayoutPanel
            {
                RowCount = 6,
                ColumnCount = 3,
                BackColor = Color.White,
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

            void Label(string text)
            {
                Label name = new Label
                {
                    Text = text,
                    TextAlign = ContentAlignment.BottomCenter,
                    BackColor = Color.White,
                    Dock = DockStyle.Fill
                };
                panel.Controls.Add(name);
            }

            Label("Items:");
            Label("About your item:");
            Label("Shopping Cart");

            ListBox itemsList = new ListBox();
            {
                Text = "NorrlandsShoppen";
                itemsList.Height = 230;
                itemsList.Width = 407;
                Dock = DockStyle.Top;
            };
            panel.Controls.Add(itemsList);

            ListBox aboutItemBox = new ListBox();
            {
                aboutItemBox.Height = 230;
                aboutItemBox.Width = 450;
            }
            panel.Controls.Add(aboutItemBox);

            ListBox shoppingCartBox = new ListBox();
            {
                shoppingCartBox.Height = 230;
                shoppingCartBox.Width = 407;
            }
            panel.Controls.Add(shoppingCartBox);

            TextBox itemSum = new TextBox(); // där informationen om varorna hamnar när man klickar på dom  
            {
                itemSum.Height = 320;
                itemSum.Width = 320;
                // totalSum.HorizontalScrollbar = true;                 

                Label totalSumL = new Label
                {
                    Text = "Total sum of purchase:",
                    TextAlign = ContentAlignment.TopLeft,
                    BackColor = Color.White, 
                    Dock = DockStyle.Top, 
                }; 
                itemSum.Controls.Add(totalSumL); 
                panel.Controls.Add(itemSum);
            }
            
            List<double> discountProcent = new List<double> { }; //hår ligger procentsatsen sparad på rabattkoderna 
            List<string> discountCode = new List<string> { };
            string[] rows = File.ReadAllLines("TextFile2.txt"); // här är hela databasen m koder o namn  
            foreach (string line in rows)
            {
                string[] codes = line.Split(';');
                DiscountCode D = new DiscountCode
                {
                    Name = codes[0],
                    Procent = double.Parse(codes[1]),
                };
                discountProcent.Add(D.Procent);
                discountCode.Add(D.Name);
            }

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

            

            PictureBox box1 = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 150,
                Height = 150,

            };
            panel.Controls.Add(box1);
            TextBox discountText = new TextBox
            {
                Text = "Please enter discount code here",
                BackColor = Color.White,
                Height = 320,
                Width = 320,
                TextAlign = HorizontalAlignment.Center,
            };
            panel.Controls.Add(discountText);

            Button("Add to cart", ClickedAddToCart);
            Button("BUY", ClickedBuyButton);
            Button("USE DISCOUNT", ClickedDiscountButton);
            Button("Clear Shoppingcart", RemoveAllItemsHandler);
            Button("Remove from Cart", ClickedRemoveFromCart);
            Button("Load ShoppingCart", LoadCart);

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

            void ClickedDiscountButton(object sender, EventArgs e)
            {
                string yourVar = discountText.Text; // tar insträngen i texboxen o sparar den i yourVar 
                foreach (string line in discountCode)
                {
                    if (line == yourVar)
                    {

                        ////int index = yourVar.LastIndexOfAny; 
                        //double amount = 1.0 - discountProcent[index]; 
                        //double totalPrice = 100;// den totala summan på det som är i shoppingvart 
                        //double afterDiscount = totalPrice * amount; 
                        //MessageBox.Show("Your new price :"+ afterDiscount); 
                    }

                    else
                    {
                        MessageBox.Show("Your code is not correct!");
                    }
                }
                //    MessageBox.Show("Braaa nu spara du massa para bror!"); 
            }

            void ClickedBuyButton(object sender, EventArgs e)
            {
                string yourVar = discountText.Text;
                if (yourVar == null)
                {
                    // visa kvittot 
                    MessageBox.Show("Här kommer ditt kvitto på ditt köp:");
                }
                else
                {
                    //Visa kvittot med rabatten avdragen 
                    MessageBox.Show("Här kommer ditt kvitto på ditt köp med rabatt:");
                }

            }

            void ClickedAboutItem(object sender, EventArgs e)
            {
                int index = itemsList.SelectedIndex;
                string desc = description[index];

                if (index <= -1)
                {
                    MessageBox.Show("Please choose an item! ");
                }
                else if(aboutItemBox.Items.Count >= 0)
                {
                    aboutItemBox.Items.Clear();
                    aboutItemBox.Items.Add(desc);
                    int price = money[index];
                    itemSum.Text = "Price: " + price.ToString();
                    string pathToPic = picPath[index];
                    box1.Image = Image.FromFile(picPath[index]);
                }

            }
            itemsList.Click += ClickedAboutItem;

            void ClickedAddToCart(object sender, EventArgs e)
            {
                int total = 0;
                //throw?? för att undvika 
                int i = itemsList.SelectedIndex;
                int price = money[i];
                int totalSU = total + price;

                if (i == -1)
                {
                    MessageBox.Show("Please choose an item! ");
                }
                else
                {
                    itemSum.Text = "Total price: " + totalSU.ToString();
                    shoppingCartBox.Items.Add(itemsList.SelectedItem);
                    total += price;
                }

                string sPath = @"C:\Windows\Temp\TempText.txt";
                System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
                foreach (var item in shoppingCartBox.Items)
                {
                    SaveFile.WriteLine(item);
                }
                    SaveFile.Close();


            }
            void ClickedRemoveFromCart(object sender, System.EventArgs e)
            {
                if (shoppingCartBox.SelectedIndex <= -1)
                {
                    MessageBox.Show("Select an item to remove! ");
                }
                else
                {
                    shoppingCartBox.Items.RemoveAt(shoppingCartBox.SelectedIndex);
                }
            }
            void RemoveAllItemsHandler(object sender, EventArgs e)
            {
                shoppingCartBox.Items.Clear();
                MessageBox.Show("All items has been removed ");
            }
            void LoadCart(object sender, EventArgs e)
            {

                if(shoppingCartBox.Items.Count > 1)
                {

                    MessageBox.Show("Please clear your shoppingcart before loading! ");
                }
                else
                {
                    var Text = File.ReadAllLines(@"C:\Windows\Temp\TempText.txt");
                    foreach (var line in Text)
                    {
                        shoppingCartBox.Items.Add(line);
                    }


                }
                
            }
        }
    }
}

