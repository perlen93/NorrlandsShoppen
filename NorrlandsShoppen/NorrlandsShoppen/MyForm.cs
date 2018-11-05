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

            Label item = new Label
            {
                Text = "Items",
                TextAlign = ContentAlignment.BottomCenter,
                BackColor = Color.White,
                Dock = DockStyle.Top
            };
            panel.Controls.Add(item);

            Label aboutItem = new Label
            {
                Text = "About your item;",
                TextAlign = ContentAlignment.BottomCenter,
                BackColor = Color.White,
                Dock = DockStyle.Top
            };
            panel.Controls.Add(aboutItem);

            Label shoppingCart = new Label
            {
                Text = "Shopping Cart",
                TextAlign = ContentAlignment.BottomCenter,
                BackColor = Color.White,
                Dock = DockStyle.Top
            };
            panel.Controls.Add(shoppingCart);

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
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

                
            };
            panel.Controls.Add(itemsList);

           

            ListBox aboutItemBox = new ListBox();
            {
                aboutItemBox.Height = 635;
                aboutItemBox.Width = 635;
                aboutItemBox.HorizontalScrollbar = true;              
                //aboutItemBox.Controls.Add(aboutItem);
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
                itemSum.Height = 635;
                itemSum.Width = 635;
                             
                panel.Controls.Add(itemSum);
            }
            
            TextBox totalPrice = new TextBox(); // där informationen om varorna hamnar när man klickar på dom 
            {
                totalPrice.Height = 635;
                totalPrice.Width = 635;              

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

            List<string> shoppingCartList = new List<string> {};

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
                    BackColor = Color.Yellow,

                    Height = 44,
                    Width = 95,
                };
                button.Click += handler;
                panel.Controls.Add(button);
            };      
                     
            void ClickedBuyButton(object sender, EventArgs e)
            {
                string yourVar = discountText.Text;
                if (yourVar == null )
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

            void ClickedDiscountButton(object sender, EventArgs e)
            {
                int index = itemsList.SelectedIndex;

               string yourVar = discountText.Text; // tar insträngen i texboxen o sparar den i yourVar skriva att indexet är discount.Text;
               foreach (string line in discountName)
               {
                    if (yourVar == discountText.Text)
                    {
                        MessageBox.Show("Hej!");
                        ////int index = yourVar.LastIndexOfAny;
                        //double amount = 1.0 - discountProcent[index];
                        //double totalPrice = 100;// den totala summan på det som är i shoppingvart
                        //double afterDiscount = totalPrice * amount;
                        MessageBox.Show("Your new price :");//+ afterDiscount);
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Your code is not correct!");
                    }
                
            }  }

            void ClickedAboutItem(object sender, EventArgs e)
            {
                int index = itemsList.SelectedIndex;
                string desc = description[index];

                if (index <= -1)
                {
                    MessageBox.Show("Please choose an item! ");
                }
                else if (aboutItemBox.Items.Count >= 1)
                {
                    aboutItemBox.Items.Clear();
                    aboutItemBox.Items.Add(desc);
                    int price = money[index];
                    itemSum.Text = "Price: " + price.ToString();
                    string pathToPic = picPath[index];
                    box1.Image = Image.FromFile(picPath[index]);
                }
                else if (aboutItemBox.Items.Count >= 0)
                {
                    aboutItemBox.Items.Add(description[index]);
                    int price = money[index];
                    itemSum.Text = "Price: " + price.ToString();
                    string pathToPic = picPath[index]; 
                    box1.Image = Image.FromFile(picPath[index]);    

                }
            }
                itemsList.Click += ClickedAboutItem;

            int total = 0;
            void ClickedAddToCart(object sender, EventArgs e)
            {
                int i = itemsList.SelectedIndex;                               
                int price = money[i]; // priset för varan sparas i ínt price
                int totalSU =total+price;
                
                if (i <= -1)
                {
                    MessageBox.Show("Please choose an item! ");
                }
                else
                {                   
                    aboutItemBox.Items.Add(description[i]);
                    totalPrice.Text = "Total price: " + totalSU.ToString();
                }       
                           
                // HÄR MÅSTE VI SKRIVA NGT SOM GÖR ATT DE I SHOPPINGCART SPARAS I EN VARIABEL FÖR TOTALSUMMA
                // Var price = Items.Sum(t => t.ItemPrice);
                // Kvitto = totalsumma, o produkter o ev. rabattkod, tack för att du handlar hos oss
                                
                //totalSum.Items.Add(money[i]);

                if (itemsList.SelectedIndex <= 0)
                {
                    MessageBox.Show("You need to choose an item ");
                }
                else
                {
                    shoppingCartBox.Items.Add(itemsList.SelectedItem);
                    total += price;                    
                    //totalSum.Text = totalPrice.ToString();
                    //Console.WriteLine(totalPrice);
                }                
            }      

            void ClickedRemoveFromCart(object sender, System.EventArgs e)
            {              
                var heej = shoppingCartBox.SelectedItem;

                int i = itemsList.SelectedIndex;
                int price = money[i]; // priset för varan sparas i ínt price
                int totalSU = total - price;
                          
                foreach(string line in items)
                {
                    if (heej == line)
                    {
                        
                    }
                }

                if (shoppingCartBox.SelectedIndex <= 0)
                {
                    MessageBox.Show("Select an item to remove!");
                }
                else
                {
                    shoppingCartBox.Items.RemoveAt(shoppingCartBox.SelectedIndex);
                }                              
                totalPrice.Text = "Total price: " + totalSU.ToString();
            }

            void RemoveAllItemsHandler(object sender, EventArgs e)
            {
                MessageBox.Show("All items has been removed ");
                shoppingCartBox.Items.Clear();
            }
            //C:\Windows\Temp ska de sparas i    

            Button("USE DISCOUNT", ClickedDiscountButton);
            Button("Add to cart", ClickedAddToCart);
            Button("Remove from Cart", ClickedRemoveFromCart);
            Button("Clear Shoppingcart", RemoveAllItemsHandler);
            Button("BUY", ClickedBuyButton);
        }
    }
}

