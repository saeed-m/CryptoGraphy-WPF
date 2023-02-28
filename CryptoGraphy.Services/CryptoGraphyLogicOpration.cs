using ClosedXML.Excel;
using CryptoGraphy.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace CryptoGraphy.Services
{
    public class CryptoGraphyLogicOpration
    {
        /// <summary>
        /// this method get the text file read all lines and convert each line to hash and save it in Excel File
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="SaveLocation"></param>
        /// <returns>Object Of Response Message</returns>
        public async Task<OprationResponseModel> SaveToExcelFile(string filePath, string SaveLocation)
        {
            if (filePath == string.Empty)
            {
                return new OprationResponseModel { IsSuccessfull = false, ResponseMessage = "Please Select File." };
            }

            if(SaveLocation == string.Empty)
            {
                return new OprationResponseModel { IsSuccessfull = false, ResponseMessage = "Please Select File Name and location." };
            }

            try
            {
                int TextFileTotalLineCount = 0;
                List<HashResultModel> md5hashlist = new List<HashResultModel>();
                List<HashResultModel> sha1hashlist = new List<HashResultModel>();
                List<HashResultModel> sha256hashlist = new List<HashResultModel>();
                List<HashResultModel> sha384hashlist = new List<HashResultModel>();
                List<HashResultModel> sha512hashlist = new List<HashResultModel>();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    // Read and display lines from the file until the end of the file is reached
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {

                            TextFileTotalLineCount++;
                            //Convertion
                            md5hashlist.Add(new HashResultModel { Algorithm = "MD5", BackColor = null, HashedValue = EncryptionService.GetMD5Hash(line.Trim()) });
                            sha1hashlist.Add(new HashResultModel { Algorithm = "SHA1", BackColor = null, HashedValue = EncryptionService.GetSHA1Hash(line.Trim()) });
                            sha256hashlist.Add(new HashResultModel { Algorithm = "SHA256", BackColor = null, HashedValue = EncryptionService.GetSHA256Hash(line.Trim()) });
                            sha384hashlist.Add(new HashResultModel { Algorithm = "SHA384", BackColor = null, HashedValue = EncryptionService.GetSHA384Hash(line.Trim()) });
                            sha512hashlist.Add(new HashResultModel { Algorithm = "SHA512", BackColor = null, HashedValue = EncryptionService.GetSHA512Hash(line.Trim()) });
                        }

                    }
                }

                using (var workbook = new XLWorkbook())
                {

                    int StartIndex = 2;
                    TextFileTotalLineCount += 1;
                    var worksheet = workbook.Worksheets.Add("Encription");
                    worksheet.Cell($"A{StartIndex}").Value = "MD5";
                    worksheet.Range($"A{StartIndex}:A{TextFileTotalLineCount}").Merge();
                    worksheet.Range($"A{StartIndex}:A{TextFileTotalLineCount}").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Range($"A{StartIndex}:A{TextFileTotalLineCount}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range($"A{StartIndex}:A{TextFileTotalLineCount}").Style.Fill.BackgroundColor = XLColor.Aqua;
                    //worksheet.Columns().AdjustToContents(MaxWidth)
                    var col1 = worksheet.Column("A");
                    //col1.Style.Fill.BackgroundColor = XLColor.Red;
                    col1.Width = 20;
                    var col2 = worksheet.Column("B");
                    //col1.Style.Fill.BackgroundColor = XLColor.Red;
                    col2.Width = 130;

                    for (int i = StartIndex; i <= TextFileTotalLineCount; i++)
                    {
                        worksheet.Cell($"B{i}").Value = md5hashlist[TextFileTotalLineCount - i].HashedValue;

                    }

                    StartIndex += TextFileTotalLineCount-1;
                    int endindex = TextFileTotalLineCount * 2 -1;
                    worksheet.Cell($"A{StartIndex}").Value = "SHA1";
                    worksheet.Range($"A{StartIndex}:A{endindex}").Merge();
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Fill.BackgroundColor = XLColor.BabyBlue;


                    for (int i = StartIndex; i <= endindex; i++)
                    {
                        var listindex = (i - 1) - TextFileTotalLineCount;
                        worksheet.Cell($"B{i}").Value = sha1hashlist[listindex].HashedValue;

                    }

                    StartIndex = endindex + 1;
                    endindex = (TextFileTotalLineCount * 3 -2);
                    worksheet.Cell($"A{StartIndex}").Value = "SHA256";
                    worksheet.Range($"A{StartIndex}:A{endindex}").Merge();
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Fill.BackgroundColor = XLColor.Green;
                    for (int i = StartIndex; i <= endindex; i++)
                    {
                        var listindex = (i - 1) - (TextFileTotalLineCount * 2 -1);
                        worksheet.Cell($"B{i}").Value = sha256hashlist[listindex].HashedValue;

                    }

                    StartIndex = endindex + 1;
                    endindex = (TextFileTotalLineCount * 4 -3);
                    worksheet.Cell($"A{StartIndex}").Value = "SHA384";
                    worksheet.Range($"A{StartIndex}:A{endindex}").Merge();
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Fill.BackgroundColor = XLColor.Yellow;
                    for (int i = StartIndex; i <= endindex; i++)
                    {
                        var listindex = (i - 1) - (TextFileTotalLineCount * 3 -2);
                        worksheet.Cell($"B{i}").Value = sha384hashlist[listindex].HashedValue;

                    }
                    StartIndex = endindex + 1;
                    endindex = (TextFileTotalLineCount * 5 -4);
                    worksheet.Cell($"A{StartIndex}").Value = "SHA512";
                    worksheet.Range($"A{StartIndex}:A{endindex}").Merge();
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range($"A{StartIndex}:A{endindex}").Style.Fill.BackgroundColor = XLColor.Violet;
                    for (int i = StartIndex; i <= endindex; i++)
                    {
                        var listindex = (i - 1) - (TextFileTotalLineCount * 4 - 3);
                        worksheet.Cell($"B{i}").Value = sha512hashlist[listindex].HashedValue;

                    }

                    workbook.SaveAs(SaveLocation);
                    return new OprationResponseModel { IsSuccessfull = true, ResponseMessage = "Opration Compelete Successfully." };

                }
            }
            catch (Exception ex)
            {
                return new OprationResponseModel { IsSuccessfull = false, ResponseMessage = ex.Message };
            }

        }

        /// <summary>
        /// this method get the string value and convert to hash and send collection of hashesh to ui
        /// </summary>
        /// <param name="valueToHash"></param>
        /// <returns>ObservableCollection<HashResultModel> </returns>
        public ObservableCollection<HashResultModel> ConvertSingleLineToHashes(string valueToHash)
        {
            var converter = new BrushConverter();
            ObservableCollection<HashResultModel> hashResult = new ObservableCollection<HashResultModel>();
            var Md5Hash = EncryptionService.GetMD5Hash(valueToHash);
            var SHA1Hash = EncryptionService.GetSHA1Hash(valueToHash);
            var SHA256hash = EncryptionService.GetSHA256Hash(valueToHash);
            var SHA384hash = EncryptionService.GetSHA384Hash(valueToHash);
            var SHA512hash = EncryptionService.GetSHA512Hash(valueToHash);

            //Create DataGrid Items
            hashResult.Add(new HashResultModel { Id = 1, Algorithm = "MD5", BackColor = (Brush)converter.ConvertFromString("#1098fd"), HashedValue = Md5Hash });
            hashResult.Add(new HashResultModel { Id = 2, Algorithm = "SHA1", BackColor = (Brush)converter.ConvertFromString("#ff120d"), HashedValue = SHA1Hash });
            hashResult.Add(new HashResultModel { Id = 3, Algorithm = "SHA256", BackColor = (Brush)converter.ConvertFromString("#ecb23a"), HashedValue = SHA256hash });
            hashResult.Add(new HashResultModel { Id = 4, Algorithm = "SHA384", BackColor = (Brush)converter.ConvertFromString("#ced45a"), HashedValue = SHA384hash });
            hashResult.Add(new HashResultModel { Id = 5, Algorithm = "SHA512", BackColor = (Brush)converter.ConvertFromString("#1098fa"), HashedValue = SHA512hash });

            return hashResult;

        }


    }


}
