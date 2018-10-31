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
 
    public class MyForm : Form 
 
    {   // Lägger till instans så ovasett om man är i main/myforom/metod kan nå  
        PictureBox box1; 
        String curItem; 
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
        void ClickedEventHandler(object sender, EventArgs e) 
        { 
            MessageBox.Show("Här kommer ditt kvitto på ditt köp:" + curItem); 
        } 
        void ClickedDiscountButton(object sender, EventArgs e) 
        { 
            MessageBox.Show("Braaa nu spara du massa para bror!"); 
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
            } 
 
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
 
                for (int i = 0; i <= separatedItems.Length; i++) 
                { 
                    shoppingCartBox.Items.AddRange(separatedItems); 
                } 
                 
            } 
            TableLayoutPanel panel = new TableLayoutPanel 
            { 
                RowCount = 6, 
                ColumnCount = 3, 
                BackColor = Color.Black, 
                Dock = DockStyle.Fill 
            }; 
            Label item = new Label 
            { 
                Text = "Items", 
                TextAlign = ContentAlignment.TopCenter, 
                BackColor = Color.White, 
                Dock = DockStyle.Top 
            }; 
            PictureBox pic = new PictureBox(); 
            { 
                pic.Size = new Size(210, 110); 
                Controls.Add(pic); 
            } 
            Label shoppingCart = new Label 
            { 
                Text = "Shopping Cart", 
                TextAlign = ContentAlignment.TopCenter, 
                BackColor = Color.White, 
                Dock = DockStyle.Top 
 
            }; 
            Label aboutItem = new Label 
            { 
                Text = "About your item;", 
                TextAlign = ContentAlignment.TopCenter, 
                BackColor = Color.White, 
                Dock = DockStyle.Top 
            }; 
            ListBox aboutItemBox = new ListBox(); 
            { 
                aboutItemBox.Height = 635; 
                aboutItemBox.Width = 635; 
                aboutItemBox.HorizontalScrollbar = true; 
            }
            panel.Controls.Add(new TextBox
            {
                Text = "Please enter discount code here: ",
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = HorizontalAlignment.Center
            });
            panel.Controls.Add(new Label
            {
                Text = "Total Price: ",
                BackColor = Color.White,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.TopCenter,
            });

            void AboutItemAdd(object sender, EventArgs e)
            {
                //click
                //selecteditems
                //aboutitembox
                //  aboutItemBox.Items.Add(itemsList.SelectedItem);
               
                aboutItemBox.Items.AddRange(description.ToArray());



                //if (aboutItemBox.SelectedItems)
                //{

                //aboutItemBox.Items.Clear();

                //}
                itemsList.Click += AboutItemAdd;
            }
            


            void ClickedAddToCart(object sender, EventArgs e) 
            { 
                MessageBox.Show("Din vara är lagd till i korgen.");
                shoppingCartBox.Items.Add(itemsList.SelectedItem);
            }
            void ClickedRemoveFromCart(object sender, EventArgs e) 
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

            Button addToCart = new Button
            {
                Text = "Add to Cart",
                BackColor = Color.Pink,
                Height = 44,
                Width = 95,
                // Location = new Point (639,150)
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
            panel.Controls.Add(pic); 
            panel.Controls.Add(RemoveFromCart); 
            panel.Controls.Add(box1); 
            panel.Controls.Add(RemoveAll); 
            itemsList.Controls.Add(item); 
            Controls.Add(panel); 
            aboutItemBox.Controls.Add(aboutItem); 
            shoppingCartBox.Controls.Add(shoppingCart); 
        } 
    } 
}