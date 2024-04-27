using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Syncfusion.XlsIO;
using Syncfusion.Drawing;
using System.IO;

namespace AIS.Data.RequestResponseModel.TimeTableSetUp
{
    public class ExcelFormatTimeTable
    {
        public MemoryStream CreateExcel( )
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                //Create a workbook
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                //Add a picture
                //FileStream imageStream = new FileStream("AdventureCycle.png", FileMode.Open);
                //Image image = Image.FromStream(imageStream);
                //IPictureShape shape = worksheet.Pictures.AddPicture(1, 1, image, 20, 20);

                //Disable gridlines in the worksheet
                worksheet.IsGridLinesVisible = false;

                //Enter values to the cells from A3 to A5
                worksheet.Range["A3"].Text = "46036 Michigan Ave";
                worksheet.Range["A4"].Text = "Canton, USA";
                worksheet.Range["A5"].Text = "Phone: +1 231-231-2310";

                //Make the text bold
                worksheet.Range["A3:A5"].CellStyle.Font.Bold = true;

                //Merge cells
                worksheet.Range["D1:E1"].Merge();

                //Enter text to the cell D1 and apply formatting
                worksheet.Range["D1"].Text = "INVOICE";
                worksheet.Range["D1"].CellStyle.Font.Bold = true;
                worksheet.Range["D1"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["D1"].CellStyle.Font.Size = 35;

                //Apply alignment in the cell D1
                worksheet.Range["D1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
                worksheet.Range["D1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;

                //Enter values to the cells from D5 to E8
                worksheet.Range["D5"].Text = "INVOICE#";
                worksheet.Range["E5"].Text = "DATE";
                worksheet.Range["D6"].Number = 1028;
                worksheet.Range["E6"].Value = "12/31/2018";
                worksheet.Range["D7"].Text = "CUSTOMER ID";
                worksheet.Range["E7"].Text = "TERMS";
                worksheet.Range["D8"].Number = 564;
                worksheet.Range["E8"].Text = "Due Upon Receipt";

                //Apply RGB backcolor to the cells from D5 to E8
                worksheet.Range["D5:E5"].CellStyle.Color = Color.FromArgb(42, 118, 189);
                worksheet.Range["D7:E7"].CellStyle.Color = Color.FromArgb(42, 118, 189);

                //Apply known colors to the text in cells D5 to E8
                worksheet.Range["D5:E5"].CellStyle.Font.Color = ExcelKnownColors.White;
                worksheet.Range["D7:E7"].CellStyle.Font.Color = ExcelKnownColors.White;

                //Make the text as bold from D5 to E8
                worksheet.Range["D5:E8"].CellStyle.Font.Bold = true;

                //Apply alignment to the cells from D5 to E8
                worksheet.Range["D5:E8"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                worksheet.Range["D5:E5"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                worksheet.Range["D7:E7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                worksheet.Range["D6:E6"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;

                //Enter value and applying formatting in the cell A7
                worksheet.Range["A7"].Text = "  BILL TO";
                worksheet.Range["A7"].CellStyle.Color = Color.FromArgb(42, 118, 189);
                worksheet.Range["A7"].CellStyle.Font.Bold = true;
                worksheet.Range["A7"].CellStyle.Font.Color = ExcelKnownColors.White;

                //Apply alignment
                worksheet.Range["A7"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
                worksheet.Range["A7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

                //Enter values in the cells A8 to A12
                worksheet.Range["A8"].Text = "Steyn";
                worksheet.Range["A9"].Text = "Great Lakes Food Market";
                worksheet.Range["A10"].Text = "20 Whitehall Rd";
                worksheet.Range["A11"].Text = "North Muskegon,USA";
                worksheet.Range["A12"].Text = "+1 231-654-0000";

                //Create a Hyperlink for e-mail in the cell A13
                IHyperLink hyperlink = worksheet.HyperLinks.Add(worksheet.Range["A13"]);
                hyperlink.Type = ExcelHyperLinkType.Url;
                hyperlink.Address = "Steyn@greatlakes.com";
                hyperlink.ScreenTip = "Send Mail";

                //Merge column A and B from row 15 to 22
                worksheet.Range["A15:B15"].Merge();
                worksheet.Range["A16:B16"].Merge();
                worksheet.Range["A17:B17"].Merge();
                worksheet.Range["A18:B18"].Merge();
                worksheet.Range["A19:B19"].Merge();
                worksheet.Range["A20:B20"].Merge();
                worksheet.Range["A21:B21"].Merge();
                worksheet.Range["A22:B22"].Merge();

                //Enter details of products and prices
                worksheet.Range["A15"].Text = "  DESCRIPTION";
                worksheet.Range["C15"].Text = "QTY";
                worksheet.Range["D15"].Text = "UNIT PRICE";
                worksheet.Range["E15"].Text = "AMOUNT";
                worksheet.Range["A16"].Text = "Cabrales Cheese";
                worksheet.Range["A17"].Text = "Chocos";
                worksheet.Range["A18"].Text = "Pasta";
                worksheet.Range["A19"].Text = "Cereals";
                worksheet.Range["A20"].Text = "Ice Cream";
                worksheet.Range["C16"].Number = 3;
                worksheet.Range["C17"].Number = 2;
                worksheet.Range["C18"].Number = 1;
                worksheet.Range["C19"].Number = 4;
                worksheet.Range["C20"].Number = 3;
                worksheet.Range["D16"].Number = 21;
                worksheet.Range["D17"].Number = 54;
                worksheet.Range["D18"].Number = 10;
                worksheet.Range["D19"].Number = 20;
                worksheet.Range["D20"].Number = 30;
                worksheet.Range["D23"].Text = "Total";

                //Apply number format
                worksheet.Range["D16:E22"].NumberFormat = "$.00";
                worksheet.Range["E23"].NumberFormat = "$.00";

                //Apply incremental formula for column Amount by multiplying Qty and UnitPrice
                application.EnableIncrementalFormula = true;
                worksheet.Range["E16:E20"].Formula = "=C16*D16";

                //Formula for Sum the total
                worksheet.Range["E23"].Formula = "=SUM(E16:E22)";

                //Apply borders
                worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Grey_25_percent;
                worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Grey_25_percent;
                worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Black;
                worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Black;

                //Apply font setting for cells with product details
                worksheet.Range["A3:E23"].CellStyle.Font.FontName = "Arial";
                worksheet.Range["A3:E23"].CellStyle.Font.Size = 10;
                worksheet.Range["A15:E15"].CellStyle.Font.Color = ExcelKnownColors.White;
                worksheet.Range["A15:E15"].CellStyle.Font.Bold = true;
                worksheet.Range["D23:E23"].CellStyle.Font.Bold = true;

                //Apply cell color
                worksheet.Range["A15:E15"].CellStyle.Color = Color.FromArgb(42, 118, 189);

                //Apply alignment to cells with product details
                worksheet.Range["A15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
                worksheet.Range["C15:C22"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                worksheet.Range["D15:E15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

                //Apply row height and column width to look good
                worksheet.Range["A1"].ColumnWidth = 36;
                worksheet.Range["B1"].ColumnWidth = 11;
                worksheet.Range["C1"].ColumnWidth = 8;
                worksheet.Range["D1:E1"].ColumnWidth = 18;
                worksheet.Range["A1"].RowHeight = 47;
                worksheet.Range["A2"].RowHeight = 15;
                worksheet.Range["A3:A4"].RowHeight = 15;
                worksheet.Range["A5"].RowHeight = 18;
                worksheet.Range["A6"].RowHeight = 29;
                worksheet.Range["A7"].RowHeight = 18;
                worksheet.Range["A8"].RowHeight = 15;
                worksheet.Range["A9:A14"].RowHeight = 15;
                worksheet.Range["A15:A23"].RowHeight = 18;

                //Save the document as a stream and retrun the stream.
                using (MemoryStream stream = new MemoryStream())
                {
                    //Save the created Excel document to MemoryStream
                    workbook.SaveAs(stream);
                    return stream;
                }
            }
        }

        public MemoryStream TimeTableTabularFormatExcel(string _SchoolName,string _sessionDetails,  List<TimeTableTabularFormate> timeTableTabularFormates, string _operation)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                //Create a workbook
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                //Add a picture
                //FileStream imageStream = new FileStream("AdventureCycle.png", FileMode.Open);
                //Image image = Image.FromStream(imageStream);
                //IPictureShape shape = worksheet.Pictures.AddPicture(1, 1, image, 20, 20);

                //Disable gridlines in the worksheet
                worksheet.IsGridLinesVisible = false;
                 
                //Merge cells school details
                worksheet.Range["B1:L1"].Merge();

                //Enter text to the cell D1 and apply formatting
                worksheet.Range["B1"].Text = _SchoolName.ToUpper();//"INVOICE";
                worksheet.Range["B1"].CellStyle.Font.Bold = true;
                worksheet.Range["B1"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["B1"].CellStyle.Font.Size = 14;
                                 
                //Apply alignmentBin the cell D1
                worksheet.Range["B1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
                worksheet.Range["B1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;
                worksheet.Range["B1"].CellStyle.Font.FontName = "Calibri";
                worksheet.Range["B1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenterAcrossSelection;
                //End school Details

                //Start Teacher Deatils
                worksheet.Range["B4:D4"].Merge();

                worksheet.Range["B4"].Text = "Teacher Name:  "+ timeTableTabularFormates[0].teacherName.ToUpper();
                worksheet.Range["B4"].CellStyle.Font.Bold = true;
                worksheet.Range["B4"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["B4"].CellStyle.Font.Size = 10;
                                  
                //Apply alignmentB4n the cell D1
                worksheet.Range["B4"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
                worksheet.Range["B4"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;
                worksheet.Range["B4"].CellStyle.Font.FontName = "Calibri";
                worksheet.Range["B4"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
               
                //Session Details 
                worksheet.Range["E4:I4"].Merge();

                worksheet.Range["E4"].Text = "Session :  " + _sessionDetails;
                worksheet.Range["E4"].CellStyle.Font.Bold = true;
                worksheet.Range["E4"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["E4"].CellStyle.Font.Size = 10;
                               
                //Apply alignmentE4n the cell D1
                worksheet.Range["E4"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
                worksheet.Range["E4"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;
                worksheet.Range["E4"].CellStyle.Font.FontName = "Calibri";
                worksheet.Range["E4"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenterAcrossSelection;

                //Total ClassSum
                int _totalClass = 0;
                worksheet.Range["J4:L4"].Merge();

                worksheet.Range["J4"].Text = "Total Class In week :  " + _totalClass.ToString();
                worksheet.Range["J4"].CellStyle.Font.Bold = true;
                worksheet.Range["J4"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["J4"].CellStyle.Font.Size = 10;
                                
                //Apply alignmentJ4n the cell D1
                worksheet.Range["J4"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
                worksheet.Range["J4"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;
                worksheet.Range["J4"].CellStyle.Font.FontName = "Calibri";
                worksheet.Range["J4"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenterAcrossSelection;



                //End Teacher Details.



                //Enter details of products and prices
                worksheet.Range["B5"].Text = "  Day Name";
                worksheet.Range["C5"].Text = "  Period-1";
                worksheet.Range["D5"].Text = "  Period-2";
                worksheet.Range["E5"].Text = "  Period-3";
                worksheet.Range["F5"].Text = "  Period-4";
                worksheet.Range["G5"].Text = "  Period-5";
                worksheet.Range["H5"].Text = "  Period-6";
                worksheet.Range["I5"].Text = "  Period-7";
                worksheet.Range["J5"].Text = "  Period-8";
                worksheet.Range["K5"].Text = "  Period-9";
                worksheet.Range["L5"].Text = "  Total Period";
                worksheet.Range["B5:L5"].CellStyle.Color = Color.FromArgb(42, 118, 189);
                worksheet.Range["B5:L5"].CellStyle.Font.Color = ExcelKnownColors.White;
               
                worksheet.Range["B5:L5"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                worksheet.Range["B5:L5"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                worksheet.Range["B5:L5"].ColumnWidth = 9.2;
                worksheet.Range["B5:L5"].RowHeight = 21.5;
                worksheet.Range["B5:L5"].CellStyle.Font.FontName = "Calibri";
                worksheet.Range["A15:E15"].CellStyle.Font.Bold = true;
                worksheet.Range["B5:L5"].CellStyle.Font.Size = 10;

                //worksheet.Range["C15:C22"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;


                //Time TableDetails.
                string B = "",C="",D="",E="",F="",G="",H="",I="",J="",K="",L="";
                string BL = "", CL = "", DL = "", EL = "", FL = "", GL = "", HL = "";
                int cellValue = 6;
                 
                for(int iCount=0; iCount< timeTableTabularFormates.Count;iCount++)
                {
                    B = "B" + cellValue;
                    C = "C" + cellValue;
                    D = "D" + cellValue;
                    E = "E" + cellValue;
                    F = "F" + cellValue;
                    G = "G" + cellValue;
                    H = "H" + cellValue;
                    I = "I" + cellValue;
                    J = "J" + cellValue;
                    K = "K" + cellValue;
                    L = "L" + cellValue;
                    BL = B +":"+ L;
                    worksheet.Range[B].Text = timeTableTabularFormates[iCount].dayName.ToUpper();
                    worksheet.Range[C].Text = timeTableTabularFormates[iCount].period1.ToUpper();
                    worksheet.Range[D].Text = timeTableTabularFormates[iCount].period2.ToUpper();
                    worksheet.Range[E].Text = timeTableTabularFormates[iCount].period3.ToUpper();
                    worksheet.Range[F].Text = timeTableTabularFormates[iCount].period4.ToUpper();
                    worksheet.Range[G].Text = timeTableTabularFormates[iCount].period5.ToUpper();
                    worksheet.Range[H].Text = timeTableTabularFormates[iCount].period6.ToUpper();
                    worksheet.Range[I].Text = timeTableTabularFormates[iCount].period7.ToUpper();
                    worksheet.Range[J].Text = timeTableTabularFormates[iCount].period8.ToUpper();
                    worksheet.Range[K].Text = timeTableTabularFormates[iCount].period9.ToUpper();
                    worksheet.Range[L].Number = timeTableTabularFormates[iCount].totalPeriod;
                    _totalClass=_totalClass + timeTableTabularFormates[iCount].totalPeriod;
                    //worksheet.Range["B5:L5"].CellStyle.Color = Color.FromArgb(42, 118, 189);
                    //worksheet.Range["B5:L5"].CellStyle.Font.Color = ExcelKnownColors.White;
                    //worksheet.Range["B5:L5"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    worksheet.Range[BL].ColumnWidth = 10.5;
                    worksheet.Range[BL].RowHeight = 45;
                    worksheet.Range[BL].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                    worksheet.Range[BL].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                    worksheet.Range[BL].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Grey_25_percent;
                    worksheet.Range[BL].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Grey_25_percent;                    
                    worksheet.Range[BL].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    worksheet.Range[BL].CellStyle.Font.Size = 9.5;
                    worksheet.Range[BL].WrapText = true;
                    worksheet.Range[BL].CellStyle.Font.FontName = "Calibri";
                    worksheet.Range[BL].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    worksheet.Range[BL].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                    cellValue++;
                    
                }

                worksheet.Range["J4"].Text = "Total Class In week :  " + _totalClass.ToString();
                //Apply borders
                //worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                //worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                //worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Grey_25_percent;
                //worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Grey_25_percent;

                //worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                //worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                //worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Black;
                //worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Black;

                ////Apply font setting for cells with product details
                //worksheet.Range["A3:E23"].CellStyle.Font.FontName = "Calibri";
                //worksheet.Range["A3:E23"].CellStyle.Font.Size = 10;
                //worksheet.Range["A15:E15"].CellStyle.Font.Color = ExcelKnownColors.White;
                //worksheet.Range["A15:E15"].CellStyle.Font.Bold = true;
                //worksheet.Range["D23:E23"].CellStyle.Font.Bold = true;

                ////Apply cell color
                //worksheet.Range["A15:E15"].CellStyle.Color = Color.FromArgb(42, 118, 189);

                ////Apply alignment to cells with product details
                //worksheet.Range["A15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
                //worksheet.Range["C15:C22"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                //worksheet.Range["D15:E15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

                //worksheet.Range["B1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenterAcrossSelection;
                ////Apply row height and column width to look good
                ////worksheet.Range["A1"].ColumnWidth = 36;
                //worksheet.Range["B1"].ColumnWidth = 11;
                //worksheet.Range["B1"].RowHeight = 39;

                //worksheet.Range["C1"].ColumnWidth = 8;
                //worksheet.Range["D1:E1"].ColumnWidth = 18;
                //worksheet.Range["A1"].RowHeight = 47;
                //worksheet.Range["A2"].RowHeight = 15;
                //worksheet.Range["A3:A4"].RowHeight = 15;
                //worksheet.Range["A5"].RowHeight = 18;
                //worksheet.Range["A6"].RowHeight = 29;
                //worksheet.Range["A7"].RowHeight = 18;
                //worksheet.Range["A8"].RowHeight = 15;
                //worksheet.Range["A9:A14"].RowHeight = 15;
                //worksheet.Range["A15:A23"].RowHeight = 18;

                //Save the document as a stream and retrun the stream.
                using (MemoryStream stream = new MemoryStream())
                {
                    //Save the created Excel document to MemoryStream
                    workbook.SaveAs(stream);
                    return stream;
                }
            }
        }
    }
}


