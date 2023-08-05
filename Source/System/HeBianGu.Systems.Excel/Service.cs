
using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NPOI.HSSF.UserModel;
using System;

namespace HeBianGu.Systems.Excel
{
    public interface INpoiServiceOption
    {
    }
    [Displayer(Name = "Excel设置", GroupName = "系统设置")]
    public class NpoiService : ServiceSettingInstance<NpoiService, IExcelService>, IExcelService, INpoiServiceOption
    {
        public bool Export(IEnumerable<object> collection, string path, string sheetName, out string message)
        {
            message = null;
            if (collection == null) return false;
            IWorkbook workbook = new XSSFWorkbook();
            var type = collection.GetType().GetGenericArguments().FirstOrDefault();
            ISheet sheet = workbook.CreateSheet(sheetName);
            var ps = type.GetProperties().Where(x => x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
            ps = ps.OrderBy(x => x.GetCustomAttribute<DisplayAttribute>()?.GetOrder());

            IRow row0 = sheet.CreateRow(0);
            for (int i = 0; i < ps.Count(); i++)
            {
                var p = ps.ElementAt(i);
                var display = p.GetCustomAttribute<DisplayAttribute>();
                string header = display?.Name ?? p.Name;
                row0.CreateCell(i).SetCellValue(header);
            }


            for (int r = 0; r < collection.Count(); r++)
            {
                var item = collection.ElementAt(r);
                IRow row = sheet.CreateRow(r + 1);

                for (int i = 0; i < ps.Count(); i++)
                {
                    var p = ps.ElementAt(i);
                    var display = p.GetCustomAttribute<DisplayAttribute>();
                    string header = display?.Name ?? p.Name;
                    var value = p.GetValue(item);
                    row.CreateCell(i).SetCellValue(value?.ToString());
                }
            }

            using (FileStream url = File.OpenWrite(path))
            {
                workbook.Write(url);
            }

            return true;
        }

        public IEnumerable<T> Read<T>(string filePath, Func<object, T> convert, out string message)
        {
            message = null;
            IWorkbook wk = null;
            List<T> result = new List<T>();
            string extension = System.IO.Path.GetExtension(filePath);
            try
            {
                FileStream fs = File.OpenRead(filePath);
                if (extension.Equals(".xls"))
                {
                    //把xls文件中的数据写入wk中
                    wk = new HSSFWorkbook(fs);
                }
                else
                {
                    //把xlsx文件中的数据写入wk中
                    wk = new XSSFWorkbook(fs);
                }

                fs.Close();
                //读取当前表数据
                ISheet sheet = wk.GetSheetAt(0);

                IRow row = sheet.GetRow(0);  //读取当前行数据
                                             //LastRowNum 是当前表的总行数-1（注意）
                int offset = 0;
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    row = sheet.GetRow(i);  //读取当前行数据
                    if (row == null)
                        continue;
                    if (convert == null)
                        continue;
                    {
                        ////LastCellNum 是当前行的总列数
                        //for (int j = 0; j < row.LastCellNum; j++)
                        //{
                        //    //读取该行的第j列数据
                        //    string value = row.GetCell(j).ToString();
                        //    Console.Write(value.ToString() + " ");
                        //}
                        //Console.WriteLine("\n");
                        var value = convert.Invoke(row);
                        result.Add(value);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Instance?.Error(ex);
                message = ex.Message;
            }
            return null;
        }

    }

    //[SettingConfig(Name = "参数设置", Group = "基本设置")]
    public class Setting : LazySettingInstance<Setting>
    {

    }


}
