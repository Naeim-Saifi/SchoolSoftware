using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.Drawing;
using System.IO;
using System.Collections.Generic;
using Syncfusion.Blazor.PivotView;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AIS.Data.RequestResponseModel.QuestionSetUp;
using System.Linq;
using Syncfusion.Blazor.Charts.BulletChart.Internal;

namespace AdminDashboard.Server.API_Service.Service.QuestionSetup
{
    public class WordFormatQuestionPaper
    {
        
        
        public MemoryStream CreateQuestionPaperInWordFormat(string documentType,string _SchoolName, string _sessionDetails, List<QuestionPaperDetailsListOutPutModel> viewQuestionPaperDetailsList, string _operation)
        {
           // Creating a new document
            WordDocument document = new WordDocument();
            //Adding a new section to the document
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 80;
            //section.PageSetup.Borders.LineWidth = 3;
            // //Sets page setup options
             section.PageSetup.Orientation = PageOrientation.Portrait;
            //Set page size of the section
            // section.PageSetup.PageSize = new SizeF(612, 792);
            //Create Paragraph styles
            //WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
            //style.CharacterFormat.FontName = "Calibri";
            //style.CharacterFormat.FontSize = 11f;
            //style.ParagraphFormat.BeforeSpacing = 0;
            //style.ParagraphFormat.AfterSpacing = 8;
            //style.ParagraphFormat.LineSpacing = 13.8f;

            //style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            //style.ApplyBaseStyle("Normal");
            //style.CharacterFormat.FontName = "Calibri Light";
            //style.CharacterFormat.FontSize = 16f;
            //style.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
            //style.ParagraphFormat.BeforeSpacing = 12;
            //style.ParagraphFormat.AfterSpacing = 0;
            //style.ParagraphFormat.Keep = true;
            //style.ParagraphFormat.KeepFollow = true;
            //style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
            //IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            //WPicture picture = paragraph.AppendPicture(fileDataValue["images/ais_logo.jpg"]) as WPicture;
            //picture.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
            //picture.VerticalOrigin = VerticalOrigin.Margin;
            //picture.VerticalPosition = -45;
            //picture.HorizontalOrigin = HorizontalOrigin.Column;
            //picture.HorizontalPosition = 263.5f;
            //picture.WidthScale = 20;
            //picture.HeightScale = 15;


            //***************************

            // //Creates a new Word document
            // WordDocument document = new WordDocument();
            // IWSection section = document.AddSection();
            // //Sets page setup options
            // section.PageSetup.Orientation = PageOrientation.Portrait;
            // section.PageSetup.Margins.All = 80;
            // section.PageSetup.Borders.LineWidth = 3;
            // //Sets the PrinterPaperTray value for FirstPageTray in page setup options
            // section.PageSetup.FirstPageTray = PrinterPaperTray.EnvelopeFeed;
            // //Sets the PrinterPaperTray value for OtherPagesTray in page setup options
            // section.PageSetup.OtherPagesTray = PrinterPaperTray.MiddleBin;
            // //Adds a paragraph to created section
            // IWParagraph paragraph = section.AddParagraph();
            // //Appends the text to the created paragraph
            // //paragraph.AppendText("AdventureWorks Cycles, the fictitious company on which the AdventureWorks sample databases are based, is a large, multinational manufacturing company.");
            // //Saves and closes the Word document instance
            ////MemoryStream stream = new MemoryStream();
            // //Saves the Word document to  MemoryStream

            //**********
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();
            //paragraph.ApplyStyle("Normal");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            WTextRange textRange = paragraph.AppendText(_SchoolName) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            //textRange.CharacterFormat.TextColor = Syncfusion.Drawing.Color.Red;

            //Add Exam Name on top of the page
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 5;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Exam Type - " + viewQuestionPaperDetailsList[0].examType + "\n") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            int itotalMaxMarks = viewQuestionPaperDetailsList.Sum(m => m.marks);

            //Add Classname on top of the page
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 5;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;            
            textRange = paragraph.AppendText("Class-"+ viewQuestionPaperDetailsList[0].className.ToUpper()+ "\t\t\t\t\t\t\t\tTime:-1Hour\n") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
           // paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
            textRange = paragraph.AppendText("Subject - "+ viewQuestionPaperDetailsList[0].subjectName.ToUpper()+ "\t\t\t\t\t\tMax.Marks:-" + itotalMaxMarks+ "\n") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //Exam Details
         

            //paragraph = section.AddParagraph();
            //paragraph.ParagraphFormat.FirstLineIndent = 5;
            //paragraph.BreakCharacterFormat.FontSize = 12f;
            ////Tab firstTab = paragraph.ParagraphFormat.Tabs.AddTab(11, TabJustification.Left, TabLeader.Dotted);

            //paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            //textRange = paragraph.AppendText("Time:-1hour\t\t\t Subject-"+ viewQuestionPaperDetailsList[0].subjectName.ToUpper() + "\t\t\t Max. Marks:-"+itotalMaxMarks+" \r\n") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;
            //End exam details





            //General Instruction 
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 5;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange = paragraph.AppendText("General instructions:-\r\nQuestion number 1 to 5 are of one marks each.\r\nQuestion number 6 to 10 are of two marks each.\r\nQuestion number 11 and 12 are of 3 marks each.\r\nQuestion number 13 is of 4 marks. \r\n") as WTextRange;
            textRange.CharacterFormat.FontSize = 11f;
            textRange.CharacterFormat.FontName = "Calibri";


            //end general instruction


            //Appends paragraph
            //paragraph = section.AddParagraph();
            //paragraph.ApplyStyle("Heading 1");
            //paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            //textRange = paragraph.AppendText("Question Details "+ _sessionDetails) as WTextRange;
            //textRange.CharacterFormat.FontSize = 18f;
            //textRange.CharacterFormat.FontName = "Calibri";
            //Appends paragraph
            paragraph = section.AddParagraph();            
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Section-A") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            int _QuestionNo = 1;


            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 5;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange = paragraph.AppendText(" Tick the correct answer:-") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;




            for (int i = 0; i < viewQuestionPaperDetailsList.Count;i++)
            {
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.FirstLineIndent = 5;
                paragraph.BreakCharacterFormat.FontSize = 10f;
                // textRange = paragraph.AppendText("In 2000, Adventure Works Cycles bought a small manufacturing plant, Importadores Neptuno, located in Mexico. Importadores Neptuno manufactures several critical subcomponents for the Adventure Works Cycles product line. These subcomponents are shipped to the Bothell location for final product assembly. In 2001, Importadores Neptuno, became the sole manufacturer and distributor of the touring bicycle product group.") as WTextRange;
                textRange = (WTextRange)paragraph.AppendText("Q-"+ (_QuestionNo +i)+ ". "+viewQuestionPaperDetailsList[i].questionDetails.ToString());
                
                textRange.CharacterFormat.FontSize = 10f;
                textRange.CharacterFormat.FontName = "Calibri";

                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.FirstLineIndent = 5;
                paragraph.BreakCharacterFormat.FontSize = 10f;
                textRange = (WTextRange)paragraph.AppendText("\t(A) -   " + viewQuestionPaperDetailsList[i].answeer_A.ToString() + "\t\t(B) -   " + viewQuestionPaperDetailsList[i].answeer_B.ToString() +
                    "\t\t(C) -   " + viewQuestionPaperDetailsList[i].answeer_C.ToString() + "\t\t(D) -   " + viewQuestionPaperDetailsList[i].answeer_D.ToString());
                textRange.CharacterFormat.FontSize = 10f;
                textRange.CharacterFormat.FontName = "Calibri";
            }

            //Creates a new text watermark
            TextWatermark textWatermark = new TextWatermark(_SchoolName, "Calibri", 350, 100);
            //Sets the created watermark to the document
            document.Watermark = textWatermark;

            //paragraph = section.AddParagraph();
            //paragraph.ApplyStyle("Heading 1");
            //paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //textRange = paragraph.AppendText("Product Overview") as WTextRange;


            //
            section.AddParagraph();
            Syncfusion.DocIO.FormatType formatType = Syncfusion.DocIO.FormatType.Docx;
            //Save as .doc format
            if (documentType == "WordDoc")
                formatType = Syncfusion.DocIO.FormatType.Doc;
            //Save as .xml format
            else if (documentType == "WordML")
                formatType = Syncfusion.DocIO.FormatType.WordML;
            //Save the document as a stream and return the stream
            using (MemoryStream stream = new MemoryStream())
            {
                //Save the created Word document to MemoryStream
                document.Save(stream, formatType);
                document.Close();
                stream.Position = 0;
                return stream;
            }
        }
    }

}
