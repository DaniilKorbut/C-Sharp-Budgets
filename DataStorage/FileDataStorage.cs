using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataStorage
{
    public class FileDataStorage<TObject> where TObject : class, IStorable
    {
        private static readonly string BaseFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BudgetsKMA",
                typeof(TObject).Name);

        public FileDataStorage()
        {
            if (!Directory.Exists(BaseFolder))
            {
                Directory.CreateDirectory(BaseFolder);
            }
        }

        public async Task AddOrUpdateAsync(TObject obj)
        {
            string strObj = JsonSerializer.Serialize(obj);
            string filePath = Path.Combine(BaseFolder, obj.Guid.ToString("N"));
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                await streamWriter.WriteAsync(strObj);
            }
        }

        public async Task<TObject> GetAsync(Guid guid)
        {
            string filePath = Path.Combine(BaseFolder, guid.ToString("N"));
            if (File.Exists(filePath))
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    string strObj = await streamReader.ReadToEndAsync();
                    TObject obj = JsonSerializer.Deserialize<TObject>(strObj);
                    return obj;
                }
            }

            return null;
        }

        public async Task<List<TObject>> GetAllAsync()
        {
            var result = new List<TObject>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                // string filePath = Path.Combine(BaseFolder, guid.ToString("N"));
                using (StreamReader streamReader = new StreamReader(file))
                {
                    string strObj = await streamReader.ReadToEndAsync();
                    TObject obj = JsonSerializer.Deserialize<TObject>(strObj);
                    result.Add(obj);
                }
            }

            return result;
        }
    }
}
