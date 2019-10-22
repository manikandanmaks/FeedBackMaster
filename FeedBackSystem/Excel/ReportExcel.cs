using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using FeedBackSystem.Model.DtoModels;

namespace FeedBackSystem.Excel
{
    public class ReportExcel
    {
        int rowIndex = 1;
        ExcelRange cell;
        ExcelFill fill;
        Border border;
        public byte[] GenerateExcel(List<TitleAndQuestions> list, int type)
        {
            
            if (File.Exists("D:\\FeedBackSystem\\FeedBackSystem\\Excel\\Reports.xlsx") && type == 1)
            {
                File.Delete("D:\\FeedBackSystem\\FeedBackSystem\\Excel\\Reports.xlsx");
            }
            var FileName = "D:\\FeedBackSystem\\FeedBackSystem\\Excel\\Reports.xlsx";
            var file = new FileInfo(FileName);
            using (var excelpckg = new ExcelPackage(file))
            {

                if (type == 1)
                {



                    ExcelWorksheet sheet = excelpckg.Workbook.Worksheets.Add("UserReports 1");

                    sheet.Name = "Forms Submition Report";
                    sheet.Column(1).Width = 20;
                    sheet.Column(2).Width = 50;
                    sheet.Column(3).Width = 20;
                    sheet.Column(4).Width = 20;

                    #region ReportHeader
                    sheet.Cells[rowIndex, 1, rowIndex, 4].Merge = true;
                    cell = sheet.Cells[rowIndex, 1];
                    cell.Value = "Forms Submition Report";
                    cell.Style.Font.Bold = true;
                    cell.Style.Font.Size = 20;
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rowIndex += 1;
                    #endregion

                    #region TableHeader            
                    cell = sheet.Cells[rowIndex, 1];
                    cell.Value = "UserName";
                    cell.Style.Font.Bold = true;
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.LightGray);
                    border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                    cell = sheet.Cells[rowIndex, 2];
                    cell.Value = "Question";
                    cell.Style.Font.Bold = true;
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.LightGray);
                    border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                    cell = sheet.Cells[rowIndex, 3];
                    cell.Value = "Comment";
                    cell.Style.Font.Bold = true;
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.LightGray);
                    border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                    cell = sheet.Cells[rowIndex, 4];
                    cell.Value = "Rating";
                    cell.Style.Font.Bold = true;
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.LightGray);
                    border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    rowIndex += 1;
                    #endregion

                    #region tableBody
                    if (list.Count > 0)
                    {
                        foreach (var reports in list)
                        {
                            cell = sheet.Cells[rowIndex, 1];
                            cell.Value = reports.UserName;
                            cell.Style.Font.Bold = true;
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(Color.White);
                            border = cell.Style.Border;
                            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                            cell = sheet.Cells[rowIndex, 2];
                            cell.Value = reports.Question;
                            cell.Style.Font.Bold = true;
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(Color.White);
                            border = cell.Style.Border;
                            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                            cell = sheet.Cells[rowIndex, 3];
                            cell.Value = reports.Comment;
                            cell.Style.Font.Bold = true;
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(Color.White);
                            border = cell.Style.Border;
                            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                            cell = sheet.Cells[rowIndex, 4];
                            cell.Value = reports.Rating;
                            cell.Style.Font.Bold = true;
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(Color.White);
                            border = cell.Style.Border;
                            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                            rowIndex += 1;
                        }
                    }
                    #endregion


                    excelpckg.Save();


                }


            
            else if (type == 2)
            {


                ExcelWorksheet sheet = excelpckg.Workbook.Worksheets.Add("UserReports 2");

                sheet.Name = "un submitted report";

                sheet.Column(2).Width = 50;


                #region ReportHeader
                sheet.Cells[rowIndex, 1, rowIndex, 4].Merge = true;
                cell = sheet.Cells[rowIndex, 1];
                cell.Value = "un submitted report";
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 20;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rowIndex += 1;
                #endregion

                #region TableHeader            
                cell = sheet.Cells[rowIndex, 2];
                cell.Value = "UserName";
                cell.Style.Font.Bold = true;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightGray);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                rowIndex += 1;
                #endregion

                #region tableBody
                if (list.Count > 0)
                {
                    foreach (TitleAndQuestions reports in list)
                    {
                        cell = sheet.Cells[rowIndex, 2];
                        cell.Value = reports.UserName;
                        cell.Style.Font.Bold = true;
                        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(Color.White);
                        border = cell.Style.Border;
                        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                        rowIndex += 1;
                    }
                }
                    #endregion



                    excelpckg.Save();



                }

            else if (type == 3)
            {

                ExcelWorksheet sheet = excelpckg.Workbook.Worksheets.Add("UserReports 3");

                sheet.Name = "Average rating for each Question ";

                sheet.Column(2).Width = 50;
                sheet.Column(3).Width = 20;
               


                #region ReportHeader
                sheet.Cells[rowIndex, 1, rowIndex, 4].Merge = true;
                cell = sheet.Cells[rowIndex, 1];
                cell.Value = "Average rating for each Question ";
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 20;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rowIndex += 1;
                #endregion


                #region table header
                cell = sheet.Cells[rowIndex, 2];
                cell.Value = "Question";
                cell.Style.Font.Bold = true;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightGray);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;



                cell = sheet.Cells[rowIndex, 3];
                cell.Value = " avg Rating";
                cell.Style.Font.Bold = true;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightGray);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                rowIndex += 1;
                #endregion

                #region tableBody
                if (list.Count > 0)
                {
                    foreach (TitleAndQuestions reports in list)
                    {


                        cell = sheet.Cells[rowIndex, 2];
                        cell.Value = reports.Question;
                        cell.Style.Font.Bold = true;
                        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(Color.White);
                        border = cell.Style.Border;
                        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                        cell = sheet.Cells[rowIndex, 3];
                        cell.Value =Convert.ToInt32( reports.AvgRating)+"%";
                        cell.Style.Font.Bold = true;
                        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(Color.White);
                        border = cell.Style.Border;
                        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                        rowIndex += 1;
                    }
                }
                #endregion

                excelpckg.Save();



            }
        }
            return  new byte[10];  
        }

      
    }
}