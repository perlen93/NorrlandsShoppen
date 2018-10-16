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
        //två olika sätt att göra det på antingen list, eller listview
        {
            string path = @"C:\Users\Viktor\source\repos\Projektarbete2\Projektarbete2\Shop.txt"; // C:\Users\Viktor\source\repos\Projektarbete\Projektarbete\Shop.txt
            List<string> items = new List<string>();
            items.Add(File.ReadAllText(path)); //lägger till artikellistan(sökvägen) i listan som heter articles


            // List<string> shoppingcart = new List<string>(); //för att ha en varukorg som de kan fara i
            // ListBox shoppingCart= new ListBox();


            TableLayoutPanel panel = new TableLayoutPanel
            {
                RowCount = 6,
                ColumnCount = 3,
                BackColor = Color.Black,
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


            panel.Controls.Add(new Label
            {
                Text = "Items", //Detta blir typ rubriken över listboxen med artiklar
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            });
            ListBox articles1 = new ListBox(); //Lägga alla listbox i en innerpanel för att de ska ändra strlk när man ändrar strkl på fönstret. inner panel måste ha docksytle.fill

            //Hur lägga till filen m artiklarna i ListView??
            foreach (string a in items)
            {
                //a.Add(articles1); // lägg till den i BoxView SKRIVA UT DEN? 
            };

            panel.Controls.Add(new Label
            {
                Text = "Shopping Cart", //Detta blir typ rubriken över listboxen med valda artiklar
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            });
            int numbersOfItems = 0;
            ListBox shoppingCart = new ListBox(); //lista för valen som man valt från articles1
            //foreach (string i in shoppingCart)
            {
                numbersOfItems = +1; //för att få koll på antal artiklar(till kvittot/eller)
                //a.Add(articles1); // lägg till den i BoxView SKRIVA UT DEN? 
            };

            panel.Controls.Add(new Label
            {
                Text = "About your item;", //Detta blir typ rubriken över listboxen med information om varan
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.LightPink,
                Dock = DockStyle.Fill
            });
            ListBox aboutArticle = new ListBox(); //box för att printa ut info o bild om det man väljer


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

            panel.Controls.Add(discButton);
            panel.Controls.Add(buyButton);
            panel.Controls.Add(articles1);
            panel.Controls.Add(shoppingCart);
            panel.Controls.Add(aboutArticle);
            panel.Controls.Add(summa);
            Controls.Add(panel);

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
    }


}




