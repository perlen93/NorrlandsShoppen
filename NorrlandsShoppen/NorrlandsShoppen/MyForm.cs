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
    class MyForm : Form
    {    // Lägger till denna som instans så att alla ovasett om man är i main/myform eller i en metod kan använda den.ev. byta till flow om de är flow vi vill ha.
        TableLayoutPanel table;
        
        // Metod för alla paneler behövs här. Vad ska vi ha för in parametrar in?
          

        public MyForm()
        {
          
            // Samlar bilderna i en array som heter filenames från mappen images
            string[] filenames = Directory.GetFiles("images");
            // För varje namn i arrayen filenames
            foreach (string name in filenames)
            {
                // Skapa bilden
                CreatePicture(name);
            }

            // C:\Users\Viktor\source\repos\Projektarbete\Projektarbete\Shop.txt
            string path = @"C:/Users/perle/source/repos/NorrlandsShoppen/NorrlandsShoppen/TextFile1.txt";
            List<string> items = new List<string> { };
            // Lägger till artikellistan(sökvägen) i listan som heter articles
            items.Add(File.ReadAllText(path)); 

             ListBox itemsList = new ListBox();
            {
                itemsList.Height = 635;
                itemsList.Width = 635;
                itemsList.HorizontalScrollbar = true;
            };

            foreach (string line in items)
            {
                string[] separatedItems = line.Split(',');
                itemsList.Items.AddRange(separatedItems);            
            }
            int numbersOfItems = 0;
            itemsList.Click += ClickedEventHandler;
            // välj att det som är [1] ska till listboxen för aboutarticle, samt [2] ska in som pris samt [3] ska in som beskrivning


             List<string> shoppingCartList = new List<string> { };
            ListBox shoppingCartBox = new ListBox();
            {
                shoppingCartBox.Height = 635;
                shoppingCartBox.Width = 635;
                shoppingCartBox.HorizontalScrollbar = true;
            }
                    
            ListBox shoppingCartBox = new ListBox{};
           foreach (string line in shoppingCartList)
            {
                string[] separatedItems = line.Split(',');
                // För varje char i speareradeitemslistan(strängarna) så ska de add to shoppingcartbox 
                for (int i = 0; i <= separatedItems.Length; i++)
                {
                    // Lägg till de från separatedItems tills shoppingcartbox   
                    shoppingCartBox.Items.AddRange(separatedItems);
                }
                // För att få koll på antal artiklar(till kvittot/eller)
                numbersOfItems = +1;
            }

            //Det finns en metod för att index från textfilen ska över till den andra via clickeventet, skapa klickevent som säger de index som är valt ska över till shoppingCart
            // gör detta som en klickmetod
            // För att visa shoppingCartList för anv.

            Console.WriteLine("You have " + numbersOfItems + "in your shoppingcart");

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

            // Detta blir typ rubriken över listboxen med valda artiklar
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

            Label summa = new Label
            {
                Text = "Total sum of pruchase:",
                BackColor = Color.Orange,
                Dock = DockStyle.Fill
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
            panel.Controls.Add(buyButton);
            panel.Controls.Add(itemsList);
            panel.Controls.Add(shoppingCartBox);
            panel.Controls.Add(aboutArticle);
            panel.Controls.Add(summa);
            panel.Controls.Add(pic);
            Controls.Add(panel);
            itemsList.Controls.Add(item);
            shoppingCartBox.Controls.Add(shoppingCart);
            aboutItemBox.Controls.Add(aboutItem); 
        }

          // för att slippa skapa ny label varjegång kan vi anropa denna metod och ha texten som parameter
        private void Label(string text)
        {
            Controls.Add(new Label
            {
                Text = text,
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            });
        }

        // Metod för att få fram en bild. anropa bilden:CreatPicture(sökvägen till bilden) !! Omg
        private void CreatePicture(string path)
        {
            PictureBox box1 = new PictureBox
            {
                Image = Image.FromFile(path),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 150,
                Height = 150
            };
            // flow.Controls.Add(box1); fick error på flow "do not exist"
        }

        void ClickedEventHandler(object sender, EventArgs e)
        {
            // Här kan man sätta in kvittot? dvs shoppingCart
            MessageBox.Show("Sådääär då har du köpt en massa"); 
        }

        void ClickedDiscountButton(object sender, EventArgs e)
        {
            // Summan av (shoppingCart - (det som rabattkoden referar till)) så ska de läggas till i som Pris
            MessageBox.Show("Braaa nu spara du massa para bror!");
            // Här kan man göra om för att kunna anv discount. Hejsan!
        }
            // Få in en clickeventhandle med mousedubleclick på något vänster. Denna ska göra så att när man klickar på en rubrik så kommer info om detta upp. 


    }
} 




