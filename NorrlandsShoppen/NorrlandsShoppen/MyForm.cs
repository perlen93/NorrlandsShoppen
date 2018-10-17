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
    {

        public MyForm()
        
        {
            string[] filenames = Directory.GetFiles("images"); // samlar den i en array som heter filenames från mappen images
            foreach (string name in filenames) //för varje namn i arrayen filenames
            {
                CreatePicture(name);//skapa bilden
            }
            string path = @"C:\Users\Viktor\source\repos\Projektarbete2\Projektarbete2\Shop.txt"; // C:\Users\Viktor\source\repos\Projektarbete\Projektarbete\Shop.txt
            List<string> items = new List<string>();
            items.Add(File.ReadAllText(path)); 

            
            ;
            TableLayoutPanel table = new TableLayoutPanel
            {
                RowCount = 3,
               ColumnCount = 3,
                BackColor = Color.Orange,
                Dock = DockStyle.Fill,
                
            };

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 33));



            Label things = new Label
            {
                Text = "Items", //Detta blir typ rubriken över listboxen med artiklar
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.Green,

            };
            ListBox articles = new ListBox(); //Lägga alla listbox i en innerpanel för att de ska ändra strlk när man ändrar strkl på fönstret. inner panel måste ha docksytle.fill

            //Hur lägga till filen m artiklarna i ListView??
            foreach (string a in items)
            {
                //a.Add(articles1); // lägg till den i BoxView SKRIVA UT DEN? 
            };

            Label cart = new Label
            {
                
                Text = "Shopping Cart", //Detta blir typ rubriken över listboxen med valda artiklar
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.Green,
                

            };
            int numbersOfItems = 0;

            ListBox shoppingCart = new ListBox(); //lista för valen som man valt från articles1
            //foreach (string i in shoppingCart)
            {
                numbersOfItems = +1; //för att få koll på antal artiklar(till kvittot/eller)
                //a.Add(articles1); // lägg till den i BoxView SKRIVA UT DEN? 
            };

            Label addItem = new Label
            {
                Text = "About your item;", //Detta blir typ rubriken över listboxen med information om varan

                BackColor = Color.Green,
                AutoSize = true,
                


            };


            ListBox aboutItem = new ListBox();//box för att printa ut info o bild om det man väljer
            aboutItem.Size = new Size(200, 200);
            aboutItem.BackColor = Color.Black;


            TextBox Discount = new TextBox
            {
                Text = "Please enter discount code here",
                BackColor = Color.AntiqueWhite,
                Dock = DockStyle.Fill,
               
                
            };

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
                BackColor = Color.White,
                Dock = DockStyle.Fill
            };

            table.Controls.Add(discButton);
            table.Controls.Add(buyButton);
            table.Controls.Add(articles);
            table.Controls.Add(shoppingCart);
            table.Controls.Add(aboutItem);
            table.Controls.Add(summa);
            table.Controls.Add(Discount);
            articles.Controls.Add(things);
            shoppingCart.Controls.Add(cart);
            aboutItem.Controls.Add(addItem);
            table.Controls.Add(box1);


            Controls.Add(table);
            

        }
        private void ClickedEventHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Sådääär då har du köpt en massa"); //här kan man sätta in kvittot? dvs shoppingCart
        }
        private void ClickedDiscountButton(object sender, EventArgs e)
        {
            //typ summan av (shoppingCart - (det som rabattkoden referar till)) så ska de läggas till i som Pris
            MessageBox.Show("Braaa nu spara du massa para bror!"); //här kan man göra om för att kunna anv discount. Hejsan!

        }

        public void CreatePicture(string path) // metod för att få fram en bild. anropa bilden:CreatPicture(sökvägen till bilden) !! Omg
        {
            PictureBox box1 = new PictureBox
            {
                Image = Image.FromFile(path),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 150,
                Height = 150
            };

        }

        //public FlowLayoutPanel panel = new FlowLayoutPanel
        //{

        //    BackColor = Color.Orange,
        //    Dock = DockStyle.Fill,
        //    AutoSize = true,


        //    };


    }


}




