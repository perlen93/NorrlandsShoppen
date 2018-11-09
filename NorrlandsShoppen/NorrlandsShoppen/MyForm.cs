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

    class DiscountCode
    {
        public string Name;
        public double Procent;
    }

    public class MyForm : Form
    {
        public MyForm()
        {
        WindowState = FormWindowState.Maximized;
       

            TableLayoutPanel panel = new TableLayoutPanel { RowCount = 4, ColumnCount = 3, BackColor = Color.White,  Dock = DockStyle.Fill, AutoSize = true };

            Controls.Add(panel);
            Label("Items:");
            Label("About your item:");
            Label("Shopping Cart");

            ListBox itemsBox = new ListBox();
            Text = "NorrlandsShoppen";
            itemsBox.Height = 500;
            itemsBox.Width = 498;
            Dock = DockStyle.Top;
            Font = new Font("Segoe UI Light", 14); 
            panel.Controls.Add(itemsBox);

            TableLayoutPanel innerpanel = new TableLayoutPanel { RowCount = 2, ColumnCount = 1, BackColor = Color.White, Dock = DockStyle.Fill, AutoSize = true };
            panel.Controls.Add(innerpanel);

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
            innerpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            innerpanel.RowStyles.Add(new RowStyle(SizeType.Percent, 65));
            innerpanel.RowStyles.Add(new RowStyle(SizeType.Percent, 35));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 6));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 6));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 17));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 17));

            ListBox shoppingCartBox = new ListBox();
            shoppingCartBox.Height = 500;
            shoppingCartBox.Width = 498;
            panel.Controls.Add(shoppingCartBox);

            TextBox itemPrice = new TextBox();
            {
                itemPrice.Height = 635;
                itemPrice.Width = 635;
                itemPrice.ReadOnly = true;
                itemPrice.Text = "Price:";
                panel.Controls.Add(itemPrice);
            }
            TextBox totalPrice = new TextBox();
            {
                totalPrice.Height = 635;
                totalPrice.Width = 635;
                totalPrice.ReadOnly = true;
                totalPrice.Text = "Total price:";
                panel.Controls.Add(totalPrice);
            }
            List<double> discountProcent = new List<double> { }; //hår ligger procentsatsen sparad på rabattkoderna 
            List<string> discountName = new List<string> { };
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
                discountName.Add(D.Name);
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
            itemsBox.Items.AddRange(items.ToArray());

            Dictionary<string, int> pricePerItem = new Dictionary<string, int> { };
            for (int numItem = 0; numItem < items.Count; numItem++)
            {
                pricePerItem.Add(items[numItem], money[numItem]);
            }
            PictureBox box1 = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 498,
                Height = 407,
            };
            innerpanel.Controls.Add(box1);

            ListView aboutItemBox = new ListView();
            {
                aboutItemBox.Height = 490;
                aboutItemBox.Width = 498;
                Dock = DockStyle.Fill;
                aboutItemBox.View = View.List;
            }
            innerpanel.Controls.Add(aboutItemBox);

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

            void Button(string text, EventHandler handler)
            {
                Button button = new Button
                {
                    Text = text,
                    BackColor = Color.LightBlue,
                    Dock = DockStyle.Fill,
                };
                button.Click += handler;
                panel.Controls.Add(button);
            };
            List<string> historicalShoppingCart = new List<string> { };

            double total = 0;
            double kuk = total;
            void ClickedBuyButton(object sender, EventArgs e)
            {
                string[] clist = shoppingCartBox.Items.OfType<string>().ToArray();

                if (clist.Length <= 0)
                {
                    MessageBox.Show("In order to buy, you must add an item to shoppingcart");
                }
                else
                {
                    foreach (var item in shoppingCartBox.Items)
                    {
                        foreach (KeyValuePair<string, int> pair in pricePerItem)
                        {
                            string hrk = pair.Value.ToString();
                            string key = pair.Key.ToString();
                            if (key == item.ToString())
                            {
                                historicalShoppingCart.Add(key + " - " + hrk + ":-");
                            }
                        }
                    }
                    var message = string.Join(Environment.NewLine, historicalShoppingCart);
                    MessageBox.Show("Here's you're receipt " + "\n" +
                    "\n" + message + "\n" + "\n" + "You're total amount to pay is " + total + " Kr" + "\n" +
                    "\n" + "Thanks for choosing Norrlandsshoppen," + "\n" + "hope to see you again soon for more älg! 😀");
                    shoppingCartBox.Items.Clear();
                    total = 0;
                    historicalShoppingCart.Clear();
                }
                totalPrice.Text = "Total price: " + total.ToString(); 
            }
            void ClickedDiscountButton(object sender, EventArgs e)
            {
                string yourVar = discountText.Text;
                double DP = 0;

                for (int io = 0; io < discountName.Count; io++)
                {
                    if (yourVar == discountName[io])
                    {
                        DP = discountProcent[io];
                        break;
                    }
                };
                if (DP > 0)
                {
                    double amount = 1.0 - DP;
                    double priceAfterDiscount = total * amount;
                    MessageBox.Show("Your new price: " + priceAfterDiscount + ". Your discount is: " + DP * 100 + " % off.");
                    totalPrice.Text = "Total price: " + priceAfterDiscount + " SEK";
                    total = priceAfterDiscount;
                }
                else
                {
                    MessageBox.Show("Your code is not correct!");
                }
            }
            void ClickedAboutItem(object sender, EventArgs e)
            {
                int index = itemsBox.SelectedIndex;

                if (index != -1)
                {
                    string desc = description[index];
                    aboutItemBox.Items.Clear();
                    aboutItemBox.Items.Add(desc);
                    int price = money[index];
                    itemPrice.Text = "Price: " + price.ToString();
                    string pathToPic = picPath[index];
                    box1.Image = Image.FromFile(picPath[index]);
                }
            }
            itemsBox.Click += ClickedAboutItem;

            void ClickedAddToCart(object sender, EventArgs e)
            {
                int i = itemsBox.SelectedIndex;
                if (i != -1)
                {
                    int price = money[i];
                    double totalSU = total + price;
                    totalPrice.Text = "Total price: " + totalSU.ToString();
                    shoppingCartBox.Items.Add(itemsBox.SelectedItem);
                    total += price;

                    string sPath = @"C:\Windows\Temp\TempText.txt";
                    StreamWriter SaveFile = new StreamWriter(sPath);
                    foreach (var item in shoppingCartBox.Items)
                    {
                        SaveFile.WriteLine(item);
                    }
                    SaveFile.WriteLine(totalSU);
                    SaveFile.Close();
                }
            }

            void ClickedRemoveFromCart(object sender, System.EventArgs e)
            {
                int i = itemsBox.SelectedIndex;
                if (shoppingCartBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Select an item to remove! ");
                }
                else
                {
                    foreach (KeyValuePair<string, int> pair in pricePerItem)
                    {
                        string hrk = pair.Value.ToString();
                        string key = pair.Key.ToString();
                        string hoh = shoppingCartBox.SelectedItem.ToString();
                        if (hoh == key)
                        {
                            int value = pricePerItem[key];
                            total -= value;
                            break;
                        }
                    }
                    totalPrice.Text = "Total price: " + total.ToString();
                    shoppingCartBox.Items.RemoveAt(shoppingCartBox.SelectedIndex);
                }
            }
            void RemoveAllItemsHandler(object sender, EventArgs e)
            {
                shoppingCartBox.Items.Clear();
                int total2 = 0;
                total = total2;
                totalPrice.Clear();
                totalPrice.Text = "Total price: " + total2;
            }

            void LoadCart(object sender, EventArgs e)
            {
                int i = itemsBox.SelectedIndex;
                if (shoppingCartBox.Items.Count > 1)
                {
                    MessageBox.Show("Please clear you're shoppingcart before loading ");
                }
                else if (i == -1)
                {
                    MessageBox.Show("You must first have a Cart to clear it, mate");
                }
                else
                {
                    int price = money[i];
                    var Text = File.ReadAllLines(@"C:\Windows\Temp\TempText.txt");
                    foreach (var line in Text)
                    {
                        shoppingCartBox.Items.Add(line);
                        double totalSU = total + price;
                        totalPrice.Text = "Total price: " + line;           
                    }
                    total = Int32.Parse(Text.Last());
                    shoppingCartBox.Items.RemoveAt(shoppingCartBox.Items.Count - 1);
                }
            }
            Button("USE DISCOUNT", ClickedDiscountButton);
            Button("Add to cart", ClickedAddToCart);
            Button("Remove from Cart", ClickedRemoveFromCart);
            Button("Clear Shoppingcart", RemoveAllItemsHandler);
            Button("BUY", ClickedBuyButton);
            Button("Load ShoppingCart", LoadCart);
        }
    }
}