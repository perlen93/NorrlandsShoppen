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

            ListBox aboutItemBox = new ListBox();
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

            TextBox totalSum = new TextBox(); // där informationen om varorna hamnar när man klickar på dom 
            {                
                totalSum.Height = 635;
                totalSum.Width = 635;
                // totalSum.HorizontalScrollbar = true;                

                Label totalSumL = new Label
                {
                    Text = "Total sum of purchase:",
                    TextAlign = ContentAlignment.TopCenter,
                    BackColor = Color.White,
                    Dock = DockStyle.Top,                    
                };
                totalSum.Controls.Add(totalSumL);
                panel.Controls.Add(totalSum);
            }            

            List<double> discountProcent = new List<double> { }; //hår ligger procentsatsen sparad på rabattkoderna
            List<string> discountCode = new List<string> { };
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

            List<string> shoppingCartList = new List<string> { };

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

            Button("USE DISCOUNT", ClickedDiscountButton);
            Button("Add to cart", ClickedAddToCart);
            Button("Remove from Cart", ClickedRemoveFromCart);
            Button("Clear Shoppingcart", RemoveAllItemsHandler);         
            Button("BUY", ClickedBuyButton);

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
                    MessageBox.Show("Braaa nu spara du massa para bror!");
            }

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
                        
         

            void ClickedAboutItem(object sender, EventArgs e)
            {              
                aboutItemBox.Items.Add(itemsList.SelectedItem);


                int i = itemsList.SelectedIndex;
                aboutItemBox.Items.Add(description[i]);

                string pathToPic = picPath[i];         
                box1.Image = Image.FromFile(picPath[i]);             
            }
            itemsList.Click += ClickedAboutItem;
           
           

            void ClickedAddToCart(object sender, EventArgs e)
            {
                int i = itemsList.SelectedIndex;
                int price = money[i]; // priset för varan sparas i ínt price

                int total += price;

                Text.Add(total);
                //totalSum.Items.Add(money[i]);

                if (itemsList.SelectedIndex <= 0)
                {
                    MessageBox.Show("You need to choose an item ");
                }
                else
                {
                    shoppingCartBox.Items.Add(itemsList.SelectedItem);
                    totalSum.Text = totalPrice.ToString();
                    Console.WriteLine(totalPrice);
                }
                
                               
            }

            void ClickedRemoveFromCart(object sender, System.EventArgs e)
            {
                if (shoppingCartBox.SelectedIndex <= 0)
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
                MessageBox.Show("All items has been removed ");
                shoppingCartBox.Items.Clear();
            }

            //void Title(string text)
            //{
            //    Label name = new Label
            //    {
            //        Text = text,
            //        TextAlign = ContentAlignment.TopCenter,
            //        BackColor = Color.White,
            //        Dock = DockStyle.Fill
            //    };
            //    //kod för att få den att lägga sig i rätt ruta i layouten ? 
            //    panel.Controls.Add(name);
            //}

            //C:\Windows\Temp ska de sparas i
           
               
            


        }
    }
}

