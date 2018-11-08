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
            TableLayoutPanel panel = new TableLayoutPanel
            {
                RowCount = 4,
                ColumnCount = 3,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                AutoSize = true,
            };
            Controls.Add(panel);
            Label("Items:");
            Label("About your item:");
            Label("Shopping Cart");

            ListBox itemsList = new ListBox();
            {
                Text = "NorrlandsShoppen";
                itemsList.Height = 230;
                itemsList.Width = 498;
                Dock = DockStyle.Top;

            };
            panel.Controls.Add(itemsList);

            TableLayoutPanel innerpanel = new TableLayoutPanel
            {
                RowCount = 2,
                ColumnCount = 1,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                AutoSize = true,
            };
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
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 7));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 14));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 14));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 14));          
                          
          

            ListBox shoppingCartBox = new ListBox(); 
            {
                shoppingCartBox.Height = 230;
                shoppingCartBox.Width = 407;
                
            }
            panel.Controls.Add(shoppingCartBox);
            
            TextBox itemSum = new TextBox(); // där informationen om varorna hamnar när man klickar på dom 
            {
                itemSum.Height = 635;
                itemSum.Width = 635;
                itemSum.ReadOnly = true;
                itemSum.Text = "Price:";

                panel.Controls.Add(itemSum);
            }
            
            TextBox totalPrice = new TextBox(); // där informationen om varorna hamnar när man klickar på dom 
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
            foreach(string line in rows) 
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
            itemsList.Items.AddRange(items.ToArray());          

            PictureBox box1 = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 407,
                Height = 407,
            };
            innerpanel.Controls.Add(box1);

            ListView aboutItemBox = new ListView();
            {
                aboutItemBox.Height = 635;
                aboutItemBox.Width = 635;
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
            double total = 0;
            void ClickedBuyButton(object sender, EventArgs e)
            {
                string[] clist = shoppingCartBox.Items.OfType<string>().ToArray();
                string yourVar = discountText.Text;
                //if (yourVar != null )

                if (clist.Length <= 0)
                {
                    MessageBox.Show("In order to buy, you must add an item to shoppingcart");
                }
                else
                {
                    // för att ta varorna som finns i clist(som hämstas fr shoppingcartBox) till en array så de kan visas i messageBox              
                    string toDisplay = string.Join(Environment.NewLine, clist);

                    MessageBox.Show("Here's you're receipt " + "\n" +
                           "\n" + toDisplay + "\n" + "\n" + "You're total amount to pay is " + total + " Kr" + "\n" +
                           "\n" + "Thanks for choosing Norrlandsshoppen," + "\n" + "hope to see you again soon for more älg! 😀");
                }               
                // Kvitto = totalsumma, o produkter o ev. rabattkod, tack för att du handlar hos oss  
            }

            void ClickedDiscountButton(object sender, EventArgs e)
            {                
                // tar insträngen i texboxen o sparar den i yourVar skriva att indexet är discount.Text;
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
                    MessageBox.Show("Your new price: " + priceAfterDiscount +". Your discount is: " + DP*100 +" % off.");
                    totalPrice.Text = "Total price: " + priceAfterDiscount + " SEK";
                    total = priceAfterDiscount;
                   // totalPrice.Text = "Total price: " + priceAfterDiscount+" SEK";
                }
                else
                {
                    MessageBox.Show("Your code is not correct!");
                }                      
            }  

            void ClickedAboutItem(object sender, EventArgs e)
            {
                int index = itemsList.SelectedIndex;

                if (index <= -1)
                {
                    MessageBox.Show("Please choose an item! ");
                }
                else if (aboutItemBox.Items.Count >= 0)
                {
                    aboutItemBox.Items.Clear();
                    string desc = description[index];
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
                int i = itemsList.SelectedIndex;

                if (i == -1)
                {
                    MessageBox.Show("Please choose an item ");
                }
                else
                {
                    int price = money[i];
                    double totalSU = total + price;
                    totalPrice.Text = "Total price: " + totalSU.ToString();
                    shoppingCartBox.Items.Add(itemsList.SelectedItem);
                    total += price;

                    string sPath = @"C:\Windows\Temp\TempText.txt";
                    System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath); // måste göra Temp mappen icke skrivskyddad.
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
                int i = itemsList.SelectedIndex;
                if (shoppingCartBox.SelectedIndex <= -1)
                {
                    MessageBox.Show("Select an item to remove! ");
                }
                else
                {
                    int price = money[i];
                    double totalSU = total - price;
                    totalPrice.Text = "Total price: " + totalSU.ToString();
                    total -= price;
                }
                shoppingCartBox.Items.RemoveAt(shoppingCartBox.SelectedIndex);
            }

            void RemoveAllItemsHandler(object sender, EventArgs e)
            {
                shoppingCartBox.Items.Clear();
                int total2 = 0;
                total = total2;
                totalPrice.Clear();
                totalPrice.Text = "Total price: ";
                MessageBox.Show("All items has been removed ");
            }

            void LoadCart(object sender, EventArgs e)
            {
                int i = itemsList.SelectedIndex;
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

