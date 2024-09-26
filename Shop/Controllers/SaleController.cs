using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.ViewModels;
using System.util;
using Microsoft.AspNetCore.Hosting;
using Shop.Models;

namespace Shop.Controllers
{
    public class SaleController : BaseAdminController
    {

        private readonly IAllSale _allSale;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public SaleController(IAdminAllProducts adminRepository, IAllSale allSale, IWebHostEnvironment webHostEnvironment) : base(adminRepository, allSale)
        {
            _allSale = allSale;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task <IActionResult> Index()
        {
            var order = await _allSale.GetActiveOrders();

            var viewModel = new SaleViewModel { OrderInfo= order };
            return View(viewModel);
        }

        public async Task<ActionResult> GeneratePDF(string name, string surname, string city, string postcode,
            string street, string numberHouse, string numberFlat, string phone,
           string email, string NIP, string[] detailName, string[] detailQuantity, string[] detailPrice, int orderId)
        {
            MemoryStream ms = new MemoryStream();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            document.Open();

            BaseFont baseFont = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font font = new Font(baseFont, Font.DEFAULTSIZE, Font.NORMAL);
            Font fontBold = new Font(baseFont, Font.DEFAULTSIZE, Font.BOLD);


            PdfPTable table = new PdfPTable(2); 

            var company =await _allSale.GetDataCompany();

            string logoPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "logomak_logo (2) (1).png");

           
            Image logo = Image.GetInstance(logoPath);
            logo.ScaleAbsolute(60, 50);  
            PdfPCell logoCell = new PdfPCell(logo);
            logoCell.Border = PdfPCell.NO_BORDER;  
            logoCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            table.AddCell(logoCell);

           
            string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            PdfPCell dateCell = new PdfPCell(new Phrase($"Faktura nr:{orderId}/{currentDate}", fontBold));
            dateCell.Border = PdfPCell.NO_BORDER;  
            dateCell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
             table.AddCell(dateCell);

            Chunk underlineChunk = new Chunk("Sprzedawca",fontBold);
            underlineChunk.SetUnderline(0.1f, -2f);  

            PdfPCell saleText = new PdfPCell(new Phrase(underlineChunk));
            saleText.Border = PdfPCell.NO_BORDER;
            saleText.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            table.AddCell(saleText);

            Chunk underlineChunk1 = new Chunk("Nabywca",fontBold);
            underlineChunk1.SetUnderline(0.1f, -2f);  
            
            PdfPCell bayText = new PdfPCell(new Phrase(underlineChunk1));
            bayText.Border = PdfPCell.NO_BORDER;
            bayText.HorizontalAlignment= PdfPCell.ALIGN_RIGHT;
            table.AddCell(bayText);


            PdfPCell senderCell = new PdfPCell(new Phrase($"{company.name}\n{company.street} {company.number}" +
                $"\n{company.postcode} {company.city}\n{company.phoneNumber}\n{company.email}\nNIP: {company.NIP} ", font));
            senderCell.Border = PdfPCell.NO_BORDER; 
            table.AddCell(senderCell);

           
            if (NIP != null)
            {
              PdfPCell recipientCell = new PdfPCell(new Phrase($"{name} {surname}\n{street} {numberHouse}/{numberFlat}" +
                $"\n{postcode} {city}\n{phone}\n{email}\nNIP: {NIP}", font));
                recipientCell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT; 
                recipientCell.Border = PdfPCell.NO_BORDER; 
                table.AddCell(recipientCell); 
            }
            else
            {
                PdfPCell recipientCell = new PdfPCell(new Phrase($"{name} {surname}\n{street} {numberHouse}/{numberFlat}" +
                                $"\n{postcode} {city}\n{phone}\n{email}", font));
                recipientCell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT; 
                recipientCell.Border = PdfPCell.NO_BORDER; 
                table.AddCell(recipientCell);
            }
          
            document.Add(table);
           

            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("\n"));

            Font font1 = FontFactory.GetFont("Arial", BaseFont.CP1250, BaseFont.EMBEDDED, 8);
            Font fontBold1 = FontFactory.GetFont("Arial", BaseFont.CP1250, BaseFont.EMBEDDED, 8, Font.BOLD);

            PdfPTable table1 = new PdfPTable(8);

            float[] columnWidths = { 0.5f, 2.5f, 0.5f, 0.7f, 1f, 0.5f, 1f, 1f };
            table1.SetWidths(columnWidths);

            table1.AddCell(new PdfPCell(new Phrase("Ip.",font1)));
            table1.AddCell(new PdfPCell(new Phrase("Nazwa towaru",font1)));
            table1.AddCell(new PdfPCell(new Phrase("Ilość", font1)));
            table1.AddCell(new PdfPCell(new Phrase("Jm", font1)));
            table1.AddCell(new PdfPCell(new Phrase("Cena netto", font1)));
            table1.AddCell(new PdfPCell(new Phrase("VAT",font1)));
            table1.AddCell(new PdfPCell(new Phrase("Kwota VAT", font1)));
          
            table1.AddCell(new PdfPCell(new Phrase("Cena brutto")));

            int number = 1;
            List<decimal> pricesWithTax = new List<decimal>();
            List<decimal> pricesNet = new List<decimal>();
            List<decimal> vatsAmount = new List<decimal>();
           
           
            decimal vat = 23;
            
            
            for (int i = 0; i < detailName.Length; i++)
            {
                decimal priceWithTax = decimal.Parse(detailPrice[i]);
               
                decimal vatAmount =  priceWithTax * (vat / 100);
                decimal priceNet = priceWithTax - vatAmount;

                table1.AddCell(new PdfPCell(new Phrase( number.ToString(), font1)));
                table1.AddCell(new PdfPCell(new Phrase( detailName[i], font1)));
                table1.AddCell(new PdfPCell(new Phrase(detailQuantity[i], font1)));
                table1.AddCell(new PdfPCell(new Phrase("szt", font1)));
                table1.AddCell(new PdfPCell(new Phrase(priceNet.ToString("F2"), font1)));
                table1.AddCell(new PdfPCell(new Phrase($"{vat.ToString()}%", font1)));
                table1.AddCell(new PdfPCell(new Phrase(vatAmount.ToString("F2"), font1)));
                table1.AddCell(new PdfPCell(new Phrase(detailPrice[i], font1)));
                number++;

                pricesWithTax.Add(priceWithTax);
                pricesNet.Add(priceNet);
                vatsAmount.Add(vatAmount);
            }

             decimal totalNet = pricesNet.Sum();
            decimal totalWithTax = pricesWithTax.Sum();
            decimal totalVat = vatsAmount.Sum();


            PdfPCell emptyCell = new PdfPCell(new Phrase(""));
            emptyCell.Border = PdfPCell.NO_BORDER;

            table1.AddCell(emptyCell);
            table1.AddCell(emptyCell);
            table1.AddCell(emptyCell);
            table1.AddCell(new PdfPCell(new Phrase("Razem", fontBold1)));
            table1.AddCell(new PdfPCell(new Phrase(totalNet.ToString("F2"), fontBold1)));
            table1.AddCell(new PdfPCell(new Phrase($"{vat.ToString()}%", fontBold1)));
            table1.AddCell(new PdfPCell(new Phrase(totalVat.ToString("F2"), fontBold1)));
            table1.AddCell(new PdfPCell(new Phrase(totalWithTax.ToString("F2"), fontBold1)));

            document.Add(table1);


            document.Close();
            writer.Close();

            // Возвращаем PDF в виде массива байтов
            return File(ms.ToArray(), "application/pdf", $"{name} {surname}.pdf");
        }


        public async Task<IActionResult> FinishOrder(int orderId)
        {
          await  _allSale.FinishOrder(orderId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Customer(string sort)
        {
            var customer = await _allSale.GetCustomers(sort);
            var viewModel = new SaleViewModel { Customers = customer };
            return View(viewModel);
        }

        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            var customerOrders = await _allSale.GetCustomerOrders(customerId);
            var viewModel = new SaleViewModel { CustomerOrder = customerOrders };
            return View(viewModel);
        }

    }
}
