using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Web;

namespace ProjectMy.Models
{
    public class PrintModel
    {
        public List<OrderDetailViewModel> ordersItems { get; set; }
        public string CustomerName { get; set; }
        public float TotalAmount { get; set; }
        public float totalpaid { get; set; }
        public float balance { get; set; }

        public PrintModel()
        {

        }

        public PrintModel(List<OrderDetailViewModel> orders, string CustomerName = "Test", int totalpaid = 0, int balance = 0)
        {
            this.balance = balance;
            this.CustomerName = CustomerName;
            this.ordersItems = orders;
            this.totalpaid = totalpaid;
        }

        public void PrintReceiptForTransaction()
        {

            PrintDocument recordDoc = new PrintDocument();

            recordDoc.DocumentName = "Customer Receipt";
            recordDoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage); // function below
            recordDoc.PrintController = new StandardPrintController(); // hides status dialog popup

            PrinterSettings ps = new PrinterSettings();
            //ps.PrinterName = "EPSON TM-T20II Receipt";
            recordDoc.PrinterSettings = ps;
            recordDoc.Print();

            recordDoc.Dispose();

        }

        private void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            const int FIRST_COL_PAD = 20;
            const int SECOND_COL_PAD = 7;
            const int THIRD_COL_PAD = 20;


            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            graphics.DrawString("Welcome to Resturant", new Font("Courier New", 14),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Reciept No:" + 123,
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Date :" + DateTime.Now,
                     new Font("Courier New", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            //String Source = "Ashar";
            graphics.DrawString("QTY" + "   Item                Price" , new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            String underLine1= "------------------------------------------";
            graphics.DrawString(underLine1, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            foreach (var item in this.ordersItems)
            {
                Offset = Offset + 20;
                StringBuilder sb = new StringBuilder();
                sb.Append(item.ItemQty.ToString().PadRight(5));
                //int maxLength = 30;
                //var length = item.ItemName.Length;
                //float numbofLines = length / maxLength;
                //if(numbofLines>1)
                //{
                //    for (int i=1;i<=numbofLines;i++)
                //    {
                //        if ((length / maxLength * i) >= 1)
                //        {
                //            sb.AppendLine(item.ItemName.Substring(maxLength * (i - 1), maxLength * i));
                //        }

                //        else {
                //            sb.AppendLine(item.ItemName.Substring(maxLength * (i - 1), length));
                //        }
                //    }

                    
                //}
                sb.Append(item.ItemName.PadRight(10));
                sb.Append(item.ItemTotalPrice.ToString().PadLeft(15));
                graphics.DrawString( sb.ToString() , new Font("Courier New", 10),
                         new SolidBrush(Color.Black), startX, startY + Offset);
            }


            Offset = Offset + 20;
            underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Total    " + this.TotalAmount, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Sub Total    " + this.TotalAmount, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Total Paid   " + this.totalpaid, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;


            graphics.DrawString("Balance " + this.balance, new Font("Courier New", 10),
                                 new SolidBrush(Color.Black), startX, startY + Offset);
        }

    }
}