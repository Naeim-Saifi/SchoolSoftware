using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Drawing;
using System.IO;

namespace Create_Word_document.Data
{
    public class WordService
    {
        public MemoryStream CreateWord()
        {
            //Creating a new document.
            using (WordDocument document = new WordDocument())
            {
                //Adding a new section to the document.
                WSection section = document.AddSection() as WSection;
                //Set Margin of the section.
                section.PageSetup.Margins.All = 72;
                //Set page size of the section.
                section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);
                section.PageSetup.Orientation = PageOrientation.Landscape;
                section.PageSetup.Margins.All = 72;
                section.PageSetup.Borders.LineWidth = 2;
                //Sets the PrinterPaperTray value for FirstPageTray in page setup options
                section.PageSetup.FirstPageTray = PrinterPaperTray.EnvelopeFeed;
                //Sets the PrinterPaperTray value for OtherPagesTray in page setup options     
                section.PageSetup.OtherPagesTray = PrinterPaperTray.MiddleBin;


                //Create Paragraph styles.
                WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
                style.CharacterFormat.FontName = "Calibri";
                style.CharacterFormat.FontSize = 11f;
                style.ParagraphFormat.BeforeSpacing = 0;
                style.ParagraphFormat.AfterSpacing = 8;
                style.ParagraphFormat.LineSpacing = 13.8f;

                style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
                style.ApplyBaseStyle("Normal");
                style.CharacterFormat.FontName = "Calibri Light";
                style.CharacterFormat.FontSize = 16f;
                style.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
                style.ParagraphFormat.BeforeSpacing = 12;
                style.ParagraphFormat.AfterSpacing = 0;
                style.ParagraphFormat.Keep = true;
                style.ParagraphFormat.KeepFollow = true;
                style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
                IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

                paragraph.ApplyStyle("Normal");
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                WTextRange textRange = paragraph.AppendText("School Name") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.TextColor = Syncfusion.Drawing.Color.Red;

                //Appends paragraph.
                paragraph = section.AddParagraph();
                paragraph.ApplyStyle("Heading 1");
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
                textRange = paragraph.AppendText("Question Details") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.FontName = "Calibri";

                //Appends paragraph.
                 for(int x=0;x<=5;x++)
                {
                    paragraph = section.AddParagraph();
                    paragraph.ParagraphFormat.FirstLineIndent = 5;
                    paragraph.BreakCharacterFormat.FontSize = 12f;
                    textRange = paragraph.AppendText("Objective Question"+x.ToString()) as WTextRange;
                    textRange.CharacterFormat.FontSize = 10f;
                    textRange.CharacterFormat.FontName = "Calibri";

                    paragraph = section.AddParagraph();
                    paragraph.ParagraphFormat.FirstLineIndent = 5;
                    paragraph.BreakCharacterFormat.FontSize = 12f;
                    string Objective = "1."+ "Answeer-1" + "\t\t2."+ "Answeer-2" + "\t\t3." + "Answeer-3" + "\t\t4." + "Answeer-4";
                    textRange = paragraph.AppendText(Objective) as WTextRange;
                    textRange.CharacterFormat.FontSize = 10f;
                    textRange.CharacterFormat.FontName = "Calibri";
                }

                 
                //********************
                ////Adds new section to the document
                //IWSection section = document.AddSection();
                ////Adds a paragraph to created section
                //IWParagraph paragraph = section.AddParagraph();
                //string paraText = "AdventureWorks Cycles, the fictitious company on which the AdventureWorks sample databases are based, is a large, multinational manufacturing company.";
                ////Appends the text to the created paragraph
                //paragraph.AppendText(paraText);
                ////Adds the new section to the document
                //section = document.AddSection();
                ////Sets a section break
                //section.BreakCode = SectionBreakCode.Oddpage;
                ////Adds a paragraph to created section
                //paragraph = section.AddParagraph();
                ////Appends the text to the created paragraph
                //paragraph.AppendText(paraText);

                ////********
                ////IWParagraph paragraph = section.AddParagraph();
                ////Adds tab stop at position 11
                //Tab firstTab = paragraph.ParagraphFormat.Tabs.AddTab(11, TabJustification.Left, TabLeader.Dotted);
                ////Adds tab stop at position 62
                //paragraph.ParagraphFormat.Tabs.AddTab(62, TabJustification.Left, TabLeader.Single);
                //paragraph.AppendText("This sample\t illustrates the use of tabs in the paragraph. Tabs\t can be inserted or removed from the paragraph.");
                ////Removes tab stop from the collection
                //paragraph.ParagraphFormat.Tabs.RemoveByTabPosition(11);
                ////Saves the Word document to MemoryStream.
                MemoryStream stream = new MemoryStream();
                document.Save(stream, FormatType.Docx);
                stream.Position = 0;
                return stream;


            }
        }
    }
}